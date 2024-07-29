namespace Brokerless.DTOs.User
{
    public class ConversationListReturnDTO
    {
        public int ConversationId { get; set; }
        public string ConversationName { get; set;}
        public string ConversationProfilePic {  get; set;}
        public DateTime LastUpdated { get; set; }
        public bool HasUnreadMessage { get; set; }
    }
}
