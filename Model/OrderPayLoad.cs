using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Model
{
    [Serializable]
    public class OrderPayLoad
    {
        public UserProfile userProfile { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Size { get; set; }
        public string RunningType { get; set; }
    }
}