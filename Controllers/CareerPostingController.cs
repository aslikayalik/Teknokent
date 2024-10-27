using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class CareerPostingController : Controller
    {
        private readonly ICareerPostingRepository _careerPostingRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public CareerPostingController(ICareerPostingRepository careerPostingRepository, IHostingEnvironment hostingEnv)
        {

            _careerPostingRepository = careerPostingRepository;
            _hostingEnv = hostingEnv;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _careerPostingRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var careerPostings = _careerPostingRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(careerPostings);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CareerPosting careerPosting)
        {
            if (careerPosting.ImgFile != null)
            {
                var fileName = Path.GetFileName(careerPosting.ImgFile.FileName);
                string ext = Path.GetExtension(careerPosting.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "careerPostingLogoImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await careerPosting.ImgFile.CopyToAsync(fileSteam);
                }
                careerPosting.ImgPath = imgPath;

                _careerPostingRepository.Add(careerPosting);
                TempData[SD.Success] = "İlan başarıyla eklendi.";
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
            var careerPosting = await _careerPostingRepository.GetByIdAsync(id);

            if (careerPosting == null)
            {
                return View("Error");
            }


            return View(careerPosting);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CareerPosting careerPosting)
        {
            if (ModelState.IsValid)
            {
                _careerPostingRepository.Update(careerPosting);
                TempData[SD.Success] = "Post başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(careerPosting);
        }


        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var careerPostingDetails = await _careerPostingRepository.GetByIdAsync(id);
            if (careerPostingDetails == null) return View("Error");

            return View(careerPostingDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var careerPostingDetails = await _careerPostingRepository.GetByIdAsync(id);
            if (careerPostingDetails == null) return View("Error");
            _careerPostingRepository.Delete(careerPostingDetails);
            TempData[SD.Success] = "Post başarıyla silindi.";

            return RedirectToAction("Index");
        }


        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _careerPostingRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var careerPostings = _careerPostingRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(careerPostings);
        }

    }
}
