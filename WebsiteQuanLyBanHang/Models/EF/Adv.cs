﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_Adv")]
    public class Adv : CommonAbstract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Alias { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(500)]
        public string Image {  get; set; }
        public int Type { get; set; }
        [StringLength(500)]
        public string Link { get; set; }

        public bool IsActive { get; set; }
    }
}