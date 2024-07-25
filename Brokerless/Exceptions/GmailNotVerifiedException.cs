namespace Brokerless.Exceptions
{
    public class GmailNotVerifiedException: Exception
    {
        public GmailNotVerifiedException() : base("Google Id is not verified, Try With other Mail") { }

    }
}
