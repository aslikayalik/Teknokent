using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Proje İsmi")]
        public string ProjectName { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Tamamlanış Tarihi")]
        public DateTime CompleteDate { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Proje Tipi")]
        public string ProjectType { get; set; }
    }
}
