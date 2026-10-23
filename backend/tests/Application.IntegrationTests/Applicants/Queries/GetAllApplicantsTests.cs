using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Application.ATS.Applicants.Queries.GetApplicant;
using FluentAssertions;
using Xunit;
using Totvs.Ats.Application.ATS.Applicants.Queries.GetAllApplicants;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;
using Totvs.Ats.Domain.Entities.Applicants;

namespace Totvs.Ats.Application.IntegrationTests.Applicants.Queries;

public class GetAllApplicantsTests : TestBase
{
    public GetAllApplicantsTests(Testing testing)
        : base(testing)
    {
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task WHEN_existing_applicants_on_repository_THEN_response_not_is_empty(int applicantsToAdd)
    {
        var currentApplicants = (int)await CountApplicantAsync();
        List<Guid> Ids = new List<Guid>();
        for (var i = 0; i < applicantsToAdd; i++)
        {
            var applicant = ApplicantBuilder.GetApplicant();
            await AddAsync(applicant);
            Ids.Add(applicant.Id.Value);
        }

        var result = await SendAsync(new GetAllApplicantsQuery());

        result.Should().NotBeEmpty();
        result.Should().HaveCount(currentApplicants + applicantsToAdd);

        foreach (var id in Ids)
        {
            var deleteApplicantCommand = new DeleteApplicantCommand(id);
            await SendAsync(deleteApplicantCommand);
        }
    }
}