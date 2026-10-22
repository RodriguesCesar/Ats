using Totvs.Ats.Application.Common;
using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Application.Common.Mappers;
using Totvs.Ats.Domain.Common.Validators;
using Totvs.Ats.Domain.Entities.Jobs;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Application.ATS.Jobs.Commands.UpdateJob;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.UpdateProduct;

internal class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Result<JobDto>>
{
    private readonly IJobRepository _jobRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger _logger;
    private readonly IValidator<Job> _validator;

    public UpdateJobCommandHandler(
        IJobRepository jobRepository,
        IDateTimeService dateTimeService,
        ILogger<UpdateJobCommandHandler> logger,
        IValidator<Job> validator)
    {
        _jobRepository = jobRepository;
        _dateTimeService = dateTimeService;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<JobDto>> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var jobToUpdate = await _jobRepository.GetById(request.Id);

        if (jobToUpdate is null || jobToUpdate.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Id not found.") { Metadata = { { "Status", ResultStatus.NotFound } } });

        jobToUpdate.Update(
              title: request.Title,
              description: request.Description,
              country: request.Country,
              city: request.City,
              contactPhone: request.ContactPhone,
              contactEmail: request.ContactEmail,
              manager: request.Manager,
              jobType: request.JobType,
              jobExperience: request.JobExperience,
              requiredSkills: request.RequiredSkills,
              salaryFrom: request.SalaryFrom,
              salaryTo: request.SalaryTo,
            modifiedOn: _dateTimeService.Now,
            modifiedBy: request.UserId);

        var validationResult = await _validator.ValidateAsync(jobToUpdate, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("Job  requested to update is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

        try
        {
            await _jobRepository.Update(jobToUpdate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Problem updating the  Job {Id} {Title}", request.Id, request.Title);
        }

        return Result.Ok(jobToUpdate.Adapt<JobDto>());
    }
}