using AutoMapper;
using ProjectFinal.Models;
using ProjectFinal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class NewCategoryController : BaseController
    {
        private readonly INewCategoryRepository _newCategory;

        public NewCategoryController(INewCategoryRepository newCategory)
        {
            _newCategory = newCategory;
        }

        public ActionResult Index()
        {
            ViewBag.ListNewCategory = _newCategory.GetListNewCategory();
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryModel newCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _newCategory.Add(newCategoryModel);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = _newCategory.GetNewCategoryDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = Mapper.Map<NewCategory, NewCategoryModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(NewCategoryModel newCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _newCategory.Update(newCategoryModel);
                return RedirectToAction("Index");
            }
            else
            {
                var model = _newCategory.GetNewCategoryDetail(newCategoryModel.Id);
                var viewModel = Mapper.Map<NewCategory, NewCategoryModel>(model);
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _newCategory.GetNewCategoryDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            _newCategory.Delete(id);
            return RedirectToAction("Index");
        }
    }
}