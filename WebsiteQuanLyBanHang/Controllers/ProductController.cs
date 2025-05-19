using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQuanLyBanHang.Models;

namespace WebsiteQuanLyBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.Products.ToList();
            return View(items);
        }

        public ActionResult ProductCategory(string alias, int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var category = db.ProductCategories.Find(id);
            if (category != null)
            {
                ViewBag.CategoryName = category.Title;
            }
            ViewBag.CategoryId = id;
            return View(items);
        }

        public ActionResult PartialItemsByCategoryId()
        {
            var items = db.Products.Where(x => x.IsNew && x.IsActive).Take(10).ToList();
            return PartialView(items);
        }

        public ActionResult PartialProductSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(10).ToList();
            return PartialView(items);
        }

        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            return View(item);
        }
    }
}