using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Domain.Entities.Applicants;
using Totvs.Ats.Domain.Entities.Applicants.ValueObjects;
using Totvs.Ats.Infrastructure.Database.Models;

namespace Totvs.Ats.Infrastructure.Database.Repositories;

internal class ApplicantRepository : IApplicantRepository
{


    private readonly IMongoCollection<ApplicantModel> _ApplicantsCollection;

    public ApplicantRepository(IOptions<TotvsAtsDatabaseSetting> TotvsAtsStoreDatabaseSetting)
    {
        var mongoClient = new MongoClient(
       TotvsAtsStoreDatabaseSetting.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            TotvsAtsStoreDatabaseSetting.Value.DatabaseName);

        _ApplicantsCollection = mongoDatabase.GetCollection<ApplicantModel>(
            TotvsAtsStoreDatabaseSetting.Value.ApplicantCollectionName);
    }

    public async Task<Applicant?> GetById(Guid id)
    {
        var applicants = await _ApplicantsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        return applicants == default ? null : applicants.Adapt<Applicant>();
    }

    public async Task<List<Applicant>> GetAll()
    {
        var applicants = await _ApplicantsCollection.Find(x => true).ToListAsync();

        return applicants.Select(x => x.Adapt<Applicant>()).ToList();
    }

    public async Task<Applicant> Create(Applicant applicant)
    {
        await _ApplicantsCollection.InsertOneAsync(applicant.Adapt<ApplicantModel>());
        return applicant;
    }
    public async Task Update(Applicant applicant)
    {
        var model = applicant.Adapt<ApplicantModel>();
        await _ApplicantsCollection.ReplaceOneAsync(x => x.Id == model.Id, model);

    }

    public async Task Delete(Applicant applicant)
    {
        var model = applicant.Adapt<ApplicantModel>();

        await _ApplicantsCollection.DeleteOneAsync(x => x.Id == model.Id);
    }

    public async void Clear()
    {
        await _ApplicantsCollection.DeleteManyAsync(x => true);
    }

    public async Task<long> CountAsync()
    {
        return await _ApplicantsCollection.EstimatedDocumentCountAsync();
    }
}