using Brokerless.DTOs.Property;
using Brokerless.DTOs.User;
using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface IPropertyRepository: IBaseRepository<Property, int>
    {
        public Task<List<PropertyReturnDTO>> GetAll(int? userId, PropertySearchFilterDTO propertySearchFilterDTO, bool isAdmin);
        public Task<PropertyReturnDTO> GetPropertyWithSellerDetails(int GetPropertyWithSellerDetails, bool isPropertyDetailsAllowed, bool isAdmin);
        public Task<PropertyUserViewed> GetPropertyWithViewedUserById(int userId, int propertyId);
        public Task<List<PropertyViewedUserReturnDTO>> GetViewedUsersOfPropertyById(int userId, int propertyId);
        public Task<PropertyUserViewed> GetPropertyViewedUserWithSellerIdAndUserId(int sellerId, int userId, int propertyId);
        public Task<List<PropertyReturnDTO>> GetUserRequestedProperties(int userId);
        public Task<Property> GetPropertyOfSellerByPropertyIdWithTagsAndFiles(int userId, int propertyId);
        public Task<PropertyAnalyticsResultDTO> GetPropertyAnalytics(int userId, int propertyId);
    }
}
