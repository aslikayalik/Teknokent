using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public NewsController(INewsRepository newsRepository, IHostingEnvironment hostingEnv)
        {

            _newsRepository = newsRepository;
            _hostingEnv = hostingEnv;
        }


        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _newsRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var newses = _newsRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(newses);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            if (news.ImgFile != null)
            {
                var fileName = Path.GetFileName(news.ImgFile.FileName);
                string ext = Path.GetExtension(news.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "newsImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await news.ImgFile.CopyToAsync(fileSteam);
                }
                news.ImgPath = imgPath;

                _newsRepository.Add(news);
                TempData[SD.Success] = "Haber başarıyla eklendi.";
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
            var news = await _newsRepository.GetByIdAsync(id);

            if (news == null)
            {
                return View("Error");
            }


            return View(news);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                _newsRepository.Update(news);
                TempData[SD.Success] = "Haber başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(news);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var newsDetails = await _newsRepository.GetByIdAsync(id);
            if (newsDetails == null) return View("Error");

            return View(newsDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsDetails = await _newsRepository.GetByIdAsync(id);
            if (newsDetails == null) return View("Error");
            _newsRepository.Delete(newsDetails);
            TempData[SD.Success] = "Haber başarıyla silindi.";

            return RedirectToAction("Index");
        }

    }
}
