using Totvs.Ats.Application.CommonTests.Builders;
using FluentAssertions;
using Mapster;
using Xunit;
using Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;
using Totvs.Ats.Domain.Entities.Applicants;

namespace Totvs.Ats.Application.IntegrationTests.Applicants.Commands;

public class AddApplicantTests : TestBase
{
    public AddApplicantTests(Testing testing)
        : base(testing)
    {

    }

    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_result_should_contain_the_error()
    {
        var command = ApplicantBuilder.GetApplicantEmpty().Adapt<AddApplicantCommand>();

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("'Id Value' must not be empty.");

        var deleteApplicantCommand = new DeleteApplicantCommand(command.Id);
        await SendAsync(deleteApplicantCommand);

    }

    [Fact]
    public async Task WHEN_only_FirstName_is_filled_THEN_result_should_contain_the_error()
    {
        var command = ApplicantBuilder.GetApplicantWithFirstName().Adapt<AddApplicantCommand>();

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("'Id Value' must not be empty.");

        var deleteApplicantCommand = new DeleteApplicantCommand(command.Id);
        await SendAsync(deleteApplicantCommand);

    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_Applicant_is_created()
    {
        var command = ApplicantBuilder.GetApplicant().Adapt<AddApplicantCommand>();

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.FirstName.Should().Be(command.FirstName);
        result.Value.LastName.Should().Be(command.LastName);

        var deleteApplicantCommand = new DeleteApplicantCommand(command.Id);
        await SendAsync(deleteApplicantCommand);
    }
}