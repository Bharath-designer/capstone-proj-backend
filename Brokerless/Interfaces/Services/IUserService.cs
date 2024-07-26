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
        public Task CreateConversationWithPropertyOwner(int userId, CreateConversationDTO createConversationDTO);

    }
}
