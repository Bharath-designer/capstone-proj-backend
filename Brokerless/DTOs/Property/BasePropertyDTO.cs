using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;
using Brokerless.Models;
using Brokerless.Validations;

namespace Brokerless.DTOs.Property
{
    public class BasePropertyDTO
    {
        [Required]
        public PropertyType? PropertyType { get; set; }
        public PropertyCategory? PropertyCategory { get; set; }
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
        [NoNullElements("The Tags list cannot contain null elements.")]
        public List<string> Tags { get; set; }
    }
}
