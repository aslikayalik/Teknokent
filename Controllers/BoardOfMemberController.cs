using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teknokent.Data;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class BoardOfMemberController : Controller
    {
        private readonly IBoardOfMemberRepository _boardOfMemberRepository;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ApplicationDbContext _db;

        public BoardOfMemberController(IBoardOfMemberRepository boardOfMemberRepository, IHostingEnvironment hostingEnv, ApplicationDbContext db)
        {

            _boardOfMemberRepository = boardOfMemberRepository;
            _hostingEnv = hostingEnv;
            _db = db;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _boardOfMemberRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var boardOfMembers = _boardOfMemberRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(boardOfMembers);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BoardOfMember boardOfMember)
        {
            if (boardOfMember.ImgFile != null)
            {
                var fileName = Path.GetFileName(boardOfMember.ImgFile.FileName);
                string ext = Path.GetExtension(boardOfMember.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "boardOfMemberImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await boardOfMember.ImgFile.CopyToAsync(fileSteam);
                }
                boardOfMember.ImgPath = imgPath;

                _boardOfMemberRepository.Add(boardOfMember);
                TempData[SD.Success] = "Kurul üyesi başarıyla eklendi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[SD.Error] = "Hata";
            }

            return View();
        }

        /*
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var boardOfMember = await _boardOfMemberRepository.GetByIdAsync(id);

            if (boardOfMember == null)
            {
                return View("Error");
            }


            return View(boardOfMember);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BoardOfMember boardOfMember)
        {
            
                _boardOfMemberRepository.Update(boardOfMember);
                TempData[SD.Success] = "Kurul üyesi başarıyla güncellendi.";
                return RedirectToAction("Index");
          
        }

        */



        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var boardOfMemberDetails = await _boardOfMemberRepository.GetByIdAsync(id);
            if (boardOfMemberDetails == null) return View("Error");

            return View(boardOfMemberDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boardOfMemberDetails = await _boardOfMemberRepository.GetByIdAsync(id);
            if (boardOfMemberDetails == null) return View("Error");
            _boardOfMemberRepository.Delete(boardOfMemberDetails);
            TempData[SD.Success] = "Kurul üyesi başarıyla silindi.";

            return RedirectToAction("Index");
        }


         public IActionResult IndexE(int page = 1, int pageSize = 7)
        {

            var totalCount = _boardOfMemberRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var boardOfMembers = _boardOfMemberRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(boardOfMembers);
         }


     
        private bool BoardOfMemberExists(int id)
        {
            return _db.BoardOfMember.Any(e => e.Id == id);
      
        }


        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var boardOfMember = await _boardOfMemberRepository.GetByIdAsync(id);
            if (boardOfMember == null)
            {
                return NotFound();
            }
            ViewBag.ImgPath = boardOfMember.ImgPath; // Eski resmi sakla
            return View(boardOfMember);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BoardOfMember boardOfMember, IFormFile ImgFile)
        {
            if (id != boardOfMember.Id)
            {
                return NotFound();
            }

            try
            {
                if (ImgFile != null && ImgFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImgFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\boardOfMemberImages", fileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await ImgFile.CopyToAsync(fileSteam);
                    }
                    boardOfMember.ImgPath = "wwwroot\\images\\boardOfMemberImages\\" + fileName;
                }
                else
                {
                    boardOfMember.ImgPath = ViewBag.ImgPath; // Eğer yeni resim yüklenmemişse, eski resmi koru
                }

               
                _boardOfMemberRepository.Update(boardOfMember);
                TempData[SD.Success] = "Kurul üyesi başarıyla güncellendi.";
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardOfMemberExists(boardOfMember.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

    }
}
