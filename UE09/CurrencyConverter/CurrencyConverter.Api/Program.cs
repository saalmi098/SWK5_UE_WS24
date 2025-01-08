using CurrencyConverter.Logic;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app, app.Environment);
ConfigureEndpoints(app);

app.Run();



void ConfigureServices(IServiceCollection services)
{
	services.AddControllers(options => options.ReturnHttpNotAcceptable = true);

	services.AddOpenApiDocument(settings =>
	  settings.PostProcess = doc => doc.Info.Title = "Currency Converter API");

	services.AddCors(builder =>
	  builder.AddDefaultPolicy(policy =>
		policy.AllowAnyOrigin()
			    .AllowAnyMethod()
			    .AllowAnyHeader()));

	services.AddSingleton<ICurrencyCalculator, CsvCurrencyCalculator>();
}

void ConfigureMiddleware(IApplicationBuilder app, IWebHostEnvironment env)
{
	if (env.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
	}

	app.UseHttpsRedirection();

	app.UseDefaultFiles();
	app.UseStaticFiles();

  // Specify location of open API document (relative to wwwroot).
	app.UseSwaggerUi(settings => settings.DocumentPath = "api/v1/openapi.json");

  app.UseCors();

	app.UseRouting();

	app.UseAuthorization();
}

void ConfigureEndpoints(IEndpointRouteBuilder app)
{
  app.MapControllers();
}