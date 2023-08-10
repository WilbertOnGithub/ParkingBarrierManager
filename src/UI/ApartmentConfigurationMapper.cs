using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

    public static ApartmentConfiguration ViewModelToEntity(
        ApartmentConfigurationViewModel viewModel,
        IEnumerable<Intercom> intercoms)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        ArgumentNullException.ThrowIfNull(intercoms);

        var result = new ApartmentConfiguration(
            new ApartmentId(viewModel.ApartmentNumber),
            new MemoryLocation((short)viewModel.ApartmentNumber),
            viewModel.DisplayName,
            dialToOpen: viewModel.DialToOpen,
            new AccessCode(viewModel.AccessCode));

        result.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Primary, viewModel.PrimaryPhoneNumber));
        result.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Secondary, viewModel.SecondaryPhoneNumber));
        result.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Tertiary, viewModel.TertiaryPhoneNumber));
        result.UpsertPhoneNumber(new DivertPhoneNumber(DivertOrder.Quaternary, viewModel.QuaternaryPhoneNumber));

        foreach (var intercom in intercoms)
        {
            if (viewModel.Intercoms.Any(x => x.Id == intercom.Id.Id && x.IsUsed))
            {
                result.LinkIntercom(intercom);
            }
        }

        return result;
    }
}