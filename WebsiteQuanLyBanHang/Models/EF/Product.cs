using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_Product")]
    public class Product : CommonAbstract
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.ReviewProducts = new HashSet<ReviewProduct>();
            this.Wishlists = new HashSet<Wishlist>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Tiêu Đề Không Được Để Trống!")]
        [StringLength(50)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ProductCode { get; set; }
        public int ProductCategoryId { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyWords { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsNew {  get; set; }
        public bool IsHot { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }
        public virtual ProductCategory ProductCategories { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ReviewProduct> ReviewProducts { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}