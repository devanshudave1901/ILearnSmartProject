using Azure;
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
      

            return "good";

        }

        //public async Task<File> FetchBlobFileFromAzure(string blobUrl, string azureContainerName,string connectionString)
        //{
        //    try
        //    {
        //        // fetching the file from blob storage using the url and return it as a file
        //        BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, azureContainerName);

        //        BlobClient blobClient = new BlobClient(new Uri(blobUrl), new AzureSasCredential(connectionString));

        //        var file = await blobClient.DownloadContentAsync();

        //        using (var stream = file.Value.Content.ToStream())
        //        {
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                await stream.CopyToAsync(memoryStream);
        //                var fileBytes = memoryStream.ToArray();
        //                var fileName = Path.GetFileName(blobUrl);
        //                return File(fileBytes, "application/octet-stream", fileName);
        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching file from Azure Blob Storage: {ex.Message}");
        //        return null;
        //    }
        //}
    }
}
