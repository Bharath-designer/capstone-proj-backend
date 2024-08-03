using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Auth
{
    public class GoogleLoginDTO
    {
        [Required]
        public string? Token { get; set; }
    }
}
