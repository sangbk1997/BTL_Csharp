using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTLCsharp.Models
{
    public class SignUpModel
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please enter your email")]
        [Display(Name ="Email")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string email { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        [Display(Name = "Username")]
        public string username { get; set; }
        [Required]
        [StringLength(20,MinimumLength =6,ErrorMessage ="Password have at least six letter...!")]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage ="Password not match !")]
        [Display(Name = "Repeat-Password")]
        public string repeatPsw { get; set; }
        [Display(Name = "Age")]
        [Range(1,200,ErrorMessage ="Age must from 1 to 200")]
        public int age { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }

    }
}