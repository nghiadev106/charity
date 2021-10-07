using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectFinal.Models
{
    public class VideoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên video ")]
        [StringLength(500, ErrorMessage = "Tên video không quá 500 ký tự")]
        public string Name { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Bạn cần nhập mô tả")]
        public string Description { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Bạn cần nhập Link Video")]
        public string Link { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }
    }
}