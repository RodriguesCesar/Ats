using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.CommonTests.Builders;

public static class ApplicantDtoBuilder
{
    public static ApplicantDto GetApplicantDtoEmpty()
    {
        return new ApplicantDto
        (
            Id: default,
            FirstName: default!,
            LastName: default!,
            Summary: default,
            ProfilePhoto: default,
            Email: default!,
            Phone: default,
            Address: default,
            Skills: default,
            ApplyDate: default,
            ModifiedOn: default
        );
    }
}

