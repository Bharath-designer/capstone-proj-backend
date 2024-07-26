using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.User
{
    public class CreateConversationDTO
    {
        [Required]
        public int? PropertyId { get; set; }
    }
}
