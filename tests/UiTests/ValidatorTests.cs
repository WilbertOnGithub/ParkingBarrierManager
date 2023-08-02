using Arentheym.ParkingBarrier.UI.Validators;

using Xunit;
using FluentAssertions;

namespace Arentheym.ParkingBarrier.UI.Tests;

public class ValidatorTests
{
    [Fact]
    public void AccessCodeCanBeEmpty()
    {
        // Arrange
        AccessCode accessCode = new();

        // Assert / Act
        accessCode.IsValid(string.Empty).Should().BeTrue();
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    public void AccessCodeCanContainOneToSixDigits(string data)
    {
        // Arrange
        AccessCode accessCode = new();

        // Assert / Act
        accessCode.IsValid(data).Should().BeTrue();
    }

    [Theory]
    [InlineData("A")]
    [InlineData("1234567")]
    [InlineData("1234-567")]
    public void AccessCodeCannotContainInvalidData(string data)
    {
        // Arrange
        AccessCode accessCode = new();

        // Assert / Act
        accessCode.IsValid(data).Should().BeFalse();
    }
}