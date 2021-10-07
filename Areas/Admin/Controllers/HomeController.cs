using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using ProjectFinal.Models;
using ProjectFinal.Repositories;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        ProjectFinalEntities db = new ProjectFinalEntities();

        private readonly IPayment _payment;

        public HomeController(IPayment payment)
        {
            _payment = payment;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.CountTrans = db.Order.Count();
            ViewBag.CountNews = db.News.Count();
            ViewBag.CountVideos = db.Video.Count();

            var lstOrder = db.Order.ToList();
            decimal totalMoney = 0;
            for (int i = 0; i < lstOrder.Count ; i++)
            {
                totalMoney = (decimal) (totalMoney + lstOrder[i]?.TotalMoney);
            }

            ViewBag.Total = totalMoney;

            ViewBag.TopTrans = _payment.GetListOrderAdmin().OrderByDescending(x => x.TotalMoney).Take(10).ToList();
            return View();
        }
    }
}