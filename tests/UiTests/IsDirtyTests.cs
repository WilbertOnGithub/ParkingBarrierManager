using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.UI.ViewModels;
using FluentAssertions;
using Xunit;

namespace Arentheym.ParkingBarrier.UI.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class IsDirtyTests
{
    [Fact]
    public void ApartmentConfigurationViewModel_is_not_dirty_after_calling_setoriginal()
    {
        // Arrange
        var sut = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };

        // Act
        sut.SetOriginal();

        // / Assert
        sut.IsDirty.Should().BeFalse("because the object has not changed.");
    }

    [Fact]
    public void ApartmentConfigurationViewModel_changing_property_triggers_isdirty()
    {
        // Arrange
        var sut = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };
        sut.SetOriginal();

        // Act
        sut.AccessCode = "1234";

        // / Assert
        sut.IsDirty.Should().BeTrue("because properties have changed.");
    }

    [Fact]
    public void ApartmentConfigurationViewModel_changing_nested_viewmodel_triggers_isdirty()
    {
        // Arrange
        var sut = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };
        sut.SetOriginal();

        // Act
        sut.Intercoms[0].IsUsed = false;

        // / Assert
        sut.IsDirty.Should().BeTrue("because properties have changed.");
    }

    [Fact]
    public void ApartmentConfigurationViewModel_restoring_property_to_original_restores_isdirty()
    {
        // Arrange
        var sut = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };
        sut.SetOriginal();

        // Act
        sut.AccessCode = "1234";
        sut.AccessCode = string.Empty;

        // / Assert
        sut.IsDirty.Should().BeFalse("because the changed property has reverted to the original.");
    }

    [Fact]
    public void ApartmentConfigurationViewModel_isdirty_throws_exception_when_setoriginal_not_called()
    {
        // Arrange
        var sut = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };

        // Act
        bool exceptionThrown = false;
        try
        {
            _ = sut.IsDirty;
        }
        catch (InvalidOperationException)
        {
            exceptionThrown = true;
        }

        // / Assert
        exceptionThrown
            .Should()
            .BeTrue($"because {nameof(sut.IsDirty)} was called without calling {nameof(sut.SetOriginal)} first.");
    }
}
