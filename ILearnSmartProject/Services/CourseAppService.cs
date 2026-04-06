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

        public async Task<int> CreateCourse(Course course)
        {

            // add the file to blob
            var file = course.CourseVideoFile;
            var url = await UploadFileToBlob(file);

            // breaking the url.

            var BlobName = url.Split('/').Last();

            course.BlobName = BlobName;

            await _courseRepository.CreateNewCourse(course);

            return 0;
        }

        public async Task<int> UpdateCourse(Course course)
        {
            // add the file to blob
            var file = course.CourseVideoFile;
            var BlobName = course.BlobName;
            if (course.CourseVideoFile != null)
            {
               var url = await UploadFileToBlob(file);
                BlobName = url.Split('/').Last();
                course.BlobName = BlobName;
            }
            // breaking the url.
   
            
            await _courseRepository.UpdateCourse(course);
            return 0;
        }


        public async Task<string> UploadFileToBlob(IFormFile file)
        {
            var azureContainerName = _appSettings.ContainerName;
            var connectionString = _appSettings.ConnectionString;

            // this method will upload the file to azure blob storage and return the url of the uploaded file
            // from this method not repository.
            var sucess = await _courseRepository.UploadFileToBlob(file, azureContainerName, connectionString);

            return sucess;
        }

        public async Task<List<int>> GetDashboardData()
        {
            var data = await _courseRepository.GetDashboardData();

            return data;
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

        public async Task<List<Course>> GetAllCourses()
        {
            return await _courseRepository.GetAllCourses();
        }
        public async Task<List<Course>> GetAllStudentCourses(string userId)
        {
            return await _courseRepository.GetAllStudentCourses(userId);
        }
        public async Task<List<Course>> GetCourseById(int id)
        {
            return await _courseRepository.GetCourseById(id);
        }
        public async Task<List<CoursesUserPurchase>> GetStudentCourseById(int id, string sessionUserID)
        {
            return await _courseRepository.GetStudentCourseById(id,sessionUserID);
        }

        
        public async Task<int> DeleteCourse(int id)
        {
            return await _courseRepository.DeleteCourse(id);
        }

        
    }
}