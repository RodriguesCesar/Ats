using FluentResults;
using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.ATS.Applicants.Commands.AddApplicant;

public readonly record struct AddApplicantCommand(
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
        Guid UserId)
    : IRequest<Result<ApplicantDto>>;