namespace Totvs.Ats.Application.Common.Interfaces.Client;

public interface IJobClient
{
    Task UpdateJob(Guid jobId, int unitsChange);
}