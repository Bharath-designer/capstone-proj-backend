
using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.User
{
    public class OtpDTO
    {
        [Required]
        public string OTP { get; set; }
    }
}
