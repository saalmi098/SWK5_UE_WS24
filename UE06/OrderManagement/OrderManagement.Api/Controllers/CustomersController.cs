using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.Mapping;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IOrderManagementLogic logic;

    // Dependency Injection
    public CustomersController(IOrderManagementLogic orderManagementLogic)
    {
        logic = orderManagementLogic ?? throw new ArgumentException(null, nameof(orderManagementLogic));
    }

    // eine Route darf kein Verb enthalten!!! (wichtig fuer Semesterprojekt)
    // (z.B. createPerson -> das ist SOAP (=funktionsorientiert))
    // REST = Ressourcen-Orientiert. Verb ergibt sich aus dem HTTP-Verb (GET, POST, PUT, DELETE ...)


    // GET <base-url>/api/customers
    // oder:
    // GET <base-url>/api/customers?rating=A (optionaler Parameter / Nullable)
    // [HttpGet("myFancyRoute")] // GET <base-url>/api/customers/myFancyRoute
    [HttpGet]
    public async Task<IEnumerable<CustomerDto>> GetCustomers([FromQuery] Rating? rating)
    {
        if (rating is null)
        {
            return (await logic.GetCustomersAsync()).Select(c => c.ToCustomerDto());
        }
        else
        {
            return (await logic.GetCustomersByRatingAsync(rating.Value)).Select(c => c.ToCustomerDto());
        }
    }

    // GET <base-url>/api/customers/<GUID>
    [HttpGet("{customerId}")] // Automatisches Mapping, sofern analog zum Variablen-Namen
    public async Task<ActionResult<CustomerDto>> GetCustomerById([FromRoute] Guid customerId)
    {
        var customer = await logic.GetCustomerAsync(customerId);

        if (customer is null)
        {
            // 404 returnen, falls customerId ungueltig (TODO Funktioniert (nur bei mir) noch nicht?)
            return NotFound(); // erfordert ActionResult<...> im Rueckgabetyp
        }

        return Ok(customer.ToCustomerDto()); // Syntax "Ok(...)" ist optional
    }
}