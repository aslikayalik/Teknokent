using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class ManagementOfficeController : Controller
    {
        private readonly IManagementOfficeRepository _managementOfficeRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public ManagementOfficeController(IManagementOfficeRepository managementOfficeRepository, IHostingEnvironment hostingEnv)
        {

            _managementOfficeRepository = managementOfficeRepository;
            _hostingEnv = hostingEnv;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _managementOfficeRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var managementOffices = _managementOfficeRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(managementOffices);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagementOffice managementOffice)
        {
            if (managementOffice.ImgFile != null)
            {
                var fileName = Path.GetFileName(managementOffice.ImgFile.FileName);
                string ext = Path.GetExtension(managementOffice.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "managementOfficeImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await managementOffice.ImgFile.CopyToAsync(fileSteam);
                }
                managementOffice.ImgPath = imgPath;

                _managementOfficeRepository.Add(managementOffice);
                TempData[SD.Success] = "Ofis yönetim üyesi başarıyla eklendi.";
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
            var managementOffice = await _managementOfficeRepository.GetByIdAsync(id);

            if (managementOffice == null)
            {
                return View("Error");
            }


            return View(managementOffice);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManagementOffice managementOffice)
        {
            if (ModelState.IsValid)
            {
                _managementOfficeRepository.Update(managementOffice);
                TempData[SD.Success] = "Ofis yönetim üyesi başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(managementOffice);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var managementOfficeDetails = await _managementOfficeRepository.GetByIdAsync(id);
            if (managementOfficeDetails == null) return View("Error");

            return View(managementOfficeDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var managementOfficeDetails = await _managementOfficeRepository.GetByIdAsync(id);
            if (managementOfficeDetails == null) return View("Error");
            _managementOfficeRepository.Delete(managementOfficeDetails);
            TempData[SD.Success] = "Yönetim ofisi üyesi başarıyla silindi.";

            return RedirectToAction("Index");
        }

        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _managementOfficeRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var managementOffices = _managementOfficeRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(managementOffices);
        }


    }
}
