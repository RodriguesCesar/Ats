using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;
using Totvs.Ats.Infrastructure.Clients.ApplicantClient.Models;


namespace Totvs.Ats.Infrastructure.Clients.ApplicantClient
{
    public class ApplicantClient: IApplicantClient
    {
        private readonly HttpClient _httpClient;
        public ApplicantClient(HttpClient httpClient) 
        { 
              _httpClient = httpClient;
        }

        public async Task UpdateApplicant(Guid applicanttId, int unitsChange)
        {
            var url = $"{_httpClient.BaseAddress}/Applicant-update/";
            var request = new UpdateApplicantRequest(applicanttId, unitsChange);
            var requestContent = new StringContent(JsonSerializer.Serialize(request));
            await _httpClient.PostAsync(url, requestContent);
        }
    }
}
