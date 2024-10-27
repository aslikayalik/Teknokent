using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Başlık")] 
        public string Title { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Kategoriler")]
        public string Category { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Etiketler")]
        public string Label { get; set; }




        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Yorumlama")]
        public string Yorumlama { get; set; }



        [DisplayName("İçerik")]
        public string? Content { get; set; }



        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Yazar")]
        public string Author { get; set; }

    }
}
