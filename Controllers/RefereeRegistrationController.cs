using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Teknokent.Interfaces;
using Teknokent.Models;

namespace Teknokent.Controllers
{
    public class RefereeRegistrationController : Controller
    {
        private readonly IRefereeRegistrationRepository _refereeRegistrationRepository;

        public RefereeRegistrationController(IRefereeRegistrationRepository refereeRegistrationRepository)
        {

            _refereeRegistrationRepository = refereeRegistrationRepository;

        }


        [Authorize(Policy = "Admin")]

        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount = _refereeRegistrationRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var refereeRegistrations = _refereeRegistrationRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(refereeRegistrations);
        }


        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RefereeRegistration refereeRegistration)
        {

            if (!ValidateIban(refereeRegistration.IbanNo))
            {
                ModelState.AddModelError("IBAN", "Geçersiz IBAN!");
               
                return View(refereeRegistration);
            }
            
            if (!IsValidTCKN(refereeRegistration.TC))
            {
                ModelState.AddModelError("TC", "Geçersiz TC Kimlik Numarası!");
               
                return View(refereeRegistration);
            }

            if (!ModelState.IsValid)
            {
                TempData[SD.Error] = "Formda hatalar var, lütfen tekrar deneyin.";
                return View(refereeRegistration);

            }
            _refereeRegistrationRepository.Add(refereeRegistration);
            TempData[SD.Success] = "Hakem Kayıt başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var project = await _refereeRegistrationRepository.GetByIdAsync(id);

            if (project == null)
            {
                return View("Error");
            }


            return View(project);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RefereeRegistration refereeRegistration)
        {

            if (!ValidateIban(refereeRegistration.IbanNo))
            {
                ModelState.AddModelError("IBAN", "Geçersiz IBAN!");
              
                return View(refereeRegistration);
            }
            
            if (!IsValidTCKN(refereeRegistration.TC))
            {
                ModelState.AddModelError("TC", "Geçersiz TC Kimlik Numarası!");
               
                return View(refereeRegistration);
            }
            

            if (!ModelState.IsValid)
            {
                TempData[SD.Error] = "Formda hatalar var, lütfen tekrar deneyin.";

                return View(refereeRegistration);


            }
         
            _refereeRegistrationRepository.Update(refereeRegistration);
            TempData[SD.Success] = "Hakem kayıt başarıyla güncellendi.";
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var refereeRegistrationDetails = await _refereeRegistrationRepository.GetByIdAsync(id);
            if (refereeRegistrationDetails == null) return View("Error");

            return View(refereeRegistrationDetails);
        }

        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var refereeRegistrationDetails = await _refereeRegistrationRepository.GetByIdAsync(id);
            if (refereeRegistrationDetails == null) return View("Error");
            _refereeRegistrationRepository.Delete(refereeRegistrationDetails);
            TempData[SD.Success] = "Hakem kayıt başarıyla silindi.";

            return RedirectToAction("Index");
        }






        private bool IsValidTCKN(string tckn)
        {
            if (string.IsNullOrWhiteSpace(tckn) || tckn.Length != 11 || !tckn.All(char.IsDigit) || tckn[0] == '0')
            {
                return false;
            }

            int[] digits = tckn.Select(c => int.Parse(c.ToString())).ToArray();
            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            int tenthDigit = (oddSum * 7 - evenSum) % 10;
            int eleventhDigit = (oddSum + evenSum + digits[9]) % 10;

            return digits[9] == tenthDigit && digits[10] == eleventhDigit;
        }



        private static bool ValidateIban(string iban)
        {
            iban = iban.Replace(" ", "").ToUpper();


            if (string.IsNullOrEmpty(iban) || iban.Length < 2)
                return false;

            string countryCode = iban.Substring(0, 2);
            int ibanLength = iban.Length;


            var countryLengths = new Dictionary<string, int>
        {

            { "TR", 26 },

        };

            if (!countryLengths.ContainsKey(countryCode) || ibanLength != countryLengths[countryCode])
                return false;


            var regex = new Regex(@"^[A-Z0-9]{" + (ibanLength - 4) + @"}$");
            if (!regex.IsMatch(iban.Substring(4)))
                return false;

            //  Luhn algoritması
            string rearrangedIban = iban.Substring(4) + iban.Substring(0, 4);
            string numericIban = "";
            foreach (char c in rearrangedIban)
            {
                if (char.IsDigit(c))
                    numericIban += c;
                else
                    numericIban += (c - 55).ToString();
            }

            int remainder = 0;
            foreach (char c in numericIban)
                remainder = (10 * remainder + (c - '0')) % 97;

            return remainder == 1;
        }

    }
}
