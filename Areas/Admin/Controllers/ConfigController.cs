using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ProjectFinal.Models;
using ProjectFinal.Repositories;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly IConfigRepositiory _configRepositiory;
        ProjectFinalEntities db = new ProjectFinalEntities();

        public ConfigController(IConfigRepositiory configRepositiory)
        {
            _configRepositiory = configRepositiory;
        }


        public ActionResult Edit()
        {
            var model = db.Config.SingleOrDefault();
            var viewModel = Mapper.Map<Config, ConfigModel>(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ConfigModel model)
        {
            if (ModelState.IsValid)
            {
                _configRepositiory.Update(model);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}