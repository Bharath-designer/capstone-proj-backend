using Brokerless.DTOs.Property;
using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface IPropertyService
    {
        public Task CreateProperty(int userId, BasePropertyDTO basePropertyDTO, List<IFormFile> formFiles);
        public Task UpdateProperty(int userId, UpdateBasePropertyDTO basePropertyDTO, List<IFormFile> formFiles);
        public Task<List<PropertyReturnDTO>> GetPropertiesWithFilters(int? userId, PropertySearchFilterDTO propertySearchFilterDTO);
        public Task<PropertyReturnDTO> GetPropertyByIdForUser(int? userId, int propertyId);
        public Task RequestForProperty(int userId, RequestPropertyDTO requestPropertyDTO);
    }
}
