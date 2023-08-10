using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// The unique identifier for an <see cref="ApartmentConfiguration"/>
/// </summary>
public partial class ApartmentId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApartmentId"/> class.
    /// </summary>
    /// <param name="number">The number of the apartment.</param>
    public ApartmentId(int number)
    {
        if (number is < 51 or > 189)
        {
            throw new ArgumentException("Expecting number to be between 51 and 189.");
        }
        Number = number;
    }

    /// <summary>
    /// Gets the number for the apartment.
    /// </summary>
    public int Number { get; }

    /// <summary>
    /// List the properties needed to determine equality.
    /// </summary>
    /// <returns>A list of <see cref="object"/>.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Number;
    }
}