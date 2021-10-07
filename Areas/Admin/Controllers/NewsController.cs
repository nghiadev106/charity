using AutoMapper;
using ProjectFinal.Models;
using ProjectFinal.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        // GET: Admin/New
        private readonly INewsRepository _news;
        private readonly INewCategoryRepository _newCategory;

        public NewsController(INewsRepository news, INewCategoryRepository newCategory)
        {
            _news = news;
            _newCategory = newCategory;
        }

        public ActionResult Index()
        {
            ViewBag.ListNews = _news.GetListNews();
            return View();
        }
        public ActionResult Create()
        {
            var categories = _newCategory.GetListNewCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsModel newsModel)
        {
            if (ModelState.IsValid)
            {
                if (newsModel.LogoFile == null || newsModel.LogoFile.ContentLength == 0)
                {
                    newsModel.Image = null;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(newsModel.LogoFile.FileName);
                    string extention = Path.GetExtension(newsModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    newsModel.Image = "/UploadFiles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/"), fileName);
                    newsModel.LogoFile.SaveAs(fileName);
                }
                _news.Add(newsModel);
                return RedirectToAction("Index");
            }
            else
            {
                var categories = _newCategory.GetListNewCategory();
                SelectList categoryList = new SelectList(categories, "Id", "Name");
                ViewBag.categoryList = categoryList;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = _news.GetNewsDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var categories = _newCategory.GetListNewCategory();
            SelectList categoryList = new SelectList(categories, "Id", "Name");
            ViewBag.categoryList = categoryList;
            var viewModel = Mapper.Map<News,NewsModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(NewsModel newsModel)
        {
            var service = _news.GetNewsDetail(newsModel.Id);
            if (ModelState.IsValid)
            {
                if (newsModel.LogoFile == null || newsModel.LogoFile.ContentLength == 0)
                {
                    newsModel.Image = service.Image;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(newsModel.LogoFile.FileName);
                    string extention = Path.GetExtension(newsModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    newsModel.Image = "/UploadFiles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/"), fileName);
                    newsModel.LogoFile.SaveAs(fileName);
                }
                _news.Update(newsModel);
                return RedirectToAction("Index");
            }
            else
            {
                var model = _news.GetNewsDetail(newsModel.Id);
                var viewModel = Mapper.Map<News, NewsModel>(model);

                var categories = _newCategory.GetListNewCategory();
                SelectList categoryList = new SelectList(categories, "Id", "Name");
                ViewBag.categoryList = categoryList;

                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _news.GetNewsDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            _news.Delete(id);
            return RedirectToAction("Index");
        }
    }
}