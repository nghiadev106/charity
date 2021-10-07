using AutoMapper;
using ProjectFinal.Common;
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
    public class ServiceController : BaseController
    {
        // GET: Admin/Service
        private readonly IServices _service;

        public ServiceController(IServices service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            ViewBag.ListService = _service.GetListService();
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ServiceModel serviceModel)
        {
            if (ModelState.IsValid)
            {
                if (serviceModel.LogoFile == null || serviceModel.LogoFile.ContentLength == 0)
                {
                    serviceModel.Image = null;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(serviceModel.LogoFile.FileName);
                    string extention = Path.GetExtension(serviceModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    serviceModel.Image = "/UploadFiles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/"), fileName);
                    serviceModel.LogoFile.SaveAs(fileName);
                }
                _service.Add(serviceModel);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var model=_service.GetServiceDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            var viewModel= Mapper.Map<Service, ServiceModel>(model);
            viewModel.Money = Convert.ToDecimal(CommonUtil.RenderPrice(viewModel.Money));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ServiceModel serviceModel)
        {
            var service = _service.GetServiceDetail(serviceModel.Id);
            if (ModelState.IsValid)
            {
                if (serviceModel.LogoFile == null || serviceModel.LogoFile.ContentLength == 0)
                {
                    serviceModel.Image = service.Image;
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(serviceModel.LogoFile.FileName);
                    string extention = Path.GetExtension(serviceModel.LogoFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    serviceModel.Image = "/UploadFiles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/UploadFiles/"), fileName);
                    serviceModel.LogoFile.SaveAs(fileName);
                }
                _service.Update(serviceModel);
                return RedirectToAction("Index");
            }
            else
            {
                var model = _service.GetServiceDetail(serviceModel.Id);
                var viewModel = Mapper.Map<Service, ServiceModel>(model);
                viewModel.Money = Convert.ToDecimal(CommonUtil.RenderPrice(viewModel.Money));
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = _service.GetServiceDetail(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}