using Totvs.Ats.Application.ATS.Applicants.Queries.GetAllApplicants;
using Totvs.Ats.Application.UnitTests.Applicants;

namespace Totvs.Ats.Application.UnitTests.Applicants.Queries;

public class GetAllApplicantsTests : ApplicantTestBase
{
    private readonly GetAllApplicantsQueryHandler _sut;
    public GetAllApplicantsTests()
    {
        _sut = new GetAllApplicantsQueryHandler
        (
            ApplicantRepository.Object
        );
    }

    [Fact]
    public async Task WHEN_not_existing_applicants_on_repository_THEN_response_is_empty()
    {
        MockSetup.SetupRepositoryGetAllEmptyResponse(ApplicantRepository);

        var result = await _sut.Handle(new GetAllApplicantsQuery(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task WHEN_existing_applicants_on_repository_THEN_response_not_is_empty()
    {
        MockSetup.SetupRepositoryGetAllValidResponse(ApplicantRepository);

        var result = await _sut.Handle(new GetAllApplicantsQuery(), CancellationToken.None);

        result.Should().NotBeEmpty();
    }
}