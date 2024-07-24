using System.ComponentModel.DataAnnotations;

namespace Brokerless.Models
{
    public class UserSubscription
    {
        [Key]
        public int UserSubscriptionId { get; set; }
        public DateTime SubscribedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int AvailableListingCount { get; set; }
        public int AvailableSellerViewCount { get; set; }
        public int Validity {  get; set; }
        public int SubscriptionTemplateId { get; set; } // Foreign Key
        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }
        public SubscriptionTemplate SubscriptionTemplate { get; set; }

    }
}
