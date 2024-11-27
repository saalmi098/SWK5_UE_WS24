using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.HostedServices;
using OrderManagement.Api.Mapperly;
using OrderManagement.Api.Mapping;
using OrderManagement.Domain;
using OrderManagement.Logic;

namespace OrderManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiConventionType(typeof(WebApiConventions))] // WebApiConventions aktivieren (damit alle Actions automatisch nach bestimmten Regeln dokumentiert werden fuer OpenAPI / Swagger)
public class CustomersController : ControllerBase
{
    private readonly IOrderManagementLogic logic;
    private readonly UpdateChannel updateChannel;

    // Dependency Injection
    public CustomersController(IOrderManagementLogic orderManagementLogic, UpdateChannel updateChannel)
    {
        logic = orderManagementLogic ?? throw new ArgumentNullException(nameof(orderManagementLogic));
        this.updateChannel = updateChannel ?? throw new ArgumentNullException(nameof(updateChannel));
    }

    // eine Route darf kein Verb enthalten!!! (wichtig fuer Semesterprojekt)
    // (z.B. createPerson -> das ist SOAP (=funktionsorientiert))
    // REST = Ressourcen-Orientiert. Verb ergibt sich aus dem HTTP-Verb (GET, POST, PUT, DELETE ...)


    // GET <base-url>/api/customers
    // oder:
    // GET <base-url>/api/customers?rating=A (optionaler Parameter / Nullable)
    // [HttpGet("myFancyRoute")] // GET <base-url>/api/customers/myFancyRoute
    [HttpGet]
    //[ApiConventionMethod(typeof(WebApiConventions), nameof(WebApiConventions.Get))] // so koennte man abweichende WebApiConvention angeben fuer eine Methode
    public async Task<IEnumerable<CustomerDto>> GetCustomers([FromQuery] Rating? rating)
    {
        if (rating is null)
        {
            //return (await logic.GetCustomersAsync()).Select(c => c.ToCustomerDto());
            return (await logic.GetCustomersAsync()).ToCustomerDtoEnumeration(); // Mapperly
        }
        else
        {
            return (await logic.GetCustomersByRatingAsync(rating.Value)).Select(c => c.ToCustomerDto());
        }
    }

    // GET <base-url>/api/customers/<GUID>
    [HttpGet("{customerId}")] // Automatisches Mapping, sofern analog zum Variablen-Namen
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)] // wird standardmaessig schon in OpenAPI/Swagger angezeigt
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // extra angeben fuer OpenAPI/Swagger -> Swagger zeigt automatisch dass ein ProblemDetails (mit Title, Detail etc.) zurueckkommt
    [ProducesResponseType(StatusCodes.Status404NotFound)] // wir wissen, dass ein 404er auftreten kann -> explizit fuer OpenAPI/Swagger angeben
    public async Task<ActionResult<CustomerDto>> GetCustomerById([FromRoute] Guid customerId)
    {
        var customer = await logic.GetCustomerAsync(customerId);

        if (customer is null)
        {
            // 404 returnen, falls customerId ungueltig (TODO Funktioniert (nur bei mir) noch nicht?)
            //return NotFound(); // erfordert ActionResult<...> im Rueckgabetyp

            // bessere Fehlermeldung:
            return NotFound(StatusInfo.InvalidCustomerId(customerId));
        }

        return Ok(customer.ToCustomerDto()); // Syntax "Ok(...)" ist optional
    }

    // POST /api/customers
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerForCreationDto customer)
    {
        if (customer.Id != Guid.Empty && await logic.CustomerExistsAsync(customer.Id))
        {
            //return Conflict(); // bei den uebergebenen Daten passt etwas nicht, es gibt die ID schon (HTTP-Statuscode 409)

            // bessere Fehlermeldung:
            return Conflict(StatusInfo.CustomerAlreadyExists(customer.Id));
        }

        // Validierung der Customer-Eigenschaften: if (<error>) return BadRequest();

        Customer customerDomain = customer.ToCustomer();
        await logic.AddCustomerAsync(customerDomain);

        // Erfolgreicher POST -> 201 Created (nicht 200 OK)
        return CreatedAtAction(
            actionName: nameof(GetCustomerById),
            routeValues: new { customerId = customerDomain.Id }, // mitgeben, unter welcher URL der neue Eintrag erreichbar ist
            value: customerDomain.ToCustomerDto()
        );
    }

    // PUT /api/customers/<GUID>
    // hier exemplarisch auf Return-Typ CustomerDto verzichtet (da Client die Properties sowieso schon kennt)
    [HttpPut("{customerId}")]
    public async Task<ActionResult> UpdateCustomer([FromRoute] Guid customerId, CustomerForUpdateDto customerDto)
    {
        Customer? customer = await logic.GetCustomerAsync(customerId);

        if (customer is null)
        {
            return NotFound();
        }

        // Validierung der Customer-Eigenschaften: if (<error>) return BadRequest();

        customerDto.UpdateCustomer(customer);
        await logic.UpdateCustomerAsync(customer);
        return NoContent(); // 204 No Content (kein Body; statt 200 OK)
    }

    // DELETE /api/customers/<GUID>
    [HttpDelete("{customerId}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute] Guid customerId)
    {
        if (await logic.DeleteCustomerAsync(customerId))
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    // Beispiel fuer HostedService (Background-Service)
    // Update customer totals
    // POST /api/customers/<GUID>/totals
    [HttpPost("{customerId}/totals")]
    public async Task<ActionResult> UpdateCustomerTotals(Guid customerId, CancellationToken cancellationToken)
    {
        // in jeder API-Action kann man einen cancellationToken uebergeben, um asynchrone Operationen abzubrechen (kommt automatisch aus dem HTTP-Kontext)

        if (!await logic.CustomerExistsAsync(customerId))
        {
            return NotFound(StatusInfo.InvalidCustomerId(customerId));
        }

        //await logic.UpdateTotalRevenueAsync(customerId); // blocking operation
        if (await updateChannel.AddUpdateTaskAsync(customerId, cancellationToken))
        {
            // 202 Accepted (wird akzeptiert, aber nicht sofort bearbeitet)
            return Accepted(); // geht direkt zurueck zum Client, Client ist nicht blockiert
        }

        return BadRequest(StatusInfo.UpdateCustomerTotalsCancelled());

        // Ergebnis: Request in Swagger ausfuehren
        // -> es kommt sofort ein Ergebnis (kein langer Ladebalken
        // -> ein paar Sekunden spaeter kommt die Ausgabe in die Konsole, dass Revenue aktualisiert wurde
    }
}