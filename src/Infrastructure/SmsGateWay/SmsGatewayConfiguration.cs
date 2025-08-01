namespace Arentheym.ParkingBarrier.Infrastructure.SmsGateway;

public class SmsGatewayConfiguration
{
    public string ApiKey { get; set; } = string.Empty;

    public float PricePerMessage { get; set; }
}
