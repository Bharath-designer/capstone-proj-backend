using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public MessageType Type { get; set; }

    }
}
