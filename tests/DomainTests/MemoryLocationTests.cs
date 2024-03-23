namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class MemoryLocationTests
{
    [Theory]
    [ClassData(typeof(ValidMemoryLocationsTestData))]
    public void MemoryLocation_must_be_between_0_and_499(short memoryLocation)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new MemoryLocation(memoryLocation);
        };

        // Assert
        act.Should().NotThrow<ArgumentException>("because this is a valid memory location.");
    }
}
