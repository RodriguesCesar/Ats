using Totvs.Ats.Api.Endpoints;
using Totvs.Ats.Api.Middleware;
using Totvs.Ats.Api.Telemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using OpenTelemetry.Instrumentation.AspNetCore;

namespace Totvs.Ats.Api;

internal static class DependencyInjection
{
    private const string ServiceName = "Totvs";
    private static readonly ApplicantEndpoints ApplicantEndpoints = new();
    private static readonly JobEndpoints JobEndpoints = new();
    private static readonly SwaggerEndpoints SwaggerEndpoints = new();

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpLogging(httpLogging =>
        {
            httpLogging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
        });

        services.AddOpenTelemetry().WithMetrics(options =>
            options.AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName).AddTelemetrySdk())
                .AddOtlpExporter(otlpOption =>
                {
                    otlpOption.Endpoint = new Uri("http://your-open-collector-instance:4317", UriKind.Absolute);
                })
                .AddPrometheusExporter()
                .AddMeter(ApplicationApplicantMetrics.ServiceMetricName)
                .AddMeter(ApplicationJobMetrics.ServiceMetricName)
        )
            .WithTracing(options =>
            options
                .AddSource(ServiceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName).AddTelemetrySdk())
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter()
        );

        services.Configure<AspNetCoreInstrumentationOptions>(options =>
            {
                options.RecordException = true;
            }
        );

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        SwaggerEndpoints.DefineEndpoints(app);
        ApplicantEndpoints.DefineEndpoints(app);
        JobEndpoints.DefineEndpoints(app);

        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        app.UseHttpLogging();
        app.UseMiddleware<ErrorHandlerMiddleware>();

        return app;
    }
}