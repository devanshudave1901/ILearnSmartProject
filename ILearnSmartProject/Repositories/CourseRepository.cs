using Azure;
using Azure.Storage.Blobs;
using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


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
        public async Task<int> UpdateCourse(Course course)
        {
  
            var update = _learnSmartContext.Courses.Update(course);

            var result = await _learnSmartContext.SaveChangesAsync();
            return 0;
        }
        
        public async Task<List<Course>> GetAllCourses()
        {
            var courses = await _learnSmartContext.Courses.ToListAsync();
            return courses;

        }
        public async Task<List<Course>> GetAllStudentCourses(string userId)
        {
            // RETRIVING THE COURSEs that are not entrolled by the student

            var courses = await(from c in _learnSmartContext.Courses
                                
                where !_learnSmartContext.CoursesUserPurchases.Any(e => e.Course.Id == c.Id && e.User.Id.ToString() == userId)
                select c).ToListAsync();
        
            return courses;

        }
        
        public async Task<List<Course>> GetCourseById(int id)
        {
            var courses = await _learnSmartContext.Courses.Where(u=>u.Id == id).ToListAsync();
            return courses;

        }

        public async Task<List<int>> GetDashboardData()
        {
            var totalCoursesRevenue = await (from cup in _learnSmartContext.CoursesUserPurchases
                    join c in _learnSmartContext.Courses on cup.Course.Id equals c.Id
                    select c.CoursePrice).SumAsync();
            var revenue = Convert.ToInt32(totalCoursesRevenue);


            var totalStudents = await _learnSmartContext.Users.CountAsync();
            var totalPurchases = await _learnSmartContext.CertificatesIssued.CountAsync();

            return new List<int> { revenue, totalStudents, totalPurchases };
        }

        public async Task<List<CoursesUserPurchase>> GetStudentCourseById(int id, string sessionUserID)
        {
            // including the data from the course user purhceses to know whether the course is marked as finished or not
            var courses = await (from c in _learnSmartContext.Courses
                                 join cup in _learnSmartContext.CoursesUserPurchases on c.Id equals cup.Course.Id
                                 where c.Id == id && _learnSmartContext.CoursesUserPurchases.Any(e => e.Course.Id == c.Id && e.User.Id.ToString() == sessionUserID)
                                 // including the coursesUserPurchases data along with courses
                                select cup).Include(c => c.Course).Include(u=>u.User).ToListAsync();
            return courses;

        }
        
        public async Task<int> DeleteCourse(int id)
        {
            var course = await _learnSmartContext.Courses.Where(u => u.Id == id).ExecuteDeleteAsync();
            await _learnSmartContext.SaveChangesAsync();
            return 0;

        }


      




        

    }
}
