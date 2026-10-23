using Totvs.Ats.Application.Common.Interfaces;
using Totvs.Ats.Application.Common.Mappers;
using Totvs.Ats.Domain.Common.Validators;
using Totvs.Ats.Domain.Entities.Jobs;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using System.Diagnostics.Metrics;
using Totvs.Ats.Domain.Entities.Jobs.Enums;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.AddJob;

internal class AddJobCommandHandler : IRequestHandler<AddJobCommand, Result<JobDto>>
{
    private readonly ILogger _logger;
    private readonly IJobRepository _jobRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IJobClient _jobClient;
    private readonly IValidator<Job> _validator;

    public AddJobCommandHandler(
        IJobRepository jobRepository,
        IDateTimeService dateTimeService,
        IJobClient jobClient,
        ILogger<AddJobCommandHandler> logger,
        IValidator<Job> validator)
    {
        _jobRepository = jobRepository;
        _dateTimeService = dateTimeService;
        _jobClient = jobClient;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<JobDto>> Handle(AddJobCommand request, CancellationToken cancellationToken)
    {
        var jobToAdd = Job.Create(
              id: request.Id,
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
              postDate: request.PostDate,
              salaryFrom: request.SalaryFrom,
              salaryTo: request.SalaryTo,
              applicants: null,
            createdOn: _dateTimeService.Now,
            createdBy: request.UserId);

        var validationResult = await _validator.ValidateAsync(jobToAdd, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogInformation("job requested to add is invalid. Errors: {Errors}", validationResult.Errors);
            var errors = validationResult.Errors.MapToValidationErrors().MapToFluentErrors();
            return Result.Fail(errors);
        }

        var job = await _jobRepository.Create(jobToAdd);

        try
        {
            await _jobClient.UpdateJob(job.Id.Value, 1);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating the stock for the job {Id}", job.Id.Value);
        }

        return job.Adapt<JobDto>();
    }
}
