using AutoMapper;
using ProjectFinal.Models;
using ProjectFinal.Repositories;
using ProjectFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _news;
        private readonly INewCategoryRepository _newCategoryRepository;

        public NewsController( INewsRepository news, INewCategoryRepository newCategoryRepository)
        {
            _news = news;
            _newCategoryRepository = newCategoryRepository;
        }

        // GET: News
        public ActionResult Index(int page = 1)
        {
            int pageSize = 6;
            int totalRow = 0;
            var NewsModel = _news.GetListNewsPaging(page, pageSize, out totalRow);
            var NewsViewModel = Mapper.Map<IEnumerable<News>, IEnumerable<NewsModel>>(NewsModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<NewsModel>()
            {
                Items = NewsViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            ViewBag.SearchNews = "NewsSearch";
            return View(paginationSet);
        }


        public ActionResult NewDetail(int id)
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            var data = _news.GetNewsDetail(id);
            ViewBag.SearchNews = "NewsSearch";
            return View(data);
        }


        public ActionResult SearchNews(string keyword, int page = 1)
        {
            int pageSize = 6;
            int totalRow = 0;
            var newsModel = _news.SearchNews(keyword, page, pageSize, out totalRow);
            var newsViewModel = Mapper.Map<IEnumerable<News>, IEnumerable<NewsModel>>(newsModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<NewsModel>()
            {
                Items = newsViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            ViewBag.keyword = keyword;
            ViewBag.SearchNews = "NewsSearch";
            return View(paginationSet);
        }

        public ActionResult NewByCategoryId(int id, int page = 1)
        {
            int pageSize = 6;
            int totalRow = 0;
            var NewsModel = _news.GetListNewsByCategoryPaging(id, page, pageSize, out totalRow);
            var NewsViewModel = Mapper.Map<IEnumerable<News>, IEnumerable<NewsModel>>(NewsModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var category = _newCategoryRepository.GetNewCategoryDetail(id);
            ViewBag.Category = Mapper.Map<NewCategory, NewCategoryModel>(category);
            var paginationSet = new PaginationSet<NewsModel>()
            {
                Items = NewsViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            ViewBag.SearchNews = "NewsSearch";
            return View(paginationSet);
        }
    }
}