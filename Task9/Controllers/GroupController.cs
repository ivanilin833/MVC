using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task9.Domain;
using Task9.Infrastucture;

namespace Task9.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _group;

        public GroupController(IGroupService group)
        {
            _group = group;
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _group.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string nameGroup)
        {
            if (!string.IsNullOrEmpty(nameGroup))
            {
                var group = new Group { Name = nameGroup };
                await _group.Update(id, group);

                return RedirectToAction("University", "Courses");
            }
            return View(await _group.Get(id));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string nameGroup, int id)
        {
            if (!string.IsNullOrEmpty(nameGroup))
            {
                var group = new Group { Name = nameGroup, CourseId = id };
                await _group.Add(group);

                return RedirectToAction("University", "Courses");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Message"] = await _group.Delete(id);
            return View();
        }
    }
}
