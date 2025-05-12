using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyBanHang.Models
{
    public class RevenueStatisticViewModel
    {
        public DateTime Date { get; set; }
        public decimal Benefit { get; set; }
        public decimal Revenues { get; set; } 
    }
}