using ProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectFinal.Repositories
{
    public interface IAccount
    {
        int Login(string userName, string passWord);
        User GetUserDetail(string userName, string passWord);
        User Add(AccountModel userModel);
        User Delete(int id);
        List<User> GetListUser();
        User GetUserDetail(int Id);
        void Update(AccountUpdateModel userModel);
        User GetUserName(string UserName);
    }
    public class Account: IAccount
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        public User GetUserDetail(string userName, string passWord)
        {
            return db.User.SingleOrDefault(x => x.UserName == userName && x.Pass==passWord);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.User.Where(x=>x.Status==1).SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                    if (result.Status != 1)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Pass == passWord)
                            return 1;
                        else
                            return -2;
                    }
                
            }
        }

        public User Add(AccountModel userModel)
        {
            var newUser = new User();
            newUser.UserName = userModel.UserName;
            newUser.Pass = userModel.Pass;
            newUser.Status = userModel.Status;
            newUser.CreateDate = DateTime.Now;

            db.User.Add(newUser);
            db.SaveChanges();
            return newUser;
        }

        public User Delete(int id)
        {
            var user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return user;
        }

        public List<User> GetListUser()
        {
            var lst = db.User.ToList();
            return lst;
        }

        public User GetUserName(string UserName)
        {
            var lst = db.User.SingleOrDefault(x => x.UserName == UserName);
            return lst;
        }

        public User GetUserDetail(int Id)
        {
            var lst = db.User.SingleOrDefault(x => x.Id == Id);
            return lst;
        }

        public void Update(AccountUpdateModel userModel)
        {
            var oldUser = db.User.Find(userModel.Id);
            oldUser.Pass = userModel.Pass;
            oldUser.Status = userModel.Status;
            db.User.Attach(oldUser);
            db.Entry(oldUser).State = EntityState.Modified;
            db.Entry(oldUser).Property(x => x.UserName).IsModified = false;
            db.Entry(oldUser).Property(x => x.CreateDate).IsModified = false;
            db.SaveChanges();
        }
    }
}