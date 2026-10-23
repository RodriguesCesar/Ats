using FluentResults;

namespace Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;

public readonly record struct DeleteApplicantCommand(Guid Id) : IRequest<Result>;