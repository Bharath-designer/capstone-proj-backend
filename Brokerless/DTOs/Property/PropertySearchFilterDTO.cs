using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;
using Brokerless.Validations;

namespace Brokerless.DTOs.Property
{
    public class PropertySearchFilterDTO
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
        public string? City { get; set; }
        public string? State { get; set; }
        [NoNullElements("The Tags list cannot contain null elements.")]
        public List<string>? Tags { get; set; }
        public OrderByType OrderBy { get; set; } = OrderByType.DateDesc;
        public PropertyType? PropertyType { get; set; }
        public PropertyCategory? PropertyCategory { get; set; }
        
    }
}
