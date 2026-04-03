using ILearnSmartProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILearnSmartProject.Controllers
{
    public class AdminController : Controller
    {
        private CourseAppService _courseAppService;


        public AdminController(CourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }
        // GET: CourseController
        public ActionResult Index()
        {
         

           
            return View();
        }

        public ActionResult CreateOrEditCourse()
        {
            return View();
        }
        public IActionResult GetVideo()
        {
            var file = _courseAppService.FetchBlobFileFromAzure("https://ilearnsmart.blob.core.windows.net/coursevideos/DevanshuDave_A1_Recording.mp4").Result;

            var stream = file.OpenReadStream();


            return File(stream, file.ContentType);
        }
        public async Task<ActionResult> AdminCourseControl()
        {

            var courses = await _courseAppService.GetAllCourses();
            ViewBag.courses = courses.ToList();
            return View();
        }


        // GET: CourseController/Details/5

    }
}
