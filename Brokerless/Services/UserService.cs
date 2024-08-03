using Brokerless.DTOs.Property;
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
        private readonly IConversationRepository _conversationRepository;

        public UserService(IUserRepository userRepository, ISubscriptionTemplateRepository subscriptionTemplateRepository,
            IPropertyRepository propertyRepository,
            IConversationRepository conversationRepository
            )
        {
            _userRepository = userRepository;
            _subscriptionTemplateRepository = subscriptionTemplateRepository;
            _propertyRepository = propertyRepository;
            _conversationRepository = conversationRepository;
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
                }
                else
                {
                    int carryForwardDays = CalculateCarryForwardLimit(userSubscription.ExpiresOn, userSubscription.AvailableListingCount, exisitingSubscription.Validity);
                    userSubscription.AvailableListingCount = subscriptionTemplate.MaxListingCount + carryForwardDays;

                }

                if (userSubscription.AvailableSellerViewCount == 0)
                {
                    userSubscription.AvailableSellerViewCount = subscriptionTemplate.MaxSellerViewCount;
                }
                else
                {
                    int carryForwardDays = CalculateCarryForwardLimit(userSubscription.ExpiresOn, userSubscription.AvailableSellerViewCount, exisitingSubscription.Validity);

                    userSubscription.AvailableSellerViewCount = subscriptionTemplate.MaxSellerViewCount + carryForwardDays;
                }
            }
            else
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
            user.PhoneNumberVerified = false;
            user.PhoneNumber = userMobileNumberUpdateDTO.PhoneNumber;
            user.CountryCode = userMobileNumberUpdateDTO.CountryCode;
            await _userRepository.Update(user);
        }

        public async Task VerifyMobileNumber(int userId, OtpDTO otpDTO)
        {
            User user = await _userRepository.GetById(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.PhoneNumber == null)
            {
                throw new MobileNumberNotAdded();
            }

            string VALID_OTP = "0000";

            if (otpDTO.OTP != VALID_OTP)
            {
                throw new InvalidOTPException();
            }

            user.PhoneNumberVerified = true;
            await _userRepository.Update(user);
        }

        public async Task<CreateConversationReturnDTO> CreateConversationWithPropertyOwner(int userId, CreateConversationDTO createConversationDTO)
        {
            PropertyUserViewed propertyViewedByUser = await _propertyRepository.GetPropertyWithViewedUserById(userId, (int)createConversationDTO.PropertyId);

            if (propertyViewedByUser == null)
            {
                throw new PropertyDetailsNotRequestedException();
            }

            Property property = propertyViewedByUser.Property;

            int sellerId = property.SellerId;

            Conversation? conversation = await _conversationRepository.GetConversationWithUserWithId(userId, sellerId);

            if (conversation == null) // Creating a new conversation between 2 users
            {
                User sender = await _userRepository.GetById(userId);
                User receiver = await _userRepository.GetById(sellerId);

                conversation = new Conversation
                {
                    Users = new List<User> { sender, receiver },
                    HasUnreadMessage = true,
                    LastUpdatedOn = DateTime.Now,
                    LastConversationBy = userId
                };
                await _conversationRepository.Add(conversation);
            }

            return new CreateConversationReturnDTO
            {
                ConversationId = conversation.ConversationId
            };
        }

        public async Task<List<ConversationListReturnDTO>> GetAllConversationForAUser(int userId, int pageNumber)
        {
            List<ConversationListReturnDTO> conversationListReturnDTOs = await _conversationRepository.GetUserConversation(userId, pageNumber);

            return conversationListReturnDTOs;
        }

        public async Task CreateMessageForConversation(int userId, CreateMessageDTO createMessageDTO)
        {

            Conversation conversation = await _conversationRepository.GetUserConversationById(userId, (int)createMessageDTO.ConversationId);

            if (conversation == null)
            {
                throw new ConversationNotFoundException();
            }

            conversation.HasUnreadMessage = true;
            conversation.LastUpdatedOn = DateTime.Now;
            conversation.LastConversationBy = userId;
            conversation.Chats = new List<Chat> { new Chat {
                Message = createMessageDTO.Message,
                UserId = userId,
            } };

            await _conversationRepository.Update(conversation);

        }

        public async Task<ChatMessagesReturnDTO> GetAllMessageForAConversation(int userId, int conversationId)
        {
            ChatMessagesReturnDTO message = await _conversationRepository.GetConversationMessages(userId, conversationId);

            if (message == null)
            {
                throw new ConversationNotFoundException();
            }

            if (message.ConversationDetails.HasUnreadMessage)
            {
                Conversation conversation = await _conversationRepository.GetById(conversationId);
                conversation.HasUnreadMessage = false;
                await _conversationRepository.Update(conversation);
                message.ConversationDetails.HasUnreadMessage = false;
            }


            return message;
        }

        public async Task<ProfileDetailsDTO> GetUserProfileDetails(int userId)
        {
            ProfileDetailsDTO profileDetailsDTO = await _userRepository.GetUserProfileDetails(userId);

            if (profileDetailsDTO == null)
            {
                throw new UserNotFoundException();
            }

            return profileDetailsDTO;

        }

        public async Task<List<PropertyReturnDTO>> GetUserListings(int userId)
        {
            var myListings = await _userRepository.GetMyListings(userId);
            return myListings;
        }

        public async Task<PropertyReturnDTO> GetUserListedPropertyDetails(int userId, int propertyId)
        {
            var myListedPropertyDetails = await _userRepository.GetPropertyDetailsById(userId, propertyId);

            if (myListedPropertyDetails == null)
            {
                throw new PropertyNotFound();
            }

            return myListedPropertyDetails;
        }

        public async Task<List<PropertyViewedUserReturnDTO>> GetViewedUsersOfProperty(int userId, int propertyId)
        {
            var userDetails = await _propertyRepository.GetViewedUsersOfPropertyById(userId, propertyId);
            return userDetails;
        }

        public async Task<CreateConversationReturnDTO> CreateConversationWithPropertyRequestedUser(int userId, CreateConversationWithRequestedUserDTO createConversationDTO)
        {
            Conversation? conversation = await _conversationRepository.GetConversationWithUserWithId(userId, (int)createConversationDTO.UserId);

            if (conversation == null)
            {
                PropertyUserViewed propertyUserViewed = await _propertyRepository.GetPropertyViewedUserWithSellerIdAndUserId(userId, (int)createConversationDTO.UserId, (int)createConversationDTO.PropertyId);

                if (propertyUserViewed == null)
                {
                    throw new UserNotRequestedForYourProperty();
                }
                else
                {
                    User sender = await _userRepository.GetById(userId);
                    User receiver = await _userRepository.GetById((int)createConversationDTO.UserId);

                    conversation = new Conversation
                    {
                        Users = new List<User> { sender, receiver },
                        HasUnreadMessage = true,
                        LastUpdatedOn = DateTime.Now,
                        LastConversationBy = userId
                    };
                    await _conversationRepository.Add(conversation);
                }

            }

            return new CreateConversationReturnDTO
            {
                ConversationId = conversation.ConversationId
            };

        }

        public async Task<List<PropertyReturnDTO>> GetUserRequestedPropertyDetails(int userId)
        {
            List<PropertyReturnDTO> userRequestedProperties = await _propertyRepository.GetUserRequestedProperties(userId);
            return userRequestedProperties;
        }

        public async Task<PropertyAnalyticsResultDTO> GetPropertyAnalytics(int userId, int propertyId)
        {
            PropertyAnalyticsResultDTO result = await _propertyRepository.GetPropertyAnalytics(userId, propertyId);
            return result;
        }
    }
}
