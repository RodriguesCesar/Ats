using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Application.Common.Mappers;
using Totvs.Ats.Domain.Common.Validators;
using Totvs.Ats.Domain.Entities.Applicants;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;

public class AddApplicantCommandHandler : IRequestHandler<AddApplicantCommand, Result<ApplicantDto>>
{
    private readonly ILogger _logger;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IApplicantClient _applicantClient;
    private readonly IValidator<Applicant> _validator;

    public AddApplicantCommandHandler(
        IApplicantRepository applicantRepository,
        IDateTimeService dateTimeService,
        IApplicantClient applicantClient,
        ILogger<AddApplicantCommandHandler> logger,
        IValidator<Applicant> validator)
    {
        _applicantRepository = applicantRepository;
        _dateTimeService = dateTimeService;
        _applicantClient = applicantClient;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<ApplicantDto>> Handle(AddApplicantCommand request, CancellationToken cancellationToken)
    {
        var applicantToAdd = Applicant.Create(
            id: request.Id,
            firstName: request.FirstName,
            email: request.Email,
            lastName: request.LastName,
            applyDate: request.ApplyDate,
            address: request.Address,
            phone: request.Phone,
            profilePhoto: request.ProfilePhoto,
            skills: request.Skills,
            summary: request.Summary,
            createdOn: _dateTimeService.Now,
            createdBy: request.UserId);

        var validationResult = await _validator.ValidateAsync(applicantToAdd, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("applicant requested to add is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

        var applicant = await _applicantRepository.Create(applicantToAdd);

        try
        {
            await _applicantClient.UpdateApplicant(applicant.Id.Value, 1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating the stock for the applicant {Id}", applicant.Id.Value);
        }

        return applicant.Adapt<ApplicantDto>();
    }
}