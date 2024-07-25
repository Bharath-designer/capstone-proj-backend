using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
