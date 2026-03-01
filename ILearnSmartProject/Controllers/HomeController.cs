using ILearnSmartProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ILearnSmartProject.Services;

namespace ILearnSmartProject.Controllers
{
    public class HomeController : Controller
    {
        private UserAppService _userAppService;
        public HomeController(UserAppService userAppService)
        {
            _userAppService = userAppService;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
