using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.User
{
    public class CreateMessageDTO
    {
        [Required]
        public int? ConversationId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
