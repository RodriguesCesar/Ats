using Totvs.Ats.Application.ATS.Jobs.DTOs;

namespace Totvs.Ats.Application.ATS.Jobs.Queries.GetAllJobs;

public readonly record struct GetAllJobsQuery : IRequest<List<JobListItemDto>>;