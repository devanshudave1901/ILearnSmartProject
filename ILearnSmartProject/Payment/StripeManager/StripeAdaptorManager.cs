using ILearnSmartProject.Models;
using ILearnSmartProject.Payment;
using Microsoft.Extensions.Options;
using Stripe;
namespace ILearnSmartProject.Payment.StripeManager                                                                                                                      
{
    public class StripeAdaptorManager : ICheckOutSession
    {
        private readonly IOptions<StripeModel> _model;
        public StripeAdaptorManager(IOptions<StripeModel> model ) { 
            _model = model;
        }
  
        public async Task<List<string>> CreateCheckOutSession( string priceId, string successUrl, string cancelUrl)
        {

            string apiKey = _model.Value.SecretKey;

            StripeConfiguration.ApiKey = apiKey;

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = successUrl+"/stripe/webhook",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                {
                    new Stripe.Checkout.SessionLineItemOptions
                    {
                        Price = "price_1TAl8I0bWEACmO5c8NTifqgp",
                        Quantity = 1,                                    
                    },
                },
                Mode = "payment",
            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);                                 

            List<String> sessionDetails = new List<string>();
            sessionDetails.Add(session.Id);
            sessionDetails.Add(session.Url);
                                                                                                                               
                                                                                     
            return sessionDetails;
        }

 

    }
}
