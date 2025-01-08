using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CurrencyConverter.Api.Controllers;

public static class WebApiConventions
{
	[ProducesDefaultResponseType]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
	public static void GetBy(
	  [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
  [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
	{
	}

	[ProducesDefaultResponseType]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
	public static void Update(
	  [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
  [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
	{
	}

	[ProducesDefaultResponseType]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status409Conflict)]
	[ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
	public static void Insert(
	  [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
  [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
	{
	}

	[ProducesDefaultResponseType]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
	public static void Delete(
	  [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
  [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
	{
	}
}
