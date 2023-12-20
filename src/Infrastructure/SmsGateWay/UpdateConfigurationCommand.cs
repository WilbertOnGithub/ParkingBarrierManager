using System.Globalization;
using System.Text;
using Arentheym.ParkingBarrier.Domain;
using Microsoft.Extensions.Primitives;

namespace Arentheym.ParkingBarrier.Infrastructure.SmsGateway;

/// <summary>
/// The SMS command to update the configuration for an apartment.
/// </summary>
internal class UpdateConfigurationCommand(MasterCode code, ApartmentConfiguration configuration)
{
    public override string ToString()
    {
        var sb = new StringBuilder();

        string name = configuration.DisplayName.Replace(" ", ">", StringComparison.InvariantCulture);

        sb.Append(CultureInfo.InvariantCulture, $"{code.Code}");
        sb.Append(CultureInfo.InvariantCulture, $"MEM{configuration.MemoryLocation}");
        for (int i = 0; i < 4; i++)
        {
            sb.Append(CultureInfo.InvariantCulture, $"\"{configuration.PhoneNumbers[i].Number}\",");
        }
        sb.Append(CultureInfo.InvariantCulture, $"\"{configuration.Id.Number}\",");
        sb.Append(CultureInfo.InvariantCulture, $"\"{configuration.AccessCode.Code}\",");
        sb.Append(CultureInfo.InvariantCulture, $"{(configuration.DialToOpen ? 1 : 0)},");
        sb.Append(CultureInfo.InvariantCulture, $"0,");
        sb.Append(CultureInfo.InvariantCulture, $"\"{name}\"");

        return sb.ToString();
    }
}