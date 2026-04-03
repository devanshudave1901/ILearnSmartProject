using ILearnSmartProject.Models;
using ILearnSmartProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ILearnSmartProject.Controllers
{
    public class CourseController : Controller
    {
        private CourseAppService _courseAppService;


        public CourseController(CourseAppService courseAppService)
        {
            _courseAppService = courseAppService;
        }
        // GET: CourseController
        public ActionResult Index()
        {
            var file = _courseAppService.FetchBlobFileFromAzure("https://ilearnsmart.blob.core.windows.net/coursevideos/DevanshuDave_A1_Recording.mp4").Result;
            return View();
        }


        // GET: CourseController/Details/5
        [HttpPost]
        [RequestSizeLimit(200_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 200_000_000)]
        public async Task<ActionResult> SubmitCourse([FromForm] Course course)
        {
            SubjectClass subjectClass = new SubjectClass();
            Email emailObserver = new Email();
            subjectClass.Subscribe(emailObserver);

            if (course.IsEdit == "true")
            {
                await _courseAppService.UpdateCourse(course);
               subjectClass.NotifyObservers("Course with ID: " + course.Id + " has been updated.");
            }
            else
            {
                var courseId = await _courseAppService.CreateCourse(course);
                subjectClass.NotifyObservers("New course created with ID: " + courseId);
            }

            //var courseId = await _courseAppService.UploadFileToBlob(course.CourseVideoFile);
            // implement email observer feature to notify
            subjectClass.Unsubscribe(emailObserver);


            return RedirectToAction("AdminCourseControl", "Admin");
        }

        // GET: CourseController/Create
     

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
