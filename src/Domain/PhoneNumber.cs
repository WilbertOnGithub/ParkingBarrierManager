using System.Text.RegularExpressions;
using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// Represents a phone number.
/// </summary>
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
            throw new ArgumentException("Phone number can be empty or a number of exactly 10 digits (ignoring whitespace).");
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

    [GeneratedRegex(@"^(?:\s*\d\s*){10}$|^$", RegexOptions.Compiled)]
    private static partial Regex PhoneNumberRegex();
}