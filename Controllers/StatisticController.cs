using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public StatisticController(IStatisticRepository statisticRepository, IHostingEnvironment hostingEnv)
        {

            _statisticRepository = statisticRepository;
            _hostingEnv = hostingEnv;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _statisticRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var statistics = _statisticRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(statistics);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Statistic statistic)
        {
            if (statistic.ImgFile != null)
            {
                var fileName = Path.GetFileName(statistic.ImgFile.FileName);
                string ext = Path.GetExtension(statistic.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "statisticImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await statistic.ImgFile.CopyToAsync(fileSteam);
                }
                statistic.ImgPath = imgPath;
            }

            _statisticRepository.Add(statistic);
            TempData[SD.Success] = "İstatistik başarıyla eklendi.";
            return RedirectToAction("Index");
        }



        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var statistic = await _statisticRepository.GetByIdAsync(id);

            if (statistic == null)
            {
                return View("Error");
            }


            return View(statistic);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                _statisticRepository.Update(statistic);
                TempData[SD.Success] = "İstatistik başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(statistic);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var statisticDetails = await _statisticRepository.GetByIdAsync(id);
            if (statisticDetails == null) return View("Error");

            return View(statisticDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statisticDetails = await _statisticRepository.GetByIdAsync(id);
            if (statisticDetails == null) return View("Error");
            _statisticRepository.Delete(statisticDetails);
            TempData[SD.Success] = "İstatistik başarıyla silindi.";

            return RedirectToAction("Index");
        }



    }
}
