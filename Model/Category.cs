using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Model
{
    [Serializable]
    public class Category
    {
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string ImageUrl { get; set; }
    }
}