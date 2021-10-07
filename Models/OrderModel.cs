using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFinal.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public Nullable<int> PaymentStatusId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string LastEditBy { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public string ContentPayment { get; set; }
        public string OrderCode { get; set; }
        public Nullable<long> ServiceId { get; set; }

        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}