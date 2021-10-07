using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Models
{
    public class NewsModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tiêu đề tin tức")]
        [StringLength(500, ErrorMessage = "Tiêu đề  tin tức không quá 500 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mô tả tin tức")]
        [StringLength(500, ErrorMessage = "Mô tả tin tức không quá 500 ký tự")]
        public string Description { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Bạn cần nhập nội dung chi tiết tin tức")]
        public string Detail { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn  tin tức")]
        public Nullable<int> NewCategoryId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastEditdate { get; set; }
        public string CreateBy { get; set; }
        public string LastEditBy { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn loại tin tức")]
        public Nullable<int> Type { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }
        public HttpPostedFileWrapper LogoFile { get; set; }
    }
}