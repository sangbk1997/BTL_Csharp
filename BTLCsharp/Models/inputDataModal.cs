using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTLCsharp.Models
{
    public class inputDataModal
    {
        [Display(Name ="Type transcript which you can listen here.")]
        [Required(ErrorMessage = "You must type content of audio to submit result !")]
        public string value { get; set; }
    }
}