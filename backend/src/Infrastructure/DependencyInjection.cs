using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Infrastructure.Database.Repositories;
using Totvs.Ats.Infrastructure.Services;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Infrastructure.Clients.ApplicantClient.Configuration;
using Totvs.Ats.Infrastructure.Clients.ApplicantClient;
using Totvs.Ats.Infrastructure.Clients.JobClient.Configuration;
using Totvs.Ats.Infrastructure.Clients.JobClient;
namespace Totvs.Ats.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationSection configuration)
    {
       
        services.Configure<ApplicantClientConfiguration>(configuration.GetSection("ApplicantClientConfiguration"));
        services.Configure<JobClientConfiguration>(configuration.GetSection("JobClientConfiguration"));

        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddSingleton<IApplicantRepository, ApplicantRepository>();
        services.AddSingleton<IJobRepository, JobRepository>();
        services.AddApplicantClientConfiguration(configuration);
        services.AddJobClientConfiguration(configuration);
        return services;
    }
}