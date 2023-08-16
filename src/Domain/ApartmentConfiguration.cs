using Arentheym.ParkingBarrier.Domain.Core;

namespace Arentheym.ParkingBarrier.Domain;

/// <summary>
/// Represents the configuration for an apartment for one or more intercoms.
/// </summary>
public class ApartmentConfiguration : Entity<ApartmentId>
{
    private readonly List<Intercom> intercoms = new ();
    private readonly List<DivertPhoneNumber> phoneNumbers = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="ApartmentConfiguration"/> class.
    /// </summary>
    /// <remarks>
    /// This is the constructor to normally use. It automatically creates a new unique identifier for the owner.
    /// </remarks>
    /// <param name="id">The number of the apartment.</param>
    public ApartmentConfiguration(ApartmentId id)
        : base(id)
    {
        DisplayName = string.Empty;
        MemoryLocation = new MemoryLocation(0);
        DialToOpen = true;
        AccessCode = AccessCode.NoAccessCode;

        /*
        phoneNumbers.Add(new DivertPhoneNumber(DivertOrder.Primary, string.Empty));
        phoneNumbers.Add(new DivertPhoneNumber(DivertOrder.Secondary, string.Empty));
        phoneNumbers.Add(new DivertPhoneNumber(DivertOrder.Tertiary, string.Empty));
        phoneNumbers.Add(new DivertPhoneNumber(DivertOrder.Quaternary, string.Empty));
        */
    }

    public ApartmentConfiguration(ApartmentId id, MemoryLocation memoryLocation, string displayName, bool dialToOpen, AccessCode accessCode)
        : base(id)
    {
        DisplayName = displayName;
        DialToOpen = dialToOpen;
        MemoryLocation = new MemoryLocation(0);
        MemoryLocation = memoryLocation;
        AccessCode = accessCode;
    }

    public ApartmentConfiguration(ApartmentId id, MemoryLocation memoryLocation, string displayName, bool dialToOpen)
        : this(id, memoryLocation, displayName, dialToOpen, AccessCode.NoAccessCode)
    {
    }

    public ApartmentConfiguration(ApartmentId id, MemoryLocation memoryLocation, string displayName)
        : this(id, memoryLocation, displayName, false, AccessCode.NoAccessCode)
    {
    }

    public ApartmentConfiguration(ApartmentId id, MemoryLocation memoryLocation)
        : this(id, memoryLocation, string.Empty, false, AccessCode.NoAccessCode)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApartmentConfiguration"/> class.
    /// </summary>
    /// <remarks>
    /// Private constructor only used by EF Core.
    /// </remarks>
    /// <param name="id">The unique identifier for the <see cref="ApartmentConfiguration"/>.</param>
    /// <param name="dialToOpen">Indicates if linked phone numbers can call intercom to open barrier.</param>
    /// <param name="displayName">The name of the occupant.</param>
    /// <param name="memoryLocation">The memory location of the configuration.</param>
    /// <param name="accessCode">The access code on the keypad to open the barrier.</param>
    private ApartmentConfiguration(
        ApartmentId id,
        bool dialToOpen,
        string displayName,
        MemoryLocation memoryLocation,
        AccessCode accessCode)
        : base(id)
    {
        AccessCode = accessCode;
        DialToOpen = dialToOpen;
        MemoryLocation = memoryLocation;
        DisplayName = displayName;
    }

    /// <summary>
    /// Gets a value indicating whether the linked phone numbers can call the intercom to open the barrier.
    /// </summary>
    public bool DialToOpen { get; private set;  }

    /// <summary>
    /// Gets the name for the apartment occupant.
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// Gets the memory location for this configuration.
    /// </summary>
    public MemoryLocation MemoryLocation { get; private set; }

    /// <summary>
    /// Gets the value for the access code on the keypad to open the barrier.
    /// </summary>
    public AccessCode AccessCode { get; private set; }

    /// <summary>
    /// Gets the list of intercoms used by this configuration.
    /// </summary>
    public IReadOnlyCollection<Intercom> Intercoms => intercoms.OrderBy(x => x.Name).ToList().AsReadOnly();

    /// <summary>
    /// Gets the list of diverted phone numbers in use for this configuration.
    /// </summary>
    public IReadOnlyList<DivertPhoneNumber> PhoneNumbers => phoneNumbers.OrderBy(x => x.Order).ToList().AsReadOnly();

    /// <summary>
    /// Insert or update the phone number.
    /// </summary>
    /// <param name="phoneNumber">The <see cref="DivertPhoneNumber"/>.</param>
    public void UpsertPhoneNumber(DivertPhoneNumber phoneNumber)
    {
        var index = phoneNumbers.FindIndex(x => x.Order == phoneNumber.Order);
        phoneNumbers.RemoveAt(index);
        phoneNumbers.Add(phoneNumber);
    }

    /// <summary>
    /// Insert or update the intercom.
    /// </summary>
    /// <param name="intercom">The <see cref="Intercom"/></param>
    public void LinkIntercom(Intercom intercom)
    {
        intercoms.Add(intercom);
    }

    public void UnlinkIntercom(Intercom intercom)
    {
        intercoms.Remove(intercom);
    }
}