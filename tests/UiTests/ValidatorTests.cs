using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.UI.Validators;

using Xunit;
using FluentAssertions;

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
    public void AccessCode_Is_Valid_With_Valid_Data(string data)
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
    public void AccessCode_Is_InValid_With_Invalid_Data(string data)
    {
        // Arrange
        AccessCode accessCode = new();

        // Act / Assert
        accessCode.IsValid(data).Should().BeFalse();
    }

    [Theory]
    [InlineData("")]
    [InlineData("1234567890")]
    [InlineData(" 1 2 3 4 5 6 7 8 9 0 ")]
    public void PhoneNumber_Is_Valid_With_Valid_Data(string data)
    {
        // Arrange
        PhoneNumber phoneNumber = new();

        // Act / Assert
        phoneNumber.IsValid(data).Should().BeTrue();
    }

    [Theory]
    [InlineData("abc")]             // No digits
    [InlineData("12345678901")]     // Too long
    [InlineData("123456")]          // Too short
    [InlineData(" 1 2 3 4 5 6 ")]   // Too short with whitespace
    public void PhoneNumber_Is_Invalid_With_Invalid_Data(string data)
    {
        // Arrange
        PhoneNumber phoneNumber = new();

        // Act / Assert
        phoneNumber.IsValid(data).Should().BeFalse();
    }
}