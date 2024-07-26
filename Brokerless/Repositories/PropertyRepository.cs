using Brokerless.Context;
using Brokerless.DTOs.Property;
using Brokerless.Enums;
using Brokerless.Interfaces.Repositories;
using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Repositories
{
    public class PropertyRepository : BaseRepository<Property, int>, IPropertyRepository
    {
        public PropertyRepository(BrokerlessDBContext _context) : base(_context) { }

        public async Task<List<PropertyReturnDTO>> GetAll(PropertySearchFilterDTO propertySearchFilterDTO)
        {
            int PAGE_SIZE = 5;
            int PAGE_NUMBER = propertySearchFilterDTO.PageNumber;

            var baseQuery = _context.Properties.AsQueryable();

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
                    .Where(p => p.Tags.Any(t => propertySearchFilterDTO.Tags.Contains(t.TagValue)));
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
                Tags = p.Tags.Select(t => t.TagValue).ToList(),
                CommercialDetails = p.CommercialDetails,
                HostelDetails = p.HostelDetails,
                HouseDetails = p.HouseDetails,
                LandDetails = p.LandDetails,
                ProductDetails = p.ProductDetails,
                PostedOn = p.PostedOn
            }).ToListAsync();


            return propertyReturnDTOs;
        }

       
        public async Task<PropertyUserViewed> GetPropertyWithViewedUserById(int userId, int propertyId)
        {
            var property = await _context.PropertyUserViewed
                .Where(p => p.PropertyId == propertyId && p.UserId == userId)
                .Include(p=>p.Property)
                .FirstOrDefaultAsync();

            return property;
        
        }

        public async Task<PropertyReturnDTO> GetPropertyWithSellerDetails(int propertyId, bool isPropertyDetailsAllowed)
        {
            PropertyReturnDTO? propertyReturnDTO = await _context.Properties
                .Where(p => p.PropertyId == propertyId)
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
                    Tags = p.Tags.Select(t => t.TagValue).ToList(),
                    CommercialDetails = p.CommercialDetails,
                    HostelDetails = p.HostelDetails,
                    HouseDetails = p.HouseDetails,
                    LandDetails = p.LandDetails,
                    ProductDetails = p.ProductDetails,
                    PostedOn = p.PostedOn,
                    LocationLat = isPropertyDetailsAllowed ? p.LocationLat : null,
                    LocationLon =  isPropertyDetailsAllowed ? p.LocationLon : null,
                    SellerDetails = isPropertyDetailsAllowed ? new SellerDetailsReturnDTO
                    {
                        SellerId = p.SellerId,
                        Name = p.Seller.FullName,
                        CountryCode = p.Seller.CountryCode,
                        PhoneNumber = p.Seller.PhoneNumber
                    } : null
                }).FirstOrDefaultAsync();

            return propertyReturnDTO;
        }


    }
}
