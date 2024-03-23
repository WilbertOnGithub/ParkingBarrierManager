using System.Diagnostics.CodeAnalysis;

namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class PhoneNumberTests
{
    [Theory]
    [ClassData(typeof(ValidCountryCodesTestData))]
    public void Valid_PhoneNumbers(string phoneNumber)
    {
        // Arrange
        Action act = () =>
        {
            _ = new PhoneNumber(phoneNumber);
        };

        // Act / Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid phone number.");
    }
}
