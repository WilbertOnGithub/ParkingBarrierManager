using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.Application;

public class SmsGatewayService
{
    private readonly ILogger<SmsGatewayService> logger;
    private readonly ISmsGateway smsGateway;

    [SuppressMessage(
        "Design",
        "CA1062:Validate arguments of public methods",
        Justification = "Cannot be null due to dependency injection.")]
    public SmsGatewayService(
        ILogger<SmsGatewayService> logger,
        ISmsGateway smsGateway)
    {
        this.logger = logger;
        this.smsGateway = smsGateway;
    }

    public void GetBalanceDetails()
    {
        smsGateway.GetBalanceDetails();
    }
}