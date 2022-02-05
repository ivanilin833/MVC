using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Domain;
using Task9.Infrastucture;

namespace Task9.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _course;

        public CoursesController(ICourseService сourses)
        {
            _course = сourses;
        }

        public async Task<IActionResult> University()
        {
            return View(await _course.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _course.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string nameCourse)
        {
            if (!string.IsNullOrEmpty(nameCourse))
            {
                var cours = new Course { Name = nameCourse };
                await _course.Update(id, cours);

                return RedirectToAction("University", "Courses");
            }

            return View(await _course.Get(id));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string nameCourse, string descriptionCourse)
        {
            if (!string.IsNullOrEmpty(nameCourse) && !string.IsNullOrEmpty(descriptionCourse))
            {
                var cours = new Course { Name = nameCourse, Description = descriptionCourse };
                await _course.Add(cours);

                return RedirectToAction("University", "Courses");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Message"] = await _course.Delete(id);
            return View();
        }
    }
}
