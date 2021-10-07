using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFinal.Models;

namespace ProjectFinal.Areas.Admin.Controllers
{
    public class PaymentVatController : BaseController
    {
        // GET: Admin/PaymentVat

        ProjectFinalEntities db = new ProjectFinalEntities();
        public ActionResult Index()
        {
            var data = db.PaymentExpVat.ToList();
            return View(data);
        }
    }
}