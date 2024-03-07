using Totvs.Ats.Application.CommonTests.Builders;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentValidation;
using FluentValidation.Results;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.Common.Interfaces.Repository;

namespace Totvs.Ats.Application.UnitTests.Applicants;

public class MockApplicantSetup
{
    internal void SetupValidationErrorResponse(Mock<IValidator<Applicant>> validator)
    {
        validator
            .Setup
            (
                mockValidator => mockValidator.ValidateAsync(
                    It.IsAny<Applicant>(),
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync
            (
                new ValidationResult { Errors = { new ValidationFailure("Error", "error", 0) }}
            );
    }

    internal void SetupValidationValidResponse(Mock<IValidator<Applicant>> validator)
    {
        validator
            .Setup
            (
                mockValidator => mockValidator.ValidateAsync(
                    It.IsAny<Applicant>(),
                    It.IsAny<CancellationToken>())
            ).ReturnsAsync
            (
                new ValidationResult()
            );
    }

    internal void SetupRepositoryCreateValidResponse(Mock<IApplicantRepository> applicantRepository, Applicant applicant)
    {
        applicantRepository
            .Setup(repo => repo.Create(It.IsAny<Applicant>()))
            .ReturnsAsync(applicant);
    }

    public void SetupRepositoryUpdateErrorResponse(Mock<IApplicantRepository> applicantRepository)
    {
        applicantRepository
            .Setup(repo => repo.Update(It.IsAny<Applicant>()))
            .ThrowsAsync(new Exception());
    }

    public void SetupRepositoryDeleteErrorResponse(Mock<IApplicantRepository> applicantRepository)
    {
        applicantRepository
            .Setup(repo => repo.Delete(It.IsAny<Applicant>()))
            .ThrowsAsync(new Exception() );
    }

    internal void SetupRepositoryGetByIdValidResponse(Mock<IApplicantRepository> applicantRepository, Applicant applicant)
    {
        applicantRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(applicant);
    }

    internal void SetupRepositoryGetByIdNullResponse(Mock<IApplicantRepository> applicantRepository)
    {
        applicantRepository
            .Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Applicant?)null);
    }

    internal void SetupRepositoryGetAllEmptyResponse(Mock<IApplicantRepository> applicantRepository)
    {
        applicantRepository
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(new List<Applicant>());
    }

    internal void SetupRepositoryGetAllValidResponse(Mock<IApplicantRepository> applicantRepository)
    {
        applicantRepository
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(new List<Applicant> { ApplicantBuilder.GetApplicant() });
    }

    internal void SetupApplicantClientErrorResponse(Mock<IApplicantClient> stockClient)
    {
        stockClient
            .Setup(repo => repo.UpdateApplicant(It.IsAny<Guid>(), It.IsAny<int>()))
            .ThrowsAsync(new HttpRequestException());
    }
}