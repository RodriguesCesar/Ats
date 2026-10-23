using System.Net;
using System.Numerics;
using Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;

namespace Totvs.Ats.Application.CommonTests.Builders;

public static class AddApplicantCommandBuilder
{
    public static AddApplicantCommand GetAddApplicantCommandEmpty()
    {
        return new AddApplicantCommand
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
         UserId: default
        );
    }

    public static AddApplicantCommand GetAddApplicantCommandWithFirstName(string firstName = "AddApplicantCommand FirstName")
    {
        return GetAddApplicantCommandEmpty() with
        {
            FirstName = firstName
        };
    }
}