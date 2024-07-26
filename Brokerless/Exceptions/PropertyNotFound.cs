namespace Brokerless.Exceptions
{
    public class PropertyNotFound: Exception
    {
        public PropertyNotFound(): base("Property with the given id not found") { }
    }
}
