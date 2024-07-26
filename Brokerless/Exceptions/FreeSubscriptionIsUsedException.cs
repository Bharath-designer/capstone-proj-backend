namespace Brokerless.Exceptions
{
    public class FreeSubscriptionIsUsedException: Exception
    {
        public FreeSubscriptionIsUsedException(): base("Can't upgrade to Free plan.") { }
    }
}
