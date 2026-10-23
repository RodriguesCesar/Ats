using FluentResults;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Domain.Entities.Jobs.Enums;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.AddJob;

public readonly record struct AddJobApplicantCommand(
         Guid ApplicantId,
         Guid JobId)
    : IRequest<Result<JobApplicantDto>>;