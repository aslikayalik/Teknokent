using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;

namespace Teknokent.Controllers
{
    public class GeneralInformationController : Controller
    {
        private readonly IGeneralInformationRepository _generalInformationRepository;

        public GeneralInformationController(IGeneralInformationRepository generalInformationRepository)
        {

            _generalInformationRepository = generalInformationRepository;

        }


        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _generalInformationRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var generalInformations = _generalInformationRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(generalInformations);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GeneralInformation generalInformation)
        {
            if (!ModelState.IsValid)
            {
                return View(generalInformation);

            }
            _generalInformationRepository.Add(generalInformation);
            TempData[SD.Success] = "Genel bilgi başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var project = await _generalInformationRepository.GetByIdAsync(id);

            if (project == null)
            {
                return View("Error");
            }


            return View(project);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GeneralInformation generalInformation)
        {
            if (ModelState.IsValid)
            {
                _generalInformationRepository.Update(generalInformation);
                TempData[SD.Success] = "Genel bilgi başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(generalInformation);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var generalInformationDetails = await _generalInformationRepository.GetByIdAsync(id);
            if (generalInformationDetails == null) return View("Error");

            return View(generalInformationDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generalInformationDetails = await _generalInformationRepository.GetByIdAsync(id);
            if (generalInformationDetails == null) return View("Error");
            _generalInformationRepository.Delete(generalInformationDetails);
            TempData[SD.Success] = "Genel bilgi başarıyla silindi.";

            return RedirectToAction("Index");
        }


        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _generalInformationRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var generalInformations = _generalInformationRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(generalInformations);
        }



    }
}
