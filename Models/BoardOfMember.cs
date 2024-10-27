using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Teknokent.Data;

namespace Teknokent.Models
{
    public class BoardOfMember 
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


        [DisplayName("Adres")]
        public string? Address { get; set; }


        [DisplayName("Telefon Numarası")]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz telefon numarası")]
        public string? Phone { get; set; }


        [DisplayName("E-Posta")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Geçersiz E-Posta adresi")]
        public string? Email { get; set; }


        [DisplayName("Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }


        [DisplayName("İçerik")]
        public string? Description { get; set; }




        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Görsel")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
       

    }
}
