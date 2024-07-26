using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public UserRole UserRole { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfileUrl { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberVerified { get; set; } = false;
        public UserSubscription UserSubscription { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Property> Listings { get; set; }
        public List<Conversation> Conversations { get; set; }
        public List<PropertyUserViewed> PropertiesViewed { get; set; }

    }
}
