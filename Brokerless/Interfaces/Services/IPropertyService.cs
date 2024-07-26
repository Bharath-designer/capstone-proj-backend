using Brokerless.DTOs.Property;

namespace Brokerless.Interfaces.Services
{
    public interface IPropertyService
    {
        public Task CreateProperty(int userId, BasePropertyDTO basePropertyDTO, List<IFormFile> formFiles);
        public Task<List<PropertyReturnDTO>> GetPropertiesWithFilters(PropertySearchFilterDTO propertySearchFilterDTO);
        public Task<PropertyReturnDTO> GetPropertyByIdForUser(int userId, int propertyId);
    }
}
