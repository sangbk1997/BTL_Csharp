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
        public static String dayLearned()
        {
            String Day = DateTime.Now.Day.ToString();
            String Month = DateTime.Now.Month.ToString();
            String Year = DateTime.Now.Year.ToString();
            String dayLearned = Day + "\\" + Month + "\\" + Year;
            return dayLearned;
        }
        public ActionResult AudioByCategory(string category)

        {
            db = new Model2();
            var categories = db.Categories.Select(s => s.meta_Category).ToList();
            if (categories != null)
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    categories[i] = categories[i].Replace(' ', '-');
                }
            }
            if (!categories.Contains(category))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var audios = db.Audios.ToList();
                List<Audio> listAudio = new List<Audio>();
                foreach (var audio in audios)
                {
                    if (audio.Category.Replace(' ', '-').Equals(category))
                    {
                        listAudio.Add(audio);
                    }
                }
                if (listAudio == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.audios = listAudio;
                return View();
            }


        }
        public ActionResult AudioByLevel(int level)
        {
            db = new Model2();
            var listLevel = db.Audios.Select(s => s.level);
            var levels = listLevel.Distinct().ToArray();
            if (level < Int32.Parse(levels[0].ToString()) || level > Int32.Parse(levels[levels.Length - 1].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var audios = db.Audios.Where(s => s.level == level).ToList();
                if (audios == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.audios = audios;
                return View();
            }
        }
        // GET: Audio
        public ActionResult ListeningModes(int id)
        {
            db = new Model2();
            var choosedAudio = db.Audios.Find(id);
            ViewBag.audio = choosedAudio;
            if (ViewBag.audio == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                choosedAudio.views++;
                db.SaveChanges();
                return View();
            }
        }
        [HttpGet]
        public ActionResult correctionMode(int id)
        {
            db = new Model2();
            ViewBag.audio = db.Audios.Find(id);
            if (ViewBag.audio == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.display = "none";
            TempData["id"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult correctionMode(inputDataModal model)
        {

            if (model != null || model.value.ToString().Equals(""))
            {
                ViewBag.display = "display";
                db = new Model2();
                Audio obj = db.Audios.Find(TempData["id"]);
                ViewBag.audio = obj;
                string contentAudio = "";
                contentAudio = String.Concat(contentAudio, obj.content.ToString());
                string tempString = "";
                tempString = String.Concat(tempString, model.value.ToString());
                tempString = TempClass.functionClass.removeOddLetter(tempString);
                tempString = tempString.ToLower();
                contentAudio = TempClass.functionClass.removeOddLetter(contentAudio);
                contentAudio = contentAudio.ToLower();
                string[] arrayTempString = tempString.Split(new char[] { ' ' });
                List<string> listItems = TempClass.LCS.getLCS(arrayTempString, contentAudio);
                string[] arrayStringContent = contentAudio.Split(new char[] { ' ' });
                ViewData["inputData"] = tempString;
                ViewData["contentAudio"] = contentAudio;
                ViewData["LSS"] = listItems;
                TempData.Keep("id");

                // Công thức tính điểm trong chế độ CorrectionMode 
                var score = (listItems.Count * 100 / contentAudio.Length) * (obj.level+2);
                ViewBag.score = score;

                // Cong diem neu nguoi dung da dang nhap 
                addScore((int)score);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult fullMode(int id)
        {
            db = new Model2();
            Audio audio = db.Audios.Find(id);
            ViewBag.audio = audio;
            if(ViewBag.audio == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (ViewBag.audio == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            if (ViewBag.audio == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string contentAudio = audio.content.ToString();
            contentAudio = TempClass.functionClass.removeOddLetter(contentAudio);
            ViewBag.contentAudio = contentAudio;
            return View();
        }

        public ActionResult addScore(int score)
        {
            if (Session["USER_SESSION"] != null)
            {
                db = new Model2();
                var dao = new UserDao();
                var user = new User();
                user = dao.GetById((string)(Session["USER_SESSION"]));
                var objUser = db.Users.Find(user.id);
                var listHis = db.HistoricalScores.Where(s => s.idUser == user.id).ToList();
                var maxScoreDay = 0;
                if (listHis.Count > 0)
                {
                    maxScoreDay = (int)listHis.Max(s => s.score);
                }
                if((maxScoreDay +score)  >= 2000)
                {
                    TempData["limitScore"] = "The limit for each day is 2000 points. You have reach the limitation and don't add more scores ! ";
                    return Redirect("/Home/Index");
                }
                else
                {
                    // Cong them diem vao cho nguoi dung 
                    int addedScore = score;
                    if(maxScoreDay + score >= 2000)
                    {
                         addedScore = (2000 - maxScoreDay);
                    }
                    objUser.totalScores += addedScore;
                    db.SaveChanges();
                    // Them doi tuong diem vao trong lich su 
                    String day = dayLearned();
                    var findObj = db.HistoricalScores.Find(user.id, day);
                    if (findObj != null)
                    {
                        findObj.score += addedScore;
                        db.SaveChanges();
                        ViewBag.id = findObj.idUser;
                    }
                    else
                    {
                        var maxSeq = 0;
                        //var listHis = db.HistoricalScores.Where(s => s.idUser == user.id).ToList();
                        if (listHis.Count > 0)
                        {
                            maxSeq = (int)listHis.Max(s => s.seqDay);
                        }
                        var obj = new HistoricalScore();
                        obj.idUser = user.id;
                        obj.dayLearned = day;
                        obj.score = addedScore;
                        obj.seqDay = maxSeq + 1;
                        dao.addHistoricalScore(obj);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
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
                if (user.modeAccess == 1)
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
            if (Session["USER_SESSION"] != null && user.modeAccess == 1)
            {

                return View();
            }
            else
            {
                TempData["alertMsg"] = "Sorry only admin can add Categories";
                return Redirect("/Home/Index");
            }
        }
        [HttpPost]
        public ActionResult addCategory(CategoryModal obj)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.checkCategory(TempClass.FormatString.removeOddLetter(obj.category)))
                {
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
                    if (result == true)
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