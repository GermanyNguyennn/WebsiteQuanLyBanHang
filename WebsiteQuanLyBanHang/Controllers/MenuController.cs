using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQuanLyBanHang.Models;

namespace WebsiteQuanLyBanHang.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(c => c.Position).ToList();
            return PartialView("_MenuTop", items);
        }

        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CategoryId = id;
            }
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }

        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuArrivals()
        {
            var items = db.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
    }
}