using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

// Controller-based Ansatz

// [controller] uebernimmt "Time" aus Klassenname "TimeController"
//[Route("api/[controller]")]
[Route("/time2")]
[ApiController]
public class TimeController : ControllerBase
{
    // Funktionen hier drinnen, um API-Endpunkte zu beschreiben, nennt man "Actions"

    [HttpGet]
    public object Get()
    {
        // retourniert im Format "application/json" ohne es angeben zu muessen
        // (da JSON der Default-Serializer in ASP.NET ist)
        return new { Time = DateTime.UtcNow.ToString("o") };
    }
}
