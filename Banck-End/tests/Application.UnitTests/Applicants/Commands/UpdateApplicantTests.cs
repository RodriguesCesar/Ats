using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateApplicant;
using Totvs.Ats.Application.UnitTests.Applicants;
using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateProduct;

namespace Totvs.Ats.Application.UnitTests.Applicants.Commands;

public class UpdateApplicantTests : ApplicantTestBase
{
    private readonly Mock<IDateTimeService> _dateTimeService = new();
    private readonly Mock<ILogger<UpdateApplicantCommandHandler>> _logger = new();
    private readonly Mock<IValidator<Applicant>> _validator = new();
    private readonly UpdateApplicantCommandHandler _sut;

    public UpdateApplicantTests()
    {
        _sut = new UpdateApplicantCommandHandler
        (
            ApplicantRepository.Object,
            _dateTimeService.Object,
            _logger.Object,
            _validator.Object
        );
    }

    [Fact]
    public async Task WHEN_few_fields_are_filled_THEN_throws_validation_exception()
    {
        var applicant = ApplicantBuilder.GetApplicantWithEmail();
        var command = applicant.Adapt<UpdateApplicantCommand>();
        MockSetup.SetupValidationErrorResponse(_validator);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("Id not found.");

    }

    [Fact]
    public async Task WHEN_all_fields_are_filled_THEN_applicant_is_updated()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var command = applicant.Adapt<UpdateApplicantCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdValidResponse(ApplicantRepository, applicant);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        var applicantDto = result!.Value;

        applicantDto.FirstName.Should().Be(command.FirstName);
        applicantDto.LastName.Should().Be(command.LastName);
    }

    [Fact]
    public async Task WHEN_applicant_is_not_found_THEN_applicant_is_not_returned()
    {
        var applicant = ApplicantBuilder.GetApplicantEmpty();
        var command = applicant.Adapt<UpdateApplicantCommand>();
        MockSetup.SetupValidationValidResponse(_validator);
        MockSetup.SetupRepositoryGetByIdNullResponse(ApplicantRepository);

        var result = await _sut.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
    }


}