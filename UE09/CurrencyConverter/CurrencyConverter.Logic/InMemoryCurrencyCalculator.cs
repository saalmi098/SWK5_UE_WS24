using System.Collections.Concurrent;
using CurrencyConverter.Domain;

namespace CurrencyConverter.Logic;

public class InMemoryCurrencyCalculator : ICurrencyCalculator
{
	private class Entry
	{
		public string  Name;      // long form of currency name
		public string? Country;   // country currency is used in
		public decimal  Rate;      // current rate of exchange

		public Entry(string name, string? country, decimal rate)
		{
			this.Name = name;
			this.Country = country;
			this.Rate = rate;
		}
	}

	// currTable maps each currency string to an Entry object
	private static readonly IDictionary<string, Entry> currTable =
	  new ConcurrentDictionary<string, Entry>();

	static InMemoryCurrencyCalculator()
	{
		// initialize currencyTable
		currTable.Add("USD", new Entry("Dollar", "USA", 1.06m));
		currTable.Add("AUD", new Entry("Dollar", "Australia", 1.56m));
		currTable.Add("BRL", new Entry("Real", "Brazil", 5.77m));
		currTable.Add("GBP", new Entry("Pound", "GB", 0.88m));
		currTable.Add("CAD", new Entry("Dollar", "Canada", 1.52m));
		currTable.Add("CNY", new Entry("Yuan", "China", 7.30m));
		currTable.Add("DKK", new Entry("Krone", "Denmark", 7.44m));
		currTable.Add("HKD", new Entry("Dollar", "Hong Kong", 8.29m));
		currTable.Add("INR", new Entry("Rupee", "India", 87.56m));
		currTable.Add("JPY", new Entry("Yen", "Japan", 140.81m));
		currTable.Add("MYR", new Entry("Ringgit", "Malysia", 4.66m));
		currTable.Add("MXN", new Entry("Peso", "Mexico", 20.54m));
		currTable.Add("EUR", new Entry("Euro", "Europe", 1.0m));
	}

	private Task<decimal> EuroRate(string currSymbol)
	{
		if (currTable.TryGetValue(currSymbol, out Entry? entry))
			return Task.FromResult(entry.Rate);
		else
			throw new ArgumentException("invalid currency " + currSymbol);
	}


	public Task<CurrencyData> GetCurrencyDataAsync(string currSymbol)
	{
		if (currTable.TryGetValue(currSymbol, out Entry? entry))
			return Task.FromResult(new CurrencyData(currSymbol, entry.Name, entry.Country, entry.Rate));
		else
			throw new ArgumentException("invalid currency " + currSymbol);
	}

	public async Task<decimal> RateOfExchangeAsync(string srcCurr, string targCurr) =>
	  (await EuroRate(targCurr)) / (await EuroRate(srcCurr));

	public Task<IEnumerable<string>> GetCurrenciesAsync() =>
	  Task.FromResult<IEnumerable<string>>(currTable.Keys.OrderBy(s => s));

	public Task<bool> CurrencyExistsAsync(string currSymbol) =>
	  Task.FromResult(currTable.ContainsKey(currSymbol));

	public Task InsertAsync(CurrencyData data)
	{
		if (currTable.ContainsKey(data.Symbol))
			throw new ArgumentException("currency " + data.Symbol + " already exists");
		currTable.Add(data.Symbol, new Entry(data.Name, data.Country, data.EuroRate));
		return Task.CompletedTask;
	}

	public Task UpdateAsync(CurrencyData data)
	{
		if (currTable.TryGetValue(data.Symbol, out Entry? entry))
		{
			entry.Name = data.Name;
			entry.Country = data.Country;
			entry.Rate = data.EuroRate;
			return Task.CompletedTask;
		}
		else
			throw new ArgumentException("invalid currency " + data.Symbol);
	}

	public Task DeleteAsync(string symbol)
	{
		currTable.Remove(symbol);
		return Task.CompletedTask;
	}
}
