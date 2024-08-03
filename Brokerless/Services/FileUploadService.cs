using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Brokerless.Interfaces.Services;
using Azure;
using Brokerless.Models;
using Brokerless.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Brokerless.Exceptions;

namespace Brokerless.Services
{
    public class FileUploadService : IFileUploadService
    {

        private readonly string _azureConnectionString;
        private readonly string _containerName;

        public FileUploadService(IConfiguration configuration)
        {
            _azureConnectionString = configuration.GetValue<string>("AzureStorage:ConnectionString");
            _containerName = configuration.GetValue<string>("AzureStorage:ContainerName");
        }

        public async Task<List<PropertyFile>> UploadFilesToAzure(List<IFormFile> filesToUpload)
        {
            List<PropertyFile> files = new List<PropertyFile>();
            List<string> allowedFileTypes = new List<string> { "video/mp4", "image/jpeg", "image/jpg", "image/png" };
            long maxFileSizeInBytes = 20 * 1024 * 1024; // 5 MB



            foreach (var file in filesToUpload)
            {
                long fileSize = file.Length;
                string fileContentType = file.ContentType;

                if (fileSize > maxFileSizeInBytes)
                {
                    throw new FileRuleException($"File size exceeds the maximum limit of 20 MB. File: {file.FileName}, Size: {fileSize} bytes");
                }
                if (!allowedFileTypes.Contains(fileContentType))
                {
                    throw new FileRuleException($"Invalid file type. Only mp4, jpg, jpeg, and png files are allowed. File: {file.FileName}, Type: {fileContentType}");
                }

            }

            foreach (var file in filesToUpload)
            {
                string url = await UploadFileToAzure(file);
                var propertyFile = new PropertyFile
                {
                    FileSize = file.Length,
                    FileType = file.ContentType.Contains("video") ? FileType.Video : FileType.Image,
                    FileUrl = url
                };
                files.Add(propertyFile);
            }

            return files;
        }

        private async Task<string> UploadFileToAzure(IFormFile file)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureConnectionString);


            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            await containerClient.CreateIfNotExistsAsync();

            string uniqueBlobName = Guid.NewGuid().ToString();
            BlobClient blobClient = containerClient.GetBlobClient(uniqueBlobName);

            using (var stream = file.OpenReadStream())
            {
                var headers = new BlobHttpHeaders
                {
                    ContentDisposition = $"inline; filename={file.FileName}"
                };
                await blobClient.UploadAsync(stream, overwrite: true);
                await blobClient.SetHttpHeadersAsync(headers);
                return blobClient.Uri.ToString();
            }
        }

        public async Task DeleteFileFromAzure(string blobUrl)
        {
            Uri uri = new Uri(blobUrl);
            string blobName = uri.Segments.Last();

            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureConnectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }


    }
}
