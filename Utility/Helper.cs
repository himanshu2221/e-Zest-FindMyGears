using FindMyGears.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Utility
{
    [Serializable]
    public class Helper
    {
        List<SubCategory> categoryList;
        List<Questionary> questionaryList;
        List<Products> productList;

        public List<SubCategory> ShowCategories()
        {
            categoryList = new List<SubCategory> {
                new SubCategory{SubCategoryName="Shoes",SubCategoryID=201,CategoryID=101,ImageUrl="http://d2s0f1q6r2lxto.cloudfront.net/pub/ProTips/wp-content/uploads/2016/07/MENSRUNNINGSHOES.jpg"},
                new SubCategory{SubCategoryName="T-Shirts",SubCategoryID=202,CategoryID=101,ImageUrl="https://5.imimg.com/data5/HC/DH/MY-1498574/sports-t-shirts-250x250.jpg"},
                new SubCategory{SubCategoryName="Track Pants",SubCategoryID=203,CategoryID=101,ImageUrl="https://media.yoox.biz/items/36/36986834mm_14_f.jpg"}
            };

            return categoryList;

        }



        public int GetId(string name)
        {
            int id = 0;
            //if (name == "Running")
            id = 101;

            return id;
        }

        public List<Products> GetProducts()
        {
            productList = new List<Products>
            {
                new Products{ProductId=12653,Price="6000 Rs",ImageUrl="https://i.pinimg.com/originals/78/71/e9/7871e974c9a0964acf51c13342f58a6c.jpg" },
                new Products{ProductId=12653,Price="8000 Rs",ImageUrl="http://www.polygonaltree.co.uk/images/polygonaltree.co.uk/mens-nike-running-shoe-free-run-2-black-dark-red-3L1J.jpg" },
                new Products{ProductId=12653,Price="7500 Rs",ImageUrl="http://www.sharebrandshop.com/images/Adidas/Clima/5-adidas-Clima-Cool-Running-shoe.jpg" },
                new Products{ProductId=12653,Price="4000 Rs",ImageUrl="http://www.ceabadessenc.com/images/hotsalepumacom/241556fsdafew.jpg" },
                new Products{ProductId=12653,Price="9000 Rs",ImageUrl="http://beingfitandhealthyrocks.com/wp-content/uploads/2013/06/Puma-Voltaic-4.jpg" },

            };
            return productList;
        }
    }
}