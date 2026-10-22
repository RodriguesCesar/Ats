using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Entities.Applicants;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;
using FluentAssertions;
using Mapster;
using Xunit;
using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateApplicant;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;

namespace Totvs.Ats.Application.IntegrationTests.Applicants.Commands;

public class UpdateApplicantTests : TestBase
{
    public UpdateApplicantTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_updating_applicant_that_does_not_exist_on_repository_THEN_does_not_return_applicant()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var command = applicant.Adapt<UpdateApplicantCommand>();

        var applicantUpdatedResult = await SendAsync(command);

        applicantUpdatedResult.IsSuccess.Should().BeFalse();
        applicantUpdatedResult.Errors.First().Message.Should().Be("Id not found.");
        //TODO: Add more tests or asserts
    }


    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_applicant_is_updated()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var command = applicant.Adapt<UpdateApplicantCommand>();

        await AddAsync(applicant);

        var applicantUpdated = await SendAsync(command);

        applicantUpdated.Should().NotBeNull();
        var applicantUpdatedValue = applicantUpdated!.Value;
        var result = await GetApplicantById(applicantUpdated!.Value.Id);

        result.Should().NotBeNull();
        result!.FirstName.Should().Be(applicantUpdatedValue.FirstName);
        result.FirstName.Should().Be(applicantUpdatedValue.FirstName);

        var deleteApplicantCommand = new DeleteApplicantCommand(command.Id);
        await SendAsync(deleteApplicantCommand);
    }

    [Fact]
    public async Task WHEN_few_fields_are_filled_THEN_returns_validation_error()
    {
        var applicant = ApplicantBuilder.GetApplicant();

        await AddAsync(applicant);

        var updatedApplicant = new Applicant(applicant.Id, string.Empty, string.Empty, string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, applicant.ApplyDate, applicant.CreatedOn,
            applicant.CreatedBy, applicant.LastModifiedOn, applicant.LastModifiedBy);
        var command = updatedApplicant.Adapt<UpdateApplicantCommand>();
        var applicantUpdatedResult = await SendAsync(command);

        applicantUpdatedResult.Should().NotBeNull();
        applicantUpdatedResult!.IsSuccess.Should().BeFalse();
        applicantUpdatedResult!.Errors.Should().NotBeEmpty();

        var deleteApplicantCommand = new DeleteApplicantCommand(applicant.Id.Value);
        await SendAsync(deleteApplicantCommand);

        //TODO: Add more test cases or assertions
    }

}