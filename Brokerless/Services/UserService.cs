using Brokerless.Enums;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;
using Brokerless.Repositories;

namespace Brokerless.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public async Task<User> CreateUser(string email, string userName, string profilePic)
        {
            var user = new User
            {
                Email = email,
                FullName = userName,
                ProfileUrl = profilePic,
                UserRole = UserRole.User
            };

            await _userRepository.Add(user);
            return user;
        }
    }
}
