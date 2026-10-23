using Totvs.Ats.Application.Common;
using FluentResults;
using Microsoft.Extensions.Logging;
using Totvs.Ats.Application.Common.Interfaces.Repository;

namespace Totvs.Ats.Application.ATS.Jobs.Commands.DeleteJob;

internal class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result>
{
    private readonly IJobRepository _jobRepository;
    private readonly ILogger _logger;

    public DeleteJobCommandHandler(
        IJobRepository jobRepository,
        ILogger<DeleteJobCommandHandler> logger)
    {
        _jobRepository = jobRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var jobToDelete = await _jobRepository.GetById(request.Id);

        if (jobToDelete is null || jobToDelete.Id.Value == Guid.Empty)
            return Result.Fail(new Error("Id not found.") { Metadata = { { "Status", ResultStatus.NotFound } } });

        await _jobRepository.Delete(jobToDelete);

        _logger.LogInformation("Product {jobId} deleted.", request.Id);
        return Result.Ok();
    }
}