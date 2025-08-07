using System.Diagnostics.CodeAnalysis;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Azure.Messaging.ServiceBus;

namespace Arentheym.ParkingBarrier.SMSResponseReceiver;

public class IncomingSMSReceiver(ILogger<IncomingSMSReceiver> logger, ServiceBusClient serviceBusClient)
{
    [Function("IncomingSMSReceiver")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] [NotNull]HttpRequest request)
    {
        using (var reader = new StreamReader(request.Body))
        {
            string body = await reader.ReadToEndAsync();
            await SendToServiceBus(body);
        }

        return new OkResult();
    }

    private async Task SendToServiceBus(string body)
    {
        var queueName = Environment.GetEnvironmentVariable("ServiceBusQueueName") ?? "sms-messages";
        await using var sender = serviceBusClient.CreateSender(queueName);

        var message = new ServiceBusMessage(body)
        {
            ContentType = "application/json",
            MessageId = Guid.NewGuid().ToString()
        };
        message.ApplicationProperties.Add("Timestamp", DateTimeOffset.UtcNow);

        try
        {
            await sender.SendMessageAsync(message);
        }
        catch (ServiceBusException ex)
        {
            logger.LogError(ex, "Failed to send message {Body} to Service Bus", body);
        }
    }
}
