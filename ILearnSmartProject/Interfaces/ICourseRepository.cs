using ILearnSmartProject.Models;

namespace ILearnSmartProject.Interfaces
{
    public interface ICourseRepository
    {
        public Task<int> CreateNewCourse(Course course);
        public Task<List<Course>> GetAllCourses();
        public  Task<List<Course>> GetCourseById(int id);
        public  Task<int> UpdateCourse(Course course);
        public Task<int> DeleteCourse(int id);
    }
}
