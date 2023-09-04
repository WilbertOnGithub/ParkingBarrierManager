using Arentheym.ParkingBarrier.Application;
using FluentResults;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;

namespace Arentheym.ParkingBarrier.Infrastructure.SmsGateway;

public class MessageBirdGateway : ISmsGateway
{
    private readonly Client client;

    public MessageBirdGateway(string apiKey)
    {
        client = Client.CreateDefault(apiKey);
    }

    public Result Send()
    {
        return Result.Ok();
    }

    public Result<string> GetBalance()
    {
        try
        {
            Balance balance = client.Balance();
            return Result.Ok<string>($"{balance.Type} {balance.Amount}");
        }
        catch (ErrorException e)
        {
            var errors = new List<IError>();

            // Either the request fails with error descriptions from the endpoint.
            if (e.HasErrors)
            {
                foreach (MessageBird.Objects.Error error in e.Errors)
                {
                    errors.Add(new FluentResults.Error($"Code: {error.Code} Description: {error.Description} Parameter: {error.Parameter}"));
                }
            }

            // Or it fails without error information from the endpoint,
            // in which case the reason contains a 'best effort' description.
            if (e.HasReason)
            {
                errors.Add(new FluentResults.Error(e.Reason));
            }

            return Result.Fail(errors);
        }
    }
}