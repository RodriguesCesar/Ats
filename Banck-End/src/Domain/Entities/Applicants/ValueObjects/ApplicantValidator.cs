using Totvs.Ats.Domain.Common.Validators;
using FluentValidation;

namespace Totvs.Ats.Domain.Entities.Applicants;

public class ApplicantValidator : AbstractValidator<Applicant>
{
    private const int MinLength = 5;
    private const int MaxLength = 350;

    public ApplicantValidator()
    {
        RuleFor(applicant => applicant.Id.Value)
            .NotEmpty();

        RuleFor(applicant => applicant.FirstName)
            .NotEmpty()
            .WithErrorCode("Applicant_FirstName_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Applicant_FirstName_MinimumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Applicant_FirstName_MaximumLength");

        RuleFor(applicant => applicant.LastName)
            .NotEmpty()
            .WithErrorCode("Applicant_LastName_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Applicant_LastName_MinimumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Applicant_LastName_MaximumLength");

        RuleFor(applicant => applicant.Email)
            .NotEmpty()
                .WithAutomaticErrorCode(this);


    }
}
