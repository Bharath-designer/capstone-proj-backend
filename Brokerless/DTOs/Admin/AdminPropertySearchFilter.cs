using Brokerless.DTOs.Property;

namespace Brokerless.DTOs.Admin
{
    public class AdminPropertySearchFilter:PropertySearchFilterDTO
    {
        public bool? IsApproved { get; set; }
    }
}
