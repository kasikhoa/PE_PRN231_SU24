using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE
{
    public class BeginWithCapitalLetterAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value?.ToString();
            string pattern = @"^(?:[A-Z][a-zA-Z0-9]*)(?: [A-Z][a-zA-Z0-9]*)*$";

            var rs = Regex.IsMatch(name ?? string.Empty, pattern);
            return rs ? ValidationResult.Success : new ValidationResult("PaintingName is not valid!!!");
        }
    }
}
