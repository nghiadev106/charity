using AutoMapper;
using ProjectFinal.Models;
using ProjectFinal.Repositories;
using System.Web.Mvc;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }
        // GET: Admin/Account
        public ActionResult Index()
        {
            ViewBag.ListUser = _account.GetListUser();
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AccountModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = _account.GetUserName(userModel.UserName);
                if(user!= null)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại.");
                    return View();
                }
                else
                {
                    _account.Add(userModel);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = _account.GetUserDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = Mapper.Map<User, AccountUpdateModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AccountUpdateModel userModel)
        {
            if (ModelState.IsValid)
            {
                _account.Update(userModel);
                return RedirectToAction("Index");
            }
            else
            {
                var model = _account.GetUserDetail(userModel.Id);
                var viewModel = Mapper.Map<User, AccountUpdateModel>(model);
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _account.GetUserDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            _account.Delete(id);
            return RedirectToAction("Index");
        }
    }
}