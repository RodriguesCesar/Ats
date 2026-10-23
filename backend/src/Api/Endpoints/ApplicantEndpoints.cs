using Totvs.Ats.Api.Telemetry;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Totvs.Ats.Application.ATS.Applicants.Queries.GetAllApplicants;
using Totvs.Ats.Application.ATS.Applicants.Queries.GetApplicant;
using Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;
using Totvs.Ats.Application.ATS.Applicants.Commands.UpdateApplicant;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;
using Totvs.Ats.Totvs.Ats.Api.Contracts.Requests.Applicant;
using Totvs.Ats.Totvs.Ats.Api.Contracts.Responses.Applicant;

namespace Totvs.Ats.Api.Endpoints;

internal class ApplicantEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/applicants", GetAllApplicants)
            .WithName("GetAllApplicants");

        app.MapGet("/applicants/{id:guid}", GetApplicant)
            .WithName("GetApplicant");

        app.MapPost("/applicants", AddApplicant)
            .WithName("AddApplicant");

        app.MapPut("/applicants", UpdateApplicant)
            .WithName("UpdateApplicant");

        app.MapDelete("/applicants/{id:guid}", DeleteApplicant)
            .WithName("DeleteApplicant");
    }

    private static async Task<IResult> GetAllApplicants(IMediator mediator, ILogger<ApplicantEndpoints> logger, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all Applicants. (Just an example on how to log inside a 'controller' if you need it)");
        var applicants = await mediator.Send(new GetAllApplicantsQuery(), cancellationToken);
        return Results.Ok(applicants.Adapt<List<ApplicantListItemResponse>>());
    }

    private static async Task<IResult> GetApplicant(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var applicant = await mediator.Send(new GetApplicantQuery(id), cancellationToken);

        return
            applicant is not null
                ? Results.Ok(applicant.Adapt<ApplicantResponse>())
                : Results.NotFound();
    }

    private static async Task<IResult> AddApplicant([FromBody] AddApplicantRequest request, IMediator mediator, ILogger<ApplicantEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new AddApplicantCommand(
            Id: request.Id,
            FirstName: request.FirstName,
            LastName: request.LastName,
            Summary: request.Summary,
            Email: request.Email,
            Phone: request.Phone,
            Address: request.Address,
            Skills: request.Skills,
            ProfilePhoto: request.ProfilePhoto,
            ApplyDate: request.ApplyDate,
            UserId: Guid.NewGuid());  // TODO replace with proper user identity

        var addApplicantResult = await mediator.Send(command, cancellationToken);

        return addApplicantResult.ToHttpResult(
            mapping: obj => obj.Adapt<ApplicantResponse>(),
            actionWhenSuccess: ApplicationApplicantMetrics.NewApplicantAdded);
    }

    private static async Task<IResult> UpdateApplicant([FromBody] UpdateApplicantRequest request, IMediator mediator, ILogger<ApplicantEndpoints> logger, CancellationToken cancellationToken)
    {
        var command = new UpdateApplicantCommand(
            Id: request.Id,
            FirstName: request.FirstName,
            LastName: request.LastName,
            Summary: request.Summary,
            Email: request.Email,
            Phone: request.Phone,
            Address: request.Address,
            Skills: request.Skills,
            ProfilePhoto: request.ProfilePhoto,
            ModifiedOn: request.ModifiedOn ?? DateTime.Now,
            UserId: Guid.NewGuid());  // TODO replace with proper user identity

        var updateApplicantResult = await mediator.Send(command, cancellationToken);

        return updateApplicantResult.ToHttpResult(mapping: obj => obj.Adapt<ApplicantResponse>());
    }

    private static async Task<IResult> DeleteApplicant(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        var command = new DeleteApplicantCommand(id);
        var deleteApplicantResult = await mediator.Send(command, cancellationToken);
        return deleteApplicantResult.ToHttpResult();
    }
}