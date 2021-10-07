using ProjectFinal.Models;
using System;
using System.Web.Mvc;
using ProjectFinal.Repositories;
using System.Configuration;
using System.Linq;
using ProjectFinal.VNPAY;
using AutoMapper;
using System.Collections.Generic;

namespace ProjectFinal.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPayment _payment;
        private readonly INewCategoryRepository _newCategoryRepository;
        ProjectFinalEntities db = new ProjectFinalEntities();

        public PaymentController(IPayment payment, INewCategoryRepository newCategoryRepository)
        {
            _payment = payment;
            _newCategoryRepository = newCategoryRepository;
        }
        // GET: Payment
        public ActionResult Index(int serviceId)
        {
            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            ViewBag.ServiceId = serviceId;
            return View();
        }

        [HttpPost]
        public ActionResult VAT(PaymentExpVat model)
        {
            if (ModelState.IsValid)
            {
                db.PaymentExpVat.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult RequetVNPAY(OrderInfo objOrderInfo)
        {
            var config = db.Config.SingleOrDefault();
            string vnp_Returnurl = config.vnp_Returnurl; //URL nhan ket qua tra ve 
            string vnp_Url = config.vnp_Url; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = config.vnp_TmnCode; //Ma website
            string vnp_HashSecret = config.vnp_HashSecret; //Chuoi bi mat

            //Get payment input
            //OrderInfo order = new OrderInfo();

            //Save order to db
            Order order = new Order();
            order.OrderId = Guid.NewGuid().ToString();
            order.CreateDate = DateTime.Now;
            order.LastEditDate = DateTime.Now;
            order.ContentPayment = objOrderInfo.ContentPayment;
            order.PaymentStatusId = 0;
            order.TotalMoney = objOrderInfo.TotalMoney;
            order.Name = objOrderInfo.Name;
            order.Phone = objOrderInfo.Phone;
            order.Address = objOrderInfo.Address;
            order.Email = objOrderInfo.Email;
            order.ServiceId = objOrderInfo.ServiceId;
            order.PaymentTypeId = 3;

            var result = _payment.CreateOrder(order);
            if(result != null)
            {
                objOrderInfo.OrderId = result.OrderId;
                objOrderInfo.Amount = Convert.ToDecimal(objOrderInfo.TotalMoney);
                objOrderInfo.CreatedDate = DateTime.Now;
                objOrderInfo.Language = "vn";
                objOrderInfo.Bank = objOrderInfo.Bank;

                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", "2.0.0");
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);

                string locale = objOrderInfo.Language;
                if (!string.IsNullOrEmpty(locale))
                {
                    vnpay.AddRequestData("vnp_Locale", locale);
                }
                else
                {
                    vnpay.AddRequestData("vnp_Locale", "vn");
                }

                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_TxnRef", result.OrderId.ToString());
                vnpay.AddRequestData("vnp_OrderInfo", objOrderInfo.ContentPayment);
                vnpay.AddRequestData("vnp_OrderType", objOrderInfo.ServiceId.ToString()); //default value: other
                vnpay.AddRequestData("vnp_Amount", (objOrderInfo.Amount * 100).ToString());
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
                vnpay.AddRequestData("vnp_CreateDate", Convert.ToDateTime(objOrderInfo.CreateDate).ToString("yyyyMMddHHmmss"));

                if (objOrderInfo.Bank != null && !string.IsNullOrEmpty(objOrderInfo.Bank))
                {
                    vnpay.AddRequestData("vnp_BankCode", objOrderInfo.Bank);
                }

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                return Redirect(paymentUrl);
            }
            else
            {
                return null;
            }
        }

        public ActionResult ReturnUrl(string vnp_Amount, string vnp_BankCode, string vnp_BankTranNo, string vnp_CardType, string vnp_OrderInfo, string vnp_PayDate, string vnp_ResponseCode, string vnp_TmnCode, string vnp_TransactionNo, string vnp_TxnRef, string vnp_SecureHashType, string vnp_SecureHash)
        {
            var config = db.Config.SingleOrDefault();
            ViewBag.Mesage = "";
            if (Request.QueryString.Count > 0)
            {
                
                string vnp_HashSecret = config.vnp_HashSecret; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();
                //if (vnpayData.Count > 0)
                //{
                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                // }

                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                string orderId = vnpay.GetResponseData("vnp_TxnRef");
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                ///string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                //vnp_SecureHash: MD5 cua du lieu tra ve
                //String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toan thanh cong
                        ViewBag.Message = "00";
                        ProjectFinalEntities db = new ProjectFinalEntities();
                        var dbEdit = db.Order.Find(orderId);
                        dbEdit.PaymentStatusId = 1;
                        ViewBag.OrderId = dbEdit.OrderId;
                        db.SaveChanges();
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.Message = "01";
                    }
                }
                else
                {
                    ViewBag.Message = "01";
                }
            }

            var model = _newCategoryRepository.GetListNewCategory();
            var listnewCategoryViewModel = Mapper.Map<IEnumerable<NewCategory>, IEnumerable<NewCategoryModel>>(model);
            ViewBag.ListCategory = listnewCategoryViewModel;
            return View();
        }

    }
}