using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
 

namespace MyPham.Dao
{
    public class SearchDao
    {
        MyPhamEntities db = new MyPhamEntities();

        public List<Product> searchAll(string searchString)
        {
            List<Product> lstProduct = db.Products.Where(p => p.Name.Contains(searchString.ToString())).ToList();
            return lstProduct;
        }
    }
}