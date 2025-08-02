using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using FluentResults;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;

using Error = FluentResults.Error;

namespace Arentheym.ParkingBarrier.Infrastructure.SmsGateway;

public class MessageBirdGateway(SmsGatewayConfiguration configuration) : ISmsGateway
{
    private readonly Client client = Client.CreateDefault(configuration.ApiKey);

    public Result<string> SendSms(ApartmentConfiguration apartmentConfiguration, Intercom intercom)
    {
        ArgumentNullException.ThrowIfNull(apartmentConfiguration);
        ArgumentNullException.ThrowIfNull(intercom);

        long[] recipients = new long[1];
        recipients[0] = Convert.ToInt64(intercom.PhoneNumber.Number, CultureInfo.InvariantCulture);
        string sender = "MessageBird";
        string body = new UpdateConfigurationCommand(intercom.MasterCode, apartmentConfiguration).ToString();

        try
        {
            Message _ = client.SendMessage(sender, body, recipients);
            return Result.Ok(body);
        }
        catch (ErrorException ex)
        {
            IList<IError> errors = CompileErrorList(ex);
            errors.Insert(0, new Error($"Error while trying to send SMS for apartment {apartmentConfiguration.Id.Number} to {intercom.PhoneNumber.Number}"));
            return Result.Fail(errors);
        }
    }

    public Result<float> GetBalance()
    {
        try
        {
            Balance balance = client.Balance();

            float remainingCredits = float.Parse(balance.Amount, CultureInfo.InvariantCulture);
            return Result.Ok(remainingCredits);
        }
        catch (ErrorException ex)
        {
            return Result.Fail(CompileErrorList(ex));
        }
    }

    public float GetPricePerMessage()
    {
        return configuration.PricePerMessage;
    }

    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Performance not a concern"
    )]
    private static IList<IError> CompileErrorList(ErrorException ex)
    {
        var errors = new List<IError>();

        // Either the request fails with error descriptions from the endpoint.
        if (ex.HasErrors)
        {
            errors.AddRange(
                ex.Errors.Select(error => new FluentResults.Error(
                    $"Code: {error.Code} Description: {error.Description} Parameter: {error.Parameter}"
                ))
            );
        }

        // Or it fails without error information from the endpoint,
        // in which case the reason contains a 'best effort' description.
        if (ex.HasReason)
        {
            errors.Add(new FluentResults.Error(ex.Reason));
        }

        return errors;
    }
}
