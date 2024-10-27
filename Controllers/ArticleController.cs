using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Controllers
{

    // [Authorize(Policy = "Admin")]
    //  [Authorize]
    public class ArticleController : Controller
    {

        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {

            _articleRepository = articleRepository;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _articleRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var articles = _articleRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(articles);
        }
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);

            }
            _articleRepository.Add(article);
            TempData[SD.Success] = "Yazı başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);

            if (article == null)
            {
                return View("Error");
            }


            return View(article);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                _articleRepository.Update(article);
                TempData[SD.Success] = "Yazı başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(article);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var articleDetails = await _articleRepository.GetByIdAsync(id);
            if (articleDetails == null) return View("Error");

            return View(articleDetails);
        }


        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articleDetails = await _articleRepository.GetByIdAsync(id);
            if (articleDetails == null) return View("Error");
            _articleRepository.Delete(articleDetails);
            TempData[SD.Success] = "Yazı başarıyla silindi.";

            return RedirectToAction("Index");
        }



    }
}
