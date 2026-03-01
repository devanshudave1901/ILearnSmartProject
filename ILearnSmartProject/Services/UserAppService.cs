using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILearnSmartProject.Models;
using Microsoft.EntityFrameworkCore;


namespace ILearnSmartProject.Services
{
    public class UserAppService
    {
        private readonly LearnSmartContext _learnSmartContext;
        public UserAppService(LearnSmartContext learnSmartContext)
        {

            _learnSmartContext = learnSmartContext;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var userData = await _learnSmartContext.Users.ToListAsync();

            // passsing that to model 


            return userData;
        }

    }
}