using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQuanLyBanHang.Models.EF;
using WebsiteQuanLyBanHang.Models;

namespace WebsiteQuanLyBanHang.Controllers
{
    public class NewsController : Controller
    {
        // GET: New
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Detail(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }
        public ActionResult PartialNews()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);
        }
    }
}