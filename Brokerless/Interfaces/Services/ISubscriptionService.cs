using Brokerless.DTOs.Payment;
using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface ISubscriptionService
    {
        public Task<MakePaymentReturnDTO> UpdateSubscription(int userId, string subscriptionName);
    }
}
