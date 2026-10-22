using Totvs.Ats.Domain.Common.Validators;
using FluentValidation;
using Totvs.Ats.Domain.Entities.Jobs;

namespace Totvs.Ats.Domain.Entities.Jobs;

internal class JobValidator : AbstractValidator<Job>
{
    private const int MinLength = 5;
    private const int MaxLength = 500;

    public JobValidator()
    {
        RuleFor(job => job.Id.Value)
            .NotEmpty();

        RuleFor(job => job.Description)
            .NotEmpty()
            .WithErrorCode("Job_Description_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Job_Description_MinimumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Job_Description_MaximumLength");

        RuleFor(job => job.Title)
            .NotEmpty()
            .WithErrorCode("Job_ContactEmail_NotEmpty")
            .MinimumLength(MinLength)
                .WithErrorCode("Job_ContactEmail_MinimumLength")
            .MaximumLength(MaxLength)
                .WithErrorCode("Job_ContactEmail_MaximumLength");

        RuleFor(job => job.ContactEmail)
            .NotEmpty()
                .WithAutomaticErrorCode(this);


    }
}
