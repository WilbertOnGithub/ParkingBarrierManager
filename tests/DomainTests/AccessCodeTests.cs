namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class AccessCodeTests
{
    [Theory]
    [InlineData("     ")]
    [InlineData("abcd")]
    [InlineData("1234567")]
    public void AccessCode_is_invalid_if_it_does_not_have_1_to_6_digits(string potentialAccessCode)
    {
        // Arrange
        Action act = () =>
        {
            _ = new AccessCode(potentialAccessCode);
        };

        // Act / Assert
        act.Should().Throw<ArgumentException>("because this is not a valid AccessCode.");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    public void AccessCode_is_valid_with_1_to_6_digits(string potentialAccessCode)
    {
        // Arrange
        Action act = () =>
        {
            _ = new AccessCode(potentialAccessCode);
        };

        // Act / Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid AccessCode.");
    }

    [Fact]
    public void Empty_string_should_be_NoAccessCode()
    {
        // Arrange
        var accessCode = new AccessCode(string.Empty);

        // Act / Assert
        accessCode.Should().Be(AccessCode.NoAccessCode);
    }
}
