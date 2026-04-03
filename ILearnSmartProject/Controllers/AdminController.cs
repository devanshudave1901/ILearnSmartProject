using ILearnSmartProject.Models;
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

        //public ActionResult CreateOrEditCourse()
        //{
        //    return View();
        //}
        public async Task<IActionResult> GetVideoAsync(string blobName)
        {
            //var file = _courseAppService.FetchBlobFileFromAzure("https://ilearnsmart.blob.core.windows.net/coursevideos/DevanshuDave_A1_Recording.mp4").Result;
            var fetchVideo = await _courseAppService.FetchBlobFileFromAzure(blobName);

            // course[0].CourseVideoFile = fetchVideo;
            var stream = fetchVideo.OpenReadStream();


            return File(stream, fetchVideo.ContentType);
        }
        public async Task<ActionResult> AdminCourseControl()
        {

            var courses = await _courseAppService.GetAllCourses();
            ViewBag.courses = courses.ToList();
            return View();
        }


        public IActionResult CreateOrEditCourse(int Id)
        {

            var course = _courseAppService.GetCourseById(Id).Result;
            ViewBag.courses = course.ToList();
            return View();
        }

        public async Task<IActionResult> AdminViewPage(int Id)
        {

            var course = _courseAppService.GetCourseById(Id).Result;

            //var fetchVideo = await _courseAppService.FetchBlobFileFromAzure(course[0].BlobName);

           // course[0].CourseVideoFile = fetchVideo;
            ViewBag.courses = course.ToList();
            return View();
        }


        
        public IActionResult DeleteCourse(int Id)
        {
            var deletedCourse = _courseAppService.DeleteCourse(Id).Result;

            return RedirectToAction("AdminCourseControl", "Admin");
        }
        // GET: CourseController/Details/5

    }
}
