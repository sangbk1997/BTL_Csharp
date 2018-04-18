﻿using System;
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
            string contentAudio = Convert.ToString(obj.content.ToString());
            string tempString = model.value.ToString();
            tempString = TempClass.OrderSubSequence.formatString(tempString);
            string[] arrayTempString = tempString.Split(new char[] { ' ' });
            ArrayList longestCommonSequence = TempClass.LCS.getLCS(arrayTempString, contentAudio);
            /*
            for (int i=0; i < longestCommonSequence.Count; i++)
            {
                string subString = longestCommonSequence[i].ToString();
                int indexAdded = 0;
                if (contentAudio.Contains(subString))
                {
                    string temp = "<span style = \"color: blue\" >" + subString + "</span > ";
                    contentAudio = contentAudio.Replace(subString,temp);
                    indexAdded = contentAudio.IndexOf(subString) + temp.Length;
                }
                else
                {
                    string temp = "<span style = \"color: gray ;text-decoration: line-through; font-size: 12px;\" >" + subString + "</span > ";

                    string subEnd = contentAudio.Substring(indexAdded +1);
                    contentAudio = contentAudio.Substring(0, indexAdded);
                    contentAudio = string.Concat(contentAudio, temp, subEnd);
                }
            }
            string resultString = "<span style = \"color: red\" >" + contentAudio + "</span > ";
            ViewBag.result = resultString;
            */
            string resultString = "";
            contentAudio = TempClass.OrderSubSequence.formatString(contentAudio);
            string[] arrayContentAudio = contentAudio.Split(new char[] { ' ' });
            for(int i=0; i<arrayContentAudio.Length; i++)
            {
                int find = 0;
                for(int j=longestCommonSequence.Count-1; j>=0; j--)
                {
                    if (arrayContentAudio[i].Equals(longestCommonSequence[j]))
                    {
                        find = 1;
                        string temp = "<span style=\"color:blue\">" +arrayContentAudio[i] + "</span>";
                        longestCommonSequence.Remove(longestCommonSequence[j]);
                        resultString = string.Concat(resultString, temp);
                        continue;
                    }
                }
                if(find == 0)
                {
                    string temp = arrayContentAudio[i];
                    resultString = string.Concat(resultString, temp);
                }
            }
            ViewBag.result = resultString;
            TempData.Keep("id");
            return View();
        }
        public ActionResult fullMode(int id)
        {
            db = new Model2();
            ViewBag.audio = db.Audios.Find(id);
            return View();
        }
        public ActionResult quickMode(int id)
        {
            db = new Model2();
            ViewBag.audio = db.Audios.Find(id);
            return View();
        }
        public ActionResult blankMode(int id)
        {
            db = new Model2();
            ViewBag.audio = db.Audios.Find(id);
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