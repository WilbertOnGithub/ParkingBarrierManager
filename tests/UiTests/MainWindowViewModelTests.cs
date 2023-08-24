using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.Application;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.UI.ViewModels;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Arentheym.ParkingBarrier.UI.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class MainWindowViewModelTests
{
    private readonly IFixture fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

    [Fact]
    public void When_MainWindowViewModel_is_initially_loaded_we_have_no_dirty_configuration()
    {
        // Arrange
        var repositoryMock = fixture.Freeze<IRepository>();
        repositoryMock.GetApartmentConfigurationsAsync().Returns(Task.FromResult(GetDefaultConfigurations()));
        repositoryMock.GetIntercomsAsync().Returns(Task.FromResult(GetDefaultIntercoms()));

        var sut = new MainWindowViewModel(fixture.Create<DataService>());

        // Act // Assert
        sut.NumberOfDirtyConfigurations.Should().Be(0, "because no changes have been made yet.");
    }

    [Fact]
    public void When_MainWindowViewModel_is_initially_loaded_save_button_is_disabled()
    {
        // Arrange
        var repositoryMock = fixture.Freeze<IRepository>();
        repositoryMock.GetApartmentConfigurationsAsync().Returns(Task.FromResult(GetDefaultConfigurations()));
        repositoryMock.GetIntercomsAsync().Returns(Task.FromResult(GetDefaultIntercoms()));

        var sut = new MainWindowViewModel(fixture.Create<DataService>());

        // Act // Assert
        sut.ButtonEnabled.Should().BeFalse("because no changes have been made yet.");
    }

    private static IList<ApartmentConfiguration> GetDefaultConfigurations()
    {
        IList<ApartmentConfiguration> configurations = new List<ApartmentConfiguration>();
        configurations.Add(new ApartmentConfiguration(new ApartmentId(51)));

        return configurations;
    }

    private static IList<Intercom> GetDefaultIntercoms()
    {
        IList<Intercom> intercoms = new List<Intercom>();
        intercoms.Add(new Intercom("voor", new PhoneNumber("1234567890"), new MasterCode("1111")));
        intercoms.Add(new Intercom("achter", new PhoneNumber("1234567890"), new MasterCode("1111")));

        return intercoms;
    }
}