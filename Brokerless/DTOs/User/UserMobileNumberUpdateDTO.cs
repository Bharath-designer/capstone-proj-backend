using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.User
{
    public class UserMobileNumberUpdateDTO
    {
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber {  get; set; }
        [Required]
        public string CountryCode { get; set; }
    }
}
