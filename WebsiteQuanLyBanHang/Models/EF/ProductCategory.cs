using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_ProductCategory")]
    public class ProductCategory : CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
            //this.ProductImages = new HashSet<ProductImage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Alias { get; set; }
        [Required(ErrorMessage = "Tên Tiêu Đề Không Được Để Trống!")]
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Icon { get; set; }
        [StringLength(50)]
        public string SeoTitle { get; set; }
        [StringLength(50)]
        public string SeoDescription { get; set; }
        [StringLength(50)]
        public string SeoKeyWords { get; set; }

        public ICollection<Product> Products { get; set; }
        //public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}