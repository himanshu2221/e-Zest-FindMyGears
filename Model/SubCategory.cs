using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Model
{
    [Serializable]
    public class SubCategory
    {
        public int CategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string ImageUrl { get; set; }
    }
}