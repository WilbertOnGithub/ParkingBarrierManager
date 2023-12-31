﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Arentheym.ParkingBarrier.UI.Validators;

public sealed partial class PhoneNumber : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (PhoneNumberRegEx().IsMatch(value as string ?? string.Empty))
        {
            return ValidationResult.Success;
        }

        return new("Phone number can be empty or a country code followed by up to 15 digits.");
    }

    /// <summary>
    /// Regular expression for validating a phone number with a land code. Has the following parts:
    /// - Country code as defined in <see cref="https://nl.wikipedia.org/wiki/Lijst_van_landnummers_in_de_telefonie_op_nummervolgorde"/>
    /// - optional whitespace
    /// - Phone number consisting of minimum 1 and maximum 15 digits.
    ///
    /// Also matches on an empty string so an empty string can be considered as no phone number.
    /// </summary>
    [GeneratedRegex(@"^(?<CountryCode>([17]|2[07]|3[0-6]|39|4[0-1]|4[3-9]|5[1-8]|6[0-6]|8[12]|8[46]|9[0-5]|98)|(21[1-3]|21[68]|2[2-4][0-9]|25[0-8]|26[0-9]|29[01789])|(35[0-9]|37[0-9]|38[012356789])|(42[013])|(50[0-9]|59[0-9])|(67[023456789]|68[0-3]|68[5-9]|69[0-2])|(800|85[02356]|87[0-4]|88[06])|(96[0-8]|97[0-7]|99[2-6]|998)|(124[26]|126[48]|1284)|(1340|1345)|(1441|1473)|(1649|1664|167[01]|1684)|(1721|1758|1767|178[47])|(1809|186[89]|1876)|(1907|1939)|(2696)|(6723|881[67]))\s*(?<PhoneNumber>(\s*\d\s*){1,15})$|^\s*$", RegexOptions.Compiled)]
    private static partial Regex PhoneNumberRegEx();
}