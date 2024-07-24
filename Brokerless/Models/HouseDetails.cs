using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class HouseDetails
    {
        [Key]
        public int HouseDetailsId { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double? Height { get; set; }
        public int FloorCount { get; set; }
        public int RoomCount { get; set; }
        public bool HallAndKitchenAvailable { get; set; }
        public int RestroomCount {  get; set; }
        public WaterSupply WaterSupply { get; set; }
        public Electricity Electricity { get; set; }
        public bool GatedSecurity { get; set; }
        public int CarParking { get; set; }
        public FurnishingDetails FurnishingDetails { get; set; }
        public int PropertyId { get; set; } // Foreign Key
        public Property Property { get; set; }

    }
}
