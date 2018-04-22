using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTLCsharp.Models
{
    public class CategoryModal
    {
        
        public int id { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string category { get; set; }
        [Required]
        [Display(Name ="Brief Introduce")]
        public string description { get; set; }
        [Display(Name ="Link to Image")]
        public string urlImg { get; set; }
    }
}