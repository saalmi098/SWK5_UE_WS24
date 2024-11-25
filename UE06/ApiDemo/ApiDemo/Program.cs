var builder = WebApplication.CreateBuilder(args);

// damit Controller verwendet werden koennen -> damit untersucht das Framework nach ControllerBase-Impementierungen
builder.Services.AddControllers();

var app = builder.Build();

// Aufruf im Browser: http://localhost:5165/time1
// Beim Starten der Anwendung wird standardmaessig "/weatherforecast" im Browser aufgerufen
// -> Konfigurierbar in Launch Profiles -> launchSettings.json -> "launchUrl": "time1"

// app.MapGet("/time1", () => DateTime.UtcNow.ToString("o"));
// --> liefert Antwort in Format "text/plain"

// liefert Antwort im Format "application/json"
app.MapGet("/time1", () => Results.Json(new {
    Time = DateTime.UtcNow.ToString("o")
}));
// -> Minimal-API based Ansatz (ohne Controller)
// Controller sind immer ressourcen-zentriert (z.B. CustomerController, OrderController) -> uebersichtlicher
// Aber beide werden in der Praxis eingesetzt

// Mappe Controller auf API-Endpunkte
app.MapControllers();

app.Run();
