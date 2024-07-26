using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.DTOs.Property
{
    public class HouseDetailsDTO: BasePropertyDTO
    {
        [Required]
        public MeasurementUnit? MeasurementUnit { get; set; }
        [Required]
        public double? Length { get; set; }
        [Required]
        public double? Width { get; set; }
        public double? Height { get; set; }
        [Required]
        public int? FloorCount { get; set; }
        [Required]
        public int? RoomCount { get; set; }
        [Required]
        public bool? HallAndKitchenAvailable { get; set; }
        [Required]
        public int? RestroomCount { get; set; }
        [Required]
        public WaterSupply? WaterSupply { get; set; }
        [Required]
        public Electricity? Electricity { get; set; }
        [Required]
        public bool? GatedSecurity { get; set; }
        [Required]
        public bool? CarParking { get; set; }
        [Required]
        public FurnishingDetails? FurnishingDetails { get; set; }
    }
}
