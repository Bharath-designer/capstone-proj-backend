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

                //Creating dummy users
                //payload.Email = "user1@gmail.com";
                //payload.Name = "User 1";
                //payload.Picture = "https://media.istockphoto.com/id/1393872009/photo/african-american-man-with-african-hairstyle-standing-over-isolated-pink-background.webp?b=1&s=170667a&w=0&k=20&c=Le8v3DqgLlmlHplW_1WgZL-g__tOipo31Cob_11VJSQ=";


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
                    Token = Token,
                    ProfilePic = user.ProfileUrl,
                    UserName = user.FullName,
                    UserRole = user.UserRole
                };

                return authReturnDTO;
            }

            throw new GmailNotVerifiedException();
        }

        public async Task<VerifyReturnDTO> GetVerifyDetails(int userId)
        {
            User user = await _userRepository.GetById(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            } 

            return new VerifyReturnDTO
            {
                ProfilePic = user.ProfileUrl,
                UserRole = user.UserRole,
                UserName= user.FullName
            };
        }
    }
}
