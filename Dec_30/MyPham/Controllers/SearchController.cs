using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
using PagedList;
using MyPham.Dao;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using System.Text;
using MyPham.Lucene;

namespace MyPham.Controllers
{
    public class SearchController : Controller
    {
        MyPhamEntities db = new MyPhamEntities();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SearchLucene(FormCollection f, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 18;
            var result = LuceneSearcher.Search("All", f["txtSearch1"].ToString());
            if (result.Count > 0)
            {
                ViewBag.result = "Search Lucene Result";
                return View(result.ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("searchNull");
        }
        [HttpPost]
        public ActionResult SearchResult(FormCollection f, int?page)
        {
            SearchDao searchDao = new SearchDao();
            //ViewBag.ProductCategories = db.ProductCategories.ToList();
            //ViewBag.Categories = db.Categories.Take(5).ToList();
            string searchString = f["txtSearch"].ToString();
            if(searchString == "")
            {
                return RedirectToAction("Index","Home");
            }
            string lbgroup = f["lbgroup"].ToString();
            List<Product> lstProduct = new List<Product>();
            if (lbgroup == "0")
            {
                lstProduct = searchDao.searchAll(searchString);
            }
            else
            {
                return RedirectToAction("searchNull");
            }

            ViewBag.searchString = searchString;

            int pageNumber = (page ?? 1);
            int pageSize = 18;
            
            if(lstProduct.Count() == 0)
            {
                return RedirectToAction("searchNull");
            }

            ViewBag.result = lstProduct.Count() + " items matched your search for "+ searchString;

            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult searchNull()
        {
            ViewBag.ProductCategories = db.ProductCategories.ToList();
            ViewBag.Categories = db.Categories.Take(5).ToList();
            return View();
        }
    }
}