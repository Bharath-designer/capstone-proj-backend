using System.ComponentModel.DataAnnotations;

namespace Brokerless.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }
        public int UserId { get; set; } // Foreign Key
        public List<Chat> Chats { get; set; }
        public User User { get; set; }

    }
}
