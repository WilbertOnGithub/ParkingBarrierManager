using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Arentheym.ParkingBarrier.UI.Validators;

public sealed partial class AccessCode : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (AccessCodeRegEx().IsMatch(value as string ?? string.Empty))
        {
            return ValidationResult.Success;
        }

        return new("Access code must be empty or a number between 1 and 6 digits.");
    }

    [GeneratedRegex(@"^[0-9]{1,6}$|^$", RegexOptions.Compiled | RegexOptions.Singleline)]
    private static partial Regex AccessCodeRegEx();
}
