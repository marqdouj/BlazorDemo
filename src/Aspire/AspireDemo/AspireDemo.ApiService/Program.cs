using AspireDemo.ApiService.EndPoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.DefaultFonts = false; // Disable default fonts to avoid download unnecessary fonts
        options.Servers = []; //Required in Aspire
    });
}

app.MapWeatherApi();
app.MapDefaultEndpoints();

app.Run();

