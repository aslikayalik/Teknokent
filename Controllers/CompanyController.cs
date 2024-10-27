using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IHostingEnvironment _hostingEnv;


        public CompanyController(ICompanyRepository companyRepository, IHostingEnvironment hostingEnv)
        {

            _companyRepository = companyRepository;
            _hostingEnv = hostingEnv;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _companyRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var companies = _companyRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(companies);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (company.ImgFile != null)
            {
                var fileName = Path.GetFileName(company.ImgFile.FileName);
                string ext = Path.GetExtension(company.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "companyLogoImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await company.ImgFile.CopyToAsync(fileSteam);
                }
                company.ImgPath = imgPath;

                _companyRepository.Add(company);
                TempData[SD.Success] = "Şirket başarıyla eklendi.";
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
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
            {
                return View("Error");
            }


            return View(company);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Update(company);
                TempData[SD.Success] = "Şirket başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(company);
        }


        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var companyDetails = await _companyRepository.GetByIdAsync(id);
            if (companyDetails == null) return View("Error");

            return View(companyDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyDetails = await _companyRepository.GetByIdAsync(id);
            if (companyDetails == null) return View("Error");
            _companyRepository.Delete(companyDetails);
            TempData[SD.Success] = "Şirket başarıyla silindi.";

            return RedirectToAction("Index");
        }


        public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _companyRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var companies = _companyRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(companies);
        }


        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
            {
                return View("Error");
            }

            return View(company);
        }

    }
}
