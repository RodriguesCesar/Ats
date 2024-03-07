using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;

namespace Totvs.Ats.Application.UnitTests.Applicants.ExposedHandlers;

public class DeleteApplicantCommandHandlerExposed : DeleteApplicantCommandHandler
{
    public DeleteApplicantCommandHandlerExposed(
        IApplicantRepository applicantRepository,
        ILogger<DeleteApplicantCommandHandlerExposed> logger)
        : base(
            applicantRepository,
            logger)
    {
    }

    public async Task ExposedHandle(DeleteApplicantCommand request, CancellationToken cancellationToken)
    {
        await base.Handle(request, cancellationToken);
    }
}