using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

public class IntercomId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntercomId"/> class.
    /// </summary>
    /// <param name="guid">The value of the identifier.</param>
    public IntercomId(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the identifier;
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Create a new identifier.
    /// </summary>
    /// <returns>A new instantiated <see cref="IntercomId"/>.</returns>
    public static IntercomId NewId()
    {
        return new IntercomId(Guid.NewGuid());
    }

    /// <summary>
    /// List the properties needed to determine equality.
    /// </summary>
    /// <returns>A list of <see cref="object"/>.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Id;
    }
}