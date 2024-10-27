using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teknokent.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Firma Adı")]
        public string CompanyName { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Firma Sektörü")]
        public string CompanySector { get; set; }


        [DisplayName("Vergi Numarası")]
        public string? TaxNumber { get; set; }


        [DisplayName("Fax Numarası")]
        public string? FaxNumber { get; set; }


        [DisplayName("Adres")]
        public string? Address { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Telefon Numarası")]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz telefon numarası")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("E-Posta")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Geçersiz E-Posta adresi")]
        public string Email { get; set; }


        [DisplayName("Web Sitesi")]
        public string? WebSite { get; set; }

     

        [DisplayName("İçerik")]
        public string? Description { get; set; }


        [DisplayName("LinkedIn")]
        public string? LinkedIn { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Kuruluş Tarihi")]
        public DateTime EstablishmentDate { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Çalışan Sayısı")]
        [Range(1, int.MaxValue, ErrorMessage = "Çalışan sayısı 1'den az olamaz")]
        public int CountofWorker { get; set; }

        // burada mor kutucukla türkiye yazıyodu onu yapmadın


        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Logo")]
        public string ImgPath { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }


    }
}
