using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Domain.Entities.Jobs;
using Totvs.Ats.Domain.Entities.Jobs.ValueObjects;
using Totvs.Ats.Infrastructure.Database.Models;

namespace Totvs.Ats.Infrastructure.Database.Repositories;

internal class JobRepository : IJobRepository
{


    private readonly IMongoCollection<JobModel> _JobsCollection;

    public JobRepository(IOptions<TotvsAtsDatabaseSetting> TotvsAtsStoreDatabaseSetting)
    {
        var mongoClient = new MongoClient(
       TotvsAtsStoreDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            TotvsAtsStoreDatabaseSetting.Value.DatabaseName);

        _JobsCollection = mongoDatabase.GetCollection<JobModel>(
            TotvsAtsStoreDatabaseSetting.Value.JobCollectionName);
    }

    public async Task<Job?> GetById(Guid id)
    {
        var jobs = await _JobsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        return jobs == default ? null : jobs.Adapt<Job>();
    }

    public async Task<List<Job>> GetAll()
    {
        try
        {
            var jobs = await _JobsCollection.Find(x => true).ToListAsync();

            return jobs.Select(x => x.Adapt<Job>()).ToList();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Job> Create(Job job)
    {
        await _JobsCollection.InsertOneAsync(job.Adapt<JobModel>());
        return job;
    }
    public async Task Update(Job job)
    {
        var model = job.Adapt<JobModel>();
        await _JobsCollection.ReplaceOneAsync(x => x.Id == model.Id, model);

    }

    public async Task Delete(Job job)
    {
        var model = job.Adapt<JobModel>();

        await _JobsCollection.DeleteOneAsync(x => x.Id == model.Id);
    }

    public async void Clear()
    {
        await _JobsCollection.DeleteManyAsync(x => true);
    }

    public async Task<long> CountAsync()
    {
        return await _JobsCollection.EstimatedDocumentCountAsync();
    }
}