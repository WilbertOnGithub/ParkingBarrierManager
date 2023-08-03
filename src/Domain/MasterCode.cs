using System.Text.RegularExpressions;
using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// Represents the master code for the intercom. Each remote command to the
/// intercom must be accompanied by the master code.
/// </summary>
public partial class MasterCode : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MasterCode"/> class.
    /// </summary>
    /// <param name="code">The master code for the intercom.</param>
    public MasterCode(string code)
    {
        if (!MasterCodeRegEx().IsMatch(code))
        {
            throw new ArgumentException("A master code must be exactly 4 digits.");
        }

        Code = code;
    }

    /// <summary>
    /// Gets the default master code.
    /// </summary>
    public static MasterCode Default => new ("1111");

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

    [GeneratedRegex(@"^\d{4}$", RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex MasterCodeRegEx();
}