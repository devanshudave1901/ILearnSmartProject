using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ILearnSmartProject.Repositories
{
    public class CourseRepository
    {

        public CourseRepository()
        {
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
           

            // this method will upload the file to azure blob storage and return the url of the uploaded file
            // from this method not repository.

            //var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(connectionString);

            //var containerName = azureContainerName;

            //var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            //var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            //var blobClient = containerClient.GetBlobClient(fileName);
            //// uploading the video file to azure blob storage

            //using var uploadFileStream = file.OpenReadStream();
            //var res = await blobClient.UploadAsync(uploadFileStream, overwrite: true);
            //uploadFileStream.Close(); // Ensure the stream is closed after upload



            return "good";

        }
    }
}
