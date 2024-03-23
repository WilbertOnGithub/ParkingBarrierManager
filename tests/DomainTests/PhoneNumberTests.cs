using System.Diagnostics.CodeAnalysis;

namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class PhoneNumberTests
{
    [Theory]
    [ClassData(typeof(ValidPhoneNumbersTestData))]
    public void PhoneNumbers_with_valid_country_codes_should_not_throw_exception(string phoneNumber)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new PhoneNumber(phoneNumber);
        };

        // Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid phone number.");
    }
}
