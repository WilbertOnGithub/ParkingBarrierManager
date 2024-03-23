using System.Collections;

namespace Arentheym.ParkingBarrier.Domain.Tests;

/// <summary>
/// Create list of all valid MemoryLocations
/// </summary>
public class ValidMemoryLocationsTestData : IEnumerable<object[]>
{
    private static List<short> Create()
    {
        List<short> testData = new();
        for (short i = 0; i < 500; i++)
        {
            testData.Add(i);
        }

        return testData;
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var phoneNumber in Create())
        {
            yield return [phoneNumber];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
