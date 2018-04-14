using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BTLCsharp.EF;

namespace BTLCsharp.Dao
{
    public class UserDao
    {
        Model db = null;
        public UserDao()
        {
            db = new Model();
        }
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.id;
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
        public bool Update(User entity)
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

        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.username == userName);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.username == userName);
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
            return db.Users.Count(x => x.username == userName) > 0;
        }

        public bool checkEmail(string email)
        {
            return db.Users.Count(x => x.email == email) > 0;
        }

        public bool checkCategory(string category)
        {
            return db.Categories.Count(x => x.Category1 == category) > 0;
        }
        
    }
}