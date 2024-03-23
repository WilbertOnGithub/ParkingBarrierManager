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

    [Fact]
    public void An_empty_string_is_considered_to_be_an_empty_phonenumber()
    {
        // Arrange / Act
        var phoneNumber = new PhoneNumber(string.Empty);

        // Assert
        phoneNumber.Should().Be(PhoneNumber.EmptyPhoneNumber);
    }

    [Theory]
    [InlineData("abc")] // Phone numbers only contain digits
    [InlineData("31613739851999999999")] // Valid country code (31) but invalid phone number (too long)
    [InlineData("0000613739851")] // Invalid country code (0000) but valid phone number
    public void PhoneNumber_with_invalid_data_throws_exception(string phoneNumber)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new PhoneNumber(phoneNumber);
        };

        // Assert
        act.Should().Throw<ArgumentException>("because this is not a valid phone number.");
    }
}
