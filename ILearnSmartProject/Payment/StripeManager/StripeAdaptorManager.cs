using ILearnSmartProject.Models;
using ILearnSmartProject.Payment;
using ILearnSmartProject.Services;
using Microsoft.Extensions.Options;
using Stripe;
namespace ILearnSmartProject.Payment.StripeManager                                                                                                                      
{
    public class StripeAdaptorManager : ICheckOutSession
    {
        private CourseAppService _courseAppService;
        private readonly IOptions<StripeModel> _model;
        public StripeAdaptorManager(IOptions<StripeModel> model, CourseAppService courseAppService)
        { 
            _model = model;
            _courseAppService = courseAppService;
        }
  
        public async Task<List<string>> CreateCheckOutSession(string courseId, string successUrl, string cancelUrl)
        {

            // fetch courseDetails
            var courseData = await _courseAppService.GetCourseById(int.Parse(courseId));

            string apiKey = _model.Value.SecretKey;

            StripeConfiguration.ApiKey = apiKey;

            var price = courseData[0].CoursePrice;
            price = price * 100;
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = successUrl,



                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>

                {
                    new Stripe.Checkout.SessionLineItemOptions
                    {
                        PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)price,
                            Currency = "cad",
                            ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = courseData[0].CourseTitle,
                                // IMAGE FROM the local folder
                                Images = new List<string>
                                {
                                    "https://ilearnsmartproject-e3b7hfa9e7akexg4.canadacentral-01.azurewebsites.net/images/course1.jpg"
                                },
                                Description = courseData[0].CourseDescription,
                                
                            },
                        },
                        //Price = "price_1TAl8I0bWEACmO5c8NTifqgp",
                        Quantity = 1,
                    },
                },
                Mode = "payment",
            };
            var service = new Stripe.Checkout.SessionService();
            //Stripe.Checkout.Session session = service.Create(options);                                 
            Stripe.Checkout.Session session = service.Create(options);                                 

            List<String> sessionDetails = new List<string>();
            sessionDetails.Add(session.Id);
            sessionDetails.Add(session.Url);
                                                                                                                               
                                                                                     
            return sessionDetails;
        }

        public async Task<string> ConfirmPayment(string sessionId)
        {
            // confirm the payment and return the status to the client
            string session = sessionId;
            string apiKey = _model.Value.SecretKey;

            StripeConfiguration.ApiKey = apiKey;

            var serice = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session sessionDetails = serice.Get(session);

            return sessionDetails.PaymentStatus;


        }



    }
}
