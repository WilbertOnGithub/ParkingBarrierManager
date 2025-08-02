using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Domain;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.Application;

public class SmsGatewayService(ILogger<SmsGatewayService> logger, ISmsGateway gateway, DataService dataService)
{
    public Result<float> GetBalanceDetails()
    {
        Result<float> result = gateway.GetBalance();
        if (result.IsSuccess)
        {
            return result;
        }

        logger.LogError("Error occurred while trying to retrieve SMS balance");
        foreach (var error in result.Errors)
        {
            logger.LogError("{ErrorMessage}", error.Message);
        }

        return result;
    }

    /// <summary>
    /// Updates the configuration of an apartment by synchronizing its intercoms with the given apartment configuration.
    /// Sends SMS messages to update or reset intercom configurations accordingly.
    /// </summary>
    /// <param name="apartmentConfiguration">The configuration of the apartment to be updated, including its intercoms.</param>
    /// <returns>A list of strings representing the results of the SMS update operations.</returns>
    public async Task<List<string>> UpdateApartmentConfiguration([NotNull] ApartmentConfiguration apartmentConfiguration)
    {
        IList<Intercom> intercoms = await dataService.GetIntercoms().ConfigureAwait(false);
        List<Intercom> toBeEmptied = apartmentConfiguration.Intercoms.Where(x => intercoms.All(y => y.Id != x.Id)).ToList();
        List<Intercom> toBeUpdated = intercoms.Where(x => apartmentConfiguration.Intercoms.All(y => y.Id == x.Id)).ToList();

        List<string> results = [];

        foreach (var intercom in toBeUpdated)
        {
            results.Add(CheckResult(gateway.SendSms(apartmentConfiguration, intercom)));
        }

        foreach (var intercom in toBeEmptied)
        {
            results.Add(CheckResult(gateway.SendSms(ApartmentConfiguration.CreateEmptyConfiguration(apartmentConfiguration.Id.Number), intercom)));
        }

        return results;
    }

    private string CheckResult(Result<string> result)
    {
        if (result.IsSuccess)
        {
            return result.Value;
        }

        foreach (var error in result.Errors)
        {
            logger.LogError("{ErrorMessage}", error.Message);
        }

        return "Fout opgetreden bij het versturen van de SMS.";
    }

    public float GetPricePerMessage()
    {
        return gateway.GetPricePerMessage();
    }

    public Result<int> NumberOfMessagesLeft()
    {
        Result<float> result = GetBalanceDetails();
        float pricePerMessage = GetPricePerMessage();

        if (result.IsSuccess && pricePerMessage > 0)
        {
            return (int)Math.Floor(result.Value / pricePerMessage);
        }

        return Result.Fail("Could not determine number of messages left.");
    }
}
