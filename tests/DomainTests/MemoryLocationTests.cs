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
}
