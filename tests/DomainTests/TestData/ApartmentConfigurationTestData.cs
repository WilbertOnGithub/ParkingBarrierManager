using System.Collections;

namespace Arentheym.ParkingBarrier.Domain.Tests;

public class ApartmentConfigurationTestData : IEnumerable<object[]>
{
    private static List<ApartmentConfiguration> Create()
    {
        var apartmentConfigurations = new List<ApartmentConfiguration>();
        apartmentConfigurations.Add(new ApartmentConfiguration(new ApartmentId(89)));
        apartmentConfigurations.Add(new ApartmentConfiguration(new ApartmentId(89), new MemoryLocation(0)));
        apartmentConfigurations.Add(
            new ApartmentConfiguration(new ApartmentId(89), new MemoryLocation(0), string.Empty)
        );
        apartmentConfigurations.Add(
            new ApartmentConfiguration(new ApartmentId(89), new MemoryLocation(0), string.Empty, false)
        );
        apartmentConfigurations.Add(
            new ApartmentConfiguration(
                new ApartmentId(89),
                new MemoryLocation(0),
                string.Empty,
                false,
                AccessCode.NoAccessCode
            )
        );

        return apartmentConfigurations;
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var apartmentConfiguration in Create())
        {
            yield return [apartmentConfiguration];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
