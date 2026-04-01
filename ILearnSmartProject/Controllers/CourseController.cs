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
            return View();
        }


        // GET: CourseController/Details/5
        [HttpPost]
        [RequestSizeLimit(200_000_000)]
        [RequestFormLimits(MultipartBodyLengthLimit = 200_000_000)]
        public async Task<ActionResult> SubmitCourse([FromForm] Course course)
        {
            var courseId = await _courseAppService.UploadFileToBlob(course.CourseVideoFile);
            return RedirectToAction("Index", "Admin");
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

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
