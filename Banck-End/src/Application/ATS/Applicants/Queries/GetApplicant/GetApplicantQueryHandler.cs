using Totvs.Ats.Application.ATS.Applicants.DTOs;
using Totvs.Ats.Application.Common.Interfaces.Repository;

namespace Totvs.Ats.Application.ATS.Applicants.Queries.GetApplicant;

public class GetApplicantQueryHandler : IRequestHandler<GetApplicantQuery, ApplicantDto?>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetApplicantQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task<ApplicantDto?> Handle(GetApplicantQuery request, CancellationToken cancellationToken)
    {
        var result = await _applicantRepository.GetById(request.ApplicantId);

        if (result is null || result.Id.Value == Guid.Empty)
            return null;

        return result.Adapt<ApplicantDto>();
    }
}