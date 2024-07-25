using Brokerless.DTOs.Auth;

namespace Brokerless.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AuthReturnDTO> AuthenticateWithGoogle(string token);
    }
}
