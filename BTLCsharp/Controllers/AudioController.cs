using System;
using System.Collections.Generic;
using System.Collections;
using BTLCsharp.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTLCsharp.EF;
using BTLCsharp.Dao;

namespace BTLCsharp.Controllers
{

    public class AudioController : Controller
    {
        Model2 db = null;
        
        // GET: Audio
        public ActionResult ListeningModes(int id)
        {
            db = new Model2();
            ViewBag.audio = db.Audios.Find(id);
            return View();
        }
        [HttpGet]
        public ActionResult correctionMode(int id)
        {
            db = new Model2();
            ViewBag.audio = db.Audios.Find(id);
            ViewBag.display = "none";
            TempData["id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult correctionMode(inputDataModal model)
        {
           
            ViewBag.display = "display";
            db = new Model2();
            Audio obj = db.Audios.Find(TempData["id"]);
            ViewBag.audio = obj;
            string contentAudio = obj.content.ToString();
            string tempString = model.value.ToString();
            tempString = TempClass.functionClass.removeOddLetter(tempString);
            tempString = tempString.ToLower();
            contentAudio = TempClass.functionClass.removeOddLetter(contentAudio);
            contentAudio = contentAudio.ToLower();
            string[] arrayTempString = tempString.Split(new char[] { ' ' });
            List<string> listItems = TempClass.LCS.getLCS(arrayTempString, contentAudio);
            string[] arrayStringContent = contentAudio.Split(new char[] { ' ' });
            //if (Session["USER_SESSION"] != null)
            //{
            //    var dao = new UserDao();
            //    var user = new User();
            //    user = dao.GetById((string)(Session["USER_SESSION"]));
            //    int score = (listItems.Count * 100 / arrayStringContent.Length) + Int32.Parse(obj.level.ToString()) * 10;
            //    user.score += score;
            //}
            ViewData["inputData"] = tempString;
            ViewData["contentAudio"] = contentAudio;
            ViewData["LSS"] = listItems;
            TempData.Keep("id");
            return View();
        }

        public ActionResult fullMode(int id)
        {
            db = new Model2();
            Audio audio = db.Audios.Find(id);
            ViewBag.audio = audio;
            string contentAudio = audio.content.ToString();
            contentAudio = TempClass.functionClass.removeOddLetter(contentAudio);
            ViewBag.contentAudio = contentAudio;
            return View();
        }
        public ActionResult quickMode(int id)
        {
            db = new Model2();
            Audio audio = db.Audios.Find(id);
            ViewBag.audio = audio;
            string contentAudio = audio.content.ToString();
            contentAudio = TempClass.functionClass.removeOddLetter(contentAudio);
            ViewBag.contentAudio = contentAudio;
            return View();
        }
        public ActionResult blankMode(int id)
        {
            db = new Model2();
            Audio audio = db.Audios.Find(id);
            ViewBag.audio = audio;
            string contentAudio = audio.content.ToString();
            contentAudio = TempClass.functionClass.removeOddLetter(contentAudio);
            ViewBag.contentAudio = contentAudio;
            return View();
        }

        public ActionResult blankModeSubmit(int id)
        {
            if(Session["USER_SESSION"] != null)
            {
                db = new Model2();
                var dao = new UserDao();
                var user = new User();
                user = dao.GetById((string)(Session["USER_SESSION"]));
                user.score += id;
            }
            ViewBag.score = id;
            return View();
        }
        public ActionResult quickModeSubmit(int id)
        {
            if (Session["USER_SESSION"] != null)
            {
                db = new Model2();
                var dao = new UserDao();
                var user = new User();
                user = dao.GetById((string)(Session["USER_SESSION"]));
                user.score += id;
            }
            ViewBag.score = id;
            return View();
        }
        public ActionResult fullModeSubmit(int id)
        {
            if (Session["USER_SESSION"] != null)
            {
                db = new Model2();
                var dao = new UserDao();
                var user = new User();
                user = dao.GetById((string)(Session["USER_SESSION"]));
                user.score += id;
            }
            ViewBag.score = id;
            return View();
        }
        [HttpGet]
        public ActionResult SubmitAudio()
        {
          
            if (Session["USER_SESSION"] != null)
            {
                return View();
            }   
            else
            {
                TempData["alertMsg"] = "You must login to can submit new audio !";
                return Redirect("/Home/Index");
            }
        }
        [HttpPost]
        public ActionResult SubmitAudio(FormInputAudio obj)
        {
            var dao = new UserDao();
            var user = new User();
            user = dao.GetById((string)(Session["USER_SESSION"]));
            if (dao.checkAudio(TempClass.FormatString.removeOddLetter(obj.audioName)))
            {
                if(user.modeAccess == 1)
                {
                    ModelState.AddModelError("", "This Audio is updated content !");
                    var audio = dao.GetAudioById(TempClass.FormatString.removeOddLetter(obj.audioName));
                    audio.content = obj.transcriptAudio;
                    audio.level = obj.level;
                    audio.urlAudio = obj.urlAudio;
                    audio.idUser = user.id;
                    dao.UpdateAudio(audio);
                }

                else
                {
                    ModelState.AddModelError("", "This audio is exist. Please add new Audio !");
                }
                

            }
            else
            {
                var audio = new Audio();
                audio.audioName = obj.audioName;
                audio.meta_AudioName = TempClass.FormatString.removeOddLetter(obj.audioName);
                audio.Category = obj.categoryAudio;
                audio.level = obj.level;
                audio.idUser = user.id;
                audio.urlAudio = obj.urlAudio;
                audio.views = 0;
                audio.addedDate = DateTime.Now;
                audio.content = obj.transcriptAudio;
                var result = dao.submitAudio(audio);
                if (result == true)
                {
                    ModelState.AddModelError("", "Submit Audio Success!");
                    obj = new FormInputAudio();
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add Audio !");
                }
            }
            return View(obj);
           
        }
        [HttpGet]
        public ActionResult addCategory()
        {
            var dao = new UserDao();
            var user = new User();
            user = dao.GetById((string)Session["USER_SESSION"]);
            if (Session["USER_SESSION"] != null && user.modeAccess ==1 )
            {

                return View();
            }
            else
            {
                TempData["alertMsg"] ="Sorry only admin can add Categories";
                return Redirect("/Home/Index");
            }
        }
        [HttpPost]
        public ActionResult addCategory(CategoryModal obj)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.checkCategory(TempClass.FormatString.removeOddLetter(obj.category))){
                    ModelState.AddModelError("", "This Category is updated content !");
                    var category = dao.GetCategoryById(TempClass.FormatString.removeOddLetter(obj.category));
                    category.briefIntroduce = obj.description;
                    category.urlImg = obj.urlImg;
                    dao.UpdateCategory(category);
                }
                else
                {
                    var user = new User();
                    user = dao.GetById((string)(Session["USER_SESSION"]));
                    var category = new Category();
                    category.nameCategory = obj.category;
                    category.meta_Category = TempClass.FormatString.removeOddLetter(obj.category);
                    category.briefIntroduce = obj.description;
                    category.addedDate = DateTime.Now;
                    category.urlImg = obj.urlImg;
                    category.idUser = user.id;
                    var result = dao.addCategory(category);
                    if(result == true)
                    {
                        ModelState.AddModelError("", "Add Category Success!");
                        obj = new CategoryModal();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add Category !");
                    }
                }
            }
            return View(obj);
        }
    }
}