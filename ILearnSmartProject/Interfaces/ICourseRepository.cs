using ILearnSmartProject.Models;

namespace ILearnSmartProject.Interfaces
{
    public interface ICourseRepository
    {
        public Task<int> CreateNewCourse(Course course);
    }
}
