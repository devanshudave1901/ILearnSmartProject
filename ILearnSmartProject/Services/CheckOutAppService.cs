using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILearnSmartProject.Models;
using ILearnSmartProject.Payment.StripeManager;
using ILearnSmartProject.Repositories;
using Microsoft.EntityFrameworkCore;
using MailKit;

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



        public async Task<List<string>> CreateCheckOutSession(string courseId, string successUrl, string cancelUrl)
        {
            var sessionDetails = await _checkOutSession.CreateCheckOutSession(courseId,successUrl, cancelUrl);
            return sessionDetails;

        }

        public async Task<string> ConfirmPayment(string sessionId)
        {
            // confirm the payment and return the status to the client
            var paymentStatus = await _checkOutSession.ConfirmPayment(sessionId);
            return paymentStatus;
        }


    }
}
