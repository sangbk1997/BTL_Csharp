using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTLCsharp.Models
{
    public class LoginModel
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please enter your email or username")]
        [Display(Name ="Username or Email")]
        public string username { get; set; }
        [Required(ErrorMessage ="Please enter your password")]
        [Display(Name ="Password")]
        public string password { get; set; }
    }
}