using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Başlık")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Açıklama")]
        public string Content { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Url")]
        public string Url { get; set; }




        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Görsel")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
