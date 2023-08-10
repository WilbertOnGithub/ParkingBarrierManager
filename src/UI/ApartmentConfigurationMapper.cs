using System;
using System.Collections.Generic;
using System.Linq;
using Arentheym.ParkingBarrier.Domain;
using Arentheym.ParkingBarrier.UI.ViewModels;

namespace Arentheym.ParkingBarrier.UI;

public static class ManualMapper
{
    public static ApartmentConfigurationViewModel EntityToViewModel(
        ApartmentConfiguration apartmentConfiguration,
        IEnumerable<Intercom> intercoms)
    {
        ArgumentNullException.ThrowIfNull(apartmentConfiguration);
        ArgumentNullException.ThrowIfNull(intercoms);

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

        foreach (var intercom in intercoms)
        {
            if (apartmentConfiguration.Intercoms.Any(x => x.Id == intercom.Id))
            {
                result.Intercoms.Add(new IntercomViewModel
                {
                    Id = intercom.Id.Id,
                    Name = intercom.Name,
                    IsUsed = true
                });
            }
            else
            {
                result.Intercoms.Add(new IntercomViewModel
                {
                    Id = intercom.Id.Id,
                    Name = intercom.Name,
                    IsUsed = false
                });
            }
        }

        return result;
    }
}