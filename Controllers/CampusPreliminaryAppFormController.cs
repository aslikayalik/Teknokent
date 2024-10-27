using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Controllers
{
    public class CampusPreliminaryAppFormController : Controller
    {
        private readonly ICampusPreliminaryAppFormRepository _campusPreliminaryAppFormRepository;

        public CampusPreliminaryAppFormController(ICampusPreliminaryAppFormRepository campusPreliminaryAppFormRepository)
        {

            _campusPreliminaryAppFormRepository = campusPreliminaryAppFormRepository;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _campusPreliminaryAppFormRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var campusPreliminaryAppForms = _campusPreliminaryAppFormRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(campusPreliminaryAppForms);
        }

      
        public IActionResult Create()
        {

            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampusPreliminaryAppForm campusPreliminaryAppForm)
        {
            if (!ModelState.IsValid)
            {
                return View(campusPreliminaryAppForm);

            }
            _campusPreliminaryAppFormRepository.Add(campusPreliminaryAppForm);
            TempData[SD.Success] = "Yerleşke ön başvuru formu başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var campusPreliminaryAppForm = await _campusPreliminaryAppFormRepository.GetByIdAsync(id);

            if (campusPreliminaryAppForm == null)
            {
                return View("Error");
            }


            return View(campusPreliminaryAppForm);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CampusPreliminaryAppForm campusPreliminaryAppForm)
        {
            if (ModelState.IsValid)
            {
                _campusPreliminaryAppFormRepository.Update(campusPreliminaryAppForm);
                TempData[SD.Success] = "Yerleşke ön başvuru formu başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(campusPreliminaryAppForm);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var campusPreliminaryAppFormDetails = await _campusPreliminaryAppFormRepository.GetByIdAsync(id);
            if (campusPreliminaryAppFormDetails == null) return View("Error");

            return View(campusPreliminaryAppFormDetails);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campusPreliminaryAppFormDetails = await _campusPreliminaryAppFormRepository.GetByIdAsync(id);
            if (campusPreliminaryAppFormDetails == null) return View("Error");
            _campusPreliminaryAppFormRepository.Delete(campusPreliminaryAppFormDetails);
            TempData[SD.Success] = "Yerleşke ön başvuru formu başarıyla silindi.";

            return RedirectToAction("Index");
        }

       

    }
}
