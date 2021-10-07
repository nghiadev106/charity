using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFinal.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string NewCategoryName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastEditdate { get; set; }
        public string CreateBy { get; set; }
        public string LastEditBy { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Status { get; set; }
    }
}