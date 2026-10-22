using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.ATS.Applicants.Queries.GetAllApplicants;

public readonly record struct GetAllApplicantsQuery : IRequest<List<ApplicantListItemDto>>;