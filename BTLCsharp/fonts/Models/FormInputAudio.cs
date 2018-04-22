using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTLCsharp.Models
{
    public class FormInputAudio
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Enter the name of audio")]
        [Display(Name ="Name Audio")]
        public string audioName { get; set; }
        [Required(ErrorMessage ="Enter the level")]
        [Display(Name ="Level")]
        [Range(1,100,ErrorMessage ="Level must form 1 to 100")]
        public int level { get; set; }
        [Required]
        [Display(Name ="Choose Category")]
        public string categoryAudio { get; set; }
        [Required(ErrorMessage ="Enter the transcript")]
        [Display(Name ="Transcript")]
        public string transcriptAudio { get; set; }
        [Required]
        [Display(Name ="Link Audio")]
        public string urlAudio { get; set; }

    }
}