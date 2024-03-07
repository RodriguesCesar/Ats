using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;
using Totvs.Ats.Application.UnitTests.Applicants.ExposedHandlers;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.UnitTests.Applicants;

namespace Totvs.Ats.Application.UnitTests.Applicants.Commands;

public class DeleteApplicantTests : ApplicantTestBase
{
    private readonly Mock<ILogger<DeleteApplicantCommandHandlerExposed>> _logger = new();
    private readonly DeleteApplicantCommandHandlerExposed _sut;

    public DeleteApplicantTests()
    {
        _sut = new DeleteApplicantCommandHandlerExposed
        (
            ApplicantRepository.Object,
            _logger.Object
        );
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_throws_not_found_exception()
    {
        var command = new DeleteApplicantCommand(Guid.Empty);

        MockSetup.SetupRepositoryGetByIdValidResponse(ApplicantRepository, ApplicantBuilder.GetApplicantEmpty());

        var result =  _sut.ExposedHandle(command, new CancellationToken());

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_applicant_is_deleted()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var command = new DeleteApplicantCommand(applicant.Id.Value);

        MockSetup.SetupRepositoryGetByIdValidResponse(ApplicantRepository, applicant);

        await FluentActions
            .Invoking(() =>
                _sut.ExposedHandle(command, new CancellationToken()))
            .Should()
            .NotThrowAsync<KeyNotFoundException>();
    }

}