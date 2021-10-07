using ProjectFinal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IPayment _payment;

        public OrderController(IPayment payment)
        {
            _payment = payment;
        }
        // GET: Admin/Order
        public ActionResult Index()
        {
            ViewBag.ListOrder = _payment.GetListOrderAdmin();
            return View();
        }

        public ActionResult Detail(string orderId)
        {
            var model = _payment.GetOrderDetail(orderId);
            if (model == null)
            {
                return RedirectToAction("Index");
            }         
            return View(model);
        }
    }
}