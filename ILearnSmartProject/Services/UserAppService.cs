using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILearnSmartProject.Models;
using ILearnSmartProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace ILearnSmartProject.Services
{
    public class UserAppService
    {
        private readonly UserRepository _userRepository;

        public UserAppService(UserRepository userRepository)
        {

            _userRepository = userRepository;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var userData = await _userRepository.GetAllUsers();

         

            return userData;
        }

        public async Task<int> CheckLogin(string email, string password)
        {
            var userData = await _userRepository.CheckLogin(email, password);
            return userData;
        }

        public async Task<string> LoginUserType(string email)
        {
            var userType = await _userRepository.LoginUserType(email);
            return userType;

        }
        public async Task<int> RegisterUser(string FirstName, string LastName, string EmailAddress, string Password)
        {
            var userRegister = await _userRepository.RegisterUser(FirstName, LastName, EmailAddress, Password);

            return userRegister;
        }


    }
}