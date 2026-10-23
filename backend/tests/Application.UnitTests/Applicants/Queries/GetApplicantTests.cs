using Totvs.Ats.Application.ATS.Applicants.Queries.GetApplicant;
using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Application.UnitTests.Applicants;

namespace Totvs.Ats.Application.UnitTests.Applicants.Queries;

public class GetApplicantTests : ApplicantTestBase
{
    private readonly GetApplicantQueryHandler _sut;
    public GetApplicantTests()
    {
        _sut = new GetApplicantQueryHandler
        (
            ApplicantRepository.Object
        );
    }

    [Fact]
    public async Task WHEN_not_providing_valid_Id_THEN_applicant_is_not_found()
    {
        var applicant = ApplicantBuilder.GetApplicantEmpty();
        var request = new GetApplicantQuery(Guid.Empty);
        MockSetup.SetupRepositoryGetByIdValidResponse(ApplicantRepository, applicant);

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_applicant_is_found()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var request = new GetApplicantQuery(applicant.Id.Value);
        MockSetup.SetupRepositoryGetByIdValidResponse(ApplicantRepository, applicant);

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
    }
}