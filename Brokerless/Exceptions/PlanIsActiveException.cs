namespace Brokerless.Exceptions
{
    public class PlanIsActiveException:Exception
    {
        public PlanIsActiveException():base("Current plan is active") { }  
    }
}
