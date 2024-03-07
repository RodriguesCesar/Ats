using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.UnitTests;
using Totvs.Ats.Application.UnitTests.Applicants;


namespace Totvs.Ats.Application.UnitTests.Applicants;

public abstract class ApplicantTestBase : IClassFixture<MapperConfigSetup>
{
    protected readonly MockApplicantSetup MockSetup = new();
    protected readonly Mock<IApplicantRepository> ApplicantRepository = new();
}