using System.ComponentModel.DataAnnotations;
using Brokerless.Enums;

namespace Brokerless.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public PropertyType PropertyType { get; set; }
        public PropertyCategory? PropertyCategory { get; set; }
        public ListingType ListingType { get; set; }
        public double LocationLat { get; set; }
        public double LocationLon { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
        public bool PriceNegotiable { get; set; }
        public Currency Currency { get; set; }
        public double? Price { get; set; }
        public MeasurementUnit? PricePerUnit { get; set; }
        public double? Rent { get; set; }
        public DurationType? RentDuration { get; set; }
        public double? Deposit { get; set; }
        public DateTime PostedOn { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public int SellerId { get; set; } // ForeignKey
        public List<Tag> Tags { get; set; }
        public List<PropertyFile> Files { get; set; }
        public User Seller {  get; set; }
        public CommercialDetails CommercialDetails { get; set; }
        public HostelDetails HostelDetails { get; set; }
        public HouseDetails HouseDetails { get; set; }  
        public LandDetails  LandDetails { get; set; }
        public ProductDetails ProductDetails { get; set; }

    }
}
