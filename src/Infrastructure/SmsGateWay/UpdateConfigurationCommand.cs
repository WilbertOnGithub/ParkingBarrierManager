using System.Globalization;
using System.Text;
using Arentheym.ParkingBarrier.Domain;
using Microsoft.Extensions.Primitives;

namespace Arentheym.ParkingBarrier.Infrastructure.SmsGateway;

/// <summary>
/// The SMS command to update the configuration for an apartment.
/// </summary>
internal class UpdateConfigurationCommand
{
    private readonly MasterCode masterCode;
    private readonly ApartmentConfiguration apartmentConfiguration;
    public UpdateConfigurationCommand(MasterCode masterCode, ApartmentConfiguration apartmentConfiguration)
    {
        this.masterCode = masterCode;
        this.apartmentConfiguration = apartmentConfiguration;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        string name = apartmentConfiguration.DisplayName.Replace(" ", ">", StringComparison.InvariantCulture);

        sb.Append(CultureInfo.InvariantCulture, $"{masterCode.Code}");
        sb.Append(CultureInfo.InvariantCulture, $"MEM{apartmentConfiguration.MemoryLocation}");
        for (int i = 0; i < 4; i++)
        {
            sb.Append(CultureInfo.InvariantCulture, $"\"{apartmentConfiguration.PhoneNumbers[i].Number}\",");
        }
        sb.Append(CultureInfo.InvariantCulture, $"\"{apartmentConfiguration.Id.Number}\",");
        sb.Append(CultureInfo.InvariantCulture, $"\"{apartmentConfiguration.AccessCode.Code}\",");
        sb.Append(CultureInfo.InvariantCulture, $"{(apartmentConfiguration.DialToOpen ? 1 : 0)},");
        sb.Append(CultureInfo.InvariantCulture, $"0,");
        sb.Append(CultureInfo.InvariantCulture, $"\"{name}\"");

        return sb.ToString();
    }
}