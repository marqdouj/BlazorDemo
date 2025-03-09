using AspireDemo.ApiService.Data;
using AspireDemo.ApiService.EndPoints;
using AspireDemo.ApiService.Services;
using Marqdouj.CLRCommon;
using Microsoft.AspNetCore.Diagnostics;
using PIMS.ApiService.ApiEndPoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.AddSqlServerDbContext<PIMSContext>("PIMS");

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.ConfigureEmailService();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler(exApp => exApp.Run(async context =>
{
    if (context.Response.HasStarted)
        return;

    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    var exMsg = exception?.ToMessage();


    if (exception != null)
    {
        ILogger<IEmailService>? logger = null;
        exMsg = $"{exception.Source}: {exMsg}";

        try
        {
            try
            {
                var factory = exApp.ApplicationServices.GetService<ILoggerFactory>();
                logger = factory?.CreateLogger<IEmailService>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create logger: {ex}");
            }

            using var scope = exApp.ApplicationServices.CreateScope();
            var emailService = scope.ServiceProvider.GetService<IEmailService>();
            if (emailService != null)
                await emailService.SendErrorEmail(exception);
            else
            {
                logger?.LogError(exception, "ExceptionHandler - Failed to get email service.");
                Console.WriteLine("ExceptionHandler - Failed to get email service.");
            }
        }
        catch (Exception)
        {
            logger?.LogError(exception, "ExceptionHandler - SendErrorEmail failed.");
            Console.WriteLine("ExceptionHandler - SendErrorEmail failed.");
        }
    }

    var pds = context.RequestServices.GetService<IProblemDetailsService>();
    var ok = false;

    if (pds != null)
    {
        if (string.IsNullOrWhiteSpace(exMsg))
        {
            ok = await pds.TryWriteAsync(new() { HttpContext = context });
        }
        else
        {
            ok = await pds.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails =
                {
                    Title = exMsg,
                }
            });
        }
    }

    if (ok is false)
    {
        // Fallback behavior
        await context.Response.WriteAsync($"Fallback: An error has occurred. {exMsg}");
    }
}));

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.DefaultFonts = false; // Disable default fonts to avoid download unnecessary fonts
        options.Servers = []; //Required in Aspire
    });

    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<PIMSContext>();
    await context.Database.EnsureCreatedAsync();

    //NOTE: You can seed the database in development using Scalar OpenAPI endpoint /vpm/seed
    //      This may take a few minutes to complete (5+).
}

app.MapWeatherApi();
app.MapNewsletter();
app.MapPIMS(app.Environment.IsDevelopment());
app.MapDevelopment(app.Environment.IsDevelopment());
app.MapDefaultEndpoints();

app.Run();

