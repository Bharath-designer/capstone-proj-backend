using Brokerless.Context;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Repositories
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(BrokerlessDBContext _context) : base(_context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

    }
}
