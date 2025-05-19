using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_Posts")]
    public class Posts : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Tiêu Đề Không Được Để Trống!")]
        [StringLength(500)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public int CategoryId { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyWords { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Categories { get; set; }
    }
}