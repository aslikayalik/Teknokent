using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class Tto
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Konu adı")]
        public string TtoName { get; set; }

       
        [DisplayName("İçerik")]
        public string? TtoContent { get; set; }

      
        [Display(Name = "Dosya")]
        public string? FilePath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
