using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using FluentResults;
using Xunit;

namespace Arentheym.ParkingBarrier.Infrastructure.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class MessageBirdGatewayTests
{
    //private readonly IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

    [Fact]
    public void ApartmentConfiguration_is_converted_to_correct_sms_format()
    {
        // Arrange
        var configuration = new ApartmentConfiguration(
                new ApartmentId(51),
                new MemoryLocation(51),
                "display name",
                true,
                new AccessCode("0000"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, "1234567890"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Secondary, "2234567890"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Tertiary, "3234567890"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Quaternary, "4234567890"));

        var command = new UpdateConfigurationCommand(new MasterCode("1234"), configuration);

        // Act
        string result = command.ToString();

        // Assert
        result.Should().Be("1234MEM051\"1234567890\",\"2234567890\",\"3234567890\",\"4234567890\",\"51\",\"0000\",1,0,\"display>name\"");
    }

    [Fact]
    public void MessageBird_can_retrieve_balance_with_valid_key()
    {
        // Arrange
        var sut = new MessageBirdGateway(GetDevelopmentApiKey());

        // Act
        Result<string> balance = sut.GetBalance();

        // Assert
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().Be("0 credits");
    }

    [Fact]
    public void MessageBird_cannot_retrieve_balance_with_invalid_key()
    {
        // Arrange
        string invalidKey = Guid.NewGuid().ToString();
        var sut = new MessageBirdGateway(invalidKey);

        // Act
        Result<string> balance = sut.GetBalance();

        // Assert
        balance.IsFailed.Should().BeTrue();
        balance.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void MessageBird_can_send_sms_with_valid_key()
    {
        // Arrange
        var sut = new MessageBirdGateway(GetDevelopmentApiKey());
        var apartmentConfiguration = new ApartmentConfiguration(new ApartmentId(131));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, "311234567890"));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Secondary, string.Empty));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Tertiary, string.Empty));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Quaternary, string.Empty));
        apartmentConfiguration.LinkIntercom(new Intercom("voor", new PhoneNumber("3113739851"), new MasterCode("1111")));

        // Act
        IList<Result> result = sut.SendSms(apartmentConfiguration);


    }

    /// <summary>
    /// Use this API Key in your test environment. It will return a response but
    /// not work in production or deduct balance.
    /// </summary>
    private static string GetDevelopmentApiKey()
    {
        return "jKPmLsisXNsnqOV1RifH3jQwt";
    }
}