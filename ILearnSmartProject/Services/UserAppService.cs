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


    }
}