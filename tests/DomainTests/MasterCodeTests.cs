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
    public void MasterCode_is_invalid_if_it_does_not_have_4_digits(string potentialMasterCode)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new MasterCode(potentialMasterCode);
        };

        // Assert
        act.Should().Throw<ArgumentException>("because this is not a valid MasterCode.");
    }

    [Fact]
    public void MasterCode_is_valid_with_exactly_4_digits()
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new MasterCode("1234");
        };

        // Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid MasterCode.");
    }

    [Fact]
    public void MasterCode_default_is_series_of_4_ones()
    {
        // Arrange / Act
        var masterCode = MasterCode.Default;

        // Assert
        masterCode.Code.Should().Be("1111", "because this is the default Mastercode");
    }

    [Fact]
    public void MasterCodes_can_be_equal()
    {
        var left = new MasterCode("1111");
        var right = new MasterCode("1111");

        left.Should().BeEquivalentTo(right, "because they are equal");
    }
}
