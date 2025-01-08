using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Domain;

public class CurrencyData(string symbol, string name, string? country, decimal euroRate)
{
  [Required]
  public string Symbol { get; set; } = symbol;

  [Required]
  public string Name { get; set; } = name;

  public string? Country { get; set; } = country;

  [Required]
  [Range(0, double.MaxValue, ErrorMessage = "EuroRate must be positive")]
  public decimal EuroRate { get; set; } = euroRate;

  public override string ToString()
	{
		return String.Format($"{Name} ({Symbol}): euroRate={EuroRate}; country={Country}");
	}
}