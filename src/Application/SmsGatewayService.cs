using System.Diagnostics.CodeAnalysis;
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
}
