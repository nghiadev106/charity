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
    public class VideoController : BaseController
    {
        private readonly IVideoRepository _video;

        public VideoController(IVideoRepository video)
        {
            _video = video;
        }

        public ActionResult Index()
        {
            ViewBag.ListVideo = _video.GetListVideo();
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {
                _video.Add(videoModel);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model = _video.GetVideoDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel = Mapper.Map<Video, VideoModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {
                _video.Update(videoModel);
                return RedirectToAction("Index");
            }
            else
            {
                var model = _video.GetVideoDetail(videoModel.Id);
                var viewModel = Mapper.Map<Video, VideoModel>(model);
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _video.GetVideoDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            _video.Delete(id);
            return RedirectToAction("Index");
        }
    }
}