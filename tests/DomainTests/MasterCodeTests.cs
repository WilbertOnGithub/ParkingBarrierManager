namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class MasterCodeTests
{
    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("12345")]
    [InlineData("abcd")]
    public void AccessCode_is_invalid_if_it_does_not_have_4_digits(string potentialMasterCode)
    {
        Action act = () =>
        {
            _ = new MasterCode(potentialMasterCode);
        };

        // Act / Assert
        act.Should().Throw<ArgumentException>("because this is not a valid MasterCode.");
    }

    [Fact]
    public void AccessCode_is_valid_with_exactly_4_digits()
    {
        Action act = () =>
        {
            _ = new MasterCode("1234");
        };

        // Act / Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid MasterCode.");
    }
}
