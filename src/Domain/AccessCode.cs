using System.Text.RegularExpressions;
using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// The code that is configured for an apartment to open the barrier using the
/// touchpad on the intercom.
/// </summary>
public partial class AccessCode : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccessCode"/> class.
    /// </summary>
    /// <param name="code">The master code for the intercom.</param>
    public AccessCode(string code)
    {
        if (!AccessCodeRegEx().IsMatch(code))
        {
            throw new ArgumentException("Access code must consist out of 1-6 digits or empty string.");
        }

        Code = code;
    }

    /// <summary>
    /// Gets the value if access code is not used.
    /// </summary>
    public static AccessCode NoAccessCode => new(string.Empty);

    /// <summary>
    /// Gets the code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Return all the properties that determine if this object is equal.
    /// </summary>
    /// <returns>The list of properties that determine equality.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Code;
    }

    [GeneratedRegex(@"^\d{1,6}$|^$", RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex AccessCodeRegEx();
}
