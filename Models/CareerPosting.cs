using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Teknokent.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class CareerPosting
    {
        [Key]
        public int Id { get; set; }

      
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Şirket Adı")] // Ben ekledim
        public string CompanyName { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Pozisyon")] // Başlık
        public string Position { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İçerik")]
        public string Content { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Başlama Tarihi")]
        public DateTime StartDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Bitiş Tarihi")]
        [DateGreaterThan("StartDate", ErrorMessage = "Bitiş tarihi, başlama tarihinden önce olamaz")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Logo")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }

    }
}
