using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Totvs.Ats.Application.Common.Interfaces.Repository;
using Totvs.Ats.Domain.Entities.Applicants;

namespace Totvs.Ats.Application.IntegrationTests;

[Collection("Sequential")]
public abstract class TestBase : IClassFixture<Testing>
{
    private readonly IServiceScopeFactory _scopeFactory;

    protected TestBase(Testing testing)
    {
        _scopeFactory = testing.ScopeFactory;
    }

    protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }


    protected async Task<Applicant?> GetApplicantById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IApplicantRepository>();

        return await repository.GetById(id);
    }


    protected async Task AddAsync(Applicant entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IApplicantRepository>();

        await repository.Create(entity);
    }
    protected async Task RemoveAsync(Applicant entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IApplicantRepository>();

        await repository.Delete(entity);
    }
    protected async Task UpdateAsync(Applicant entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IApplicantRepository>();

        await repository.Update(entity);
    }


    public void ClearApplicant()
    {
        using var scope = _scopeFactory.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IApplicantRepository>();
        repository.Clear();
    }


    protected async Task<long> CountApplicantAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IApplicantRepository>();

        return await repository.CountAsync();
    }

}