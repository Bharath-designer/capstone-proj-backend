using Brokerless.Context;
using Brokerless.DTOs.Property;
using Brokerless.DTOs.User;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Repositories
{
    public class ConversationRepository : BaseRepository<Conversation, int> , IConversationRepository
    {
        public ConversationRepository(BrokerlessDBContext _context) : base(_context)
        {

        }

        public async Task<ChatMessagesReturnDTO> GetConversationMessages(int userId, int conversationId)
        {
            ChatMessagesReturnDTO? chats = await _context.Conversations
                .Where(c=>c.ConversationId == conversationId)
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .Select(c=> new ChatMessagesReturnDTO
                {
                    ConversationDetails = new ConversationListReturnDTO
                    {
                        ConversationId = c.ConversationId,
                        ConversationName = c.Users[0].UserId == userId ? c.Users[1].FullName : c.Users[0].FullName,
                        ConversationProfilePic = c.Users[0].UserId == userId ? c.Users[1].ProfileUrl : c.Users[0].ProfileUrl,
                        LastUpdated = c.LastUpdatedOn,
                        HasUnreadMessage = c.HasUnreadMessage ? c.LastConversationBy != userId : false
                    },
                    Chats = c.Chats.Select(ch=> new ChatDetailsDTO
                    {
                        Message = ch.Message,
                        CreatedOn = ch.CreatedOn,
                        IsSentByUser = ch.UserId == userId,
                    })
                    .OrderByDescending(c=>c.CreatedOn)
                    .ToList()
                })
                .FirstOrDefaultAsync();
            return chats;
        }

        public async Task<Conversation> GetConversationWithUserWithId(int userId, int otherUserId)
        {

            Conversation? conversation = await _context.Conversations
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .Where(c => c.Users.Any(u => u.UserId == otherUserId))
                .FirstOrDefaultAsync();

            return conversation;
        }

        public async Task<List<ConversationListReturnDTO>> GetUserConversation(int userId, int pageNumber)
        {
            int PAGE_SIZE = 5;

            if (pageNumber <=0) pageNumber = 1;

            List<ConversationListReturnDTO> conversationListReturnDTOs = await _context.Conversations
                .Where(c => c.Users.Any(u => u.UserId == userId))
                .Select(c => new ConversationListReturnDTO
                {
                    ConversationId = c.ConversationId,
                    ConversationName = c.Users[0].UserId == userId ? c.Users[1].FullName : c.Users[0].FullName,
                    ConversationProfilePic = c.Users[0].UserId == userId ? c.Users[1].ProfileUrl : c.Users[0].ProfileUrl,
                    LastUpdated = c.LastUpdatedOn,
                    HasUnreadMessage = c.HasUnreadMessage ? c.LastConversationBy != userId : false
                })
                .OrderByDescending(c=>c.LastUpdated)
                .Skip((pageNumber - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToListAsync();


            return conversationListReturnDTOs;
        }

        public async Task<Conversation> GetUserConversationById(int userId, int conversationId)
        {
            Conversation? conversation = await _context.Conversations
                .Where(c => c.Users.Any(u => u.UserId == userId) && c.ConversationId == conversationId)
                .FirstOrDefaultAsync();
            return conversation;
        }
    }
}
