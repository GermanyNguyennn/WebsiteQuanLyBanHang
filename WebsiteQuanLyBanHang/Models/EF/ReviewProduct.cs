﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_ReviewProduct")]
    public class ReviewProduct
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Avatar { get; set; }
        public virtual Product Products { get; set; }
    }
}