using Brokerless.DTOs.Auth;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Google.Apis.Auth;
using Brokerless.Models;
namespace Brokerless.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IUserService userService, ITokenService tokenService) {
            _userRepository = userRepository;
            _userService = userService;
            _tokenService = tokenService;
        }
        public async Task<AuthReturnDTO> AuthenticateWithGoogle(string token)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { "663997612183-gf37tabas8ds2e7pt6p5h1v2hrq3opiv.apps.googleusercontent.com" }
            });

            bool emailVerified = payload.EmailVerified;
            if (emailVerified)
            {

                User existingUser = await _userRepository.GetUserByEmail(payload.Email);
                User userToCreateToken = null;

                if (existingUser == null)
                {
                    string email = payload.Email;
                    string userName = payload.Name;
                    string profilePic = payload.Picture;
                    userToCreateToken = await _userService.CreateUser(email, userName, profilePic);
                    await Console.Out.WriteLineAsync("New user created");
                } else
                {
                    await Console.Out.WriteLineAsync("got existing user");
                    userToCreateToken = existingUser;
                }

                string Token = _tokenService.GenerateToken(userToCreateToken);
                await Console.Out.WriteLineAsync(Token);
                await Console.Out.WriteLineAsync("above token");
                return new AuthReturnDTO
                {
                    Token = Token
                };
            }

            throw new GmailNotVerifiedException();
        }
    }
}
