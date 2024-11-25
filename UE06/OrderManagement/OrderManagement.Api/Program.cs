using OrderManagement.Logic;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Service Collection (fuer Dependency Injection)
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    })
    .AddXmlDataContractSerializerFormatters(); // aktiviert XML-Unterstuetzung (Accept: application/xml) fuer Responses (erfordert [DataContract] und [DataMember] Attribute in Customer Klasse - zumindest bis zu dem Zeitpunkt, wo wir DTOs eingefuehrt haben)

builder.Services.AddScoped<IOrderManagementLogic, OrderManagementLogic>(); // bei jedem Request eine neue Instanz (Scope: von eingehendem Request bis ausgehendem Reponse -> immer die selbe Instanz)
//builder.Services.AddSingleton<...>(); // -> nur eine Instanz pro ASP.Net Core Anwendung (erst wenn Anwendung abstuerzt oder neu startet = neue Instanz)
//builder.Services.AddTransient<...>(); // immer eine neue Instanz

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
