using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class LandDetails
    {
        [Key]
        public int LandDetailsId { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public ZoningType ZoningType { get; set; }
        public int PropertyId { get; set; } // Foreign Key
        public Property Property { get; set; }


    }
}
