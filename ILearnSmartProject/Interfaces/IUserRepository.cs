using ILearnSmartProject.Models;

namespace ILearnSmartProject.Interfaces
{
    public interface IUserRepository
    {

        public Task<List<Users>> GetAllUsers();
        public Task<int> CheckLogin(string email, string password);
    }
}
