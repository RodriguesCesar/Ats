using System.Text.Json;
using System.Text.Json.Serialization;
using Totvs.Ats.Api;
using Totvs.Ats.Application;
using Totvs.Ats.Domain;
using Totvs.Ats.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using Totvs.Ats.Infrastructure.Database.Models;

var builder = WebApplication.CreateBuilder(args);



AddLoggingFromConfiguration(builder);

// Add services to the container.
builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration.GetSection("Infrastructure"));
builder.Services.Configure<TotvsAtsDatabaseSetting>(builder.Configuration.GetSection("TotvsAtsDatabase"));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyHeader().AllowAnyMethod())
);
var app = builder.Build();
app.UseCors();
app.Logger.LogInformation("Starting ...");

// Configure the HTTP request pipeline.
app.UseApi();
app.UseHttpsRedirection();
app.Run();

static void AddLoggingFromConfiguration(WebApplicationBuilder builder)
{
    var isJsonConsoleLoggingEnabled = builder.Configuration.GetValue<bool>("Logging:JsonConsoleLoggingEnabled");
    builder.Logging.ClearProviders();
    if (isJsonConsoleLoggingEnabled)
    {
        builder.Logging.AddJsonConsole(
            options =>
            {
                options.IncludeScopes     = true;
                options.TimestampFormat   = "hh:mm:ss";
                options.JsonWriterOptions = new JsonWriterOptions { Indented = false };
            });
    }
    else
        builder.Logging.AddConsole();
}