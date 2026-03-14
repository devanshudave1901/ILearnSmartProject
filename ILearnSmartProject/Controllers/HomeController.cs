using ILearnSmartProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ILearnSmartProject.Services;

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

        public IActionResult Index()
        {
           var users = _userAppService.GetAllUsers();

            List<Users> data = users.Result;
         
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StripeCheckOut()
        {
           

            var webURLink = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
            var sessionUrl = _checkOutAppService.CreateCheckOutSession("price_1MotwRLkdIwHu7ixYcPLm5uZ", webURLink, webURLink);

            return Redirect(sessionUrl.Result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
