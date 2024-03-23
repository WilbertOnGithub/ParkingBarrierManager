using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.UI.Validators;
using FluentAssertions;
using Xunit;

namespace Arentheym.ParkingBarrier.UI.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class ValidatorTests
{
    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    public void AccessCode_is_valid_with_valid_data(string data)
    {
        // Arrange
        AccessCode accessCode = new();

        // Act / Assert
        accessCode.IsValid(data).Should().BeTrue();
    }

    [Theory]
    [InlineData("A")]
    [InlineData("1234567")]
    [InlineData("1234-567")]
    public void AccessCode_is_inValid_with_invalid_data(string data)
    {
        // Arrange
        AccessCode accessCode = new();

        // Act / Assert
        accessCode.IsValid(data).Should().BeFalse();
    }

    [Theory]
    [InlineData("")] // Empty phone number is allowed
    [InlineData("31 1234567890")] // Valid country code with 10 digit phone number
    public void PhoneNumber_is_valid_with_valid_data(string data)
    {
        // Arrange
        PhoneNumber phoneNumber = new();

        // Act / Assert
        phoneNumber.IsValid(data).Should().BeTrue();
    }

    [Theory]
    [InlineData("abc")] // No digits
    [InlineData("31 1234567890123456")] // Phone > 15 digits
    [InlineData("3 1234567890")] // Invalid country code
    [InlineData("31")] // Country code only
    public void PhoneNumber_is_invalid_with_invalid_data(string data)
    {
        // Arrange
        PhoneNumber phoneNumber = new();

        // Act / Assert
        phoneNumber.IsValid(data).Should().BeFalse();
    }
}
