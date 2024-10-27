using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Teknokent.Data.Enums;

namespace Teknokent.Models
{
    public class CampusPreliminaryAppForm
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
        [DisplayName("Telefon Numarası")]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz telefon numarası")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("E-Posta")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Geçersiz E-Posta adresi")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Firmam var mı  ?")]
        public bool hasCompany { get; set; }


      
        [DisplayName("Firma Unvanı")]
        public string? TitleCompany { get; set; }


        [DisplayName("Vergi Numarası")]
        public string? TaxNumber { get; set; }


        [DisplayName("Vergi Dairesi")]
        public string? TaxAdministration { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Yerleşke")]
        public Campus Campus { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Yer almak istenilen bölüm  ?")]
        public WantSection wantSection { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Kiralanmak istenilen m2 ")]
        [Range(1, float.MaxValue, ErrorMessage = "Değer 1'den küçük olamaz")]
        public float wantSpace { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İstenen Personel Sayısı")]
        [Range(0, int.MaxValue, ErrorMessage = "İstenen personel sayısı - olamaz")]
        public int wantWorker { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İstenen Destek Personel Sayısı ")]
        [Range(0, int.MaxValue, ErrorMessage = "İstenen destek personel sayısı - olamaz")]
        public int wantSupportWorker { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İstenen Kapsam Dışı Personel Sayısı")]
        [Range(0, int.MaxValue, ErrorMessage = "Kapsam dışı personel sayısı - olamaz")]
        public int wantOutOfScopeWorker { get; set; }



    }
}
