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
}
