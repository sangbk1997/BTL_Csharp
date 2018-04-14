using System;
using System.Collections.Generic;
using BTLCsharp.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTLCsharp.Controllers
{

    public class AudioController : Controller
    {
        // GET: Audio
        public ActionResult ListeningModes()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SubmitAudio()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitAudio(FormInputAudio obj)
        {
            return View();
        }
        [HttpGet]
        public ActionResult addCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addCategory(CategoryModal obj)
        {
            return View();
        }
    }
}