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
                var type = await _userAppService.LoginUserType(email);
                var sessionID = SetSession("1");
      
                ViewBag.SessionID = sessionID;
                if(type == "Admin")
                {
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    return RedirectToAction("Index", "Student");

                }

            }

            
        }

        public async Task<IActionResult> RegisterUser(string FirstName, string LastName, string EmailAddress, string Password)
        {

            var userRegister = await _userAppService.RegisterUser(FirstName, LastName, EmailAddress, Password);

            if (userRegister == 0)
            {
                ViewBag.ErrorMessage = "Something Went Wrong. Please Try again! If problem presists, Please contact admin (admin@learnsmart.ca)";
                TempData["Message"] = "Something Went Wrong. Please Try again! If problem presists, Please contact admin (admin@learnsmart.ca)";
                //return View("Login");
                return RedirectToAction("UserRegisteration");
            }
            else
            {
                return RedirectToAction("Login");
            }

            
        }

        

    }
}
