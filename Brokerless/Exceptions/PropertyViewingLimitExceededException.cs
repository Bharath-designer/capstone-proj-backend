namespace Brokerless.Exceptions
{
    public class PropertyViewingLimitExceededException:Exception
    {
        public PropertyViewingLimitExceededException() : base("Property viewing limit is reached. Upgrade subscription to continue.") { }

    }
}
