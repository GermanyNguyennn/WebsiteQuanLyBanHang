using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace WebsiteQuanLyBanHang.Models.Common
{
    public class AccessStatistics
    {
        public static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static StatisticalViewModel Statistical()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<StatisticalViewModel>("Table_Statistical", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
    }
}