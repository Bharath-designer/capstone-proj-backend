using Brokerless.Context;
using Brokerless.DTOs.Admin;
using Brokerless.DTOs.Property;
using Brokerless.DTOs.User;
using Brokerless.Enums;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Repositories
{
    public class PropertyRepository : BaseRepository<Property, int>, IPropertyRepository
    {
        public PropertyRepository(BrokerlessDBContext _context) : base(_context) { }

        public async Task<List<PropertyReturnDTO>> GetAll(int? userId, PropertySearchFilterDTO propertySearchFilterDTO, bool isAdmin)
        {
            int PAGE_SIZE = 5;
            int PAGE_NUMBER = propertySearchFilterDTO.PageNumber;

            var baseQuery = _context.Properties.AsQueryable();

            if (isAdmin)
            {
                AdminPropertySearchFilter filter = (AdminPropertySearchFilter)propertySearchFilterDTO;
                if (filter.IsApproved != null)
                {
                    baseQuery = baseQuery.Where(p => p.isApproved == filter.IsApproved);
                }
            }
            else
            {
                baseQuery = baseQuery.Where(p => p.SellerId != userId);
                baseQuery = baseQuery.Where(p => p.PropertyStatus == PropertyStatus.Active);
                baseQuery = baseQuery.Where(p => p.isApproved == true);
            }


            if (propertySearchFilterDTO.City != null)
            {
                baseQuery = baseQuery.Where(p => p.City == propertySearchFilterDTO.City);
            }

            if (propertySearchFilterDTO.State != null)
            {
                baseQuery = baseQuery.Where(p => p.State == propertySearchFilterDTO.State);
            }

            if (propertySearchFilterDTO.PropertyType != null)
            {
                baseQuery = baseQuery.Where(p => p.PropertyType == propertySearchFilterDTO.PropertyType);
            }

            if (propertySearchFilterDTO.PropertyCategory != null)
            {
                baseQuery = baseQuery.Where(p => p.PropertyCategory == propertySearchFilterDTO.PropertyCategory);
            }

            if (propertySearchFilterDTO.Tags != null)
            {
                baseQuery = baseQuery
                    .Where(p => p.Tags.Any(t => propertySearchFilterDTO.Tags.Contains(t.Tag.TagValue)));
            }


            baseQuery = baseQuery.Distinct();

            if (propertySearchFilterDTO.OrderBy == OrderByType.DateAsc)
            {
                baseQuery = baseQuery.OrderBy(p => p.PostedOn);
            }
            else if (propertySearchFilterDTO.OrderBy == OrderByType.PriceDesc)
            {
                baseQuery = baseQuery.OrderByDescending(p => p.Price ?? p.Rent);
            }
            else if (propertySearchFilterDTO.OrderBy == OrderByType.PriceAsc)
            {
                baseQuery = baseQuery.OrderBy(p => p.Price ?? p.Rent);
            }
            else
            {
                baseQuery = baseQuery.OrderByDescending(p => p.PostedOn);
            }

            baseQuery = baseQuery.Skip((PAGE_NUMBER - 1) * PAGE_SIZE);
            baseQuery = baseQuery.Take(PAGE_SIZE);

            var propertyReturnDTOs = await baseQuery.Select(p => new PropertyReturnDTO
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
                Files = p.Files.Select(f => new FileReturnDTO
                {
                    FileId = f.FileId,
                    FileSize = f.FileSize,
                    FileType = f.FileType,
                    FileUrl = f.FileUrl
                }).ToList(),
            }).ToListAsync();


            return propertyReturnDTOs;
        }

        public async Task<string> GetPropertySellerEmail(int propertyId)
        {
            string? email = await _context.Properties
                .Where(p => p.PropertyId == propertyId)
                .Select(p => p.Seller.Email)
                .FirstOrDefaultAsync();

            return email;
        }

        public async Task<PropertyUserViewed> GetPropertyWithViewedUserById(int userId, int propertyId)
        {
            PropertyUserViewed? property = await _context.PropertyUserViewed
                .Where(p => p.PropertyId == propertyId && p.UserId == userId)
                .Select(p => new PropertyUserViewed
                {
                    CreatedOn = p.CreatedOn,
                    UserId = p.UserId,
                    PropertyId = p.PropertyId,
                    Property = new Property
                    {
                        City = p.Property.City,
                        Currency = p.Property.Currency,
                        Deposit = p.Property.Deposit,
                        SellerId = p.Property.SellerId,
                        Description = p.Property.Description,
                        isApproved = p.Property.isApproved,
                        Rent = p.Property.Rent,
                        LocationLat = p.Property.LocationLat,
                        LocationLon = p.Property.LocationLon,
                        Price = p.Property.Price,
                        ListingType = p.Property.ListingType,
                        PostedOn = p.Property.PostedOn,
                        PricePerUnit = p.Property.PricePerUnit,
                        PriceNegotiable = p.Property.PriceNegotiable,
                        PropertyCategory = p.Property.PropertyCategory,
                        PropertyId = p.Property.PropertyId,
                        PropertyStatus = p.Property.PropertyStatus,
                        RentDuration = p.Property.RentDuration,
                        PropertyType = p.Property.PropertyType,
                        State = p.Property.State
                    }
                })
                .FirstOrDefaultAsync();

            return property;

        }

        public async Task<PropertyReturnDTO> GetPropertyWithSellerDetails(int propertyId, bool isPropertyDetailsAllowed, bool isAdmin)
        {
            var baseQuery = _context.Properties.AsQueryable();

            if (isAdmin != true) { 
                baseQuery = baseQuery.Where(p => p.isApproved);
            }

            var propertyReturnDTO = await baseQuery.Where(p => p.PropertyId == propertyId)
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
                    LocationLat = isPropertyDetailsAllowed ? p.LocationLat : null,
                    LocationLon = isPropertyDetailsAllowed ? p.LocationLon : null,
                    PropertyStatus = p.PropertyStatus,
                    IsApproved = p.isApproved,
                    Files = p.Files.Select(f => new FileReturnDTO
                    {
                        FileId = f.FileId,
                        FileSize = f.FileSize,
                        FileType = f.FileType,
                        FileUrl = f.FileUrl
                    }).ToList(),
                    SellerDetails = isPropertyDetailsAllowed ? new SellerDetailsReturnDTO
                    {
                        SellerId = p.SellerId,
                        Name = p.Seller.FullName,
                        CountryCode = p.Seller.CountryCode,
                        PhoneNumber = p.Seller.PhoneNumber,
                        PhoneNumberVerified= p.Seller.PhoneNumberVerified,
                        Email = p.Seller.Email
                    } : null
                }).FirstOrDefaultAsync();

            return propertyReturnDTO;
        }

        public async Task<List<PropertyViewedUserReturnDTO>> GetViewedUsersOfPropertyById(int userId, int propertyId)
        {
            var propertyExists = await _context.Properties
                .Where(p => p.SellerId == userId && p.PropertyId == propertyId)
                .FirstOrDefaultAsync();


            if (propertyExists == null)
            {
                throw new PropertyNotFound();
            }

            var usersList = await _context.PropertyUserViewed
                .Where(puv => puv.PropertyId == propertyId && puv.Property.SellerId == userId)
                .Select(puv => new PropertyViewedUserReturnDTO
                {
                    UserName = puv.User.FullName,
                    CountryCode = puv.User.CountryCode,
                    Email = puv.User.Email,
                    MobileNumber = puv.User.PhoneNumber,
                    UserId = puv.User.UserId,
                    IsPhoneNumberVerified = puv.User.PhoneNumberVerified,
                    ViewedOn = puv.CreatedOn
                })
                .OrderByDescending(u => u.ViewedOn)
                .ToListAsync();

            return usersList;
        }

        public async Task<PropertyUserViewed> GetPropertyViewedUserWithSellerIdAndUserId(int sellerId, int userId, int propertyId)
        {
            PropertyUserViewed? property = await _context.PropertyUserViewed
                .Where(p => p.PropertyId == propertyId && p.UserId == userId && p.Property.SellerId == sellerId)
                .Select(p => new PropertyUserViewed
                {
                    CreatedOn = p.CreatedOn,
                    UserId = p.UserId,
                    PropertyId = p.PropertyId
                })
                .FirstOrDefaultAsync();

            return property;
        }

        public async Task<List<PropertyReturnDTO>> GetUserRequestedProperties(int userId)
        {
            List<PropertyReturnDTO> userRequestedProperties = await _context.PropertyUserViewed
                .Where(puv => puv.UserId == userId)
                .OrderByDescending(p=>p.CreatedOn)
                .Select(p => new PropertyReturnDTO
                {
                    City = p.Property.City,
                    Currency = p.Property.Currency,
                    Deposit = p.Property.Deposit,
                    Description = p.Property.Description,
                    State = p.Property.State,
                    ListingType = p.Property.ListingType,
                    Price = p.Property.Price,
                    PriceNegotiable = p.Property.PriceNegotiable,
                    PricePerUnit = p.Property.PricePerUnit,
                    PropertyId = p.Property.PropertyId,
                    PropertyCategory = p.Property.PropertyCategory,
                    PropertyType = p.Property.PropertyType,
                    Rent = p.Property.Rent,
                    RentDuration = p.Property.RentDuration,
                    CommercialDetails = p.Property.CommercialDetails,
                    HostelDetails = p.Property.HostelDetails,
                    HouseDetails = p.Property.HouseDetails,
                    LandDetails = p.Property.LandDetails,
                    ProductDetails = p.Property.ProductDetails,
                    PostedOn = p.Property.PostedOn,
                    LocationLat = p.Property.LocationLat,
                    LocationLon = p.Property.LocationLon,
                    PropertyStatus = p.Property.PropertyStatus,
                    IsApproved = p.Property.isApproved,
                    Files = p.Property.Files.Select(f => new FileReturnDTO
                    {
                        FileId = f.FileId,
                        FileSize = f.FileSize,
                        FileType = f.FileType,
                        FileUrl = f.FileUrl
                    }).ToList()
                })
                .ToListAsync();

            return userRequestedProperties;
        }

        public async Task<Property> GetPropertyOfSellerByPropertyIdWithTagsAndFiles(int userId, int propertyId)
        {
            Property? property = await _context.Properties
                .Where(p => p.PropertyId == propertyId && p.SellerId == userId)
                .Select(p => new Property
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
                    Tags = p.Tags,
                    CommercialDetails = p.CommercialDetails,
                    HostelDetails = p.HostelDetails,
                    HouseDetails = p.HouseDetails,
                    LandDetails = p.LandDetails,
                    ProductDetails = p.ProductDetails,
                    PostedOn = p.PostedOn,
                    LocationLat = p.LocationLat,
                    LocationLon = p.LocationLon,
                    PropertyStatus = p.PropertyStatus,
                    Files = p.Files,
                    isApproved = p.isApproved,
                    SellerId = p.SellerId
                })
                .FirstOrDefaultAsync();
            return property;
        }

        public async Task<PropertyAnalyticsResultDTO> GetPropertyAnalytics(int userId, int propertyId)
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istTimeZone);

            var last7Days = istNow.Date.AddDays(-6);

            var propertyExists = await _context.Properties
                    .Where(p => p.SellerId == userId && p.PropertyId == propertyId)
                    .FirstOrDefaultAsync();


            if (propertyExists == null)
            {
                throw new PropertyNotFound();
            }

            var totalViews = await _context.PropertyUserViewed
                .Where(v => v.PropertyId == propertyId)
                .CountAsync();

            var last7DaysAnalytics = await _context.PropertyUserViewed
                .Where(v => v.PropertyId == propertyId && v.CreatedOn >= last7Days)
                .GroupBy(v => v.CreatedOn.Date)
                .Select(g => new PropertyAnalyticsDTO
                {
                    Date = g.Key,
                    Visits = g.Count()
                })
                .ToListAsync();

            var resultAnalytics = Enumerable.Range(0, 7)
                .Select(i => last7Days.AddDays(i).Date)
                .GroupJoin(
                    last7DaysAnalytics,
                    date => date,
                    analytics => analytics.Date,
                    (date, analytics) => new PropertyAnalyticsDTO
                    {
                        Date = date,
                        Visits = analytics.FirstOrDefault()?.Visits ?? 0
                    })
                .ToList();

            return new PropertyAnalyticsResultDTO
            {
                TotalViews = totalViews,
                Last7DaysAnalytics = resultAnalytics
            };
        }

    }
}
