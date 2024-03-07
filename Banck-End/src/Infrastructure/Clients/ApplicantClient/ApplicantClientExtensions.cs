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

namespace Totvs.Ats.Infrastructure.Clients.ApplicantClient
{
    public static class ApplicantClientExtensions
    {
        public static IServiceCollection AddApplicantClientConfiguration(
    this IServiceCollection services,
    IConfigurationSection configuration)
        {
            var baseUrl = configuration.GetValue<string>("ApplicantClientConfiguration:BaseUrl");
            var secret = configuration.GetValue<string>("ApplicantClientConfiguration:Secret");
            var resiliencyConfigSection = configuration.GetSection("HttpClientsResiliency");

            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl), "Base url for the Applicant client not provided in the configuration.");

            services.AddHttpClient<IApplicantClient, ApplicantClient>(
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
