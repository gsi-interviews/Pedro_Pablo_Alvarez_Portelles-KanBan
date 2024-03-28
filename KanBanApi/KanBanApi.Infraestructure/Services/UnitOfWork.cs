using KanBanApi.Application.Repositories;
using KanBanApi.Application.Services;
using KanBanApi.Infraestructure.DbContexts;
using KanBanApi.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace KanBanApi.Infraestructure.Services;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DefaultDbContext _dbContext;

    public UnitOfWork(DefaultDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitChangesAsync(CancellationToken ct = default) => await _dbContext.SaveChangesAsync(ct);

    public IGenericRepositoryAsync<TEntity> GetRepository<TEntity>() where TEntity : class
        => new GenericRepositoryAsync<TEntity>(_dbContext);
}