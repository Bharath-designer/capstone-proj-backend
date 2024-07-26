using Brokerless.DTOs.User;
using Brokerless.Enums;
using Brokerless.Exceptions;
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
        private readonly IPropertyRepository _propertyRepository;

        public UserService(IUserRepository userRepository, ISubscriptionTemplateRepository subscriptionTemplateRepository, IPropertyRepository propertyRepository) {
            _userRepository = userRepository;
            _subscriptionTemplateRepository = subscriptionTemplateRepository;
            _propertyRepository = propertyRepository;
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

        public async Task UpdateUserMobileNumber(int userId, UserMobileNumberUpdateDTO userMobileNumberUpdateDTO)
        {
            User user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            user.PhoneNumber = userMobileNumberUpdateDTO.PhoneNumber;
            user.CountryCode  = userMobileNumberUpdateDTO.CountryCode;
            await _userRepository.Update(user);
        }

        public async Task VerifyMobileNumber(int userId, OtpDTO otpDTO)
        {
            User user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            string VALID_OTP = "0000";

            if (otpDTO.OTP != VALID_OTP)
            {
                throw new InvalidOTPException();
            }

            user.PhoneNumberVerified = true;
            await _userRepository.Update(user); 
        }

        public async Task CreateConversationWithPropertyOwner(int userId, CreateConversationDTO createConversationDTO)
        {
            PropertyUserViewed propertyViewedByUser = await _propertyRepository.GetPropertyWithViewedUserById(userId, (int)createConversationDTO.PropertyId);
            
            if (propertyViewedByUser == null)
            {
                throw new PropertyDetailsNotRequestedException();
            }
            
            Property property = propertyViewedByUser.Property;

            await Console.Out.WriteLineAsync(property.City);

        }
    }
}
