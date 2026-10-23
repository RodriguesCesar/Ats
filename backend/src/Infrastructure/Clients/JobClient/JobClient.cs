using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Infrastructure.Clients.JobClient.Models;


namespace Totvs.Ats.Infrastructure.Clients.JobClient
{
    public class JobClient : IJobClient
    {
        private readonly HttpClient _httpClient;
        public JobClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UpdateJob(Guid jobId, int unitsChange)
        {
            var url = $"{_httpClient.BaseAddress}/Job-update/";
            var request = new UpdateJobRequest(jobId, unitsChange);
            var requestContent = new StringContent(JsonSerializer.Serialize(request));
            await _httpClient.PostAsync(url, requestContent);
        }
    }
}
