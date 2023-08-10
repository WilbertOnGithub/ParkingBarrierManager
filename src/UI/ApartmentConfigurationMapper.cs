using System;
using System.Linq;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.UI.ViewModels;
using Riok.Mapperly.Abstractions;

namespace Arentheym.ParkingBarrier.UI;

/*
[Mapper]
public partial class ApartmentConfigurationMapper
{
    [MapProperty(nameof(apartmentConfiguration.Id.Number), nameof(ApartmentConfigurationViewModel.ApartmentNumber))]
    public partial ApartmentConfigurationViewModel EntityToViewModel(ApartmentConfiguration apartmentConfiguration);
}
*/

public static class ManualMapper
{
    public static ApartmentConfigurationViewModel EntityToViewModel(ApartmentConfiguration apartmentConfiguration)
    {
        ArgumentNullException.ThrowIfNull(apartmentConfiguration);

        var result = new ApartmentConfigurationViewModel
        {
            ApartmentNumber = apartmentConfiguration.Id.Number,
            DisplayName = apartmentConfiguration.DisplayName,
            DialToOpen = apartmentConfiguration.DialToOpen,
            AccessCode = apartmentConfiguration.AccessCode.Code,
            PrimaryPhoneNumber = apartmentConfiguration.PhoneNumbers.First(x => x.Order == DivertOrder.Primary).Number,
            SecondaryPhoneNumber =
                apartmentConfiguration.PhoneNumbers.First(x => x.Order == DivertOrder.Secondary).Number,
            TertiaryPhoneNumber =
                apartmentConfiguration.PhoneNumbers.First(x => x.Order == DivertOrder.Tertiary).Number,
            QuaternaryPhoneNumber =
                apartmentConfiguration.PhoneNumbers.First(x => x.Order == DivertOrder.Quaternary).Number
            };

        return result;
    }
}