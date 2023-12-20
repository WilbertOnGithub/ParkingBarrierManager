using System.Diagnostics.CodeAnalysis;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.Application;

public class SmsGatewayService(ILogger<SmsGatewayService> logger, ISmsGateway gateway)
{
    public Result<string> GetBalanceDetails()
    {
        Result<string> result = gateway.GetBalance();

        if (!result.IsFailed)
        {
            return gateway.GetBalance();
        }

        logger.LogError("Error while trying to retrieve SMS balance");
        foreach (var error in result.Errors)
        {
            logger.LogError("{ErrorMessage}", error.Message);
        }
        return gateway.GetBalance();
    }
}