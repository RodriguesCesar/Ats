using FluentResults;
using Totvs.Ats.Application.ATS.Applicants.DTOs;

namespace Totvs.Ats.Application.ATS.Applicants.Commands.UpdateApplicant;

public readonly record struct UpdateApplicantCommand(
         Guid Id,
         string  FirstName,
         string  LastName,
         string? Summary,
         string? ProfilePhoto,
         string  Email,
         string? Phone,
         string? Address,
         string? Skills,
         DateTimeOffset ModifiedOn,
        Guid UserId)
    : IRequest<Result<ApplicantDto>>;