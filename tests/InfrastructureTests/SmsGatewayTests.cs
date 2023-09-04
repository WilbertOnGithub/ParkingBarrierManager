using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Arentheym.ParkingBarrier.Infrastructure.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class SmsGatewayTests
{
    private readonly IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

    [Fact]
    public void ApartmentConfiguration_is_converted_to_correct_format()
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
    public void Gateway_can_retrieve_balance_with_development_api_key()
    {
        // Arrange
        var configurationMock = fixture.Freeze<SmsGatewayConfiguration>();
        configurationMock.ApiKey.Returns("B0TqcBYPhQWwSlTmyf3LRhQnN97k9rbY2o5Ct5cqr17qp9Zyct0ERGsUmZgNU1+S");

        var sut = fixture.Create<SmsGatewayService>();

        // Act
        sut.GetBalanceDetails();

        // Assert
    }
}