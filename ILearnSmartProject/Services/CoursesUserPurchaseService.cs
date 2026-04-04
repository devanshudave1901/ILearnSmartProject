using Azure.Storage.Blobs;
using ILearnSmartProject.Interfaces;
using ILearnSmartProject.Models;
using ILearnSmartProject.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ILearnSmartProject.Services
{
    public class CoursesUserPurchaseService
    {
        private readonly CoursesUserPurchaseRepository _coursePurchaseRepository;


        public CoursesUserPurchaseService(CoursesUserPurchaseRepository coursePurchaseRepository)
        {

            _coursePurchaseRepository = coursePurchaseRepository;

        }

        public async Task<int> CreateEntry(string courseId, string userId)
        {

           

            await _coursePurchaseRepository.CreateNewPurchaseEntry(courseId,userId);

            return 0;
        }

        public async Task<List<Course>> GetAllPurchasesByUserId(string userId)
        {
            return await _coursePurchaseRepository.GetAllPurchasesByUserId(userId);
        }



     }
}