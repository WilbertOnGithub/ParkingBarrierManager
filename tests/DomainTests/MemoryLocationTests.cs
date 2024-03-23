namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class MemoryLocationTests
{
    [Theory]
    [ClassData(typeof(ValidMemoryLocationsTestData))]
    public void MemoryLocation_must_be_between_0_and_499(short validMemoryLocation)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new MemoryLocation(validMemoryLocation);
        };

        // Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid memory location.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(500)]
    public void Invalid_memory_location_throws_exception(short invalidMemoryLocation)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new MemoryLocation(invalidMemoryLocation);
        };

        // Assert
        act.Should().Throw<ArgumentException>("because this is not a valid memory location.");
    }

    [Fact]
    public void MemoryLocations_can_be_equal()
    {
        var left = new MemoryLocation(0);
        var right = new MemoryLocation(0);

        left.Should().BeEquivalentTo(right, "because they are equal");
    }

    [Fact]
    public void MemoryLocation_are_displayed_with_leading_zeroes_to_3_digits()
    {
        // Arrange / act
        var memoryLocation = new MemoryLocation(0);

        // Assert
        memoryLocation
            .ToString()
            .Should()
            .Be("000", "because string representations are prefixed with leading zeroes");
    }
}
