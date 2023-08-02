using System.ComponentModel.DataAnnotations;
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

        return new("Phone number can be empty or a number of exactly 10 digits (ignoring whitespace).");
    }

    /// <summary>
    /// A regular expression that matches a phone number where the phone number is exactly 10 digits long.
    /// Whitespace in the number is allowed. The number can also be empty.
    /// </summary>
    [GeneratedRegex(@"^(?:\s*\d\s*){10}$|^$", RegexOptions.Compiled)]
    private static partial Regex PhoneNumberRegEx();
}