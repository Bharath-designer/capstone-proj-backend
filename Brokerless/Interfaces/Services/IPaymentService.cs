using Brokerless.DTOs.Payment;
using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface IPaymentService
    {
        public Task<MakePaymentReturnDTO> InitializeTransactionForSubscription(int userId, SubscriptionTemplate subscriptionTemplate);
        public Task UpdateTransactionDetails(PaymentNotificationDTO paymentNotificationDTO, IHeaderDictionary headers);

    }
}
