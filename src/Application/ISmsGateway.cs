using Arentheym.ParkingBarrier.Domain;
using FluentResults;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines methods available for the SMS gateway.
/// </summary>
public interface ISmsGateway
{
    IList<Result> SendSms(ApartmentConfiguration apartmentConfiguration);

    Result<string> GetBalance();
}
