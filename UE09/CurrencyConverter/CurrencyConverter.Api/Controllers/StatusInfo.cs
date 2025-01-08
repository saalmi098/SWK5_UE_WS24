using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Api.Controllers;

public class StatusInfo
{
	public static ProblemDetails InvalidCurrencySymbol(string symbol) => new ProblemDetails
	{
		Title = "Invalid currency symbol",
		Detail = $"Currency '{symbol}' does not exist"
	};

	public static ProblemDetails CurrencyAlreadyExists(string symbol) => new ProblemDetails
	{
		Title = "Conflicting currency symbols",
		Detail = $"Currency '{symbol}' already exists"
	};
}
