using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Infrastructure.Resilience;

namespace Totvs.Ats.Infrastructure.Clients.JobClient
{
    public static class JobClientExtensions
    {
        public static IServiceCollection AddJobClientConfiguration(
    this IServiceCollection services,
    IConfigurationSection configuration)
        {
            var baseUrl = configuration.GetValue<string>("JobClientConfiguration:BaseUrl");
            var secret = configuration.GetValue<string>("JobClientConfiguration:Secret");
            var resiliencyConfigSection = configuration.GetSection("HttpClientsResiliency");

            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "Base url for the Job client not provided in the configuration.");

            services.AddHttpClient<IJobClient, JobClient>(
                    client =>
                    {
                        client.BaseAddress = new Uri(baseUrl).EnsureSlashed();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("AccessKey", secret);
                    })
                .AddRetryPolicy(resiliencyConfigSection)
                .AddCircuitBreakerPolicy(resiliencyConfigSection);

            return services;
        }

    }
}
