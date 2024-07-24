using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class CommercialDetails
    {
        [Key]
        public int CommercialDetailsId { get; set; }
        public CommercialType CommercialType { get; set; }
        public int FloorCount { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double? Height { get; set; }
        public WaterSupply WaterSupply { get; set; }
        public Electricity Electricity { get; set; }
        public int RestroomCount { get; set; }
        public bool GatedSecurity { get; set; }
        public int CarParking {  get; set; }
        public int PropertyId { get; set; } // Foreign Key
        public Property Property { get; set; }

    }
}
