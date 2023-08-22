using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Xunit;
using FluentAssertions;

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
                    Name = string.Empty
                }
            }
        };

        // Act
        sut.SetOriginal();

        // / Assert
        sut.IsDirty.Should().BeFalse("because the object has not been changed");
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
                    Name = string.Empty
                }
            }
        };
        sut.SetOriginal();

        // Act
        sut.AccessCode = "1234";

        // / Assert
        sut.IsDirty.Should().BeTrue("because properties have been changed");
    }
}