namespace Brokerless.Enums
{
    public class ChatReaction
    {
        public int ChatReactionId { get; set; }
        public int ReactedBy { get; set; }
        public string Reaction {  get; set; }
        public int ChatId { get; set; } // Foreign Key
    }
}
