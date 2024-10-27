using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teknokent.Interfaces;
using Teknokent.Models;
using Teknokent.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Teknokent.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IHostingEnvironment _hostingEnv;

        public ProductController(IProductRepository productRepository, IHostingEnvironment hostingEnv)
        {

            _productRepository = productRepository;
            _hostingEnv = hostingEnv;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _productRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var products = _productRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(products);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {

            return View();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (product.ImgFile != null)
            {
                var fileName = Path.GetFileName(product.ImgFile.FileName);
                string ext = Path.GetExtension(product.ImgFile.FileName);
                if (ext.ToLower() != ".jpg" && ext.ToLower() != ".png")
                {
                    return View();
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, "productImages", fileName);

                using (var fileSteam = new FileStream(imgPath, FileMode.Create))
                {
                    await product.ImgFile.CopyToAsync(fileSteam);
                }
                product.ImgPath = imgPath;

                _productRepository.Add(product);
                TempData[SD.Success] = "Ürün başarıyla eklendi.";
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
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return View("Error");
            }


            return View(product);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                TempData[SD.Success] = "Product başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null) return View("Error");

            return View(productDetails);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null) return View("Error");
            _productRepository.Delete(productDetails);
            TempData[SD.Success] = "Ürün başarıyla silindi.";

            return RedirectToAction("Index");
        }


    }
}
