using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILearnSmartProject.Models;
using ILearnSmartProject.Payment.StripeManager;
using ILearnSmartProject.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ILearnSmartProject.Services
{
    public class CheckOutAppService
    {
        // this class calls the payment manager to create a checkout session and return the session url to the client
        // get the checkout session url from the payment manager and return it to the client
        private readonly ICheckOutSession _checkOutSession;


        public CheckOutAppService(ICheckOutSession checkOutSession)
        {
            _checkOutSession = checkOutSession;
        }



        public async Task<string> CreateCheckOutSession(string priceId, string successUrl, string cancelUrl)
        {
            var sessionUrl = await _checkOutSession.CreateCheckOutSession(priceId, successUrl, cancelUrl);
            return sessionUrl;
        }




    }
}
