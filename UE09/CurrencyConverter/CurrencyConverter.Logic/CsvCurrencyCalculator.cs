using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Domain;

namespace CurrencyConverter.Logic
{
  public class CsvCurrencyCalculator : ICurrencyCalculator
  {
    private const string CURRENCY_DATA_FILE = "currency-data.csv";
    private const string APP_DIRECTORY = "CurrencyConverter";
    private readonly string currencyDataFilePath;

    private class Entry
    {
      public string Name;      // long form of currency name
      public string? Country;  // country currency is used in
      public decimal Rate;      // current rate of exchange

      public Entry(string name, string? country, decimal rate)
      {
        this.Name = name;
        this.Country = country;
        this.Rate = rate;
      }
    }

    private readonly IDictionary<string, Entry> currTable = new ConcurrentDictionary<string, Entry>();

    private async Task LoadDataFromFile()
    {
      var lines = await File.ReadAllLinesAsync(currencyDataFilePath);

      foreach (var line in lines.Skip(1)) // Skip header
      {
        var parts = line.Split(',');
        if (parts.Length == 4)
        {
          var symbol = parts[0];
          var name = parts[1];
          var country = parts[2];
          var rate = decimal.Parse(parts[3], CultureInfo.InvariantCulture);
          currTable[symbol] = new Entry(name, country, rate);
        }
      }
    }

    private async Task SaveDataToFile()
    {
      var lines = new List<string> { "Symbol,Name,Country,Rate" };
      lines.AddRange(currTable.Select(kvp => $"{kvp.Key},{kvp.Value.Name},{kvp.Value.Country},{kvp.Value.Rate.ToString(CultureInfo.InvariantCulture)}"));
      await File.WriteAllLinesAsync(currencyDataFilePath, lines);
    }

    public CsvCurrencyCalculator()
    {
      currencyDataFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APP_DIRECTORY, CURRENCY_DATA_FILE);
    }
    public async Task<CurrencyData> GetCurrencyDataAsync(string currSymbol)
    {
      await LoadDataFromFile();

      if (currTable.TryGetValue(currSymbol, out Entry? entry))
      {
        return new CurrencyData(currSymbol, entry.Name, entry.Country, entry.Rate);
      }
      else
      {
        throw new ArgumentException("invalid currency " + currSymbol);
      }
    }

    public async Task<decimal> RateOfExchangeAsync(string srcCurr, string targCurr)
    {
      await LoadDataFromFile();

      if (!currTable.TryGetValue(targCurr, out Entry? targEntry))
      {
        throw new ArgumentException("invalid currency " + targCurr);
      }
      if (!currTable.TryGetValue(srcCurr, out Entry? srcEntry))
      {
        throw new ArgumentException("invalid currency " + srcEntry);
      }

      return targEntry.Rate / srcEntry.Rate;
    }

    public async Task<IEnumerable<string>> GetCurrenciesAsync()
    {
      await LoadDataFromFile();
      return currTable.Keys.OrderBy(s => s);
    }

    public async Task<bool> CurrencyExistsAsync(string currSymbol)
    {
      await LoadDataFromFile();
      return currTable.ContainsKey(currSymbol);
    }

    public async Task InsertAsync(CurrencyData data)
    {
      await LoadDataFromFile();

      if (currTable.ContainsKey(data.Symbol))
      {
        throw new ArgumentException("currency " + data.Symbol + " already exists");
      }
      currTable[data.Symbol] = new Entry(data.Name, data.Country, data.EuroRate);

      await SaveDataToFile();
    }

    public async Task UpdateAsync(CurrencyData data)
    {
      await LoadDataFromFile();

      if (currTable.TryGetValue(data.Symbol, out Entry? entry))
      {
        entry.Name = data.Name;
        entry.Country = data.Country;
        entry.Rate = data.EuroRate;

        await SaveDataToFile();
      }
      else
      {
        throw new ArgumentException("invalid currency " + data.Symbol);
      }
    }

    public async Task DeleteAsync(string symbol)
    {
      await LoadDataFromFile();

      if (currTable.Remove(symbol))
      {
        await SaveDataToFile();
      }
      else
      {
        throw new ArgumentException("invalid currency " + symbol);
      }
    }

  }
}