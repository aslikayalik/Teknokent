using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

      

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Ürün adı")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Görsel")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }

    }
}
