using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentValidation.TestHelper;
using Xunit;

namespace Totvs.Ats.Domain.UnitTests.Entities.Applicants;

public class ApplicantValidatorTests
{
    private readonly ApplicantValidator _validator;

    public ApplicantValidatorTests()
    {
        _validator = new ApplicantValidator();
    }

    [Fact]
    public void WHEN_Id_is_empty_THEN_gives_an_error()
    {
        var sut = ApplicantBuilder.GetApplicantWithId(Guid.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(person => person.Id.Value);
    }

    [Fact]
    public void WHEN_FirstName_is_empty_THEN_gives_an_error()
    {
        var sut = ApplicantBuilder.GetApplicantWithFirstName(string.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(applicant => applicant.FirstName);
    }

    [Fact]
    public void WHEN_FirstName_is_specified_THEN_does_not_give_an_error()
    {
        var sut = ApplicantBuilder.GetApplicantWithFirstName("Cesar");
        var result = _validator.TestValidate(sut);

        result
            .ShouldNotHaveValidationErrorFor(applicant => applicant.FirstName);
    }

    [Fact]
    public void WHEN_Email_is_empty_THEN_gives_an_error()
    {
        var sut = ApplicantBuilder.GetApplicantWithEmail(string.Empty);
        var result = _validator.TestValidate(sut);

        result
            .ShouldHaveValidationErrorFor(applicant => applicant.Email);
    }

}