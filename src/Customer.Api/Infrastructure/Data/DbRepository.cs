using Customer.Domain;
using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Infrastructure.Data;

public class DbRepository<TEntity, TId>(DbCustomer dbContext) : IRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
{
    public async Task CreateAsync(TEntity domainEntity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<TEntity>().AddAsync(domainEntity, cancellationToken);
    }

    public Task DeleteAsync(TEntity domainEntity, CancellationToken cancellationToken = default)
    {
         dbContext.Set<TEntity>().Remove(domainEntity);
         return Task.CompletedTask;
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().FindAsync([id], cancellationToken);
    }
    
    public async Task CommitChanges(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }
}