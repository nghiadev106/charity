using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFinal.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên Tài khoản")]
        [StringLength(50, ErrorMessage = "Tên Tài khoản không quá 50 ký tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [StringLength(50, ErrorMessage = "Mật khẩu không quá 50 ký tự")]
        public string Pass { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        [Required(ErrorMessage = "Bạn cần chon trạng thái")]
        public Nullable<int> Status { get; set; }
    }

    public class AccountUpdateModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }


        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [StringLength(50, ErrorMessage = "Mật khẩu không quá 50 ký tự")]
        public string Pass { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        [Required(ErrorMessage = "Bạn cần chon trạng thái")]
        public Nullable<int> Status { get; set; }
    }
}