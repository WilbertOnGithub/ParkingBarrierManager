namespace Arentheym.ParkingBarrier.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class ApartmentIdTests
{
    [Fact]
    public void ApartmentId_are_between_51_and_189()
    {
        // Arrange
        const int lowestApartmentNumber = 51;
        const int highestApartmentNumber = 189;

        for (int i = lowestApartmentNumber; i <= highestApartmentNumber; i++)
        {
            // Act
            Action act = () =>
            {
                _ = new ApartmentId(i);
            };

            // Assert
            act.Should().NotThrow<ArgumentException>("because this is a valid ApartmentId.");
        }
    }

    [Theory]
    [InlineData(50)]
    [InlineData(190)]
    public void ApartmentId_outside_the_valid_range_throws_exception(int apartmentId)
    {
        // Arrange / Act
        Action act = () =>
        {
            _ = new ApartmentId(apartmentId);
        };

        // Assert
        act.Should().Throw<ArgumentException>("because this is not a valid ApartmentId.");
    }
}
