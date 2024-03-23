using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Arentheym.ParkingBarrier.Domain.Tests;

/// <summary>
/// Create test data for validating phone numbers.
/// </summary>
/// <see cref="https://nl.wikipedia.org/wiki/Lijst_van_landnummers_in_de_telefonie_op_nummervolgorde"/>
public class ValidCountryCodesTestData : IEnumerable<object[]>
{
    private static List<string> CreateTestData()
    {
        List<string> testData = new();

        // Country code range 1 - 98
        testData.Add("1");
        testData.Add("7");
        testData.Add("20");
        for (int i = 30; i <= 34; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        testData.Add("36");
        testData.Add("39");
        testData.Add("40");
        testData.Add("41");
        for (int i = 43; i <= 49; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 51; i <= 58; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 60; i <= 66; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        testData.Add("81");
        testData.Add("82");
        testData.Add("84");
        testData.Add("86");
        for (int i = 90; i <= 95; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        testData.Add("98");

        // Range 211 - 299
        testData.Add("211");
        testData.Add("212");
        testData.Add("213");
        testData.Add("216");
        testData.Add("218");
        for (int i = 220; i <= 258; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 260; i <= 269; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        testData.Add("290");
        testData.Add("291");
        testData.Add("297");
        testData.Add("298");
        testData.Add("299");

        // Range 350 - 389
        for (int i = 350; i <= 359; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 350; i <= 359; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 370; i <= 383; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 385; i <= 389; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }

        // Range 420 - 423
        testData.Add("420");
        testData.Add("421");
        testData.Add("423");

        // Range 500-599
        for (int i = 500; i <= 509; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 590; i <= 599; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }

        // Range 670-692
        testData.Add("670");
        for (int i = 672; i <= 683; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 685; i <= 692; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }

        // Range 850-886
        testData.Add("800");
        testData.Add("850");
        testData.Add("852");
        testData.Add("853");
        testData.Add("855");
        testData.Add("856");
        for (int i = 870; i <= 874; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        testData.Add("880");
        testData.Add("886");

        // Range 960-998
        for (int i = 960; i <= 968; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 970; i <= 977; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        for (int i = 992; i <= 996; i++)
        {
            testData.Add(i.ToString(CultureInfo.InvariantCulture));
        }
        testData.Add("998");

        // Range 1242-1284
        testData.Add("1242");
        testData.Add("1246");
        testData.Add("1264");
        testData.Add("1268");
        testData.Add("1284");

        // Range 1340-1345
        testData.Add("1340");
        testData.Add("1345");

        // Range 1441-1473
        testData.Add("1441");
        testData.Add("1473");

        // Range 1649-1684
        testData.Add("1649");
        testData.Add("1664");
        testData.Add("1670");
        testData.Add("1671");
        testData.Add("1684");

        // Range 1721-1787
        testData.Add("1721");
        testData.Add("1758");
        testData.Add("1767");
        testData.Add("1784");
        testData.Add("1787");

        // Range 1809-1876
        testData.Add("1809");
        testData.Add("1868");
        testData.Add("1869");
        testData.Add("1876");

        // Range 1907-1939
        testData.Add("1907");
        testData.Add("1939");

        // Range 2696
        testData.Add("2696");

        // Range 6723-8817
        testData.Add("6723");
        testData.Add("8816");
        testData.Add("8817");

        for (var index = 0; index < testData.Count; index++)
        {
            testData[index] += $" {CreateRandomPhoneNumber()}"; // Add a phone number to each country code
        }

        testData.Add(string.Empty); // Empty phone number is also valid.
        return testData;
    }

    [SuppressMessage(
        "Security",
        "CA5394:Do not use insecure randomness",
        Justification = "Not necessary for unit test."
    )]
    private static string CreateRandomPhoneNumber()
    {
        var rnd = new Random();
        int numberOfDigits = rnd.Next(1, 15);
        var sb = new StringBuilder();
        for (int i = 0; i < numberOfDigits; i++)
        {
            sb.Append(rnd.Next(0, 9));
        }

        return sb.ToString();
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var phoneNumber in CreateTestData())
        {
            yield return new object[] { phoneNumber };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
