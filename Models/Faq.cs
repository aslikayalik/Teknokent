using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class Faq
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Soru")]
        public string Question { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Cevap")]
        public string Answer { get; set; }


    }
}
