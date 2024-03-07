using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Application.ATS.Applicants.Queries.GetApplicant;
using FluentAssertions;
using Xunit;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;

namespace Totvs.Ats.Application.IntegrationTests.Applicants.Queries;

public class GetApplicantTests : TestBase
{
    public GetApplicantTests(Testing testing)
    : base(testing)
    {
    }

    [Fact]
    public async Task WHEN_providing_valid_id_THEN_applicant_is_found()
    {
        var applicant = ApplicantBuilder.GetApplicant();
        var request = new GetApplicantQuery(applicant.Id.Value);
        await AddAsync(applicant);

        var result = await SendAsync(request);
        var applicantDto = result!.Value;

        applicantDto.Id.Should().Be(request.ApplicantId);

        var deleteApplicantCommand = new DeleteApplicantCommand(applicant.Id.Value);
        await SendAsync(deleteApplicantCommand);
    }
}