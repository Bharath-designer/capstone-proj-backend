namespace Brokerless.Exceptions
{
    public class PropertyPostingLimitExceededException: Exception
    {
        public PropertyPostingLimitExceededException(): base("Property posting limit is reached. Upgrade subscription to continue.") { }
    }
}
