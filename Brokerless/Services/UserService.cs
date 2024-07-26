using Brokerless.Enums;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;
using Brokerless.Repositories;

namespace Brokerless.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionTemplateRepository _subscriptionTemplateRepository;

        public UserService(IUserRepository userRepository, ISubscriptionTemplateRepository subscriptionTemplateRepository) {
            _userRepository = userRepository;
            _subscriptionTemplateRepository = subscriptionTemplateRepository;
        }
        public async Task<User> CreateUser(string email, string userName, string profilePic)
        {

            SubscriptionTemplate template = await _subscriptionTemplateRepository.GetById("Free");
            UserSubscription userSubscription = new UserSubscription
            {
                AvailableListingCount = template.MaxListingCount,
                AvailableSellerViewCount = template.MaxSellerViewCount,
                ExpiresOn = null,
                SubscribedOn = DateTime.Now,
                SubscriptionTemplate = template,
            };

            var user = new User
            {
                Email = email,
                FullName = userName,
                ProfileUrl = profilePic,
                UserRole = UserRole.User,
                UserSubscription = userSubscription
            };

            await _userRepository.Add(user);
            return user;
        }

        public async Task ActivateSubscription(User user, SubscriptionTemplate subscriptionTemplate)
        {

            User userWithSubscription = await _userRepository.GetUserWithSubscription(user.UserId);
            UserSubscription userSubscription = userWithSubscription.UserSubscription;

            SubscriptionTemplate exisitingSubscription = await _subscriptionTemplateRepository.GetById(userSubscription.SubscriptionTemplateName);

            userSubscription.SubscribedOn = DateTime.Now;

            if (userSubscription.ExpiresOn > DateTime.Now || userSubscription.ExpiresOn == null)
            {
                if (userSubscription.AvailableListingCount == 0)
                {
                    userSubscription.AvailableListingCount = subscriptionTemplate.MaxListingCount;
                } else
                {
                    int carryForwardDays = CalculateCarryForwardLimit(userSubscription.ExpiresOn, userSubscription.AvailableListingCount, exisitingSubscription.Validity);
                    userSubscription.AvailableListingCount = userSubscription.AvailableListingCount + carryForwardDays;

                }

                if (userSubscription.AvailableSellerViewCount == 0)
                {
                    userSubscription.AvailableSellerViewCount = subscriptionTemplate.MaxSellerViewCount;
                }
                else
                {
                    int carryForwardDays = CalculateCarryForwardLimit(userSubscription.ExpiresOn, userSubscription.AvailableSellerViewCount, exisitingSubscription.Validity);

                    userSubscription.AvailableSellerViewCount = userSubscription.AvailableSellerViewCount + carryForwardDays;
                }
            } else
            {
                userSubscription.AvailableListingCount = subscriptionTemplate.MaxListingCount;
                userSubscription.AvailableSellerViewCount = subscriptionTemplate.MaxSellerViewCount;
            }

            userSubscription.ExpiresOn = DateTime.Now.AddDays((double)subscriptionTemplate.Validity);
            userSubscription.SubscriptionTemplate = subscriptionTemplate;

            await _userRepository.Update(userWithSubscription);

        }


        private int CalculateCarryForwardLimit(DateTime? expiresOn, int limitsRemaining, int? totalValidity)
        {
            if (expiresOn == null || totalValidity == null) return limitsRemaining;

            int daysRemaining = (expiresOn.Value - DateTime.Now).Days;

            double percentage = (double)((double)daysRemaining / totalValidity);

            percentage = percentage * 0.80; // Deduction 20%

            return (int)(limitsRemaining * percentage);
        }
    }
}
