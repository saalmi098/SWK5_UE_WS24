using OrderManagement.Api.HostedServices;
using OrderManagement.Logic;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// In Produktivumgebung nicht alles erlauben -> sondern z.B. "ich erlaube diese beiden Base-URLs, diese Methoden, ..."
builder.Services.AddCors(builder => builder.AddDefaultPolicy(policy =>
    policy
        .AllowAnyOrigin() // jede Origin-URL wird erlaubt
        .AllowAnyMethod() // jede HTTP-Method wird erlaubt
        .AllowAnyHeader()
    ));

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

builder.Services.AddRouting(options => { options.LowercaseUrls = true; });

// NSwag (NuGet Packages: NSwag.AspNetCore, NSwag.MSBuild, NSwag.Annotations)
builder.Services.AddOpenApiDocument(settings => settings.Title = "Order Management API");

// Hosted Service (Background Service)
builder.Services.AddHostedService<QueuedUpdateService>();
builder.Services.AddSingleton<UpdateChannel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUi(settings => settings.Path = "/swagger"); // http://localhost:xxxx/swagger
app.UseReDoc(settings => settings.Path = "/redoc"); // http://localhost:xxxx/redoc

app.Run();
