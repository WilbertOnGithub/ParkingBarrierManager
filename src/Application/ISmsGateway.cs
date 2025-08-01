using Arentheym.ParkingBarrier.Domain;
using FluentResults;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines methods available for the SMS gateway.
/// </summary>
public interface ISmsGateway
{
    Result SendSms(ApartmentConfiguration apartmentConfiguration);

    Result<float> GetBalance();
}
