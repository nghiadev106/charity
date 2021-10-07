using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFinal.Models
{
    public class ConfigModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string vnp_Url { get; set; }
        public string vnp_Returnurl { get; set; }
        public string vnpay_api_url { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_HashSecret { get; set; }
    }
}