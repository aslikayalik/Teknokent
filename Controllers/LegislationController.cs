using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class LegislationController : Controller
    {
        private readonly ILegislationRepository _legislationRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public LegislationController(ILegislationRepository legislationRepository, IHostingEnvironment hostingEnv)
        {

            _legislationRepository = legislationRepository;
            _hostingEnv = hostingEnv;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _legislationRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var reports = _legislationRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(reports);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Legislation legislation)
        {
            if (legislation.File != null)
            {
                var fileName = Path.GetFileName(legislation.File.FileName);

                string ext = Path.GetExtension(legislation.File.FileName);
                if (ext.ToLower() != ".pdf")
                {
                    return View();
                }
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "legislationFiles", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await legislation.File.CopyToAsync(fileSteam);
                }
                legislation.FilePath = filePath;
            }

            _legislationRepository.Add(legislation);
            TempData[SD.Success] = "Mevzuat başarıyla eklendi.";
            return RedirectToAction("Index");
        }





        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var report = await _legislationRepository.GetByIdAsync(id);

            if (report == null)
            {
                return View("Error");
            }


            return View(report);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Legislation legislation)
        {
            if (ModelState.IsValid)
            {
                _legislationRepository.Update(legislation);
                TempData[SD.Success] = "Mevzuat başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(legislation);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var legislationDetails = await _legislationRepository.GetByIdAsync(id);
            if (legislationDetails == null) return View("Error");

            return View(legislationDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var legislationDetails = await _legislationRepository.GetByIdAsync(id);
            if (legislationDetails == null) return View("Error");
            _legislationRepository.Delete(legislationDetails);
            TempData[SD.Success] = "Mevzuat başarıyla silindi.";

            return RedirectToAction("Index");
        }


        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _legislationRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var reports = _legislationRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(reports);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult DownloadFile(string filePath)
        {

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);



            var stream = new FileStream(filePath, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }



    }
}
