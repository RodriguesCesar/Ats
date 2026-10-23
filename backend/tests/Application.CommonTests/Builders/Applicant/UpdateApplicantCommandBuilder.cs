using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateApplicant;

namespace Totvs.Ats.Application.CommonTests.Builders;

public static class UpdateApplicantCommandBuilder
{
    public static UpdateApplicantCommand GetUpdateApplicantCommandEmpty()
    {
        return new UpdateApplicantCommand
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
            ModifiedOn: default,
            UserId: default
        );
    }

    public static UpdateApplicantCommand GetUpdateApplicantCommandWithFirstName(string firstName = "UpdateApplicantCommand FirstName")
    {
        return GetUpdateApplicantCommandEmpty() with
        {
            FirstName = firstName
        };
    }
}
