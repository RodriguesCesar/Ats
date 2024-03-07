using System.Reflection;
using Totvs.Ats.Application.Pipelines;
using Totvs.Ats.Application.Common.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Totvs.Ats.Application;



public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingEnrichingPipelineBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionEnrichingPipelineBehaviour<,>));

        ApplicationMapperConfig.AddMappingConfigs();

        return services;
    }
}