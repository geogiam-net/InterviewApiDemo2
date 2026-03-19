using Demo.Api.Endpoints;
using Demo.Api.Exceptions;
using Demo.Infrastructure.AuthorizationService.Interfaces;
using Demo.Infrastructure.AuthorizationService.Services;
using Demo.Infrastructure.FuelEconomyService.Interfaces;
using Demo.Infrastructure.FuelEconomyService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmissionsService, EmissionsService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// not needed for this demo, but in production you should use https and authorization
// app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// UseStatusCodePages enables middleware that provides default responses for HTTP status codes
// (like 404, 400, 500) when your API does not return a body
app.UseStatusCodePages();
app.UseExceptionHandler();

app.MapAuthorizationEndpoint();
app.MapVehicleEmissionsEndpoint();

// not needed for this demo, but in production you should use https and authorization
// app.UseAuthorization(); 

app.Run();