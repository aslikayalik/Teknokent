using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Ofis Adı")]
        public string OfficeName { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Adres")]
        public string Location { get; set; }


        [DisplayName("Önerilen Adres")]
        public string? RecommendLocation { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Şehir")]
        public string State { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İçerik")]
        public string Content { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Görsel")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
