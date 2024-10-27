using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public EventController(IEventRepository eventRepository, IHostingEnvironment hostingEnv)
        {

            _eventRepository = eventRepository;
            _hostingEnv = hostingEnv;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _eventRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var events = _eventRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(events);
        }


        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event eventt)
        {
            if (eventt.ImgFile != null)
            {
                var fileName = Path.GetFileName(eventt.ImgFile.FileName);
                string ext = Path.GetExtension(eventt.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "eventImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await eventt.ImgFile.CopyToAsync(fileSteam);
                }
                eventt.ImgPath = imgPath;

                _eventRepository.Add(eventt);
                TempData[SD.Success] = "Etkinlik başarıyla eklendi.";
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
            var eventt = await _eventRepository.GetByIdAsync(id);

            if (eventt == null)
            {
                return View("Error");
            }


            return View(eventt);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event eventt)
        {
            if (ModelState.IsValid)
            {
                _eventRepository.Update(eventt);
                TempData[SD.Success] = "Olay başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(eventt);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventtDetails = await _eventRepository.GetByIdAsync(id);
            if (eventtDetails == null) return View("Error");

            return View(eventtDetails);
        }


        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventtDetails = await _eventRepository.GetByIdAsync(id);
            if (eventtDetails == null) return View("Error");
            _eventRepository.Delete(eventtDetails);
            TempData[SD.Success] = "Yazı başarıyla silindi.";

            return RedirectToAction("Index");
        }



    }
}
