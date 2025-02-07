namespace Customer.Domain.Entities;

public abstract class BaseEntity<TId>
{
    public virtual TId Id { get; protected set; } = default!;
}