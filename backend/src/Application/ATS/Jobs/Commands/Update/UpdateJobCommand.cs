using FluentResults;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Domain.Entities.Jobs.Enums;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.UpdateJob;

public readonly record struct UpdateJobCommand(
         Guid Id,
         string? Title,
         string? Description,
         string? Country,
         string? City,
         string? ContactPhone,
         string? ContactEmail,
         string? Manager,
         JobType JobType,
         JobExperience JobExperience,
         string? RequiredSkills,
         float SalaryFrom,
         float SalaryTo,
         DateTimeOffset ModifiedOn,
        Guid UserId)
    : IRequest<Result<JobDto>>;