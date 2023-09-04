using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Net.ProxyConfigurationInjector;
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
        Balance foo = client.Balance();
    }
}