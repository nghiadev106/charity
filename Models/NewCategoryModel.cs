using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFinal.Models
{
    public class NewCategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên danh mục tin tức")]
        [StringLength(500, ErrorMessage = "Tên danh mục tin tức không quá 500 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mô tả danh mục tin tức")]
        [StringLength(500, ErrorMessage = "Tên danh mục tin tức không quá 500 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn trạng thái")]
        public Nullable<int> Status { get; set; }
    }
}