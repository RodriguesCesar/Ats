using Totvs.Ats.Application.Common;
using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;
using Totvs.Ats.Application.UnitTests.Applicants;
using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateProduct; 

namespace Totvs.Ats.Application.UnitTests.Applicants.Commands;

public class AddApplicantTests : ApplicantTestBase
{
    private readonly Mock<IDateTimeService> _dateTimeService = new();
    private readonly Mock<ILogger<AddApplicantCommandHandler>> _logger = new();
    private readonly Mock<IApplicantClient> _applicantClient = new();
    private readonly Mock<IValidator<Applicant>> _validator = new();
    private readonly AddApplicantCommandHandler _sut;

    public AddApplicantTests()
    {
        _sut = new AddApplicantCommandHandler
        (
            ApplicantRepository.Object,
            _dateTimeService.Object,
            _applicantClient.Object,
            _logger.Object,
            _validator.Object
        );
    }

    [Fact]
    public async Task WHEN_no_fields_are_filled_THEN_result_should_contain_the_error_with_message_and_status_invalid()
    {
        var applicant = ApplicantBuilder.GetApplicantEmpty();
        var command = applicant.Adapt<AddApplicantCommand>();
        MockSetup.SetupValidationErrorResponse(_validator);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("error");
        result.Errors.First().Metadata["Status"].Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public async Task WHEN_only_FirstName_is_filled_THEN_result_should_contain_the_error()
    {
        var applicant = ApplicantBuilder.GetApplicantWithFirstName();
        var command = applicant.Adapt<AddApplicantCommand>();
        MockSetup.SetupValidationErrorResponse(_validator);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("error");
        result.Errors.First().Metadata["Status"].Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_applicant_is_created()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var command = applicant.Adapt<AddApplicantCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryCreateValidResponse(ApplicantRepository, applicant);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.FirstName.Should().Be(command.FirstName);
        result.Value.LastName.Should().Be(command.LastName);
    }

    [Fact]
    public async Task WHEN_adding_applicant_without_ApplicantClient_THEN_error_is_logged()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var command = applicant.Adapt<AddApplicantCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryCreateValidResponse(ApplicantRepository, applicant);
        MockSetup.SetupApplicantClientErrorResponse(_applicantClient);

        var result = await _sut.Handle(command, CancellationToken.None);

        _logger.Verify(l => l.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((value, _) =>
                value
                    .ToString()!
                    .Contains($"Error updating the stock for the applicant {result.Value.Id}")),
            It.IsAny<HttpRequestException>(),
            ((Func<It.IsAnyType, Exception, string>)It.IsAny<object>())!),
            Times.Once);
    }
}