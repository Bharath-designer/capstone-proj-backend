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

                // Creating dummy users
                //payload.Email = "user2@gmail.com";
                //payload.Name = "User 2";
                //payload.Picture = "https://image.lexica.art/full_jpg/7515495b-982d-44d2-9931-5a8bbbf27532";


                User user = await _userRepository.GetUserByEmail(payload.Email);

                if (user == null)
                {
                    string email = payload.Email;
                    string userName = payload.Name;
                    string profilePic = payload.Picture;
                    user = await _userService.CreateUser(email, userName, profilePic);
                } 

                string Token = _tokenService.GenerateToken(user);

                var authReturnDTO = new AuthReturnDTO
                {
                    Token = Token
                };

                return authReturnDTO;
            }

            throw new GmailNotVerifiedException();
        }
    }
}
