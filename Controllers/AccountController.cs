using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using Teknokent.Data;
using Teknokent.Models.ViewModels;
using Teknokent.Models;

namespace Teknokent.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UrlEncoder _urlEncoder;
        private ApplicationDbContext _context;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            UrlEncoder urlEncoder, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
            _roleManager = roleManager;
            _context = context;

        }

        [Authorize(Policy = "Admin")]
        public IActionResult Index()
        {
            return View();
        }





        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Register(string returnurl = null)
        {

         

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {

                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Yönetici"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "Birim Yöneticisi"
            });



            ViewData["ReturnUrl"] = returnurl;
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                RoleList = listItems
            };
            return View(registerViewModel);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
           // if (ModelState.IsValid)
           // {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name, 
                    LastName = model.LastName, PhoneNumber=model.Phone, Company=model.Company};
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.RoleSelected != null && model.RoleSelected.Length > 0 && model.RoleSelected == "Admin")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }


                    TempData[SD.Success] = "Kullanıcı başarıyla oluşturuldu.";
                    return RedirectToAction("Index", "User");


                }
                AddErrors(result);
           // }

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Value = "Admin",
                Text = "Yönetici"
            });
            listItems.Add(new SelectListItem()
            {
                Value = "User",
                Text = "Birim Yöneticisi"
            });
            model.RoleList = listItems;

         
            return View(model);

        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
          //  if (ModelState.IsValid)
           // {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    return RedirectToAction("AdminPage", "Home");
                }

                if (result.IsLockedOut)
                {
                    return View("Lockout");

                }
                else
                {
                    TempData[SD.Error] = "Mail adresi veya şifre hatalı. Lütfen tekrar deneyin.";

                    return View(model);
                }



           // }


         //   return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnurl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
