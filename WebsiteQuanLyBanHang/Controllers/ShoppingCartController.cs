using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQuanLyBanHang.Models.EF;
using WebsiteQuanLyBanHang.Models.Payments;
using WebsiteQuanLyBanHang.Models;

namespace WebsiteQuanLyBanHang.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ShoppingCartController()
        {
        }

        public ShoppingCartController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: ShoppingCart
        [AllowAnonymous]
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult AddToCart(int id, int quantity)
        //{
        //    var code = new { Success = false, Mess = "", Code = -1, Count = 0 };
        //    var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
        //    if (checkProduct != null)
        //    {
        //        ShoppingCart cart = (ShoppingCart)Session["Cart"];
        //        if (cart == null)
        //        {
        //            cart = new ShoppingCart();
        //        }
        //        ShoppingCartItem item = new ShoppingCartItem
        //        {
        //            ProductId = checkProduct.Id,
        //            ProductName = checkProduct.Title,
        //            CategoryName = checkProduct.ProductCategories.Title,
        //            Alias = checkProduct.Alias,
        //            Quantity = quantity
        //        };
        //        if (checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault) != null)
        //        {
        //            item.ProductImage = checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault).Image;
        //        }
        //        item.Price = checkProduct.Price;
        //        if (checkProduct.PriceSale > 0)
        //        {
        //            item.Price = (decimal)checkProduct.PriceSale;
        //        }
        //        item.TotalPrice = item.Quantity * item.Price;
        //        cart.AddToCart(item, quantity);
        //        Session["Cart"] = cart;
        //        code = new { Success = true, Mess = "Thêm sản phẩm vào giở hàng thành công!", Code = 1, Count = cart.items.Count };
        //    }
        //    return Json(code);
        //}


        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, Mess = "", Code = -1, Count = 0 };
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);

            if (checkProduct != null)
            {
                // Kiểm tra số lượng tồn kho
                if (quantity > checkProduct.Quantity)
                {
                    code = new
                    {
                        Success = false,
                        Mess = "Sản phẩm không đủ số lượng trong kho. Chỉ còn " + checkProduct.Quantity + " sản phẩm.",
                        Code = 0,
                        Count = 0
                    };
                    return Json(code);
                }

                ShoppingCart cart = Session["Cart"] as ShoppingCart ?? new ShoppingCart();

                // Kiểm tra nếu sản phẩm đã có trong giỏ => cộng dồn sẽ vượt quá tồn kho không
                var existingItem = cart.items.FirstOrDefault(x => x.ProductId == id);
                int existingQuantity = existingItem != null ? existingItem.Quantity : 0;
                if (existingQuantity + quantity > checkProduct.Quantity)
                {
                    code = new
                    {
                        Success = false,
                        Mess = "Số lượng trong giỏ hàng cộng thêm vượt quá tồn kho. Bạn đang có " + existingQuantity + " sản phẩm, còn lại " + (checkProduct.Quantity - existingQuantity),
                        Code = 0,
                        Count = cart.items.Count
                    };
                    return Json(code);
                }

                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategories.Title,
                    Alias = checkProduct.Alias,
                    Quantity = quantity,
                    ProductImage = checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault)?.Image,
                    Price = checkProduct.PriceSale > 0 ? (decimal)checkProduct.PriceSale : checkProduct.Price
                };
                item.TotalPrice = item.Quantity * item.Price;

                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;

                code = new
                {
                    Success = true,
                    Mess = "Thêm sản phẩm vào giỏ hàng thành công!",
                    Code = 1,
                    Count = cart.items.Count
                };
            }
            else
            {
                code = new
                {
                    Success = false,
                    Mess = "Không tìm thấy sản phẩm.",
                    Code = -1,
                    Count = 0
                };
            }
            return Json(code);
        }


        public ActionResult VNPayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VNPayLibrary vnpay = new VNPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = db.Orders.FirstOrDefault(x => x.Code == orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.Status = 2;//Đã thanh toán
                            db.Orders.Attach(itemOrder);
                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
            }
            //var a = UrlPayment(0, "DH3574");
            return View();
        }

        #region Thanh toán vnpay
        public string UrlPayment(int TypePaymentVN, string orderCode)
        {
            var urlPayment = "";
            var order = db.Orders.FirstOrDefault(x => x.Code == orderCode);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VNPayLibrary vnpay = new VNPayLibrary();
            var Price = (long)order.TotalAmount * 100;
            vnpay.AddRequestData("vnp_Version", VNPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null)
                {
                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    order.Email = req.Email;
                    order.Status = 1;
                    cart.items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    order.TotalAmount = cart.items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    if (User.Identity.IsAuthenticated)
                        order.CustomerId = User.Identity.GetUserId();
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    var strSanPham = "";
                    var thanhtien = decimal.Zero;
                    var tongTien = decimal.Zero;
                    foreach (var sp in cart.items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + WebsiteQuanLyBanHang.Common.Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                        strSanPham += "</tr>";
                        thanhtien += sp.Price * sp.Quantity;
                    }
                    tongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/sendCustomer.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", req.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WebsiteQuanLyBanHang.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", WebsiteQuanLyBanHang.Common.Common.FormatNumber(tongTien, 0));
                    WebsiteQuanLyBanHang.Common.Common.SendMail("Shop Của Nguyễn Mạnh Đức", "Đơn hàng #" + order.Code, contentCustomer.ToString(), req.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/sendAdmin.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", req.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WebsiteQuanLyBanHang.Common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", WebsiteQuanLyBanHang.Common.Common.FormatNumber(tongTien, 0));
                    WebsiteQuanLyBanHang.Common.Common.SendMail("Shop Của Nguyễn Mạnh Đức", "Đơn hàng mới của bạn #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    cart.ClearCart();
                    //code = new { Success = true, Code = req.TypePayment, Url = "" };
                    ////var url = "";
                    //if (req.TypePayment == 2)
                    //{
                    //    var url = UrlPayment(req.TypePaymentVN, order.Code);
                    //    code = new { Success = true, Code = req.TypePayment, Url = url };
                    //}
                    return RedirectToAction("CheckoutSuccess");
                }
            }
            return Json(code);
        }

        [AllowAnonymous]
        public ActionResult PartialItemCart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        //[AllowAnonymous]
        //public ActionResult Checkout()
        //{
        //    ShoppingCart cart = (ShoppingCart)Session["Cart"];
        //    if (cart != null && cart.items.Any())
        //    {
        //        ViewBag.CheckCart = cart;
        //    }
        //    return View();
        //}

        [AllowAnonymous]
        public ActionResult Checkout()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;

            if (cart != null && cart.items.Any())
            {
                foreach (var item in cart.items)
                {
                    var productInDb = db.Products.FirstOrDefault(p => p.Id == item.ProductId);

                    if (productInDb == null)
                    {
                        var code = new
                        {
                            Success = false,
                            Mess = $"Sản phẩm \"{item.ProductName}\" không tồn tại trong kho.",
                            Code = 2,
                            Count = cart.items.Count
                        };
                        return Json(code, JsonRequestBehavior.AllowGet);
                    }

                    if (item.Quantity > productInDb.Quantity)
                    {
                        var code = new
                        {
                            Success = false,
                            Mess = $"Sản phẩm \"{item.ProductName}\" chỉ còn {productInDb.Quantity} sản phẩm trong kho.",
                            Code = 1,
                            Count = cart.items.Count
                        };
                        return Json(code, JsonRequestBehavior.AllowGet);
                    }
                }

                // Giỏ hàng hợp lệ => trả View để hiển thị trang thanh toán
                ViewBag.CheckCart = cart;
                return View();
            }
            else
            {
                // Giỏ hàng trống => trả JSON thông báo lỗi
                var code = new
                {
                    Success = false,
                    Mess = "Giỏ hàng trống.",
                    Code = -1,
                    Count = 0
                };
                return Json(code, JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public ActionResult CheckoutSuccess()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult PartialItemCheckout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult PartialCheckout()
        {
            var user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            if (user != null)
            {
                ViewBag.User = user;
            }
            return PartialView();
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public ActionResult Update(int id, int quantity)
        //{
        //    ShoppingCart cart = (ShoppingCart)Session["Cart"];
        //    if (cart != null)
        //    {
        //        cart.UpdateQuantity(id, quantity);
        //        return Json(new { Success = true });
        //    }
        //    return Json(new { Success = false });
        //}

        [AllowAnonymous]
        [HttpPost]
        public JsonResult UpdateQuantity(int id, int quantity)
        {
            var cart = Session["Cart"] as ShoppingCart;
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, Mess = "", Code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, Mess = "", Code = 1, Count = cart.items.Count };
                }
            }
            return Json(code);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart(); 
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
}