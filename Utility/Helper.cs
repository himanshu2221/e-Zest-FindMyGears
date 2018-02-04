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
            int id=0;
            //if (name == "Running")
                id = 101;

            return id;
        }
    }
}