using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Brokerless.Validations
{
    public class NoNullElementsAttribute : ValidationAttribute
    {
        public NoNullElementsAttribute(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item == null)
                    {
                        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }

}
