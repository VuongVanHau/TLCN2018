using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPham.Models
{
    public partial class Product
    {
        public long ProductID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public decimal Price { get; set; }
    }
}