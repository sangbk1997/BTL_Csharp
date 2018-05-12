using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BTLCsharp.EF;

namespace BTLCsharp.Dao
{
    public class UserDao
    {
        Model2 db = null;
        public UserDao()
        {
            db = new Model2();
        }
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.id;
        }
        public bool addHistoricalScore(HistoricalScore entity)
        {
            try
            {
                db.HistoricalScores.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool submitAudio(Audio entity)
        {
            try
            {
                db.Audios.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool addCategory(Category entity)
        {
            try
            {
                db.Categories.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateUser(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.id);
                user.username = entity.username;
                if (!string.IsNullOrEmpty(entity.password))
                {
                    user.password = entity.password;
                }
                user.email = entity.email;
                user.address = entity.address;
                user.age = entity.age;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }



        public bool UpdateCategory(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.idCategory);
                category.briefIntroduce = entity.briefIntroduce;
                category.urlImg = entity.urlImg;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateAudio(Audio entity)
        {
            try
            {
                var audio = db.Audios.Find(entity.idAudio);
                audio.content = entity.content;
                audio.urlAudio = entity.urlAudio;
                audio.level = entity.level;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.meta_username == userName);
        }

        public Category GetCategoryById(string categoryName)
        {
            return db.Categories.SingleOrDefault(x => x.meta_Category == categoryName);
        }

        public Audio GetAudioById(string audioName)
        {
            return db.Audios.SingleOrDefault(x => x.meta_AudioName == audioName);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public Audio ViewDetailAudio(string id)
        {
            int temp = Int32.Parse(id);
            return db.Audios.SingleOrDefault(x => x.idAudio == temp);
        }

        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.meta_username == userName);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.password == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public  bool checkUserName(string userName)
        {
            return db.Users.Count(x => x.meta_username == userName) > 0;
        }

        public bool checkEmail(string email)
        {
            return db.Users.Count(x => x.email == email) > 0;
        }

        public bool checkCategory(string category)
        {
            return db.Categories.Count(x => x.meta_Category == category) > 0;
        }
        public bool checkAudio(string audioName)
        {
            return db.Audios.Count(x => x.meta_AudioName == audioName) > 0;
        }
       
    }
}