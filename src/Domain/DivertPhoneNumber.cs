namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// The phone number for an <see cref="ApartmentConfiguration"/> together with its <see cref="DivertOrder"/>
/// </summary>
public class DivertPhoneNumber : PhoneNumber
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DivertPhoneNumber"/> class.
    /// </summary>
    /// <param name="order">The divert order of the phone number.</param>
    /// <param name="number">The phone number.</param>
    public DivertPhoneNumber(DivertOrder order, string number)
        : base(number)
    {
        Order = order;
    }

    /// <summary>
    /// Gets the divert order for the phone number.
    /// </summary>
    public DivertOrder Order { get; }

    /// <summary>
    /// Return all the properties that determine if this object is equal.
    /// </summary>
    /// <returns>The list of properties that determine equality.</returns>
    protected override IEnumerable<object> GetEqualityValues()
    {
        yield return Number;
        yield return Order;
    }
}
