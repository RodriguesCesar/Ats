using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;
using FluentAssertions;
using Xunit;

namespace Totvs.Ats.Application.IntegrationTests.Applicants.Commands;

public class DeleteApplicantTests : TestBase
{
    public DeleteApplicantTests(Testing testing)
        : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_Returns_error_result()
    {
        var command = new DeleteApplicantCommand(Guid.Empty);

        var applicantDeleteResult = await SendAsync(command);

        applicantDeleteResult.IsSuccess.Should().BeFalse();
        applicantDeleteResult.Errors.First().Message.Should().Be("Id not found.");

        var deleteApplicantCommand = new DeleteApplicantCommand(command.Id);
        await SendAsync(deleteApplicantCommand);
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_applicant_is_deleted()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        await AddAsync(applicant);

        var deleteApplicantCommand = new DeleteApplicantCommand(applicant.Id.Value);
        await SendAsync(deleteApplicantCommand);

        var result = await GetApplicantById(applicant.Id.Value);

        result.Should().BeNull();
    }
}