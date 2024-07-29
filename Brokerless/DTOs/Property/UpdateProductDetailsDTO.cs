using Brokerless.Enums;
using System.ComponentModel.DataAnnotations;

namespace Brokerless.DTOs.Property
{
    public class UpdateProductDetailsDTO:UpdateBasePropertyDTO
    {
        [Required]
        public ProductType? ProductType { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        public int? WarrantyPeriod { get; set; }
        public DurationType? WarrantyUnit { get; set; }
    }
}
