using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Admin
{
    public class PropertyApprovalToggleDTO
    {
        [Required]
        public int? PropertyId { get; set; }
        [Required]
        public bool? IsApproved { get; set; }
    }
}
