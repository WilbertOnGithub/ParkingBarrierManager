namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class IntercomTests
{
    [Fact]
    public void Intercom_without_name_throws_exception()
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new Intercom(string.Empty, PhoneNumber.EmptyPhoneNumber, MasterCode.Default);
        };

        // Assert
        act.Should().Throw<ArgumentException>("because this is not a valid Intercom.");
    }

    [Fact]
    public void Changing_name_to_invalid_name_throws_exception()
    {
        // Arrange
        var intercom = new Intercom("name", PhoneNumber.EmptyPhoneNumber, MasterCode.Default);

        // Act
        Action act = () =>
        {
            _ = intercom.Name = string.Empty;
        };

        // Assert
        act.Should().Throw<ArgumentException>("because this is not a valid name.");
    }

    [Fact]
    public void Intercom_with_valid_data_does_not_throw_exception()
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new Intercom("This is an intercom", PhoneNumber.EmptyPhoneNumber, MasterCode.Default);
        };

        // Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid Intercom.");
    }

    [Fact]
    public void Intercom_with_valid_data_should_create_valid_formed_object()
    {
        const string intercomName = "This is an intercom";

        // Arrange / Act
        var intercom = new Intercom(intercomName, PhoneNumber.EmptyPhoneNumber, MasterCode.Default);

        // Assert
        intercom.PhoneNumber.Should().Be(PhoneNumber.EmptyPhoneNumber);
        intercom.MasterCode.Should().Be(MasterCode.Default);
        intercom.Name.Should().Be(intercomName);
    }
}
