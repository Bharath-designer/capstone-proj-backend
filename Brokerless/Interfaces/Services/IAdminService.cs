using Brokerless.DTOs.Admin;
using Brokerless.DTOs.Property;

namespace Brokerless.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<PropertyApprovalToggleReturnDTO> TogglePropertyStatus(PropertyApprovalToggleDTO propertyApprovalToggleDTO);

        public Task<List<PropertyReturnDTO>> GetAdminPropertiesWithFilters(AdminPropertySearchFilter adminPropertySearchFilterDTO);
        public Task<PropertyReturnDTO> GetAdminPropertyById(int propertyId);
    }
}
