using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Model
{
    [Serializable]
    public class Products
    {
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public int ProductId { get; set; }
    }
}