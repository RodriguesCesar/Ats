using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Common.Validators;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Totvs.Ats.Domain.UnitTests.Common.Validators;

public class CustomValidatorsTests
{
    private readonly InlineValidator<Applicant> _validator;

    public CustomValidatorsTests()
    {
        _validator = new InlineValidator<Applicant>();
    }

    [Fact]
    public void WHEN_AutomaticErrorCode_is_well_setup_THEN_validator_return_well_formed_ErrorCode()
    {
        _validator
            .RuleFor(applicant => applicant.FirstName)
            .NotEmpty()
            .WithAutomaticErrorCode(_validator);

        var result = _validator.TestValidate(ApplicantBuilder.GetApplicantWithFirstName(string.Empty));

        result.Errors
            .Should()
            .ContainSingle()
            .Which
            .ErrorCode
            .Should()
            .Be("Applicant_FirstName_NotEmpty");
    }
    
    [Fact]
    public void WHEN_AutomaticErrorCode_is_not_well_setup_THEN_validator_return_partial_ErrorCode()
    {
        _validator
            .RuleFor(applicant => applicant.Email)
            .NotEmpty()
            .WithAutomaticErrorCode(new InlineValidator<Applicant>());

        var result = _validator.TestValidate(ApplicantBuilder.GetApplicantWithEmail(string.Empty));

        result.Errors
            .Should()
            .ContainSingle()
            .Which
            .ErrorCode
            .Should()
            .Be("Applicant");
    }
    
    [Fact]
    public void WHEN_hint_is_well_setup_THEN_validator_return_well_formed_Hint()
    {
        var skills = "You can find information about the Skills pattern at www.Skills.org";
        _validator
            .RuleFor(applicant => applicant.Skills)
            .NotEmpty()
            .WithHint(skills);

        var result = _validator.TestValidate(ApplicantBuilder.GetApplicantWithSkills(string.Empty));

        result.Errors
            .MapToValidationErrors()
            .Should()
            .ContainSingle()
            .Which
            .Hint
            .Should().Be(skills);
    }
}