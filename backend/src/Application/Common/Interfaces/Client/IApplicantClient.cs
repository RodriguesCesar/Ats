namespace Totvs.Ats.Application.Common.Interfaces.Client;

public interface IApplicantClient
{
    Task UpdateApplicant(Guid applicantId, int unitsChange);
}