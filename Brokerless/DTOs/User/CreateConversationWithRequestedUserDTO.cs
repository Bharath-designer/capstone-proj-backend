using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.User
{
    public class CreateConversationWithRequestedUserDTO
    {
        [Required]
        public int? PropertyId { get; set; }
        [Required]
        public int? UserId { get; set; }
    }
}
