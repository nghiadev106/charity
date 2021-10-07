using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFinal.Models
{
    public class OrderInfo
    {
        /// <summary>
        /// Payment amount
        /// </summary>
        public decimal Amount { get; set; }
        public string OrderDescription { get; set; }

        public string BankCode { get; set; }

        public string Language { get; set; }

        public string OrderCategory { get; set; }

        public string Bank { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Creaed date
        /// </summary>
        //public DateTime CreatedDate { get; set; }
        /// <summary>
        /// VNPAY Transaction Id
        /// </summary>
        public decimal vnp_TransactionNo { get; set; }
        public string vpn_Message { get; set; }
        public string vpn_TxnResponseCode { get; set; }

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
        public Nullable<int> ServiceId { get; set; }
        public DateTime CreatedDate { get; internal set; }
    }
}