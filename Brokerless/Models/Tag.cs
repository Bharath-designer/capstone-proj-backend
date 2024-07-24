using System.ComponentModel.DataAnnotations;

namespace Brokerless.Models
{
    public class Tag
    {
        [Key]
        public string TagValue { get; set; }
        public List<Property> Properties { get; set; }
    }
}
