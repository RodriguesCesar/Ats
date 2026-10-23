using Totvs.Ats.Application.Common;
using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Application.Common.Mappers;
using Totvs.Ats.Domain.Common.Validators;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.ATS.Applicants.DTOs;
using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateApplicant;

namespace Totvs.Ats.Application.ATS.Applicants.Commands.UpdateProduct;

public class UpdateApplicantCommandHandler : IRequestHandler<UpdateApplicantCommand, Result<ApplicantDto>>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger _logger;
    private readonly IValidator<Applicant> _validator;

    public UpdateApplicantCommandHandler(
        IApplicantRepository applicantRepository,
        IDateTimeService dateTimeService,
        ILogger<UpdateApplicantCommandHandler> logger,
        IValidator<Applicant> validator)
    {
        _applicantRepository = applicantRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<ApplicantDto>> Handle(UpdateApplicantCommand request, CancellationToken cancellationToken)
    {
        var applicantToUpdate = await _applicantRepository.GetById(request.Id);

        if (applicantToUpdate is null || applicantToUpdate.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Id not found.") { Metadata = { { "Status", ResultStatus.NotFound } } });

        applicantToUpdate.Update(
            firstName: request.FirstName,
            email: request.Email,
            lastName: request.LastName,
            address: request.Address,
            phone: request.Phone,
            profilePhoto: request.ProfilePhoto,
            skills: request.Skills,
            summary: request.Summary,
            modifiedOn: _dateTimeService.Now,
            modifiedBy: request.UserId);

        var validationResult = await _validator.ValidateAsync(applicantToUpdate, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("Applicant  requested to update is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

        try
        {
            await _applicantRepository.Update(applicantToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Problem updating the  Applicant {FirstName} {LastName}", request.FirstName, request.LastName);
        }

        return Result.Ok(applicantToUpdate.Adapt<ApplicantDto>());
    }
}