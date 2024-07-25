using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class SubscriptionTemplate
    {
        [Key]
        public int SubscriptionTemplateId { get; set; }
        public string SubsriptionName { get; set; }
        public string Description { get; set; }
        public Currency? Currency { get; set; }
        public double? Price { get; set; }
        public int MaxListingCount { get; set; }
        public int MaxSellerViewCount { get; set; }
        public int? Validity { get; set; }

    }
}
