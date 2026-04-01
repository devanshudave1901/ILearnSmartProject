using ILearnSmartProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ILearnSmartProject.Services;
using Stripe;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ILearnSmartProject.Controllers
{
    public class HomeController : Controller
    {
        private UserAppService _userAppService;

       private CheckOutAppService _checkOutAppService;
        public HomeController(UserAppService userAppService, CheckOutAppService checkOutAppService)
        {
            _userAppService = userAppService;
            _checkOutAppService = checkOutAppService;
        }

        public IActionResult GetSession(string sessionID)
        {
            var data = HttpContext.Session.GetString("id");


            var id = HttpContext.Session.Id;

            return Content(data);

        }

        public IActionResult Index()
        {

            var users = _userAppService.GetAllUsers();


            List<Users> data = users.Result;

            var sessionID = ViewBag.SessionID;
            var sessionUser = GetSession(sessionID);
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    
        public async Task<IActionResult> StripeWebHook()
        {
            return Ok();
        }




        public IActionResult StripeCheckOut()
        {
           

            var webURLink = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
            var sessionDetails = _checkOutAppService.CreateCheckOutSession("price_1MotwRLkdIwHu7ixYcPLm5uZ", webURLink, webURLink);

            sessionDetails.Wait();
            var sessionId = sessionDetails.Result[0];
            var url = sessionDetails.Result[1];
            return Redirect(url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
