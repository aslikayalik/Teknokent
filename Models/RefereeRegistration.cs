using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Teknokent.Data.Enums;

namespace Teknokent.Models
{
    public class RefereeRegistration
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
        [DisplayName("TC")]
        public string TC { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Pozisyon")]
        public string Position { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İş")]
        public string Job { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Telefon Numarası")]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz telefon numarası")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("E-Posta")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Geçersiz E-Posta adresi")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Çalıştığı Kurum")]
        public string WorkPlace { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Üniversite")]
        public string UniversityName { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Banka Adı")]
        public string BankName { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Iban No")]
        public string IbanNo { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Komisyon Üye Türü")]
        public CommissionMemberType Cmt { get; set; }


    }
}
