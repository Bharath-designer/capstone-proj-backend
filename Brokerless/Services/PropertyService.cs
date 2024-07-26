using Brokerless.DTOs.Property;
using Brokerless.Enums;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;
using Brokerless.Repositories;

namespace Brokerless.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IUserRepository userRepository, ITagRepository tagRepository, IPropertyRepository propertyRepository) {
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _propertyRepository = propertyRepository;
        }


        public async Task CreateProperty(int userId, BasePropertyDTO basePropertyDTO, List<IFormFile> formFiles)
        {
            User user = await _userRepository.GetUserWithSubscription(userId);

            if (user.PhoneNumberVerified == false) {
                throw new MobileNotVerifiedException();
            }

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

            try {
                switch (basePropertyDTO.PropertyType)
                {
                    case PropertyType.Residential:
                        if (basePropertyDTO.PropertyCategory == PropertyCategory.House)
                        {
                            HouseDetailsDTO houseDetailsDTO = (HouseDetailsDTO)basePropertyDTO;
                            property.HouseDetails = new HouseDetails
                            {
                                CarParking = (bool)houseDetailsDTO.CarParking,
                                Electricity = (Electricity)houseDetailsDTO.Electricity,
                                FloorCount = (int)houseDetailsDTO.FloorCount,
                                FurnishingDetails = (FurnishingDetails)houseDetailsDTO.FurnishingDetails,
                                GatedSecurity = (bool)houseDetailsDTO.GatedSecurity,
                                HallAndKitchenAvailable = (bool)houseDetailsDTO.HallAndKitchenAvailable,
                                Height = houseDetailsDTO.Height,
                                Length = (double)houseDetailsDTO.Length,
                                MeasurementUnit = (MeasurementUnit)houseDetailsDTO.MeasurementUnit,
                                RestroomCount = (int)houseDetailsDTO.RestroomCount,
                                Width = (double)houseDetailsDTO.Width,
                                WaterSupply = (WaterSupply)houseDetailsDTO.WaterSupply,
                                RoomCount = (int)houseDetailsDTO.RoomCount
                            };
                        }
                        else
                        {
                            HostelDetailsDTO hostelDetailsDTO = (HostelDetailsDTO)basePropertyDTO;
                            property.HostelDetails = new HostelDetails
                            {
                                Food = (FoodDetails)hostelDetailsDTO.Food,
                                GatedSecurity = (bool)hostelDetailsDTO.GatedSecurity,
                                GenderPreference = (Gender)hostelDetailsDTO.GenderPreference,
                                TypesOfRooms = (TypesOfRooms)hostelDetailsDTO.TypesOfRooms,
                                Wifi = (bool)hostelDetailsDTO.Wifi
                            };
                        };
                        break;
                    case PropertyType.Commercial:
                        CommercialDetailsDTO commercialDetailsDTO = (CommercialDetailsDTO)basePropertyDTO;
                        property.CommercialDetails = new CommercialDetails
                        {
                            CarParking = (int)commercialDetailsDTO.CarParking,
                            CommercialType = (CommercialType)commercialDetailsDTO.CommercialType,
                            Electricity = (Electricity)commercialDetailsDTO.Electricity,
                            FloorCount = (int)commercialDetailsDTO.FloorCount,
                            Length = (double)commercialDetailsDTO.Length,
                            Height = commercialDetailsDTO.Height,
                            GatedSecurity = (bool)commercialDetailsDTO.GatedSecurity,
                            Width = (double)commercialDetailsDTO.Width,
                            RestroomCount = (int)commercialDetailsDTO.RestroomCount,
                            WaterSupply = (WaterSupply)commercialDetailsDTO.WaterSupply,
                            MeasurementUnit = (MeasurementUnit)commercialDetailsDTO.MeasurementUnit
                        };
                        break;
                    case PropertyType.Product:
                        ProductDetailsDTO productDetailsDTO = (ProductDetailsDTO)basePropertyDTO;
                        property.ProductDetails = new ProductDetails
                        {
                            Manufacturer = productDetailsDTO.Manufacturer,
                            ProductType = (ProductType)productDetailsDTO.ProductType,
                            WarrantyPeriod = productDetailsDTO.WarrantyPeriod,
                            WarrantyUnit = productDetailsDTO.WarrantyUnit
                        };
                        break;

                    case PropertyType.Land:
                        LandDetailsDTO landDetailsDTO = (LandDetailsDTO)basePropertyDTO;
                        property.LandDetails = new LandDetails
                        {
                            Length = (double)landDetailsDTO.Length,
                            LandDetailsId = (int)landDetailsDTO.MeasurementUnit,
                            MeasurementUnit = (MeasurementUnit)landDetailsDTO.MeasurementUnit,
                            Width = (double)landDetailsDTO.Width,
                            ZoningType = (ZoningType)landDetailsDTO.ZoningType
                        };
                        break;
                }
            }
            catch(InvalidCastException ex)
            {
                throw new CustomModelFieldError($"Invalid values for " +
                    $"'{basePropertyDTO.PropertyType}{(basePropertyDTO.PropertyCategory != null ? $" - {basePropertyDTO.PropertyCategory}" : "")}' type");
            }
            catch (Exception ex) {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new CustomModelFieldError("Validation field for given values");
            }

            await _userRepository.Update(user);

        }

        public async Task<List<PropertyReturnDTO>> GetPropertiesWithFilters(PropertySearchFilterDTO propertySearchFilterDTO)
        {

            var data = await _propertyRepository.GetAll(propertySearchFilterDTO);
            return data;
        }

        public async Task<PropertyReturnDTO> GetPropertyByIdForUser(int userId, int propertyId)
        {
            PropertyUserViewed propertyUserViewed = await _propertyRepository.GetPropertyWithViewedUserById(userId, propertyId);

            bool isPropertyDetailsAllowed = false;

            if (propertyUserViewed != null)
            {
                isPropertyDetailsAllowed = true;
            }

            PropertyReturnDTO propertyReturnDTO = await _propertyRepository.GetPropertyWithSellerDetails(propertyId, isPropertyDetailsAllowed);

            if (propertyReturnDTO == null)
            {
                throw new PropertyNotFound();
            }

            return propertyReturnDTO;
        }
    }
}
