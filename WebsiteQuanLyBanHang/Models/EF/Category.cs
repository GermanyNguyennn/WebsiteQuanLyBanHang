using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_Category")]
    public class Category : CommonAbstract
    {
        public Category()
        {
            this.News = new HashSet<News>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Danh Mục Không Được Để Trống!")]
        [StringLength(50)]
        public string Title { get; set; }
        public string Alias { get; set; }
        [StringLength(500)]
        public string Link { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int Position { get; set; }
        [StringLength(50)]
        public string SeoTitle {  get; set; }
        [StringLength(50)]
        public string SeoDescription { get; set; }
        [StringLength(50)]
        public string SeoKeyWords { get; set; }
        public bool IsActive { get; set; }
        public ICollection<News> News { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}