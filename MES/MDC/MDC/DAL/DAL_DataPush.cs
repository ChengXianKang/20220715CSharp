using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace MDC.DAL
{
    public partial class DAL_DataPush
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        /// <summary>
        /// 获取TXD-HW标准故障代码对应表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,string> GetNCCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BNC_Type,BNC_cName,BNC_Note FROM Basal_NCCode ");
            strSql.Append("WHERE BNC_AddDate > '2019-05-05 00:00:00.000' ");
            DataTable dt = conn.ExecuteDataTable(strSql.ToString(), "Base");
            DataTable dtNCCode = dt.DefaultView.ToTable(true, new string[] { "BNC_cName", "BNC_Note" });

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRowView row in dtNCCode.DefaultView)
            {
                dic.Add(row["BNC_cName"].ToString(), row["BNC_Note"].ToString());
            }

            return dic;
        }
    }
}
