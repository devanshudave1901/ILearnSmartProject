using Azure;
using Azure.Storage.Blobs;
using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Models;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ILearnSmartProject.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LearnSmartContext _learnSmartContext;
        public CourseRepository(LearnSmartContext learnSmartContext)
        {
            _learnSmartContext = learnSmartContext;
        }
        // upload files to blob storage and return the url of the uploaded file
        public async Task<string> UploadFileToBlob(IFormFile file, string azureContainerName, string connectionString)
        {
            try
            {
                BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, azureContainerName);

                BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);

                 using (Stream? data = file.OpenReadStream())

                {
                    await blobClient.UploadAsync(data, overwrite: true);
                }
                var url = blobClient.Uri.AbsoluteUri;
                return url;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file to Azure Blob Storage: {ex.Message}");
            }
      

            return "good";

        }

        public async Task<int> CreateNewCourse(Course course)
        {


            var courseDataToEnter = await _learnSmartContext.Courses.AddAsync(course);

            var result = _learnSmartContext.SaveChangesAsync().Result;
            return 0;
        }
    }
}
