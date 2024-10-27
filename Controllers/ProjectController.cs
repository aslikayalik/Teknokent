using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {

            _projectRepository = projectRepository;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _projectRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var projects = _projectRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(projects);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);

            }
            _projectRepository.Add(project);
            TempData[SD.Success] = "Proje başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            if (project == null)
            {
                return View("Error");
            }


            return View(project);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Update(project);
                TempData[SD.Success] = "Proje başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var projectDetails = await _projectRepository.GetByIdAsync(id);
            if (projectDetails == null) return View("Error");

            return View(projectDetails);
        }


        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectDetails = await _projectRepository.GetByIdAsync(id);
            if (projectDetails == null) return View("Error");
            _projectRepository.Delete(projectDetails);
            TempData[SD.Success] = "Proje başarıyla silindi.";

            return RedirectToAction("Index");
        }



    }
}
