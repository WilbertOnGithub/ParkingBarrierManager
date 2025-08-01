using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Domain;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.Application;

public class SmsGatewayService(ILogger<SmsGatewayService> logger, ISmsGateway gateway)
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

    public Result SendSMS([NotNull] ApartmentConfiguration apartmentConfiguration)
    {
        Result result = gateway.SendSms(apartmentConfiguration);

        if (result.IsSuccess)
        {
            return result;
        }

        logger.LogError(
            "Error occurred while trying to send SMS to update apartment {ApartmentNumber}",
            apartmentConfiguration.Id
        );
        foreach (var error in result.Errors)
        {
            logger.LogError("{ErrorMessage}", error.Message);
        }
        return result;
    }
}
