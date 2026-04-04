using ILearnSmartProject.Models;

namespace ILearnSmartProject.Interfaces
{
    public interface IUserRepository
    {

        public Task<List<Users>> GetAllUsers();
        public Task<int> CheckLogin(string email, string password);
        public Task<int> RegisterUser(string FirstName, string LastName, string EmailAddress, string Password);
        public  Task<string> LoginUserType(string email);

        public Task<Users> GetUserByEmail(string email);

        public Task<string> LoggedInUserEmail(string id);
    }
}
