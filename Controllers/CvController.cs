using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;



namespace Teknokent.Controllers
{
    public class CvController : Controller
    {
        private readonly ICvRepository _cvRepository;
     
        private readonly IHostingEnvironment _hostingEnv;


        public CvController(ICvRepository cvRepository, IHostingEnvironment hostingEnv)
        {
            
            _cvRepository = cvRepository;
         
            _hostingEnv = hostingEnv;
        }


        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _cvRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var cves = _cvRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(cves);
        }

     

        public IActionResult Create()
        {

            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cv cv)
        {
            if (cv.File != null)
            {

                var fileName = Path.GetFileName(cv.File.FileName);
                
                string ext = Path.GetExtension(cv.File.FileName);
                if (ext.ToLower() != ".pdf")
                {
                    return View();
                }
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "cvFiles", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await cv.File.CopyToAsync(fileSteam);
                }
          

             
                cv.FilePath = filePath;

                _cvRepository.Add(cv);
                TempData[SD.Success] = "Cv başarıyla eklendi.";
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
            var cv = await _cvRepository.GetByIdAsync(id);

            if (cv == null)
            {
                return View("Error");
            }


            return View(cv);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cv cv)
        {
            if (ModelState.IsValid)
            {
                _cvRepository.Update(cv);
                TempData[SD.Success] = "Cv başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(cv);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var cvDetails = await _cvRepository.GetByIdAsync(id);
            if (cvDetails == null) return View("Error");

            return View(cvDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cvDetails = await _cvRepository.GetByIdAsync(id);
            if (cvDetails == null) return View("Error");
            _cvRepository.Delete(cvDetails);
            TempData[SD.Success] = "Cv başarıyla silindi.";

            return RedirectToAction("Index");
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
