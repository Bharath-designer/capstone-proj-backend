using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface IUserService
    {
        public Task<User> CreateUser(string email, string userName, string profilePic);
    }
}
