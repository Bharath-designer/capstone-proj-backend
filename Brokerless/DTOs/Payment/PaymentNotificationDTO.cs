using Brokerless.Enums;

namespace Brokerless.DTOs.Payment
{
    public class PaymentNotificationDTO
    {
        public string TransactionId { get; set; }
        public TransactionStatus PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
    }
}
