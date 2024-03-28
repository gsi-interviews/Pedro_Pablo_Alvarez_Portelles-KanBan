using System.Linq.Expressions;
using KanBanApi.Domain.Entities;

namespace KanBanApi.Application.Repositories;

public interface IGenericRepositoryAsync<TEntity> where TEntity : class
{
    Task CommitAsync();
    Task SaveAsync(TEntity entity, bool commit = true);
    Task UpdateAsync(TEntity entity, bool commit = true);
    Task DeleteAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync<TKey>(TKey id);
    Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[]? includes = null,
                                          Expression<Func<TEntity, bool>>[]? filters = null);
    Task<IQueryable<TEntity>> GetAllListOnlyAsync(Expression<Func<TEntity, object>>[]? includes = null,
                                                  Expression<Func<TEntity, bool>>[]? filters = null);

}