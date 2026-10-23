using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;

namespace Totvs.Ats.Application.ATS.Applicants.DTOs;

public readonly record struct ApplicantDto
(
        Guid Id,
        string FirstName,
        string LastName,
        string? Summary,
        string? ProfilePhoto,
        string Email,
        string? Phone,
        string? Address,
        string? Skills,
        DateTime ApplyDate,
        DateTime? ModifiedOn
);