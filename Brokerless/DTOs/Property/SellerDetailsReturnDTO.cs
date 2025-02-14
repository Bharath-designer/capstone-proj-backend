﻿namespace Brokerless.DTOs.Property
{
    public class SellerDetailsReturnDTO
    {
        public int SellerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public string Email { get; set; }
    }
}
