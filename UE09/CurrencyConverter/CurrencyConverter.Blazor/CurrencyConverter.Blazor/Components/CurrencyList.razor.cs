using CurrencyConverter.Domain;
using CurrencyConverter.Logic;
using Microsoft.AspNetCore.Components;

namespace CurrencyConverter.Blazor.Components;

public partial class CurrencyList
{
	[Inject]
	public ICurrencyCalculator Logic { get; set; } = null!;

	private ICollection<CurrencyData>? currencies;

	protected override async Task OnInitializedAsync()
	{
		await Task.Delay(2000);
		currencies = [];

		foreach (var symbol in await Logic.GetCurrenciesAsync())
		{
			await Task.Delay(200);
			currencies.Add(await Logic.GetCurrencyDataAsync(symbol));
			StateHasChanged(); // erfordert Attribut [StreamRendering] in razor-Component
		}
	}
}