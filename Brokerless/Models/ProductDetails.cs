using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class ProductDetails
    {
        [Key]
        public int ProductDetailsId { get; set; }
        public ProductType ProductType { get; set; }
        public string Manufacturer {  get; set; }
        public int? WarrantyPeriod { get; set; }
        public DurationType? WarrantyUnit { get; set; } 
        public int PropertyId { get; set; } // Foreign Key
        public Property Property { get; set; }


    }
}
