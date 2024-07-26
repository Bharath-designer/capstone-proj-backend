using Brokerless.DTOs.Property;
using Brokerless.Enums;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;
using Brokerless.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;

        public PropertyService(IUserRepository userRepository, ITagRepository tagRepository) {
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }


        public async Task CreateProperty(int userId, IFormCollection formCollection, BasePropertyDTO basePropertyDTO, List<IFormFile> formFiles)
        {
            User user = await _userRepository.GetUserWithSubscription(userId);

            if (user.UserSubscription.AvailableListingCount == 0)
            {
                throw new PropertyPostingLimitExceededException();
            }

            Property property = new Property();

            if (basePropertyDTO.PropertyType == PropertyType.Residential)
            {
                if (basePropertyDTO.PropertyCategory == null)
                {
                    throw new CustomModelFieldError("Category is required");
                }
                else
                {
                    property.PropertyCategory = basePropertyDTO.PropertyCategory;
                }
            }

            if (basePropertyDTO.ListingType == ListingType.Sale)
            {
                if (basePropertyDTO.Price == null)
                {
                    throw new CustomModelFieldError("Price is required");
                } else
                {
                    property.Price = basePropertyDTO.Price;
                    property.PricePerUnit = basePropertyDTO.PricePerUnit;
                }
            }

            if (basePropertyDTO.ListingType == ListingType.Rent)
            {
                if (basePropertyDTO.Rent == null) throw new CustomModelFieldError("Rent is required");
                else property.Rent = basePropertyDTO.Rent;
                if (basePropertyDTO.RentDuration == null) throw new CustomModelFieldError("Rent Duration is required");
                else property.RentDuration = basePropertyDTO.RentDuration;
            }

            var tags = new List<Tag>();

            foreach (var tagName in basePropertyDTO.Tags)
            {
                var tag = await _tagRepository.GetById(tagName);
                if (tag == null)
                {
                    tag = new Tag { TagValue = tagName };
                    await _tagRepository.Add(tag);
                }
                tags.Add(tag);
            }


            property.PropertyType = (PropertyType)basePropertyDTO.PropertyType;
            property.City = basePropertyDTO.City;
            property.State = basePropertyDTO.State;
            property.Currency = (Currency)basePropertyDTO.Currency;
            property.Deposit = basePropertyDTO.Deposit;
            property.Description = basePropertyDTO.Description;
            property.ListingType = (ListingType)basePropertyDTO.ListingType;
            property.LocationLat = (double)basePropertyDTO.LocationLat;
            property.LocationLon = (double)basePropertyDTO.LocationLon;
            property.PriceNegotiable = (bool)basePropertyDTO.PriceNegotiable;
            property.Tags = tags;

            user.Listings = new List<Property> { property };

            user.UserSubscription.AvailableListingCount--;

            switch (basePropertyDTO.PropertyType)
            {
                case PropertyType.Residential:
                    if (basePropertyDTO.PropertyCategory == PropertyCategory.House)
                    {
                        property.HouseDetails = new HouseDetails
                        {

                        };
                    }
                    else
                    {
                        property.HostelDetails = new HostelDetails
                        {

                        };
                    }
                    break;
                case PropertyType.Commercial:
                    break;
                case PropertyType.Product:
                    break;

                case PropertyType.Land:
                    break;
            }

            //await _userRepository.Update(user);

        }

    }
}
