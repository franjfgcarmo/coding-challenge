using Customer.Domain.Entities;

namespace Customer.Domain;

public interface IRepository<TEntity, in TId> where TEntity : BaseEntity<TId>
{
    Task CreateAsync(TEntity domainEntity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity domainEntity, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task CommitChanges(CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}