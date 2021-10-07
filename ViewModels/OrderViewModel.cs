using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFinal.ViewModels
{
    public class OrderViewModel
    {
        public string OrderId { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
        public Nullable<int> PaymentStatusId { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }    
        public string ServiceName { get; set; }
    }
}