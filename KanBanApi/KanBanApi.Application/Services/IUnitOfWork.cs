using KanBanApi.Application.Repositories;

namespace KanBanApi.Application.Services;

public interface IUnitOfWork
{
    IGenericRepositoryAsync<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task CommitChangesAsync(CancellationToken ct = default);
}
