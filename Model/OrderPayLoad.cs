using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Model
{
    public class OrderPayLoad
    {
        public UserProfile userProfile { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }
}