using Arentheym.ParkingBarrier.Domain;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines methods available for the SMS gateway.
/// </summary>
public interface ISmsGateway
{
    void Send();

    void GetBalanceDetails();
}