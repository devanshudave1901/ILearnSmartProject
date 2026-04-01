using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILearnSmartProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: CourseController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminCourseControl()
        {
            return View();
        }


        // GET: CourseController/Details/5

    }
}
