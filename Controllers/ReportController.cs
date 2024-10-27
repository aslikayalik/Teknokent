using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Composition;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public ReportController(IReportRepository reportRepository, IHostingEnvironment hostingEnv)
        {

            _reportRepository = reportRepository;
            _hostingEnv = hostingEnv;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _reportRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var reports = _reportRepository.GetAll(page, pageSize);


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
        public async Task<IActionResult> Create(Report report)
        {
            if (report.File != null)
            {

                var fileName = Path.GetFileName(report.File.FileName);

                string ext = Path.GetExtension(report.File.FileName);
                if (ext.ToLower() != ".pdf")
                {
                    return View();
                }
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "reportFiles", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await report.File.CopyToAsync(fileSteam);
                }
                report.FilePath = filePath;

                _reportRepository.Add(report);
                TempData[SD.Success] = "Rapor başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[SD.Error] = "Hata";
            }



            return View();
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);

            if (report == null)
            {
                return View("Error");
            }


            return View(report);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Report report)
        {
            if (ModelState.IsValid)
            {
                _reportRepository.Update(report);
                TempData[SD.Success] = "Rapor başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(report);
        }


        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reportDetails = await _reportRepository.GetByIdAsync(id);
            if (reportDetails == null) return View("Error");

            return View(reportDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportDetails = await _reportRepository.GetByIdAsync(id);
            if (reportDetails == null) return View("Error");
            _reportRepository.Delete(reportDetails);
            TempData[SD.Success] = "Repor başarıyla silindi.";

            return RedirectToAction("Index");
        }


        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _reportRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var reports = _reportRepository.GetAll(page, pageSize);


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
