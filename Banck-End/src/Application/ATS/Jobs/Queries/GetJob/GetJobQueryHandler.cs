using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Application.Common.Interfaces.Repository;

namespace Totvs.Ats.Application.ATS.Jobs.Queries.GetJob;

internal class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobDto?>
{
    private readonly IJobRepository _jobRepository;

    public GetJobQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobDto?> Handle(GetJobQuery request, CancellationToken cancellationToken)
    {
        var result = await _jobRepository.GetById(request.JobId);

        if (result is null || result.Id.Value == Guid.Empty)
            return null;

        return result.Adapt<JobDto>();
    }
}