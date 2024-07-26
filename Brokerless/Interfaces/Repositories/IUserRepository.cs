using Brokerless.Models;

namespace Brokerless.Interfaces.Repositories
{
    public interface IUserRepository: IBaseRepository<User, int>
    {
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserWithSubscription(int userId);
    }
}
