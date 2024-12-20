﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Teknokent.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Ad")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Soyad")]
        public string LastName { get; set; }


        [DisplayName("Şirket")]
        public string? Company { get; set; }



        public DateTime DateCreated { get; set; } = DateTime.Now;

     

        [NotMapped]
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string RoleId { get; set; }
        [NotMapped]
        public string Role { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> RoleList { get; set; }


    }
}
