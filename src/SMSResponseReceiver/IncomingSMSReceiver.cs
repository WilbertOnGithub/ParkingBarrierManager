using System.Diagnostics.CodeAnalysis;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arentheym.ParkingBarrier.SMSResponseReceiver;

public class IncomingSMSReceiver(ILogger<IncomingSMSReceiver> logger)
{
    [Function("IncomingSMSReceiver")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "post")] [NotNull]HttpRequest request)
    {
        using (var reader = new StreamReader(request.Body))
        {
            string body = reader.ReadToEnd();
            logger.LogInformation("Foo");
        }

        return new OkObjectResult("Welcome to Azure Functions!");
    }
}
