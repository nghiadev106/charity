//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentBanking
    {
        public int Id { get; set; }
        public Nullable<int> PaymentTypeId { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> CreateBy { get; set; }
        public Nullable<System.DateTime> LastEditDate { get; set; }
        public Nullable<System.Guid> LastEditBy { get; set; }
        public string BankLogo { get; set; }
        public string BankInfo { get; set; }
        public string BankNumber { get; set; }
        public string BankReceive { get; set; }
    
        public virtual PaymentType PaymentType { get; set; }
    }
}