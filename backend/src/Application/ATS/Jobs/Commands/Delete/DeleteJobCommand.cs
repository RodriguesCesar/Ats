using FluentResults;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.DeleteJob;

public readonly record struct DeleteJobCommand(Guid Id) : IRequest<Result>;