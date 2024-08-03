using Brokerless.Enums;

namespace Brokerless.DTOs.Auth
{
    public class VerifyReturnDTO
    {
        public UserRole UserRole { get; set; }
        public string ProfilePic {  get; set; }
        public string UserName { get; set; }
    }
}
