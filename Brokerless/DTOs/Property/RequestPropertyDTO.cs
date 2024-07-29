using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Property
{
    public class RequestPropertyDTO
    {
        [Required]
        public int? PropertyId { get; set; }
    }
}
