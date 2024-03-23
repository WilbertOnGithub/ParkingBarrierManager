namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class ApartmentConfigurationTests
{
    [Theory]
    [ClassData(typeof(ApartmentConfigurationTestData))]
    public void Using_the_multiple_constructors_should_create_a_well_formed_object(
        [NotNull] ApartmentConfiguration configuration
    )
    {
        // Arrange / Act // Assert
        configuration.Id.Should().Be(new ApartmentId(89));
        configuration.MemoryLocation.Should().Be(new MemoryLocation(0));
        configuration.DisplayName.Should().Be(string.Empty);
        configuration.DialToOpen.Should().BeFalse();
        configuration.AccessCode.Should().Be(AccessCode.NoAccessCode);
        configuration.Intercoms.Count.Should().Be(0);
        configuration.PhoneNumbers.Count.Should().Be(0);
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
