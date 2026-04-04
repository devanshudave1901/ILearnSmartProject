using Azure;
using Azure.Storage.Blobs;
using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ILearnSmartProject.Repositories
{
    public class CoursesUserPurchaseRepository : ICoursesUserPurchaseRepository
    {
        private readonly LearnSmartContext _learnSmartContext;
        public CoursesUserPurchaseRepository(LearnSmartContext learnSmartContext)
        {
            _learnSmartContext = learnSmartContext;
        }


        public async Task<int> CreateNewPurchaseEntry(string courseId, string userId)
        {
            CoursesUserPurchase coursePurchase = new CoursesUserPurchase();
            var course = await _learnSmartContext.Courses.Where(u => u.Id.ToString() == courseId).FirstOrDefaultAsync();
            var user = await _learnSmartContext.Users.Where(u => u.Id.ToString() == userId).FirstOrDefaultAsync();
            coursePurchase.Course = course;
            coursePurchase.User = user;
            var coursePurchaseDataToEnter = await _learnSmartContext.CoursesUserPurchases.AddAsync(coursePurchase);
            var result = _learnSmartContext.SaveChangesAsync().Result;
            return 0;
        }

        public async Task<List<Course>> GetAllPurchasesByUserId(string userId)
        {
            var purchases = await _learnSmartContext.CoursesUserPurchases.Where(u => u.User.Id.ToString() == userId).Include(c => c.Course).Include(u=>u.User).ToListAsync();


            var coursePurchase = (from purchase in purchases
                                  join course in _learnSmartContext.Courses on purchase.Course.Id equals course.Id
                                  where purchase.User.Id.ToString() == userId
                                  select new Course
                                  {
                                      Id = course.Id,
                                      CourseTitle = course.CourseTitle,
                                      CourseDescription = course.CourseDescription,
                                      CoursePrice = course.CoursePrice,
                                      CourseEnabled = course.CourseEnabled,
                                      BlobName = course.BlobName,
                                      IsEdit = course.IsEdit
                                  }).ToList();


            return coursePurchase;

        }
    }
}
