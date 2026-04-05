using ILearnSmartProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ILearnSmartProject.Controllers
{
    public class StudentController : Controller
    {
        private CourseAppService _courseAppService;
        private CheckOutAppService _checkOutAppService;
        private CoursesUserPurchaseService _coursesUserPurchaseService;

        public StudentController(CourseAppService courseAppService, CheckOutAppService checkOutAppService, CoursesUserPurchaseService coursesUserPurchaseService)
        {
            _courseAppService = courseAppService;
            _checkOutAppService = checkOutAppService;
            _coursesUserPurchaseService = coursesUserPurchaseService;
        }
        public async Task<ActionResult> Index()
        {

            var courses = await _courseAppService.GetAllCourses();
            ViewBag.courses = courses.ToList();
            return View();
        }
        public async Task<ActionResult> CheckoutPage(int id)
        {
            var course = _courseAppService.GetCourseById(id).Result;
            ViewBag.courses = course.ToList();
            return View();
        }

        public async Task<ActionResult> Payment(int id)
        {
            HttpContext.Session.Remove("course");
            var webURLink = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
            var sucessURLink = $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}/Student/Success";

            var sessionDetails = _checkOutAppService.CreateCheckOutSession(id.ToString(), sucessURLink, webURLink);
            HttpContext.Session.SetString("course", id.ToString());
            sessionDetails.Wait();
            var sessionId = sessionDetails.Result[0];
            var url = sessionDetails.Result[1];

            HttpContext.Session.SetString("StripeSession", sessionId);

            return Redirect(url);
        }
        public async Task<ActionResult> MyCourses()
        {
            var sessionUserID = HttpContext.Session.GetString("id");
            var courses = await _coursesUserPurchaseService.GetAllPurchasesByUserId(sessionUserID);
            ViewBag.courses = courses.ToList();
            return View();
        }
        public async Task<IActionResult> StudentViewPage(int Id)
        {

            var course = _courseAppService.GetCourseById(Id).Result;

            //var fetchVideo = await _courseAppService.FetchBlobFileFromAzure(course[0].BlobName);

            // course[0].CourseVideoFile = fetchVideo;
            ViewBag.courses = course.ToList();
            return View();
        }
        public async Task<IActionResult> GetVideoAsync(string blobName)
        {
            //var file = _courseAppService.FetchBlobFileFromAzure("https://ilearnsmart.blob.core.windows.net/coursevideos/DevanshuDave_A1_Recording.mp4").Result;
            var fetchVideo = await _courseAppService.FetchBlobFileFromAzure(blobName);

            // course[0].CourseVideoFile = fetchVideo;
            var stream = fetchVideo.OpenReadStream();


            return File(stream, fetchVideo.ContentType);
        }
        public async Task<ActionResult> SuccessAsync()
        {
            
            var stripeSessionId = HttpContext.Session.GetString("StripeSession");

            // reconfirming from strip that user really made the payment

            
            var confirmPayment = await _checkOutAppService.ConfirmPayment(stripeSessionId);

            if(confirmPayment == "paid")
            {
                var sessionUserID = HttpContext.Session.GetString("id");
                var courseId = HttpContext.Session.GetString("course");

                var purchaseEntry = await _coursesUserPurchaseService.CreateEntry(courseId, sessionUserID);

                return RedirectToAction("MyCourses");
            }
            else
            {
                ViewBag.errorMessage = "We were unable to process your payment. If funds were deducted from your account, please contact the Admin Control Center at learn@admin.ca for assistance.";
                return RedirectToAction("Index");
            }
           

        }
    }
}
