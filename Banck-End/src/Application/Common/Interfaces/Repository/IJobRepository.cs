using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.Ats.Domain.Entities.Jobs;


namespace Totvs.Ats.Application.Common.Interfaces.Repository
{
    public interface IJobRepository
    {
        Task<Job> Create(Job job);
        Task Delete(Job job);
        Task<List<Job>> GetAll();
        Task<Job?> GetById(Guid id);
        Task Update(Job job);
        void Clear();
        Task<long> CountAsync();
    }
}
