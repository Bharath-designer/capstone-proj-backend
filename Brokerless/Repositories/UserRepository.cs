using Brokerless.Context;
using Brokerless.DTOs.Property;
using Brokerless.DTOs.User;
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

        public async Task<List<PropertyReturnDTO>> GetMyListings(int userId)
        {
            List<PropertyReturnDTO> listings = await _context.Properties
                .Where(p => p.SellerId == userId)
                .Select(p => new PropertyReturnDTO
                {
                    City = p.City,
                    Currency = p.Currency,
                    Deposit = p.Deposit,
                    Description = p.Description,
                    State = p.State,
                    ListingType = p.ListingType,
                    Price = p.Price,
                    PriceNegotiable = p.PriceNegotiable,
                    PricePerUnit = p.PricePerUnit,
                    PropertyId = p.PropertyId,
                    PropertyCategory = p.PropertyCategory,
                    PropertyType = p.PropertyType,
                    Rent = p.Rent,
                    RentDuration = p.RentDuration,
                    Tags = p.Tags.Select(t => t.Tag.TagValue).ToList(),
                    CommercialDetails = p.CommercialDetails,
                    HostelDetails = p.HostelDetails,
                    HouseDetails = p.HouseDetails,
                    LandDetails = p.LandDetails,
                    ProductDetails = p.ProductDetails,
                    PostedOn = p.PostedOn,
                    IsApproved = p.isApproved,
                    LocationLat = p.LocationLat,
                    LocationLon = p.LocationLon,
                    PropertyStatus = p.PropertyStatus,
                    Files = p.Files.Select(f=> new FileReturnDTO
                    {
                        FileId = f.FileId,
                        FileSize = f.FileSize,
                        FileType = f.FileType,
                        FileUrl = f.FileUrl
                    }).ToList(),
                })
                .OrderByDescending(p => p.PostedOn)
                .AsSplitQuery()
                .ToListAsync();
            return listings;

        }

        public async Task<PropertyReturnDTO> GetPropertyDetailsById(int userId, int propertyId)
        {
            var propertyDetails = await _context.Properties
                .Where(p => p.SellerId == userId && p.PropertyId == propertyId)
                .Select(p => new PropertyReturnDTO
                {
                    City = p.City,
                    Currency = p.Currency,
                    Deposit = p.Deposit,
                    Description = p.Description,
                    State = p.State,
                    ListingType = p.ListingType,
                    Price = p.Price,
                    PriceNegotiable = p.PriceNegotiable,
                    PricePerUnit = p.PricePerUnit,
                    PropertyId = p.PropertyId,
                    PropertyCategory = p.PropertyCategory,
                    PropertyType = p.PropertyType,
                    Rent = p.Rent,
                    RentDuration = p.RentDuration,
                    Tags = p.Tags.Select(t => t.Tag.TagValue).ToList(),
                    CommercialDetails = p.CommercialDetails,
                    HostelDetails = p.HostelDetails,
                    HouseDetails = p.HouseDetails,
                    LandDetails = p.LandDetails,
                    ProductDetails = p.ProductDetails,
                    PostedOn = p.PostedOn,
                    IsApproved = p.isApproved,
                    LocationLat = p.LocationLat,
                    LocationLon = p.LocationLon,
                    PropertyStatus = p.PropertyStatus,
                    Files = p.Files.Select(f => new FileReturnDTO
                    {
                        FileId = f.FileId,
                        FileSize = f.FileSize,
                        FileType = f.FileType,
                        FileUrl = f.FileUrl
                    }).ToList(),
                })
                .FirstOrDefaultAsync();
            return propertyDetails;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<ProfileDetailsDTO> GetUserProfileDetails(int userId)
        {
            ProfileDetailsDTO? profileDetails = await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u=> new ProfileDetailsDTO
                {
                    AvailableListingCount = u.UserSubscription.AvailableListingCount,
                    AvailableSellerViewCount = u.UserSubscription.AvailableSellerViewCount,
                    CountryCode = u.CountryCode,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Name = u.FullName,
                    PhoneNumberVerified = u.PhoneNumberVerified,
                    ProfilePic = u.ProfileUrl,
                    SubscriptionTemplateName = u.UserSubscription.SubscriptionTemplateName,
                    ExpiresOn = u.UserSubscription.ExpiresOn
                })
                .FirstOrDefaultAsync();
            return profileDetails;
        }

        public async Task<User> GetUserWithSubscription(int userId)
        {
            var user = await _context.Users.Include(u=>u.UserSubscription).FirstOrDefaultAsync(u=>u.UserId == userId);
            return user;
        }
    }
}
