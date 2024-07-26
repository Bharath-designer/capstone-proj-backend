using Brokerless.Enums;
using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Property
{
    public class LandDetailsDTO: BasePropertyDTO
    {
        [Required]
        public MeasurementUnit? MeasurementUnit { get; set; }

        [Required]
        public double? Length { get; set; }

        [Required]
        public double? Width { get; set; }

        [Required]
        public ZoningType? ZoningType { get; set; }

    }
}
