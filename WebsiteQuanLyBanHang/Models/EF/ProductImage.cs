using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_ProductImage")]
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string Alias { get; set; }
        public int ProductId { get; set; }
        [StringLength(500)]
        public string Image {  get; set; }
        public bool IsDefault { get; set; }

        public virtual Product Products { get; set; }
    }
}