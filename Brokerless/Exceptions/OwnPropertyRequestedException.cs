namespace Brokerless.Exceptions
{
    public class OwnPropertyRequestedException:Exception
    {
        public OwnPropertyRequestedException()  : base("Own property cannot be requested"){ }
    }
}
