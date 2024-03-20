using System.Linq.Expressions;
using KanBanApi.Application.Repositories;
using KanBanApi.Infraestructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KanBanApi.Infraestructure.Repositories;

public sealed class GenericRepositoryAsync<TEntity> : IGenericRepositoryAsync<TEntity> where TEntity : class
{
    private readonly DefaultDbContext dbContext;

    public GenericRepositoryAsync(DefaultDbContext _dbContext)
    {
        dbContext = _dbContext;
    }

    public async Task DeleteAsync(TEntity entity) => await DeleteCore(entity);

    public Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[]? includes = null,
                                                 Expression<Func<TEntity, bool>>[]? filters = null)
    => Task.FromResult(QueryCore(includes, filters));

    public Task<IQueryable<TEntity>> GetAllListOnlyAsync(Expression<Func<TEntity, object>>[]? includes = null,
                                                         Expression<Func<TEntity, bool>>[]? filters = null)
    => Task.FromResult(QueryCore(includes, filters).AsNoTracking());

    public async Task<TEntity?> GetByIdAsync<TKey>(TKey id) => await dbContext.Set<TEntity>().FindAsync(id);

    public Task SaveAsync(TEntity entity, bool commit = true) => SaveCore(entity, commit);

    public async Task UpdateAsync(TEntity entity, bool commit = true) => await UpdateCore(entity, commit);

    public async Task CommitAsync() => await dbContext.SaveChangesAsync();

    private async Task SaveCore(TEntity entity, bool commit = false)
    {
        DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
        await dbSet.AddAsync(entity);

        if (commit) await CommitAsync();
    }

    private async Task DeleteCore(TEntity entity)
    {
        try
        {
            DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
            dbSet.Remove(entity);
            await CommitAsync();
        }
        catch (Exception)
        {
            if (dbContext.ChangeTracker.Entries().Any((EntityEntry q) => q.Entity.Equals(entity) && q.State == EntityState.Deleted))
            {
                dbContext.Entry<TEntity>(entity).State = EntityState.Unchanged;
            }
            throw;
        }
    }

    private async Task UpdateCore(TEntity entity, bool commit = true)
    {
        dbContext.Set<TEntity>().Update(entity);
        if (commit)
            await dbContext.SaveChangesAsync();
    }

    private IQueryable<TEntity> QueryCore(Expression<Func<TEntity, object>>[]? includes = null,
                                                Expression<Func<TEntity, bool>>[]? filters = null)
    {
        DbSet<TEntity> source = dbContext.Set<TEntity>();

        IQueryable<TEntity> entities = source.OfType<TEntity>();

        if (includes != null && includes.Length > 0)
        {
            entities = includes.Aggregate(entities, (current, includeProp) => current.Include(includeProp));
        }

        if (filters != null && filters.Length > 0)
        {
            entities = filters.Aggregate(entities, (current, expression) => current.Where(expression));
        }

        return entities;
    }
}