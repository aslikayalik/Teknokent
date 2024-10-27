using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Teknokent.Controllers
{
    public class OfficeController : Controller
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public OfficeController(IOfficeRepository officeRepository, IHostingEnvironment hostingEnv)
        {

            _officeRepository = officeRepository;
            _hostingEnv = hostingEnv;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _officeRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var offices = _officeRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(offices);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Office office)
        {
            if (office.ImgFile != null)
            {
                var fileName = Path.GetFileName(office.ImgFile.FileName);
                string ext = Path.GetExtension(office.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "officeImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await office.ImgFile.CopyToAsync(fileSteam);
                }
                office.ImgPath = imgPath;

                _officeRepository.Add(office);
                TempData[SD.Success] = "Ofis başarıyla eklendi.";
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
            var office = await _officeRepository.GetByIdAsync(id);

            if (office == null)
            {
                return View("Error");
            }


            return View(office);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Office office)
        {
            if (ModelState.IsValid)
            {
                _officeRepository.Update(office);
                TempData[SD.Success] = "Ofis başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(office);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var officeDetails = await _officeRepository.GetByIdAsync(id);
            if (officeDetails == null) return View("Error");

            return View(officeDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officeDetails = await _officeRepository.GetByIdAsync(id);
            if (officeDetails == null) return View("Error");
            _officeRepository.Delete(officeDetails);
            TempData[SD.Success] = "Ofis başarıyla silindi.";

            return RedirectToAction("Index");
        }

    }
}
