using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class TtoController : Controller
    {
        private readonly ITtoRepository _ttoRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public TtoController(ITtoRepository ttoRepository, IHostingEnvironment hostingEnv)
        {

            _ttoRepository = ttoRepository;
            _hostingEnv = hostingEnv;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _ttoRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var ttos = _ttoRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(ttos);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tto tto)
        {
            if (tto.File != null)
            {
                var fileName = Path.GetFileName(tto.File.FileName);

                string ext = Path.GetExtension(tto.File.FileName);
                if (ext.ToLower() != ".pdf")
                {
                    return View();
                }
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "ttoFiles", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await tto.File.CopyToAsync(fileSteam);
                }
                tto.FilePath = filePath;
            }

            _ttoRepository.Add(tto);
            TempData[SD.Success] = "Tto başarıyla eklendi.";
            return RedirectToAction("Index");
        }




        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var tto = await _ttoRepository.GetByIdAsync(id);

            if (tto == null)
            {
                return View("Error");
            }


            return View(tto);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tto tto)
        {
            if (ModelState.IsValid)
            {
                _ttoRepository.Update(tto);
                TempData[SD.Success] = "Tto başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(tto);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ttoDetails = await _ttoRepository.GetByIdAsync(id);
            if (ttoDetails == null) return View("Error");

            return View(ttoDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ttoDetails = await _ttoRepository.GetByIdAsync(id);
            if (ttoDetails == null) return View("Error");
            _ttoRepository.Delete(ttoDetails);
            TempData[SD.Success] = "Tto başarıyla silindi.";

            return RedirectToAction("Index");
        }

    }
}
