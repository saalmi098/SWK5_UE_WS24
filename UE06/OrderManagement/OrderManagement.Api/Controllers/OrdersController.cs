using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.Mapping;
using OrderManagement.Logic;

namespace OrderManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderManagementLogic logic;

    public OrdersController(IOrderManagementLogic orderManagementLogic)
    {
        logic = orderManagementLogic ?? throw new ArgumentException(null, nameof(orderManagementLogic));
    }

    // GET <base-url>/api/customers/<customerId>/orders
    [HttpGet("/api/customers/{customerId}/orders")] // beginnt Route mit "/" -> ueberschreibt diese saemtliche Prefixes (also auch die in der Klasse definierte Route)
    [Route("")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersOfCustomer([FromRoute] Guid customerId)
    {
        if (!await logic.CustomerExistsAsync(customerId))
        {
            return NotFound();
        }

        var orders = await logic.GetOrdersOfCustomerAsync(customerId);
        return Ok(orders.Select(o => o.ToOrderDto()));
    }
}
