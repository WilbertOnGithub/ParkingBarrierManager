using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Arentheym.ParkingBarrier.UI.ViewModels;
using FluentAssertions;
using Xunit;

namespace Arentheym.ParkingBarrier.UI.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
public class EqualityTests
{
    [Fact]
    public void IntercomViewModels_are_equal_with_same_values()
    {
        // Arrange
        var id = Guid.NewGuid();

        var left = new IntercomViewModel
        {
            Id = id,
            Name = string.Empty,
            IsUsed = false,
        };

        var right = new IntercomViewModel
        {
            Id = id,
            Name = string.Empty,
            IsUsed = false,
        };

        // Act / Assert
        left.Should().Be(right, "because they have the same values");
    }

    [Fact]
    public void IntercomViewModels_are_not_equal_with_different_values()
    {
        // Arrange
        var left = new IntercomViewModel
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            IsUsed = false,
        };

        var right = new IntercomViewModel
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            IsUsed = false,
        };

        // Act / Assert
        left.Should().NotBe(right, "because they have different values");
    }

    [Fact]
    public void IntercomViewModels_are_not_equal_when_one_object_is_null()
    {
        // Arrange
        var left = new IntercomViewModel
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            IsUsed = false,
        };

        // Act / Assert
        left.Should().NotBe(null, "because one object is null");
    }

    [Fact]
    public void ApartmentConfigurationViewModels_are_equal_with_same_values()
    {
        // Arrange
        var id = Guid.NewGuid();

        var left = new ApartmentConfigurationViewModel
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
                new IntercomViewModel()
                {
                    Id = id,
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };

        var right = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>
            {
                new()
                {
                    Id = id,
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };

        // Act / Assert
        left.Should().Be(right, "because they have the same values");
    }

    [Fact]
    public void ApartmentConfigurationViewModels_are_not_equal_when_one_object_is_null()
    {
        // Arrange
        var left = new ApartmentConfigurationViewModel
        {
            AccessCode = string.Empty,
            ApartmentNumber = 131,
            DisplayName = string.Empty,
            DialToOpen = false,
            PrimaryPhoneNumber = string.Empty,
            SecondaryPhoneNumber = string.Empty,
            TertiaryPhoneNumber = string.Empty,
            QuaternaryPhoneNumber = string.Empty,
            Intercoms = new ObservableCollection<IntercomViewModel>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    IsUsed = true,
                    Name = string.Empty,
                },
            },
        };

        // Act / Assert
        left.Should().NotBe(null, "because one object is null");
    }

    [Fact]
    public void ApartmentConfigurationViewModels_are_not_equal_with_different_values()
    {
        // Arrange
        var left = new ApartmentConfigurationViewModel
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

        var right = new ApartmentConfigurationViewModel
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

        // Act / Assert
        left.Should().NotBe(right, "because they have different values");
    }
}
