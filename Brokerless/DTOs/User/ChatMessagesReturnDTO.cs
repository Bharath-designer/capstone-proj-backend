namespace Brokerless.DTOs.User
{
    
    public class ChatMessagesReturnDTO
    {
        public ConversationListReturnDTO ConversationDetails { get; set; }
        public List<ChatDetailsDTO> Chats { get; set; }
    }
}
