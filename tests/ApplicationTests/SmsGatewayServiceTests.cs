using Arentheym.ParkingBarrier.Application;
using FluentResults;

namespace Arentheym;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class SmsGatewayServiceTests
{
    private readonly IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

    [Fact]
    public void SmsGatewayService_returns_failure_when_SmsGateway_fails()
    {
        // Arrange
        var smsGatewayMock = fixture.Freeze<ISmsGateway>();
        smsGatewayMock.GetBalance().Returns(Result.Fail(new List<IError>() { new Error("Error") }));
        var sut = fixture.Create<SmsGatewayService>();

        // Act
        var result = sut.GetBalanceDetails();

        // Assert
        result.IsFailed.Should().BeTrue("because the gateway failed");
    }

    [Fact]
    public void SmsGatewayService_returns_reasons_for_failure_when_SmsGateway_fails()
    {
        // Arrange
        var smsGatewayMock = fixture.Freeze<ISmsGateway>();
        List<IError> errors = new List<IError>() { new Error("Error") };
        smsGatewayMock.GetBalance().Returns(Result.Fail(errors));
        var sut = fixture.Create<SmsGatewayService>();

        // Act
        var result = sut.GetBalanceDetails();

        // Assert
        result.Errors.Should().BeEquivalentTo(errors, "because the gateway failed");
    }
}
