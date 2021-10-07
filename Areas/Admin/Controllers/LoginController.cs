using ProjectFinal.Models;
using ProjectFinal.Repositories;
using ProjectFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccount _account;

        public LoginController(IAccount account)
        {
            _account = account;
        }
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _account.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = _account.GetUserDetail(model.UserName, model.Password);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;

                    Session.Add("USER_SESSION", userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
                else
                {
                    ModelState.AddModelError("", "đăng nhập không đúng.");
                }
            }
            return View("Index");
        }

        public ActionResult LogOff()
        {
            Session.Remove("USER_SESSION");
            return RedirectToAction("Index", "Home");
        }
    }
}