using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên chiến dịch")]
        [StringLength(500, ErrorMessage = "Tên chiến dịch không quá 500 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số tiền")]
        public Nullable<decimal> Money { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số tiền")]
        [AllowHtml]
        public string Descripttion { get; set; }

        public string Image { get; set; }

        public Nullable<System.DateTime> CreateDate { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn ngày bắt đầu")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "Bạn cần chọn ngày kết thúc")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<int> Status { get; set; }
        public HttpPostedFileWrapper LogoFile { get; set; }
    }
}