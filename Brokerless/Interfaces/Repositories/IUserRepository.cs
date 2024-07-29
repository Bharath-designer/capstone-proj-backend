using Brokerless.DTOs.Property;
using Brokerless.DTOs.User;
using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface IUserRepository: IBaseRepository<User, int>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserWithSubscription(int userId);
        public Task<ProfileDetailsDTO> GetUserProfileDetails(int userId);
        public Task<List<PropertyReturnDTO>> GetMyListings(int userId);
        public Task<PropertyReturnDTO> GetPropertyDetailsById(int userId, int propertyId);
    }
}
