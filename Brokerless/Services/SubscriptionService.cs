using Brokerless.DTOs.Payment;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;

namespace Brokerless.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionTemplateRepository _subscriptionTemplateRepository;
        private readonly IPaymentService _paymentService;

        public SubscriptionService(
            IUserRepository userRepository, 
            ISubscriptionTemplateRepository subscriptionTemplateRepository,
            IPaymentService paymentService
            ) {
            _userRepository = userRepository;
            _subscriptionTemplateRepository = subscriptionTemplateRepository;
            _paymentService = paymentService;
        }


        public async Task<MakePaymentReturnDTO> UpdateSubscription(int userId, string subscriptionName)
        {
            if (subscriptionName == "Free")
            {
                throw new FreeSubscriptionIsUsedException();
            }

            SubscriptionTemplate subscriptionTemplate = await _subscriptionTemplateRepository.GetById(subscriptionName);

            if (subscriptionTemplate == null)
            {
                throw new InvalidSubscriptionName();
            }

            User user = await _userRepository.GetUserWithSubscription(userId);
            
            if (!isUserSubscriptionUpdatable(user.UserSubscription))
            {
                throw new PlanIsActiveException();
            }

            MakePaymentReturnDTO makePaymentReturnDTO =  await _paymentService.InitializeTransactionForSubscription(userId, subscriptionTemplate);
            return makePaymentReturnDTO;
        }

        private bool isUserSubscriptionUpdatable(UserSubscription userSubscription)
        {
            if (userSubscription.AvailableListingCount == 0 || userSubscription.AvailableSellerViewCount == 0) 
                return true;

            DateTime? expiresOn = userSubscription.ExpiresOn;
            
            if (expiresOn == null) return false;

            if (expiresOn < DateTime.Now) return true;

            return false;

        }
    }
}
