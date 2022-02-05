using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task9.Domain;
using Task9.Infrastucture;

namespace Task9.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _student.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                var student = new Student { FirstName = firstName, LastName = lastName };
                await _student.Update(id, student);

                return RedirectToAction("University", "Courses");
            }

            return View(await _student.Get(id));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string firstName, string lastName, int id)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                var student = new Student { FirstName = firstName, LastName = lastName, GroupId = id };
                await _student.Add(student);

                return RedirectToAction("University", "Courses");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Message"] = await _student.Delete(id);

            return View();
        }
    }
}
