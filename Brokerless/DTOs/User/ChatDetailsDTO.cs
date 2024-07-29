namespace Brokerless.DTOs.User
{
    public class ChatDetailsDTO
    {
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSentByUser { get; set; }
    }
}
