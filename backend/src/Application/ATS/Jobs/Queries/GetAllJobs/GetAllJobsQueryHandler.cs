using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Application.ATS.Jobs.Queries.GetAllJobs;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Domain.Entities.Jobs;

namespace Totvs.Ats.Application.ATS.Jobs.Queries.GetAllJobs;

internal class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, List<JobListItemDto>>
{
    private readonly IJobRepository _jobRepository;

    public GetAllJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<List<JobListItemDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {

        var result = await _jobRepository.GetAll();
            return result.Select(p => p.Adapt<JobListItemDto>()).ToList();

    }
}