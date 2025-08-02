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
    public async Task When_MainWindowViewModel_is_initially_loaded_we_have_no_dirty_configuration()
    {
        // Arrange
        var repositoryMock = fixture.Freeze<IRepository>();
        repositoryMock.GetApartmentConfigurationsAsync().Returns(Task.FromResult(GetDefaultConfigurations()));
        repositoryMock.GetIntercomsAsync().Returns(Task.FromResult(GetDefaultIntercoms()));

        var sut = new MainViewModel(fixture.Create<DataService>());
        await sut.Initialization;

        // Act // Assert
        sut.IsDirty.Should().Be(false, "because no changes have been made yet.");
    }

    [Fact]
    public async Task When_MainWindowViewModel_is_changed_the_save_button_is_enabled()
    {
        // Arrange
        var repositoryMock = fixture.Freeze<IRepository>();
        repositoryMock.GetApartmentConfigurationsAsync().Returns(Task.FromResult(GetDefaultConfigurations()));
        repositoryMock.GetIntercomsAsync().Returns(Task.FromResult(GetDefaultIntercoms()));

        var sut = new MainViewModel(fixture.Create<DataService>());
        await sut.Initialization;

        // Act
        sut.Configurations.First().AccessCode = "1234";

        // Assert
        sut.IsDirty.Should().BeTrue("because changes have been made to the list.");
    }

    [Fact]
    public async Task When_MainWindowViewModel_is_changed_back_to_the_original_the_save_button_is_disabled_again()
    {
        // Arrange
        var repositoryMock = fixture.Freeze<IRepository>();
        repositoryMock.GetApartmentConfigurationsAsync().Returns(Task.FromResult(GetDefaultConfigurations()));
        repositoryMock.GetIntercomsAsync().Returns(Task.FromResult(GetDefaultIntercoms()));

        var sut = new MainViewModel(fixture.Create<DataService>());
        await sut.Initialization;

        // Act
        bool beforeChange = sut.IsDirty;
        sut.Configurations.First().AccessCode = "1234";
        bool afterChange = sut.IsDirty;
        sut.Configurations.First().AccessCode = string.Empty;
        bool afterRevertingChange = sut.IsDirty;

        // Assert
        beforeChange.Should().BeFalse("because no changes have (yet) been made to the list.");
        afterChange.Should().BeTrue("because changes have been made to the list.");
        afterRevertingChange.Should().BeFalse("because the change has been reverted to the original.");
    }

    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Performance not critical"
    )]
    private static IList<ApartmentConfiguration> GetDefaultConfigurations()
    {
        var configurations = new List<ApartmentConfiguration>();
        var apartmentConfiguration = new ApartmentConfiguration(new ApartmentId(51));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, string.Empty));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Secondary, string.Empty));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Tertiary, string.Empty));
        apartmentConfiguration.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Quaternary, string.Empty));
        configurations.Add(apartmentConfiguration);

        return configurations;
    }

    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Performance not critical"
    )]
    private static IList<Intercom> GetDefaultIntercoms()
    {
        var intercoms = new List<Intercom>
        {
            new("voor", new PhoneNumber("1234567890"), new MasterCode("1111")),
            new("achter", new PhoneNumber("1234567890"), new MasterCode("1111"))
        };

        return intercoms;
    }
}
