using System.ComponentModel.DataAnnotations;

namespace Brokerless.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }
        public List<Chat> Chats { get; set; }
        public List<User> Users { get; set; }
        public bool HasUnreadMessage { get; set; } = true;
        public DateTime LastUpdatedOn { get; set; }
        public int LastConversationBy { get; set; }

    }
}
