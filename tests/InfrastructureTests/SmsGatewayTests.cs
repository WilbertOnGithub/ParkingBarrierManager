using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.Infrastructure.SmsGateway;
using FluentAssertions;
using Xunit;

namespace Arentheym.ParkingBarrier.Infrastructure.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class SmsGatewayTests
{
    [Fact]
    public void ApartmentConfiguration_Is_Converted_To_Correct_SMS_Format()
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
}