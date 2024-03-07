using Totvs.Ats.Application.Common.Interfaces;
using FluentResults;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.Common.Interfaces.Client;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Application.Common;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.AddJob;

internal class AddJobApplicantCommandHandler : IRequestHandler<AddJobApplicantCommand, Result<JobApplicantDto>>
{
    private readonly ILogger _logger;
    private readonly IJobRepository _jobRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly IDateTimeService _dateTimeService;
    private readonly IJobClient _jobClient;


    public AddJobApplicantCommandHandler(
        IJobRepository jobRepository,
        IApplicantRepository applicantRepository,
        IDateTimeService dateTimeService,
        IJobClient jobClient,
        ILogger<AddJobApplicantCommandHandler> logger)
    {
        _jobRepository = jobRepository;
        _dateTimeService = dateTimeService;
        _jobClient = jobClient;
        _logger = logger;
        _applicantRepository = applicantRepository;

    }

    public async Task<Result<JobApplicantDto>> Handle(AddJobApplicantCommand request, CancellationToken cancellationToken)
    {
        var jobToUpdate = await _jobRepository.GetById(request.JobId);
        var addApplicant = await _applicantRepository.GetById(request.ApplicantId);

        if (jobToUpdate is null || jobToUpdate.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Job not found.") { Metadata = { { "Status", ResultStatus.NotFound } } });

        if (addApplicant is null || addApplicant.Id.Value == Guid.Empty)
            return Result.Fail(new Error("applicant not found.") { Metadata = { { "Status", ResultStatus.NotFound } } });

        jobToUpdate.AddApplicant(addApplicant);
        await _jobRepository.Update(jobToUpdate);

        return request.Adapt<JobApplicantDto>();
    }
}