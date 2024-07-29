namespace Brokerless.DTOs.User
{
    public class ProfileDetailsDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public int AvailableListingCount { get; set; }
        public int AvailableSellerViewCount { get; set; }
        public string SubscriptionTemplateName { get; set; }

    }
}
