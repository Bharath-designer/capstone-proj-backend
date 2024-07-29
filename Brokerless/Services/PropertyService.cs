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
        private readonly IFileUploadService _fileUploadService;

        public PropertyService(IUserRepository userRepository, ITagRepository tagRepository, IPropertyRepository propertyRepository, IFileUploadService fileUploadService)
        {
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _propertyRepository = propertyRepository;
            _fileUploadService = fileUploadService;
        }


        public async Task CreateProperty(int userId, BasePropertyDTO basePropertyDTO, List<IFormFile> formFiles)
        {

            User user = await _userRepository.GetUserWithSubscription(userId);

            if (user.PhoneNumberVerified == false)
            {
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
                }
                else
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

            try
            {
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
            catch (InvalidCastException ex)
            {
                throw new CustomModelFieldError($"Invalid values for " +
                    $"'{basePropertyDTO.PropertyType}{(basePropertyDTO.PropertyCategory != null ? $" - {basePropertyDTO.PropertyCategory}" : "")}' type");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new CustomModelFieldError("Validation failed for given values");
            }

            property.Files = await _fileUploadService.UploadFilesToAzure(formFiles);
            await _userRepository.Update(user);
        }

        public async Task<List<PropertyReturnDTO>> GetPropertiesWithFilters(int? userId, PropertySearchFilterDTO propertySearchFilterDTO)
        {

            var data = await _propertyRepository.GetAll(userId, propertySearchFilterDTO, false);
            return data;
        }

        public async Task<PropertyReturnDTO> GetPropertyByIdForUser(int? userId, int propertyId)
        {
            bool isPropertyDetailsAllowed = false;

            if (userId != null)
            {
                PropertyUserViewed propertyUserViewed = await _propertyRepository.GetPropertyWithViewedUserById((int)userId, propertyId);

                if (propertyUserViewed != null)
                {
                    isPropertyDetailsAllowed = true;
                }
            }

            PropertyReturnDTO propertyReturnDTO = await _propertyRepository.GetPropertyWithSellerDetails(propertyId, isPropertyDetailsAllowed);

            if (propertyReturnDTO == null)
            {
                throw new PropertyNotFound();
            }

            return propertyReturnDTO;
        }

        public async Task RequestForProperty(int userId, RequestPropertyDTO requestPropertyDTO)
        {

            User user = await _userRepository.GetUserWithSubscription(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            PropertyUserViewed propertyUserViewed = await _propertyRepository.GetPropertyWithViewedUserById(userId, (int)requestPropertyDTO.PropertyId);

            if (propertyUserViewed != null)
            {
                throw new PropertyDetailsRequestAlreadySatisfied();
            }

            if (user.UserSubscription.AvailableSellerViewCount == 0)
            {
                throw new PropertyViewingLimitExceededException();
            }

            Property property = await _propertyRepository.GetById((int)requestPropertyDTO.PropertyId);

            if (property == null)
            {
                throw new PropertyNotFound();
            }
            else if (property.SellerId == userId)
            {
                throw new OwnPropertyRequestedException();
            }
            else if (property.isApproved == false)
            {
                throw new PropertyNotFound();
            }

            user.PropertiesViewed = new List<PropertyUserViewed> { new PropertyUserViewed {
                Property = property
            } };

            user.UserSubscription.AvailableSellerViewCount--;

            await _userRepository.Update(user);

        }

        public async Task UpdateProperty(int userId, UpdateBasePropertyDTO basePropertyDTO, List<IFormFile> formFiles)
        {
            Property property = await _propertyRepository.GetPropertyOfSellerByPropertyIdWithTagsAndFiles(userId, (int)basePropertyDTO.PropertyId);
            if (property == null)
            {
                throw new PropertyNotFound();
            }

            if (basePropertyDTO.ListingType == ListingType.Sale)
            {
                if (basePropertyDTO.Price == null)
                {
                    throw new CustomModelFieldError("Price is required");
                }
                else
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

            var tags = property.Tags;

            if (basePropertyDTO.TagsToBeAdded != null)
            {
                foreach (var tagName in basePropertyDTO.TagsToBeAdded)
                {
                    var tag = tags.FirstOrDefault(t=>t.TagValue == tagName);
                    if (tag == null)
                    {
                        tag = new Tag { TagValue = tagName };
                        await _tagRepository.Add(tag);
                    }
                    tags.Add(tag);
                }
            }

            if (basePropertyDTO.TagsToBeRemoved != null)
            {
                foreach (var tagName in basePropertyDTO.TagsToBeRemoved)
                {
                    Tag? tag = tags.FirstOrDefault(t => t.TagValue == tagName);
                    if (tag != null)
                    {
                        tags.Remove(tag);
                    }
                }
            }

            if (basePropertyDTO.FilesToBeRemoved != null)
            {
                foreach (var fileUrl in basePropertyDTO.FilesToBeRemoved)
                {
                    PropertyFile? file = property.Files.FirstOrDefault(pf=>pf.FileUrl == fileUrl);
                    if (file != null)
                    {
                        property.Files.Remove(file);
                        await _fileUploadService.DeleteFileFromAzure(fileUrl);
                    }
                }
            }

            property.Files = await _fileUploadService.UploadFilesToAzure(formFiles);

            property.City = basePropertyDTO.City;
            property.State = basePropertyDTO.State;
            property.Currency = (Currency)basePropertyDTO.Currency;
            property.Deposit = basePropertyDTO.Deposit;
            property.ListingType = (ListingType)basePropertyDTO.ListingType;
            property.LocationLat = (double)basePropertyDTO.LocationLat;
            property.LocationLon = (double)basePropertyDTO.LocationLon;
            property.PriceNegotiable = (bool)basePropertyDTO.PriceNegotiable;
            property.PropertyStatus = (PropertyStatus)basePropertyDTO.PropertyStatus;
            try
            {
                switch (property.PropertyType)
                {
                    case PropertyType.Residential:
                        if (property.PropertyCategory == PropertyCategory.House)
                        {
                            UpdateHouseDetailsDTO houseDetailsDTO = (UpdateHouseDetailsDTO)basePropertyDTO;

                            property.HouseDetails.CarParking = (bool)houseDetailsDTO.CarParking;
                            property.HouseDetails.Electricity = (Electricity)houseDetailsDTO.Electricity;
                            property.HouseDetails.FloorCount = (int)houseDetailsDTO.FloorCount;
                            property.HouseDetails.FurnishingDetails = (FurnishingDetails)houseDetailsDTO.FurnishingDetails;
                            property.HouseDetails.GatedSecurity = (bool)houseDetailsDTO.GatedSecurity;
                            property.HouseDetails.HallAndKitchenAvailable = (bool)houseDetailsDTO.HallAndKitchenAvailable;
                            property.HouseDetails.Height = houseDetailsDTO.Height;
                            property.HouseDetails.Length = (double)houseDetailsDTO.Length;
                            property.HouseDetails.MeasurementUnit = (MeasurementUnit)houseDetailsDTO.MeasurementUnit;
                            property.HouseDetails.RestroomCount = (int)houseDetailsDTO.RestroomCount;
                            property.HouseDetails.Width = (double)houseDetailsDTO.Width;
                            property.HouseDetails.WaterSupply = (WaterSupply)houseDetailsDTO.WaterSupply;
                            property.HouseDetails.RoomCount = (int)houseDetailsDTO.RoomCount;
                        }
                        else
                        {
                            UpdateHostelDetails hostelDetailsDTO = (UpdateHostelDetails)basePropertyDTO;
                            property.HostelDetails.Food = (FoodDetails)hostelDetailsDTO.Food;
                            property.HostelDetails.GatedSecurity = (bool)hostelDetailsDTO.GatedSecurity;
                            property.HostelDetails.GenderPreference = (Gender)hostelDetailsDTO.GenderPreference;
                            property.HostelDetails.TypesOfRooms = (TypesOfRooms)hostelDetailsDTO.TypesOfRooms;
                            property.HostelDetails.Wifi = (bool)hostelDetailsDTO.Wifi;
                        };
                        break;
                    case PropertyType.Commercial:
                        UpdateCommercialDetailsDTO commercialDetailsDTO = (UpdateCommercialDetailsDTO)basePropertyDTO;
                        property.CommercialDetails.CarParking = (int)commercialDetailsDTO.CarParking;
                        property.CommercialDetails.CommercialType = (CommercialType)commercialDetailsDTO.CommercialType;
                        property.CommercialDetails.Electricity = (Electricity)commercialDetailsDTO.Electricity;
                        property.CommercialDetails.FloorCount = (int)commercialDetailsDTO.FloorCount;
                        property.CommercialDetails.Length = (double)commercialDetailsDTO.Length;
                        property.CommercialDetails.Height = commercialDetailsDTO.Height;
                        property.CommercialDetails.GatedSecurity = (bool)commercialDetailsDTO.GatedSecurity;
                        property.CommercialDetails.Width = (double)commercialDetailsDTO.Width;
                        property.CommercialDetails.RestroomCount = (int)commercialDetailsDTO.RestroomCount;
                        property.CommercialDetails.WaterSupply = (WaterSupply)commercialDetailsDTO.WaterSupply;
                        property.CommercialDetails.MeasurementUnit = (MeasurementUnit)commercialDetailsDTO.MeasurementUnit;
                        break;
                    case PropertyType.Product:
                        UpdateProductDetailsDTO productDetailsDTO = (UpdateProductDetailsDTO)basePropertyDTO;
                        property.ProductDetails.Manufacturer = productDetailsDTO.Manufacturer;
                        property.ProductDetails.ProductType = (ProductType)productDetailsDTO.ProductType;
                        property.ProductDetails.WarrantyPeriod = productDetailsDTO.WarrantyPeriod;
                        property.ProductDetails.WarrantyUnit = productDetailsDTO.WarrantyUnit;
                        break;

                    case PropertyType.Land:
                        UpdateLandDetailsDTO landDetailsDTO = (UpdateLandDetailsDTO)basePropertyDTO;
                        property.LandDetails.Length = (double)landDetailsDTO.Length;
                        property.LandDetails.LandDetailsId = (int)landDetailsDTO.MeasurementUnit;
                        property.LandDetails.MeasurementUnit = (MeasurementUnit)landDetailsDTO.MeasurementUnit;
                        property.LandDetails.Width = (double)landDetailsDTO.Width;
                        property.LandDetails.ZoningType = (ZoningType)landDetailsDTO.ZoningType;
                        break;
                }
            }
            catch (InvalidCastException ex)
            {
                throw new CustomModelFieldError($"Invalid values for " +
                    $"'{property.PropertyType}{(property.PropertyCategory != null ? $" - {property.PropertyCategory}" : "")}' type");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new CustomModelFieldError("Validation failed for given values");
            }

            await _propertyRepository.Update(property);

        }

    }
}
