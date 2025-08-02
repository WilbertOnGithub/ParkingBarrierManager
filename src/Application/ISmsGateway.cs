using Arentheym.ParkingBarrier.Domain;
using FluentResults;

namespace Arentheym.ParkingBarrier.Application;

/// <summary>
/// Defines methods available for the SMS gateway.
/// </summary>
public interface ISmsGateway
{
    Result<string> SendSms(ApartmentConfiguration apartmentConfiguration, Intercom intercom);

    Result<float> GetBalance();

    float GetPricePerMessage();
}
