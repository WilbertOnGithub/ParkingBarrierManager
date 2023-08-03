using System.Globalization;
using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// Memory location represent the location in memory where a configuration is stored in the intercom.
/// An intercom has 500 memory locations, numbered 0-499.
/// </summary>
public class MemoryLocation : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemoryLocation"/> class.
    /// </summary>
    /// <param name="location">The number of the memory location.</param>
    public MemoryLocation(short location)
    {
        if (location is < 0 or > 499)
        {
            throw new ArgumentException("Memory location must be between 0-499.");
        }

        Location = location;
    }

    /// <summary>
    /// Gets the number of the memory location.
    /// </summary>
    public short Location { get; }

    /// <summary>
    /// Display number with leading zeroes.
    /// </summary>
    /// <example>The value 3 is displayed as '003'. </example>
    /// <returns>The number as a string with leading zeroes.</returns>
    public override string ToString()
    {
        return Location.ToString("d3", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Return all the properties that determine if this object is equal.
    /// </summary>
    /// <returns>The list of properties that determine equality.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Location;
    }
}