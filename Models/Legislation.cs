using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class Legislation
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Mevzuat Adı")]
        public string LegislationName { get; set; }


        [DisplayName("Link")]
        public string? Link { get; set; }



        [Display(Name = "Dosya")]
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
