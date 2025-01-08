using CurrencyConverter.Domain;
using CurrencyConverter.Logic;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace CurrencyConverter.Api.Controllers;

[ApiController]
[ApiConventionType(typeof(WebApiConventions))]
[Route("api/currencies")]
public class CurrenciesController : ControllerBase
{
	private ICurrencyCalculator Logic { get; }
	private ILogger<CurrenciesController> Logger { get; }

	private async Task<IEnumerable<CurrencyData>> GetAllCurrencyDataAsync()
	{
		var tasks = (await Logic.GetCurrenciesAsync())
								.Select(async symbol => await Logic.GetCurrencyDataAsync(symbol));
		return await Task.WhenAll(tasks);
	}

	public CurrenciesController(ICurrencyCalculator logic, ILogger<CurrenciesController> logger)
	{
		this.Logic = logic;
		this.Logger = logger;
	}

	[HttpGet]
	public async Task<IEnumerable<CurrencyData>> GetAll()
	{
		return await GetAllCurrencyDataAsync();
	}

	[HttpGet("{symbol}")]
	[OpenApiOperation("Currency details",
	  "Returns detailed data for a given currency symbol.")]
	public async Task<ActionResult<CurrencyData>> GetBySymbol(string symbol)
	{
		if (!await Logic.CurrencyExistsAsync(symbol))
		{
			return NotFound(StatusInfo.InvalidCurrencySymbol(symbol));
		}

		return await Logic.GetCurrencyDataAsync(symbol);
	}

	[HttpGet("{sourceSymbol}/rates/{targetSymbol}")]
	[ApiConventionMethod(typeof(WebApiConventions), nameof(WebApiConventions.GetBy))]
	public async Task<ActionResult<decimal>> RateOfExchange(
	  string sourceSymbol, string targetSymbol)
	{
		if (!await Logic.CurrencyExistsAsync(sourceSymbol))
		{
			return NotFound(StatusInfo.InvalidCurrencySymbol(sourceSymbol));
		}
		if (!await Logic.CurrencyExistsAsync(targetSymbol))
		{
			return NotFound(StatusInfo.InvalidCurrencySymbol(targetSymbol));
		}

		return await Logic.RateOfExchangeAsync(sourceSymbol, targetSymbol);
	}

	[HttpPost]
	public async Task<ActionResult> Insert([FromBody] CurrencyData data)
	{
		if (await Logic.CurrencyExistsAsync(data.Symbol))
		{
			return Conflict(StatusInfo.CurrencyAlreadyExists(data.Symbol));
		}

		await Logic.InsertAsync(data);
		return CreatedAtAction(actionName: nameof(GetBySymbol),
							   routeValues: new { symbol = data.Symbol },
							   value: null);
	}

	[HttpPut]
	public async Task<ActionResult> Update([FromBody] CurrencyData data)
	{
		if (!await Logic.CurrencyExistsAsync(data.Symbol))
		{
			return NotFound(StatusInfo.InvalidCurrencySymbol(data.Symbol));
		}

		await Logic.UpdateAsync(data);
		return NoContent();
	}

	[HttpDelete("{symbol}")]
	public async Task<ActionResult> Delete(string symbol)
	{
		await Logic.DeleteAsync(symbol);
		return NoContent();
	}
}