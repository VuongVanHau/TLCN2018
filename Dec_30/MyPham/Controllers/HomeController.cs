using MyPham.Dao;
using MyPham.Lucene;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPham.Controllers
{
    public class HomeController : Controller
    {
        MyPhamEntities db = new MyPhamEntities();
        bool check = true;

        public ActionResult Index(int? page, string searchString)
        {
            int pageSize = 18;
            int pageNumber = page ?? 1;
            //ViewBag.ProductCategories = db.ProductCategories.ToList();
            //ViewBag.Categories = db.Categories.Take(5).ToList();
            if (check == true)
            {
                ViewModel view = new ViewModel();
                view.LoadProducts();
                check = false;
            }
            return View(db.Products.OrderBy(s=>s.Price).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}