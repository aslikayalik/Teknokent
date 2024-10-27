using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class Statistic
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İstatistik Adı")]
        public string Name { get; set; }


        [DisplayName("Değer")] // Ben ekledim
        public string? Value { get; set; }

      

        [DisplayName("İçerik")] // Ben ekledim
        public string? Description { get; set; }


      
        [Display(Name = "Görsel")]
        public string? ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
