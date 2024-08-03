namespace Brokerless.Exceptions
{
    public class MobileNotVerifiedException: Exception
    {
        public MobileNotVerifiedException() :base("Verify mobile number to proceed"){ }
    }
}
