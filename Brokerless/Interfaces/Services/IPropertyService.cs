using Brokerless.DTOs.Property;

namespace Brokerless.Interfaces.Services
{
    public interface IPropertyService
    {
        public Task CreateProperty(int userId, IFormCollection formCollection, BasePropertyDTO basePropertyDTO, List<IFormFile> formFiles);
    }
}
