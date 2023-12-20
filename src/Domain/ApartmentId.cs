using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// The unique identifier for an <see cref="ApartmentConfiguration"/>
/// </summary>
public class ApartmentId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApartmentId"/> class.
    /// </summary>
    /// <param name="number">The number of the apartment.</param>
    public ApartmentId(int number)
    {
        const int lowestApartmentNumber = 51;
        const int highestApartmentNumber = 189;
        if (number is < lowestApartmentNumber or > highestApartmentNumber)
        {
            throw new ArgumentException(
                $"Expecting number to be between {lowestApartmentNumber} and {highestApartmentNumber}.");
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