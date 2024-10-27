using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class Cv
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Ad")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Soyad")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Pozisyon")]
        public string Position { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("E-Posta")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Geçersiz E-Posta adresi")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Telefon Numarası")]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz telefon numarası")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İçerik")]
        public string Description { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Eklenme Tarihi")]
        public DateTime AddTime { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "CV Dosyası")]
        public string FilePath { get; set; }
        [NotMapped] 
        public IFormFile File { get; set; }



    }
}
