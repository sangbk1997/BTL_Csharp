using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BTLCsharp.Dao;
using System.Web.Mvc;
using BTLCsharp.Models;
using BTLCsharp.EF;
using TempClass;

namespace BTLCsharp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        // GET: User
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(SignUpModel obj)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.checkUserName(TempClass.FormatString.removeOddLetter(obj.username)))
                {
                    ModelState.AddModelError("", "This username is exist ! Please choose another name.");

                }
                else
                {
                    var user = new User();
                    user.username = obj.username;
                    user.meta_username = TempClass.FormatString.removeOddLetter(obj.username);
                    user.password = obj.password;
                    user.email = obj.email;
                    user.address = obj.address;
                    user.joinDate = DateTime.Now;
                    user.age = obj.age;
                    user.modeAccess = 0;
                    user.score = 0;
                    var result = dao.Insert(user);
                    string alert = "Nickname : " + user.meta_username.ToString();
                    if(result > 0)
                    {
                        ModelState.AddModelError("","Sign Up Success !");
                        ModelState.AddModelError("", alert);
                        obj = new SignUpModel();
                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to registered !");

                    }
                }

            }
            return View(obj);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel obj)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(TempClass.FormatString.removeOddLetter(obj.username), obj.password);
                if(result == 1)
                {
                    var user = dao.GetById(TempClass.FormatString.removeOddLetter(obj.username));
                    var userSession = TempClass.FormatString.removeOddLetter(obj.username);
                    Session.Add("USER_SESSION", userSession);
                    return Redirect("/Home/Index");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Invalid username.");
                }else if(result == -1)
                {
                    ModelState.AddModelError("", "Invalid password.");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to try login...!");

                }
            }
            return View(obj);

        }

        public ActionResult Logout()
        {
            Session["USER_SESSION"] = null;
            return Redirect("/");
           
        }

    }
}