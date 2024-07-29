namespace Brokerless.DTOs.User
{
    public class PropertyViewedUserReturnDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CountryCode { get; set; }
        public bool IsPhoneNumberVerified { get; set; }
    }
}
