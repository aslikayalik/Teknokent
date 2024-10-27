using System.ComponentModel.DataAnnotations;

namespace Teknokent.Data.Enums
{
    public enum WantSection
    {

        [Display(Name = "Ön Kuluçka Ofisleri")]
        FirstValue,
        [Display(Name = "Kuluçka Ofisleri")]
        SecondValue,
        [Display(Name = "Ar-Ge Ofisleri")]
        ThirdValue,
        [Display(Name = "Arazi Tahsisi")]
        FourthValue
    }
}
