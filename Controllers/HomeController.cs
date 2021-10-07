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
    public class HomeController : Controller
    {
        private readonly IServices _service;
        private readonly INewsRepository _news;
        private readonly INewCategoryRepository _newCategoryRepository;
        private readonly IVideoRepository _video;

        public HomeController(IServices service, INewsRepository news, IVideoRepository video, INewCategoryRepository newCategoryRepository)
        {
            _service = service;
            _news = news;
            _video = video;
            _newCategoryRepository = newCategoryRepository;
        }

        public ActionResult Index()
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            ViewBag.ListService = _service.GetListService().Where(x=>x.Status==1).Take(8).ToList();
            ViewBag.ListNews = _news.GetListNewsHome().Take(3).ToList();
            return View();
        }

        public ActionResult Service(int page = 1)
        {
            int pageSize = 8;
            int totalRow = 0;
            var ServiceModel = _service.GetListServicePaging(page, pageSize, out totalRow);
            var ServiceViewModel = Mapper.Map<IEnumerable<Service>, IEnumerable<ServiceModel>>(ServiceModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            //var category = _ServiceCategoryService.GetById(id);
            //ViewBag.Category = Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ServiceModel>()
            {
                Items = ServiceViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            return View(paginationSet);
        }


        public ActionResult ServiceDetail(int id)
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;

            var data = _service.GetServiceDetail(id);
            return View(data);
        }

        public ActionResult Search(string keyword,int page = 1)
        {
            int pageSize = 8;
            int totalRow = 0;
            var ServiceModel = _service.Search(keyword,page, pageSize, out totalRow);
            var ServiceViewModel = Mapper.Map<IEnumerable<Service>, IEnumerable<ServiceModel>>(ServiceModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            //var category = _ServiceCategoryService.GetById(id);
            //ViewBag.Category = Mapper.Map<ServiceCategory, ServiceCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ServiceModel>()
            {
                Items = ServiceViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            ViewBag.keyword = keyword;
            return View(paginationSet);
        }

        public ActionResult SearchVideo(string keyword, int page = 1)
        {
            int pageSize = 8;
            int totalRow = 0;
            var VideoModel = _video.SearchVieo(keyword, page, pageSize, out totalRow);
            var VideoViewModel = Mapper.Map<IEnumerable<Video>, IEnumerable<VideoModel>>(VideoModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            //var category = _VideoCategoryVideo.GetById(id);
            //ViewBag.Category = Mapper.Map<VideoCategory, VideoCategoryViewModel>(category);
            var paginationSet = new PaginationSet<VideoModel>()
            {
                Items = VideoViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            ViewBag.keyword = keyword;
            ViewBag.SearchVideo = "VideoSearch";
            return View(paginationSet);
        }
      
        public ActionResult Video(int page=1)
        {
            int pageSize = 6;
            int totalRow = 0;
            var VideoModel = _video.GetListVideoPaging(page, pageSize, out totalRow);
            var VideoViewModel = Mapper.Map<IEnumerable<Video>, IEnumerable<VideoModel>>(VideoModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            //var category = _VideoCategoryVideo.GetById(id);
            //ViewBag.Category = Mapper.Map<VideoCategory, VideoCategoryViewModel>(category);
            var paginationSet = new PaginationSet<VideoModel>()
            {
                Items = VideoViewModel,
                MaxPage = 5,
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            ViewBag.SearchVideo = "VideoSearch";
            return View(paginationSet);
        }

     

        public ActionResult About()
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            return View();
        }

        [ChildActionOnly]
        public ActionResult CategoryNews()
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);

            return PartialView(listnewCategoryViewModel);
        }

        public ActionResult Contact()
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            return View();
        }
    }
}