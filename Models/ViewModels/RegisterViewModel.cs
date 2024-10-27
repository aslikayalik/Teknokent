using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Zorunlu Alan")]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [StringLength(100, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Parola Tekrar")]
        [Compare("Password", ErrorMessage = "Oluşturduğunuz şifreniz ile eşleşmedi, tekrar giriniz lütfen.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Ad")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Soyad")]
        public string LastName { get; set; }


        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string RoleSelected { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Telefon")]
        public string Phone { get; set; }


        [DisplayName("Şirket")]
        public string? Company { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Durum")]
        public bool IsActive { get; set; }



    }
}
