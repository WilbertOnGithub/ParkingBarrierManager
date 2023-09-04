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
        return smsGateway.GetBalance();
    }
}