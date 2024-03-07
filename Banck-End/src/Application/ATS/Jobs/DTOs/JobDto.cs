using Totvs.Ats.Application.ATS.Applicants.DTOs;
using Totvs.Ats.Domain.Entities.Applicants;
using Totvs.Ats.Domain.Entities.Jobs.Enums;
using Totvs.Ats.Domain.Entities.Jobs.ValueObjects;

namespace Totvs.Ats.Application.ATS.Jobs.DTOs;

public readonly record struct JobDto
(
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
        DateTime ApplyDate,
        DateTime? ModifiedOn,
        ICollection<ApplicantDto>? Applicants
);
