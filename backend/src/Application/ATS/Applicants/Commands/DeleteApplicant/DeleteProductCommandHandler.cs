using Totvs.Ats.Application.Common;
using FluentResults;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;

namespace Totvs.Ats.Application.ATS.Applicants.Commands.DeleteApplicant;

public class DeleteApplicantCommandHandler : IRequestHandler<DeleteApplicantCommand, Result>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILogger _logger;

    public DeleteApplicantCommandHandler(
        IApplicantRepository applicantRepository,
        ILogger<DeleteApplicantCommandHandler> logger)
    {
        _applicantRepository = applicantRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteApplicantCommand request, CancellationToken cancellationToken)
    {
        var applicantToDelete = await _applicantRepository.GetById(request.Id);

        if (applicantToDelete is null || applicantToDelete.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Id not found.") { Metadata = { { "Status", ResultStatus.NotFound } } });

        await _applicantRepository.Delete(applicantToDelete);

        _logger.LogInformation("Product {applicantId} deleted.", request.Id);
        return Result.Ok();
    }
}