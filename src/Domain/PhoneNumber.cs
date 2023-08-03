using System.Text.RegularExpressions;
using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

public partial class PhoneNumber : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
    /// </summary>
    /// <param name="number">The number of transponder.</param>
    public PhoneNumber(string number)
    {
        if (!PhoneNumberRegex().IsMatch(number))
        {
            throw new ArgumentException("Phonenumber must contain exactly 10 digits or be empty.");
        }

        Number = number;
    }

    /// <summary>
    /// Gets an empty phone number.
    /// </summary>
    public static PhoneNumber EmptyPhoneNumber => new (string.Empty);

    /// <summary>
    /// Gets the number of phone number.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Return all the properties that determine if this object is equal.
    /// </summary>
    /// <returns>The list of properties that determine equality.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Number;
    }

    [GeneratedRegex(@"^\d{10}$|^$", RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex PhoneNumberRegex();
}