using Brokerless.Models;

namespace Brokerless.DTOs.Property
{
    public class PropertyReturnDTO: BasePropertyDTO
    {
        public int PropertyId { get; set; }
        public HostelDetails HostelDetails { get; set; }
        public HouseDetails HouseDetails { get; set; }
        public CommercialDetails CommercialDetails { get; set; }
        public LandDetails LandDetails { get; set; }
        public ProductDetails ProductDetails { get; set; }
        public DateTime PostedOn { get; set; }
        public SellerDetailsReturnDTO? SellerDetails { get; set; }
    }
}
