using System.ComponentModel.DataAnnotations;

namespace Brokerless.Models
{
    public class PropertyUserViewed
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedOn { get; set; }

        public PropertyUserViewed() {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istTimeZone);
            CreatedOn = istNow;
        }
    }
}
