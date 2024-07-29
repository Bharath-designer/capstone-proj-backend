using System.ComponentModel.DataAnnotations;

namespace Brokerless.Models
{
    public class PropertyUserViewed
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
