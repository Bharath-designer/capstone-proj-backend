using Brokerless.DTOs.User;
using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface IConversationRepository:IBaseRepository<Conversation, int>
    {
        public Task<Conversation> GetConversationWithUserWithId(int userId, int otherUserId);
        public Task<List<ConversationListReturnDTO>> GetUserConversation(int userId, int pageNumber);
        public Task<Conversation> GetUserConversationById(int userId, int conversationId);
        public Task<ChatMessagesReturnDTO> GetConversationMessages(int userId, int conversationId);

    }
}
