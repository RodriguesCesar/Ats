using Totvs.Ats.Application.ATS.Applicants.DTOs;
using Totvs.Ats.Application.ATS.Applicants.Queries.GetAllApplicants;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Domain.Entities.Applicants;

namespace Totvs.Ats.Application.ATS.Applicants.Queries.GetAllApplicants;

public class GetAllApplicantsQueryHandler : IRequestHandler<GetAllApplicantsQuery, List<ApplicantListItemDto>>
{
    private readonly IApplicantRepository _applicantRepository;

    public GetAllApplicantsQueryHandler(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }

    public async Task<List<ApplicantListItemDto>> Handle(GetAllApplicantsQuery request, CancellationToken cancellationToken)
    {



        try
        {
            var result = await _applicantRepository.GetAll();
            return result.Select(p => p.Adapt<ApplicantListItemDto>()).ToList();
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
}