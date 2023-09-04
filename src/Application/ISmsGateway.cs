using FluentResults;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines methods available for the SMS gateway.
/// </summary>
public interface ISmsGateway
{
    Result Send();

    Result<string> GetBalance();
}