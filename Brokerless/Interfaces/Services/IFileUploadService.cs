using Brokerless.Models;

namespace Brokerless.Interfaces.Services
{
    public interface IFileUploadService
    {

        public Task<List<PropertyFile>> UploadFilesToAzure(List<IFormFile> filesToUpload);
        public Task DeleteFileFromAzure(string blobUrl);

    }
}
