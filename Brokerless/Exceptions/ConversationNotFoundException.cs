namespace Brokerless.Exceptions
{
    public class ConversationNotFoundException:Exception
    {
        public ConversationNotFoundException(): base("Conversation Not found") { }
    }
}
