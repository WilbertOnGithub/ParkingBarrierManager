namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class ApartmentConfigurationTests
{
    [Fact]
    public void Valid_data_creates_a_well_formed_object()
    {
        const string displayName = "displayname";

        // Arrange / Act
        var apartmentConfiguration = new ApartmentConfiguration(
            new ApartmentId(89),
            new MemoryLocation(0),
            displayName,
            true,
            AccessCode.NoAccessCode
        );

        // Assert
        apartmentConfiguration.Id.Should().Be(new ApartmentId(89));
        apartmentConfiguration.MemoryLocation.Should().Be(new MemoryLocation(0));
        apartmentConfiguration.DisplayName.Should().Be(displayName);
        apartmentConfiguration.DialToOpen.Should().BeTrue();
        apartmentConfiguration.AccessCode.Should().Be(AccessCode.NoAccessCode);
        apartmentConfiguration.Intercoms.Count.Should().Be(0);
        apartmentConfiguration.PhoneNumbers.Count.Should().Be(0);
    }

    [Fact]
    public void Linking_intercom_adds_an_intercom()
    {
        // Arrange
        var apartmentConfiguration = new ApartmentConfiguration(new ApartmentId(89));

        // Act
        apartmentConfiguration.LinkIntercom(new Intercom("name", PhoneNumber.EmptyPhoneNumber, MasterCode.Default));

        // Assert
        apartmentConfiguration.Intercoms.Count.Should().Be(1, "because we added an intercom");
    }

    [Fact]
    public void Unlinking_intercom_removes_the_intercom()
    {
        // Arrange
        var apartmentConfiguration = new ApartmentConfiguration(new ApartmentId(89));
        var intercom = new Intercom("name", PhoneNumber.EmptyPhoneNumber, MasterCode.Default);

        // Act
        apartmentConfiguration.LinkIntercom(intercom);
        apartmentConfiguration.UnlinkIntercom(intercom);

        // Assert
        apartmentConfiguration.Intercoms.Count.Should().Be(0, "because the intercom was removed");
    }

    [Fact]
    public void Upserting_a_phone_number_that_does_not_exist_increases_the_phonenumber_count()
    {
        // Arrange
        var apartmentConfiguration = new ApartmentConfiguration(new ApartmentId(89));

        // Act
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, string.Empty));

        // Assert
        apartmentConfiguration.PhoneNumbers.Count.Should().Be(1);
    }

    [Fact]
    public void Upserting_a_phone_number_with_the_same_order_replaces_existing_phonenumber()
    {
        // Arrange
        var apartmentConfiguration = new ApartmentConfiguration(new ApartmentId(89));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, string.Empty));

        // Act
        const string phoneNumber = "31613739851";
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, phoneNumber));

        // Assert
        apartmentConfiguration.PhoneNumbers.Count.Should().Be(1);
        apartmentConfiguration.PhoneNumbers[0].Number.Should().Be(phoneNumber);
    }
}
