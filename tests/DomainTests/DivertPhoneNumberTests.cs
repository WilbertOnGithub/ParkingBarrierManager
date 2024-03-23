namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class DivertPhoneNumberTests
{
    [Fact]
    public void DivertPhoneNumbers_can_be_equal()
    {
        // Arrange / Act
        var left = new DivertPhoneNumber(DivertOrder.Primary, "31613739851");
        var right = new DivertPhoneNumber(DivertOrder.Primary, "31613739851");

        // Assert
        left.Should().BeEquivalentTo(right, "because they are equal");
    }
}
