using System.Text.RegularExpressions;
using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

public partial class ApartmentId : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApartmentId"/> class.
    /// </summary>
    /// <param name="number">The number of the apartment.</param>
    public ApartmentId(string number)
    {
        if (!ApartmentNumberRegex().IsMatch(number))
        {
            throw new ArgumentException("An apartment number can contain digits and " +
                                        "letters A-F, maximum length 6 characters.");
        }

        Number = number;
    }

    /// <summary>
    /// Gets the number for the apartment.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// List the properties needed to determine equality.
    /// </summary>
    /// <returns>A list of <see cref="object"/>.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Number;
    }

    [GeneratedRegex(@"[0-9A-F]{1,6}", RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex ApartmentNumberRegex();
}