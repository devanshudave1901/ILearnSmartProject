using ILearnSmartProject.Models;

namespace ILearnSmartProject.Interfaces
{
    public interface ICoursesUserPurchaseRepository
    {
        public Task<int> CreateNewPurchaseEntry(string courseId, string userId);
        public Task<List<Course>> GetAllPurchasesByUserId(string userId);
        
    }
}
