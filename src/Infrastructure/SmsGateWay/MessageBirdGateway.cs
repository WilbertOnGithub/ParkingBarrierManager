using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
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

    public void Send()
    {
        throw new NotImplementedException();
    }

    public void GetBalanceDetails()
    {
        try
        {
            Balance foo = client.Balance();
        }
        catch (ErrorException e)
        {
            // Either the request fails with error descriptions from the endpoint.
            if (e.HasErrors)
            {
                /*
                foreach (Error error in e.Errors)
                {
                    Console.WriteLine("code: {0} description: '{1}' parameter: '{2}'", error.Code, error.Description, error.Parameter);
                }
                */
            }

            // or fails without error information from the endpoint, in which case the reason contains a 'best effort' description.
            if (e.HasReason)
            {
                Console.WriteLine(e.Reason);
            }
        }
    }
}