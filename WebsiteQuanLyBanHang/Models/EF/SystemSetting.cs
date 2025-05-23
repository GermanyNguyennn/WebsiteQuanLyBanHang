﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models.EF
{
    [Table("Table_SystemSetting")]
    public class SystemSetting
    {
        [Key]
        [StringLength(50)]
        public string SettingKey { get; set; }
        [StringLength(100)]
        public string SettingValue { get; set; }
        [StringLength(100)]
        public string SettingDescription { get; set; }
        [StringLength(50)]
        public string Alias { get; set; }
    }
}