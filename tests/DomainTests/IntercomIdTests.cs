namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class IntercomIdTests
{
    [Fact]
    public void IntercomdIds_can_be_equal()
    {
        // Arrange
        Guid guid = Guid.NewGuid();

        // Act
        var left = new IntercomId(guid);
        var right = new IntercomId(guid);

        // Assert
        left.Should().BeEquivalentTo(right, "because they are equal");
    }
}
