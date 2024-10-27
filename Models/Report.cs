using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Rapor Adı")]
        public string ReportName { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Rapor Dosyası")]
        public string FilePath { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }


    }
}
