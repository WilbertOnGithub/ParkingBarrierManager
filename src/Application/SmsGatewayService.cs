using System.Diagnostics.CodeAnalysis;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.Application;

public class SmsGatewayService
{
    private readonly ILogger<SmsGatewayService> logger;
    private readonly ISmsGateway smsGateway;

    public SmsGatewayService(ILogger<SmsGatewayService> logger, ISmsGateway smsGateway)
    {
        this.logger = logger;
        this.smsGateway = smsGateway;
    }

    public Result<string> GetBalanceDetails()
    {
        Result<string> result = smsGateway.GetBalance();

        if (result.IsFailed)
        {
            logger.LogError("Error while trying to retrieve SMS balance");
            foreach (var error in result.Errors)
            {
                logger.LogError("{ErrorMessage}", error.Message);
            }
        }
        return smsGateway.GetBalance();
    }
}