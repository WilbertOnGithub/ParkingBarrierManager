using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arentheym.ParkingBarrier.Infrastructure.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class MessageBirdGatewayTests
{
    [Fact]
    public void ApartmentConfiguration_is_converted_to_correct_sms_format()
    {
        // Arrange
        var configuration = new ApartmentConfiguration(
            new ApartmentId(51),
            new MemoryLocation(51),
            "display name",
            true,
            new AccessCode("0000")
        );
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, "1234567890"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Secondary, "2234567890"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Tertiary, "3234567890"));
        configuration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Quaternary, "4234567890"));

        var command = new UpdateConfigurationCommand(new MasterCode("1234"), configuration);

        // Act
        string result = command.ToString();

        // Assert
        result
            .Should()
            .Be(
                "1234MEM051\"1234567890\",\"2234567890\",\"3234567890\",\"4234567890\",\"51\",\"0000\",1,0,\"display>name\""
            );
    }

    [SkippableFact]
    public void MessageBird_can_retrieve_balance_with_valid_key()
    {
        Skip.If(string.IsNullOrEmpty(GetDevelopmentApiKey().ApiKey));

        // Arrange
        var sut = new MessageBirdGateway(GetDevelopmentApiKey());

        // Act
        Result<float> balance = sut.GetBalance();

        // Assert
        balance.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void MessageBird_cannot_retrieve_balance_with_invalid_key()
    {
        // Arrange
        var sut = new MessageBirdGateway(
            new SmsGatewayConfiguration()
            {
                ApiKey = Guid.NewGuid().ToString(), // Create invalid key
            }
        );

        // Act
        Result<float> balance = sut.GetBalance();

        // Assert
        balance.IsFailed.Should().BeTrue();
        balance.Errors.Count.Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Try to retrieve API key that should be injected using environment variables.
    /// </summary>
    /// <remarks>
    /// When adding a configuration as an environment variable <see cref="SmsGatewayConfiguration"/>
    /// you need the syntax 'SmsGatewayConfiguration:ApiKey=value'.
    /// </remarks>
    private static SmsGatewayConfiguration GetDevelopmentApiKey()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

        var smsGatewayConfiguration = new SmsGatewayConfiguration();
        IConfigurationSection smsGatewayConfigurationSection = configuration.GetSection(
            nameof(smsGatewayConfiguration)
        );
        smsGatewayConfigurationSection.Bind(smsGatewayConfiguration);

        return smsGatewayConfiguration;
    }
}
