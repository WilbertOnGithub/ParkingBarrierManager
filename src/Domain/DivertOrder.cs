namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// The divert order list for phone numbers for an apartment.
/// </summary>
public enum DivertOrder
{
    /// <summary>
    /// The primary number to call
    /// </summary>
    Primary = 0,

    /// <summary>
    /// The first divert - when the primary does not answer.
    /// </summary>
    Secondary = 1,

    /// <summary>
    /// The second divert - when the first divert does not answer.
    /// </summary>
    Tertiary = 2,

    /// <summary>
    /// The final divert - when the second divert does not answer.
    /// </summary>
    Quaternary = 3
}
