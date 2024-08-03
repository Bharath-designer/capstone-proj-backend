namespace Brokerless.Models
{
    public class PropertyTag
    {
        public int PropertyId {  get; set; }
        public string TagValue { get; set; }
        public Property Property { get; set; }
        public Tag Tag { get; set; }
    }
}
