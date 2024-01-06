using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Infrastructure.Persistence;

public sealed class SqlServerRepository<TEntity>
    : IRepository<TEntity>
    where TEntity : class
{
    private readonly MsSqlDbContext _dbContext;

    public SqlServerRepository(MsSqlDbContext dbContext) => _dbContext = dbContext;

    public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);

    public void AddRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().AddRange(entities);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>().AnyAsync(cancellationToken);

    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        _dbContext.Set<TEntity>().AnyAsync(expression, cancellationToken);

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>().CountAsync();

    public Task<int> CountAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        _dbContext.Set<TEntity>().CountAsync(expression, cancellationToken);

    public Task<Maybe<TEntity>> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        _dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(expression, cancellationToken)
            .ContinueWith(task =>
            {
                TEntity? result = task.Result;
                return result is not null ?
                    Maybe<TEntity>.WithValue(result) :
                    Maybe<TEntity>.NoValue;
            });

    public async Task<Maybe<TEntity>> GetByIdAsync<TEntityIdentifier>(
        TEntityIdentifier id,
        CancellationToken cancellationToken = default)
        where TEntityIdentifier : notnull
    {
        TEntity? entity = await _dbContext
            .Set<TEntity>()
            .FindAsync([id], cancellationToken);

        return entity is not null ?
            Maybe<TEntity>.WithValue(entity) :
            Maybe<TEntity>.NoValue;
    }

    public Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default) =>
        _dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<List<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken) =>
        await _dbContext.Set<TEntity>()
            .Where(expression)
            .ToListAsync(cancellationToken);

    public void Remove(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

    public void RemoveRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().RemoveRange(entities);

    public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

    public void UpdateRange(IEnumerable<TEntity> entities) =>
        _dbContext.Set<TEntity>().UpdateRange(entities);
}