using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Arentheym.ParkingBarrier.Application;

public class SmsGatewayService
{
    private readonly ILogger<SmsGatewayService> logger;
    private readonly string apiKey;

    [SuppressMessage(
        "Design",
        "CA1062:Validate arguments of public methods",
        Justification = "Cannot be null due to dependency injection.")]
    public SmsGatewayService(
        ILogger<SmsGatewayService> logger,
        SmsGatewayConfiguration configuration,
        Encryptor encryptor)
    {
        this.logger = logger;

        // Decrypt the API key.
        apiKey = encryptor.Decrypt(configuration.ApiKey);
    }
}