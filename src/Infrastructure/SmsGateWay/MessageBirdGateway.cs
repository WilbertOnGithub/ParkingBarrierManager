using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using FluentResults;
using MessageBird;
using MessageBird.Exceptions;
using MessageBird.Objects;

namespace Arentheym.ParkingBarrier.Infrastructure.SmsGateway;

public class MessageBirdGateway(string apiKey) : ISmsGateway
{
    private readonly Client client = Client.CreateDefault(apiKey);

    public IList<Result> SendSms(ApartmentConfiguration apartmentConfiguration)
    {
        ArgumentNullException.ThrowIfNull(apartmentConfiguration);
        List<Result> results = new();

        // Create a separate SMS for each intercom.
        foreach (var intercom in apartmentConfiguration.Intercoms)
        {
            long[] recipients = [Convert.ToInt64(intercom.PhoneNumber.Number, CultureInfo.InvariantCulture)];
            string sender = "MessageBird";
            string body = new UpdateConfigurationCommand(
                intercom.MasterCode, apartmentConfiguration).ToString();

            try
            {
                Message message = client.SendMessage(sender, body, recipients);
                results.Add(Result.Ok());
            }
            catch (ErrorException ex)
            {
                results.Add(Result.Fail(CompileErrorList(ex)));
            }
        }

        return results;
    }

    public Result<string> GetBalance()
    {
        try
        {
            Balance balance = client.Balance();
            return Result.Ok<string>($"{balance.Amount} {balance.Type}");
        }
        catch (ErrorException ex)
        {
            return Result.Fail(CompileErrorList(ex));
        }
    }

    [SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance",
        Justification = "Performance not a concern")]
    private static IList<IError> CompileErrorList(ErrorException ex)
    {
        var errors = new List<IError>();

        // Either the request fails with error descriptions from the endpoint.
        if (ex.HasErrors)
        {
            errors.AddRange(ex.Errors.Select(
                error => new FluentResults.Error($"Code: {error.Code} Description: {error.Description} Parameter: {error.Parameter}")));
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