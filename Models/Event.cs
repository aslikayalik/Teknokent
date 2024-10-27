using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

     
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Etkinlik Adı")]
        public string Name { get; set; }


        [DisplayName("Etkinlik İçeriği")] // ben ekledim
        public string? Description { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Bitiş Tarihi")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Görsel")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }

    }
}
