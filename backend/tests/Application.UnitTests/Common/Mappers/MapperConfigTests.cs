using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Entities.Applicants;
using Mapster;
using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.UnitTests.Common.Mappers;

public class MapperConfigTests: IClassFixture<MapperConfigSetup>
{
    private const string FirstName = "Cesar";
    private static readonly Guid TestGuid = Guid.NewGuid();


    [Fact]
    public void WHEN_FirstName_is_specified_on_Applicant_THEN_ApplicantDto_should_have_FirstName_specified()
    {
        var source = ApplicantBuilder.GetApplicantWithFirstName(FirstName);
        var result = source.Adapt<ApplicantDto>();
        result.FirstName.Should().Be(FirstName);
    }

    [Fact]
    public void WHEN_FirstName_is_specified_on_AddApplicantCommand_THEN_Applicant_should_have_FirstName_specified()
    {
        var source = AddApplicantCommandBuilder.GetAddApplicantCommandWithFirstName(FirstName);
        var result = source.Adapt<Applicant>();
        result.FirstName.Should().Be(FirstName);
    }

    [Fact]
    public void WHEN_FirstName_is_specified_on_UpdateApplicantCommand_THEN_Applicant_should_have_FirstName_specified()
    {
        var source = UpdateApplicantCommandBuilder.GetUpdateApplicantCommandWithFirstName(FirstName);
        var result = source.Adapt<Applicant>();
        result.FirstName.Should().Be(FirstName);
    }

    [Fact]
    public void WHEN_Id_is_specified_on_Applicant_THEN_ApplicantDto_should_have_Id_specified()
    {
        var source = ApplicantBuilder.GetApplicantWithId(TestGuid);
        var result = source.Adapt<ApplicantDto>();
        result.Id.Should().Be(TestGuid);
    }
}