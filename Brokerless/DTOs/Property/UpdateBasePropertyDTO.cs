using Brokerless.Enums;
using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Property
{
    public class UpdateBasePropertyDTO
    {
        [Required]
        public int? PropertyId { get; set; }
        [Required] 
        public PropertyStatus? PropertyStatus { get; set; }
        [Required]
        public ListingType? ListingType { get; set; }
        [Required]
        public double? LocationLat { get; set; }
        [Required]
        public double? LocationLon { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool? PriceNegotiable { get; set; }
        [Required]
        public Currency? Currency { get; set; }
        public double? Price { get; set; }
        public MeasurementUnit? PricePerUnit { get; set; }
        public double? Rent { get; set; }
        public DurationType? RentDuration { get; set; }
        public double? Deposit { get; set; }
        public List<string>? TagsToBeAdded { get; set; }
        public List<string>? TagsToBeRemoved { get; set; }
        public List<string>? FilesToBeRemoved { get; set; }
    }
}
