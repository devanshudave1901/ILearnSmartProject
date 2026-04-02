using Azure.Storage.Blobs;
using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Models;
using ILearnSmartProject.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ILearnSmartProject.Services
{
    public class CourseAppService
    {
        private readonly CourseRepository _courseRepository;
        private readonly AzureBlobModel _appSettings;

        private readonly IConfiguration _configuration;

        public CourseAppService(CourseRepository courseRepository, IOptions<AzureBlobModel> appSettings)
        {

            _courseRepository = courseRepository;
            _appSettings = appSettings.Value;

        }

        public async Task<int> UploadFileToBlob(IFormFile file)
        {
            var azureContainerName = _appSettings.ContainerName;
            var connectionString = _appSettings.ConnectionString;

            // this method will upload the file to azure blob storage and return the url of the uploaded file
            // from this method not repository.
            var sucess = await _courseRepository.UploadFileToBlob(file, azureContainerName, connectionString);

            return 0;
        }

        public async Task<IFormFile> FetchBlobFileFromAzure(string blobUrl)
        {
            var azureContainerName = _appSettings.ContainerName;
            var connectionString = _appSettings.ConnectionString;

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, azureContainerName);


            BlobClient blobClient = blobContainerClient.GetBlobClient("DevanshuDave_A1_Recording.mp4");

            Stream stream = await blobClient.OpenReadAsync();

            var properties = await blobClient.GetPropertiesAsync();
            IFormFile file = new FormFile(stream, 0, stream.Length, null, blobClient.Name)
            {
                Headers = new HeaderDictionary(),
                ContentType = properties.Value.ContentType,
                ContentDisposition = $"attachment; filename={blobClient.Name}",
                
            };
            return file;
        }
    }
}