using Totvs.Ats.Domain.Entities.Applicants;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Totvs.Ats.Domain.UnitTests;

public class DependencyInjectionTests
{
    private readonly IServiceCollection _services = new ServiceCollection();

    [Fact]
    public void WHEN_not_registering_validators_THEN_get_an_error()
    {
        var provider = _services.BuildServiceProvider();

        FluentActions.Invoking(() =>
                provider.GetRequiredService<IValidator<Applicant>>())
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Fact]
    public void WHEN_registering_validators_THEN_can_get_the_service()
    {
        _services.AddDomain();
        var provider = _services.BuildServiceProvider();

        var resultService = provider.GetRequiredService<IValidator<Applicant>>();

        resultService
            .Should()
            .BeOfType<ApplicantValidator>();
    }
}