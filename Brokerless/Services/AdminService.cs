using Brokerless.DTOs.Admin;
using Brokerless.DTOs.Property;
using Brokerless.Exceptions;
using Brokerless.Interfaces.Repositories;
using Brokerless.Interfaces.Services;
using Brokerless.Models;

namespace Brokerless.Services
{
    public class AdminService : IAdminService
    {
        private readonly IPropertyRepository _propertyRepository;

        public AdminService(IPropertyRepository propertyRepository) {

            _propertyRepository = propertyRepository;


        }
        public async Task<PropertyApprovalToggleReturnDTO> TogglePropertyStatus(PropertyApprovalToggleDTO propertyApprovalToggleDTO)
        {
            Property property = await _propertyRepository.GetById((int)propertyApprovalToggleDTO.PropertyId); 
            
            if (property == null)
            {
                throw new PropertyNotFound();
            }

            property.isApproved = (bool)propertyApprovalToggleDTO.IsApproved;

            await _propertyRepository.Update(property);

            return new PropertyApprovalToggleReturnDTO
            {
                IsApproved = property.isApproved
            };
        }

        public async Task<List<PropertyReturnDTO>> GetAdminPropertiesWithFilters(AdminPropertySearchFilter adminPropertySearchFilterDTO)
        {
            var data = await _propertyRepository.GetAll(null, adminPropertySearchFilterDTO, true);
            return data;
        }

        public async Task<PropertyReturnDTO> GetAdminPropertyById(int propertyId)
        {
            PropertyReturnDTO propertyReturnDTO = await _propertyRepository.GetPropertyWithSellerDetails(propertyId, true, true);

            if (propertyReturnDTO == null)
            {
                throw new PropertyNotFound();
            }

            return propertyReturnDTO;
        }

    }
}
