using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ILearnSmartProject.Repositories
{

    public class UserRepository: IUserRepository
    {
        private readonly LearnSmartContext _learnSmartContext;
        public UserRepository(LearnSmartContext learnSmartContext)
        {

            _learnSmartContext = learnSmartContext;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var userData = await _learnSmartContext.Users.ToListAsync();


            return userData;
        }

        public async Task<int> CheckLogin(string email, string password)
        {
            var userCount = await _learnSmartContext.Users.Where(u => u.EmailAddress == email && u.Password == password).CountAsync();
            return userCount;
        }
        public async Task<int> RegisterUser(string FirstName, string LastName, string EmailAddress, string Password)
        {
            DateTime CreationDate = DateTime.Now;

            DateTime UpdateDate = DateTime.Now;

            bool IsActive = true;

            bool IsDeleted = true;

            // getting usertype of Student

            var studentUserID_Data = await _learnSmartContext.UsersTypes.Where(u => u.UserTypeName == "Student").ToListAsync();
            var studentID = studentUserID_Data[0].Id;

             Users newUser = new Users(FirstName, LastName, EmailAddress,Password, CreationDate, UpdateDate, IsActive, IsDeleted, studentUserID_Data[0]);
            //Users users = new Users();

            var dbEntry = await _learnSmartContext.Users.AddAsync(newUser);

           var result = _learnSmartContext.SaveChangesAsync().Result;

            return Convert.ToInt32(result);
        }
     }
}
