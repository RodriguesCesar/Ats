using FluentResults;
using Totvs.Ats.Application.ATS.Jobs.DTOs;
using Totvs.Ats.Domain.Entities.Jobs.Enums;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.AddJob;

public readonly record struct AddJobCommand(
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
         DateTime PostDate,
         float SalaryFrom,
         float SalaryTo,
        Guid UserId)
    : IRequest<Result<JobDto>>;