using Totvs.Ats.Application.ATS.Jobs.DTOs;

namespace Totvs.Ats.Application.ATS.Jobs.Queries.GetJob;

public readonly record struct GetJobQuery(Guid JobId) : IRequest<JobDto?>;