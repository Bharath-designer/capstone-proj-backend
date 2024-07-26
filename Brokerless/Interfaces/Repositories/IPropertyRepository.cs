using Brokerless.DTOs.Property;
using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface IPropertyRepository: IBaseRepository<Property, int>
    {
        public Task<List<PropertyReturnDTO>> GetAll(PropertySearchFilterDTO propertySearchFilterDTO);
        public Task<PropertyReturnDTO> GetPropertyWithSellerDetails(int GetPropertyWithSellerDetails, bool isPropertyDetailsAllowed);
        public Task<PropertyUserViewed> GetPropertyWithViewedUserById(int userId, int propertyId);
    }
}
