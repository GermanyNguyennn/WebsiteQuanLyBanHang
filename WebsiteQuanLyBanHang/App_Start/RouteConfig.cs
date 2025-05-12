using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteQuanLyBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                 name: "Checkout",
                 url: "thanh-toan",
                 defaults: new { controller = "ShoppingCart", action = "Checkout", Alias = UrlParameter.Optional },
                 namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                 name: "VNPayReturn",
                 url: "vnpay-return",
                 defaults: new { controller = "ShoppingCart", action = "VNPayReturn", Alias = UrlParameter.Optional },
                 namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "ShoppingCart",
                url: "gio-hang",
                defaults: new { controller = "ShoppingCart", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "ProductCategory",
                url: "danh-muc-san-pham/{alias}-{id}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "Posts",
                url: "bai-viet/{alias}",
                defaults: new { controller = "Posts", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "chi-tiet-san-pham/{alias}-p{id}",
                defaults: new { controller = "Product", action = "ProductDetail", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "san-pham",
                defaults: new { controller = "Product", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", Alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "DetailNews",
                url: "{alias}-n{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "DetailPosts",
                url: "{alias}-n{id}",
                defaults: new { controller = "Posts", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteQuanLyBanHang.Controllers" }
            );
        }
    }
}
