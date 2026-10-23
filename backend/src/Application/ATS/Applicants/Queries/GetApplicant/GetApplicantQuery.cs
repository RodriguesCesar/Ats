using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.ATS.Applicants.Queries.GetApplicant;

public readonly record struct GetApplicantQuery(Guid ApplicantId) : IRequest<ApplicantDto?>;