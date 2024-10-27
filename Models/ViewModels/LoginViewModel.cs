using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Teknokent.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("E-Posta")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Beni Hatırla?")]
        public bool RememberMe { get; set; }
    }
}
