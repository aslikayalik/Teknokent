using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Controllers
{
    public class FaqController : Controller
    {
        private readonly IFaqRepository _faqRepository;

        public FaqController(IFaqRepository faqRepository)
        {

            _faqRepository = faqRepository;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _faqRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var faqes = _faqRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(faqes);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return View(faq);

            }
            _faqRepository.Add(faq);
            TempData[SD.Success] = "Soru-cevap başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var faq = await _faqRepository.GetByIdAsync(id);

            if (faq == null)
            {
                return View("Error");
            }


            return View(faq);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Faq faq)
        {
            if (ModelState.IsValid)
            {
                _faqRepository.Update(faq);
                TempData[SD.Success] = "Soru cevap başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var faqDetails = await _faqRepository.GetByIdAsync(id);
            if (faqDetails == null) return View("Error");

            return View(faqDetails);
        }


        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faqDetails = await _faqRepository.GetByIdAsync(id);
            if (faqDetails == null) return View("Error");
            _faqRepository.Delete(faqDetails);
            TempData[SD.Success] = "Soru-cevap başarıyla silindi.";

            return RedirectToAction("Index");
        }


        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _faqRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var faqes = _faqRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(faqes);
        }



    }
}
