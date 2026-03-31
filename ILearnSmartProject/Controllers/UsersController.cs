using ILearnSmartProject.Models;
using ILearnSmartProject.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILearnSmartProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly LearnSmartContext _context;
        private UserAppService _userAppService;
   
        public UsersController(LearnSmartContext context, UserAppService userAppService)
        {
            _context = context;
            _userAppService = userAppService;
        }

       
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> UserRegisteration()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            // get the session key
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("Login");
        }

        public IActionResult SetSession(string id)
        {
             HttpContext.Session.SetString("id", id);
            // session id 
            return Content(HttpContext.Session.Id);
        }

        public async Task<IActionResult> Login()
        {

            if (TempData["Message"] != null)
            {
                ViewBag.ErrorMessage = "Didn't found the matching account. Please try again. Don’t have an account? Register here.";

            }
            
        

            return View();
        }
        public async Task<IActionResult> LoginAttempt(string emailAddress, string password)
        {

            string email = emailAddress;
            string pass = password;

            var userData = await _userAppService.CheckLogin(email, pass);

            if (userData == 0)
            {
                ViewBag.ErrorMessage = "Invalid email or password. Please try again. Don’t have an account? Register here.";
                TempData["Message"] = "Invalid email or password. Please try again. Don’t have an account? Register here.";
                //return View("Login");
                return RedirectToAction("Login");
            }
            else
            {
                // establish the session

                var sessionID = SetSession("1");
      
                ViewBag.SessionID = sessionID;
                return RedirectToAction("Index", "Home");
            }

            
        }

        

    }
}
