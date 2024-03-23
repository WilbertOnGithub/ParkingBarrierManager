namespace Arentheym.ParkingBarrier.Domain.Core;

/// <summary>
/// An abstract base class for DDD style entities.
/// </summary>
/// <typeparam name="TId">The type to be used for the entity id.</typeparam>
public abstract class Entity<TId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the identity of this DDD style entity.
    /// </summary>
    public TId Id { get; init; }
}
