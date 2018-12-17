using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MyPham.Models
{
    public class Cart
    {
        MyPhamEntities db = new MyPhamEntities();

        public string m_Id_Product { get; set; }
        public string m_Name { get; set; }
        public string m_Size { get; set; }
        public string m_Color { get; set; }
        public string m_Image { get; set; }
        public double m_Price { get; set; }
        public int m_Quanlity { get; set; }

        public double m_Total
        {
            get { return m_Quanlity * m_Price; }
        }

        public Cart(string productID, string size, string color)
        {
            //m_Id_Product = productID;
            //List<Product> prd = db.Products.Where(p => p.ProductID.ToString().Contains(productID)).ToList();
            //m_Name = prd.Name;
            //m_Size = size;
            //m_Color = color;
            //m_Image = prd.Image;
            //m_Price = double.Parse(prd.Price.ToString());
            //m_Quanlity = 1;
        }
    }
}