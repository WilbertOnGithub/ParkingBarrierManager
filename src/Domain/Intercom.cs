using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// Represents the intercom that is installed at the apartment complex that
/// visitors can call to gain access to the apartment complex.
/// </summary>
public class Intercom : Entity<IntercomId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Intercom"/> class.
    /// </summary>
    /// <param name="name">The name of the owner.</param>
    /// <param name="phoneNumber">A <see cref="PhoneNumber"/> used by this intercom.</param>
    /// <param name="masterCode">The <see cref="MasterCode"/> used by this intercom.</param>
    public Intercom(string name, PhoneNumber phoneNumber, MasterCode masterCode)
        : base(IntercomId.NewId())
    {
        Name = name;
        PhoneNumber = phoneNumber;
        MasterCode = masterCode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Intercom"/> class.
    /// </summary>
    /// <remarks>
    /// Private constructor only used by EF Core.
    /// </remarks>
    /// <param name="id">The unique identifier for the <see cref="Intercom"/>.</param>
    /// <param name="masterCode">The <see cref="MasterCode"/> is necessary to remotely configure the intercom.</param>
    /// <param name="name">The name of the <see cref="Intercom"/>.</param>
    /// <param name="phoneNumber">The <see cref="PhoneNumber"/> used by the intercom.</param>
    private Intercom(IntercomId id, string name, PhoneNumber phoneNumber, MasterCode masterCode)
        : base(id)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        MasterCode = masterCode;
    }

    /// <summary>
    /// Gets the master code that is needed to remotely configure the intercom.
    /// </summary>
    public MasterCode MasterCode { get; private set; }

    /// <summary>
    /// Gets the name of the owner.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the phone number for this intercom.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private set; }
}
