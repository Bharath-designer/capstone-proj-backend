using Brokerless.DTOs.Property;
using Brokerless.DTOs.User;
using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface IUserService
    {
        public Task<User> CreateUser(string email, string userName, string profilePic);
        public Task ActivateSubscription(User user, SubscriptionTemplate subscriptionTemplate);
        public Task UpdateUserMobileNumber(int userId, UserMobileNumberUpdateDTO userMobileNumberUpdateDTO);
        public Task VerifyMobileNumber(int userId, OtpDTO otpDTO);
        public Task<CreateConversationReturnDTO> CreateConversationWithPropertyOwner(int userId, CreateConversationDTO createConversationDTO);
        public Task<List<ConversationListReturnDTO>> GetAllConversationForAUser(int userId, int pageNumber);
        public Task CreateMessageForConversation(int userId,CreateMessageDTO createMessageDTO);
        public Task<ChatMessagesReturnDTO> GetAllMessageForAConversation(int userId, int conversationId);
        public Task<ProfileDetailsDTO> GetUserProfileDetails(int userId);
        public Task<List<PropertyReturnDTO>> GetUserListings(int userId);
        public Task<PropertyReturnDTO> GetUserListedPropertyDetails(int userId, int propertyId);
        public Task<List<PropertyViewedUserReturnDTO>> GetViewedUsersOfProperty(int userId, int propertyId);
        public Task<CreateConversationReturnDTO> CreateConversationWithPropertyRequestedUser(int userId, CreateConversationWithRequestedUserDTO createConversationDTO);
        public Task<List<PropertyReturnDTO>> GetUserRequestedPropertyDetails(int userId);
    }
}
