using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Teknokent.Models
{
    public class GeneralInformation
    {

        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Konu adı")]
        public string GIName { get; set; }




        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("İçerik")]
        public string GIContent { get; set; }
    }
}
