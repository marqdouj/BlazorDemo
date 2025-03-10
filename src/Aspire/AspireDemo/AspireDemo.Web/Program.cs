using AspireDemo.ApiService.Client;
using AspireDemo.PIMS.Models;
using AspireDemo.Web.Common;
using AspireDemo.Web.Components;
using AspireDemo.Web.Components.Pages;
using AspireDemo.Web.Models;
using AzureMapsControl.Components;
using Darnton.Blazor.DeviceInterop.Geolocation;
using Marqdouj.Html.ResizeObserver;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<IApiServiceClient, ApiServiceClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });

builder.Services.AddScoped<CounterState>();
builder.Services.AddScoped<ResizeObserverService>();
builder.Services.AddScoped<ILIState>();
builder.Services.AddScoped<GPSViewState>();
builder.Services.AddScoped<IServerLocalStorage, ServerLocalStorage>();

builder.Services.AddScoped<IGeolocationService, GeolocationService>();
builder.SetAzureMapConfig();
builder.Services.AddAzureMapsControl(
    configuration => configuration.ClientId = AzureMapsAuthService.MapConfig.ClientId);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
