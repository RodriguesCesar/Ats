using Totvs.Ats.Domain.Entities.Applicants;


namespace Totvs.Ats.Application.Common.Interfaces.Repository;

public interface IApplicantRepository
{
    Task<Applicant> Create(Applicant applicant);
    Task Delete(Applicant applicant);
    Task<List<Applicant>> GetAll();
    Task<Applicant?> GetById(Guid id);
    Task Update(Applicant applicant);
    void Clear();
    Task<long> CountAsync();
}