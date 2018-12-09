using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DinnergeddonWeb.Models
{
    public class DisplayUserModel
    {

        public Guid Id { get; set; }
        public string Email { get; set; }

        [Display(Name ="Username")]
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class EditUserModel
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string StringId => Id.ToString();
    }
}