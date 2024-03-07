using Totvs.Ats.Api.Telemetry;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totvs.Ats.Application.ATS.Jobs.Queries.GetAllJobs;
using Totvs.Ats.Application.ATS.Jobs.Queries.GetJob;
using Totvs.Ats.Application.ATS.Jobs.Commands.AddJob;
using Totvs.Ats.Application.ATS.Jobs.Commands.UpdateJob;
using Totvs.Ats.Application.ATS.Jobs.Commands.DeleteJob;
using Totvs.Ats.Totvs.Ats.Api.Contracts.Requests.Job;
using Totvs.Ats.Totvs.Ats.Api.Contracts.Responses.Job;
using Totvs.Ats.Application;
using Totvs.Ats.Domain.Entities.Jobs.Enums;
using FluentResults;

namespace Totvs.Ats.Api.Endpoints;

internal class JobEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/jobs", GetAllJobs)
            .WithName("GetAllJobs");

        app.MapGet("/jobs/{id:guid}", GetJob)
            .WithName("GetJob");

        app.MapPost("/jobs/add-applicant", AddJobApplicant)
            .WithName("AddJobApplicant");

        app.MapPost("/jobs", AddJob)
         .WithName("AddJob");

        app.MapPut("/jobs", UpdateJob)
            .WithName("UpdateJob");

        app.MapDelete("/jobs/{id:guid}", DeleteJob)
            .WithName("DeleteJob");
    }

    private static async Task<IResult> GetAllJobs(IMediator mediator, ILogger<JobEndpoints> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Jobs. (Just an example on how to log inside a 'controller' if you need it)");
        var jobs = await mediator.Send(new GetAllJobsQuery(), cancellationToken);
        return Results.Ok(jobs.Adapt<List<JobListItemResponse>>());
    }

    private static async Task<IResult> GetJob(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var job = await mediator.Send(new GetJobQuery(id), cancellationToken);

        return
            job is not null
                ? Results.Ok(job.Adapt<JobResponse>())
                : Results.NotFound();
    }

    private static async Task<IResult> AddJob([FromBody] AddJobRequest request, IMediator mediator, ILogger<JobEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new AddJobCommand(
         Id: Guid.NewGuid(),
              Title: request.Title,
              Description: request.Description,
              Country: request.Country,
              City: request.City,
              ContactPhone: request.ContactPhone,
              ContactEmail: request.ContactEmail,
              Manager: request.Manager,
              JobType: request.JobType.ParseEnum<JobType>(),
              JobExperience: request.JobExperience.ParseEnum<JobExperience>(),
              RequiredSkills: request.RequiredSkills,
              PostDate: DateTime.Now,
              SalaryFrom: request.SalaryFrom,
              SalaryTo: request.SalaryTo,
            UserId: Guid.NewGuid());  // TODO replace with proper user identity

        var addJobResult = await mediator.Send(command, cancellationToken);

        return addJobResult.ToHttpResult(
            mapping: obj => obj.Adapt<JobResponse>(),
            actionWhenSuccess: ApplicationJobMetrics.NewJobAdded);
    }

    private static async Task<IResult> AddJobApplicant([FromBody] AddApplicantJobRequest request, IMediator mediator, ILogger<JobEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new AddJobApplicantCommand(JobId: request.JobId, ApplicantId: request.ApplicantId);

        var addJobApplicant = await mediator.Send(command, cancellationToken);

        return addJobApplicant.ToHttpResult(mapping: obj => obj.Adapt<AddApplicantJobResponse>());
    }

    private static async Task<IResult> UpdateJob([FromBody] UpdateJobRequest request, IMediator mediator, ILogger<JobEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new UpdateJobCommand(
            Id: request.Id,
              Title: request.Title,
              Description: request.Description,
              Country: request.Country,
              City: request.City,
              ContactPhone: request.ContactPhone,
              ContactEmail: request.ContactEmail,
              Manager: request.Manager,
              JobType: request.JobType.ParseEnum<JobType>(),
              JobExperience: request.JobExperience.ParseEnum<JobExperience>(),
              RequiredSkills: request.RequiredSkills,
              SalaryFrom: request.SalaryFrom,
              SalaryTo: request.SalaryTo,
            ModifiedOn: DateTime.Now,
            UserId: Guid.NewGuid());  // TODO replace with proper user identity

        var updateJobResult = await mediator.Send(command, cancellationToken);

        return updateJobResult.ToHttpResult(mapping: obj => obj.Adapt<JobResponse>());
    }

    private static async Task<IResult> DeleteJob(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteJobCommand(id);
        var deleteJobResult = await mediator.Send(command, cancellationToken);
        return deleteJobResult.ToHttpResult();
    }
}

