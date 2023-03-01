using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Utils;

namespace MDC.DAL
{
    public partial class DAL_TPL_Glass_Delivery
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public static bool Exists(string GD_SN)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT COUNT(1) FROM TPL_Glass_Delivery");
			strSql.Append(" WHERE GD_SN = @GD_SN ");
			SqlParameter[] parameters = 
            {
				new SqlParameter("@GD_SN", SqlDbType.VarChar,50)			
            };
			parameters[0].Value = GD_SN;

            object obj = conn.ExecuteScalar(strSql.ToString(), parameters, "Base");
            int count = YJ.Common.Utils.StrToInt(obj, 0);
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
		}

        /// <summary>
        /// 查询批次状态
        /// </summary>
        /// <param name="GD_SN">序列号</param>
        /// <returns>0：未发料， 1：已发料，2：已收料，3：已投料</returns>
        public static string GetState(string GD_SN)
        {
            string state = "未发料";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 GD_State FROM TPL_Glass_Delivery");
            strSql.Append(" WHERE GD_SN = @GD_SN ");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@GD_SN", SqlDbType.VarChar,50)			
            };
            parameters[0].Value = GD_SN;

            object obj = conn.ExecuteScalar(strSql.ToString(), parameters, "Base");
            if(obj!= null)
            {
                state = obj.ToString();
            }
            return state;
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public static bool Add(Utils.Model.TPL_Glass_Delivery model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO TPL_Glass_Delivery(");
            strSql.Append("GD_Tid,GD_SN,GD_ProductCode,GD_ProductModel,GD_ProductionOrder,GD_MaterialCount,GD_ProductionLine,GD_State,GD_DeliveryShop,GD_DeliveryLineCode,GD_DeliveryLine,GD_DeliveryOP,GD_DeliveryIP,GD_DeliveryTime,GD_CheckShop,GD_CheckOP,GD_CheckIP,GD_CheckTime,GD_ReceiveOP,GD_ReceiveTime,GD_ReceiveShop,GD_ReceiveLineCode,GD_ReceiveLine,GD_ReceiveDeviceIP,GD_ReceiveOrder)");
            strSql.Append(" VALUES (");
            strSql.Append("@GD_Tid,@GD_SN,@GD_ProductCode,@GD_ProductModel,@GD_ProductionOrder,@GD_MaterialCount,@GD_ProductionLine,@GD_State,@GD_DeliveryShop,@GD_DeliveryLineCode,@GD_DeliveryLine,@GD_DeliveryOP,@GD_DeliveryIP,@GD_DeliveryTime,@GD_CheckShop,@GD_CheckOP,@GD_CheckIP,@GD_CheckTime,@GD_ReceiveOP,@GD_ReceiveTime,@GD_ReceiveShop,@GD_ReceiveLineCode,@GD_ReceiveLine,@GD_ReceiveDeviceIP,@GD_ReceiveOrder)");
            SqlParameter[] parameters = {
					new SqlParameter("@GD_Tid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ProductCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ProductModel", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ProductionOrder", SqlDbType.VarChar,50),
					new SqlParameter("@GD_MaterialCount", SqlDbType.Int,4),
					new SqlParameter("@GD_ProductionLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_State", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryLineCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryTime", SqlDbType.DateTime),
					new SqlParameter("@GD_CheckShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckTime", SqlDbType.DateTime),
					new SqlParameter("@GD_ReceiveOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveTime", SqlDbType.DateTime),
					new SqlParameter("@GD_ReceiveShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveLineCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveDeviceIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveOrder", SqlDbType.VarChar,50)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.GD_SN;
            parameters[2].Value = model.GD_ProductCode;
            parameters[3].Value = model.GD_ProductModel;
            parameters[4].Value = model.GD_ProductionOrder;
            parameters[5].Value = model.GD_MaterialCount;
            parameters[6].Value = model.GD_ProductionLine;
            parameters[7].Value = model.GD_State;
            parameters[8].Value = model.GD_DeliveryShop;
            parameters[9].Value = model.GD_DeliveryLineCode;
            parameters[10].Value = model.GD_DeliveryLine;
            parameters[11].Value = model.GD_DeliveryOP;
            parameters[12].Value = model.GD_DeliveryIP;
            parameters[13].Value = model.GD_DeliveryTime;
            parameters[14].Value = model.GD_CheckShop;
            parameters[15].Value = model.GD_CheckOP;
            parameters[16].Value = model.GD_CheckIP;
            parameters[17].Value = model.GD_CheckTime;
            parameters[18].Value = model.GD_ReceiveOP;
            parameters[19].Value = model.GD_ReceiveTime;
            parameters[20].Value = model.GD_ReceiveShop;
            parameters[21].Value = model.GD_ReceiveLineCode;
            parameters[22].Value = model.GD_ReceiveLine;
            parameters[23].Value = model.GD_ReceiveDeviceIP;
            parameters[24].Value = model.GD_ReceiveOrder;

            int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public static bool Update(Utils.Model.TPL_Glass_Delivery model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TPL_Glass_Delivery set ");
            strSql.Append("GD_Tid=@GD_Tid,");
            strSql.Append("GD_ProductCode=@GD_ProductCode,");
            strSql.Append("GD_ProductModel=@GD_ProductModel,");
            strSql.Append("GD_ProductionOrder=@GD_ProductionOrder,");
            strSql.Append("GD_MaterialCount=@GD_MaterialCount,");
            strSql.Append("GD_ProductionLine=@GD_ProductionLine,");
            strSql.Append("GD_State=@GD_State,");
            strSql.Append("GD_DeliveryShop=@GD_DeliveryShop,");
            strSql.Append("GD_DeliveryLineCode=@GD_DeliveryLineCode,");
            strSql.Append("GD_DeliveryLine=@GD_DeliveryLine,");
            strSql.Append("GD_DeliveryOP=@GD_DeliveryOP,");
            strSql.Append("GD_DeliveryIP=@GD_DeliveryIP,");
            strSql.Append("GD_DeliveryTime=@GD_DeliveryTime,");
            strSql.Append("GD_CheckShop=@GD_CheckShop,");
            strSql.Append("GD_CheckOP=@GD_CheckOP,");
            strSql.Append("GD_CheckIP=@GD_CheckIP,");
            strSql.Append("GD_CheckTime=@GD_CheckTime,");
            strSql.Append("GD_ReceiveOP=@GD_ReceiveOP,");
            strSql.Append("GD_ReceiveTime=@GD_ReceiveTime,");
            strSql.Append("GD_ReceiveShop=@GD_ReceiveShop,");
            strSql.Append("GD_ReceiveLineCode=@GD_ReceiveLineCode,");
            strSql.Append("GD_ReceiveLine=@GD_ReceiveLine,");
            strSql.Append("GD_ReceiveDeviceIP=@GD_ReceiveDeviceIP,");
            strSql.Append("GD_ReceiveOrder=@GD_ReceiveOrder");
            strSql.Append(" where GD_SN=@GD_SN ");
            SqlParameter[] parameters = {
					new SqlParameter("@GD_Tid", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@GD_ProductCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ProductModel", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ProductionOrder", SqlDbType.VarChar,50),
					new SqlParameter("@GD_MaterialCount", SqlDbType.Int,4),
					new SqlParameter("@GD_ProductionLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_State", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryLineCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_DeliveryTime", SqlDbType.DateTime),
					new SqlParameter("@GD_CheckShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckTime", SqlDbType.DateTime),
					new SqlParameter("@GD_ReceiveOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveTime", SqlDbType.DateTime),
					new SqlParameter("@GD_ReceiveShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveLineCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveDeviceIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveOrder", SqlDbType.VarChar,50),
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50)};
            parameters[0].Value = model.GD_Tid;
            parameters[1].Value = model.GD_ProductCode;
            parameters[2].Value = model.GD_ProductModel;
            parameters[3].Value = model.GD_ProductionOrder;
            parameters[4].Value = model.GD_MaterialCount;
            parameters[5].Value = model.GD_ProductionLine;
            parameters[6].Value = model.GD_State;
            parameters[7].Value = model.GD_DeliveryShop;
            parameters[8].Value = model.GD_DeliveryLineCode;
            parameters[9].Value = model.GD_DeliveryLine;
            parameters[10].Value = model.GD_DeliveryOP;
            parameters[11].Value = model.GD_DeliveryIP;
            parameters[12].Value = model.GD_DeliveryTime;
            parameters[13].Value = model.GD_CheckShop;
            parameters[14].Value = model.GD_CheckOP;
            parameters[15].Value = model.GD_CheckIP;
            parameters[16].Value = model.GD_CheckTime;
            parameters[17].Value = model.GD_ReceiveOP;
            parameters[18].Value = model.GD_ReceiveTime;
            parameters[19].Value = model.GD_ReceiveShop;
            parameters[20].Value = model.GD_ReceiveLineCode;
            parameters[21].Value = model.GD_ReceiveLine;
            parameters[22].Value = model.GD_ReceiveDeviceIP;
            parameters[23].Value = model.GD_ReceiveOrder;
            parameters[24].Value = model.GD_SN;

            int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// 确认收料
        /// </summary>
        /// <param name="SN">序列号</param>
        /// <param name="CheckOP">收料人员工号</param>
        /// <returns></returns>
        public static bool CheckGlass(string SN, string CheckShop, string CheckOP, string CheckIP)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE TPL_Glass_Delivery SET ");
            strSql.Append("GD_State=@GD_State,");
            strSql.Append("GD_CheckShop=@GD_CheckShop,");
            strSql.Append("GD_CheckOP=@GD_CheckOP,");
            strSql.Append("GD_CheckIP=@GD_CheckIP,");
            strSql.Append("GD_CheckTime=@GD_CheckTime");
            strSql.Append(" WHERE GD_SN=@GD_SN ");
            SqlParameter[] parameters = {
					new SqlParameter("@GD_State", SqlDbType.NVarChar,10),
                    new SqlParameter("@GD_CheckShop", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckOP", SqlDbType.VarChar,50),
                    new SqlParameter("@GD_CheckIP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckTime", SqlDbType.DateTime),
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50)};
            parameters[0].Value = "已收料";
            parameters[1].Value = CheckShop;
            parameters[2].Value = CheckOP;
            parameters[3].Value = CheckIP;
            parameters[4].Value = BaseUI.GetServerTime();
            parameters[5].Value = SN;

            int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 确认收料并投料(针对漏确认收料做一次性处理)
        /// </summary>
        /// <param name="SN">序列号</param>
        /// <param name="ReceiveOP">投料人员工号</param>
        /// <param name="ReceiveLineCode">投料产线编码</param>
        /// <param name="ReceiveLine">投料产线名称</param>
        /// <param name="DeviceIP">投料设备IP</param>
        /// <returns></returns>
        public static bool CheckAndReceiveGlass(string SN, string ReceiveOP, string ReceiveShop, string ReceiveLineCode, string ReceiveLine, string DeviceIP, string Order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE TPL_Glass_Delivery SET ");
            strSql.Append("GD_State=@GD_State,");
            strSql.Append("GD_CheckShop=@GD_CheckShop,");
            strSql.Append("GD_CheckOP=@GD_CheckOP,");
            strSql.Append("GD_CheckTime=@GD_CheckTime,");
            strSql.Append("GD_ReceiveOP=@GD_ReceiveOP,");
            strSql.Append("GD_ReceiveTime=@GD_ReceiveTime,");
            strSql.Append("GD_ReceiveShop=@GD_ReceiveShop,");
            strSql.Append("GD_ReceiveLineCode=@GD_ReceiveLineCode,");
            strSql.Append("GD_ReceiveLine=@GD_ReceiveLine,");
            strSql.Append("GD_ReceiveDeviceIP=@GD_ReceiveDeviceIP,");
            strSql.Append("GD_ReceiveOrder=@GD_ReceiveOrder");
            strSql.Append(" WHERE GD_SN=@GD_SN ");
            SqlParameter[] parameters = {
					new SqlParameter("@GD_State", SqlDbType.NVarChar,10),
                    new SqlParameter("@GD_CheckShop", SqlDbType.VarChar,50),
                    new SqlParameter("@GD_CheckOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_CheckTime", SqlDbType.DateTime),
					new SqlParameter("@GD_ReceiveOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveTime", SqlDbType.DateTime),
                    new SqlParameter("@GD_ReceiveShop", SqlDbType.VarChar,50),
                    new SqlParameter("@GD_ReceiveLineCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveDeviceIP", SqlDbType.VarChar,50),
                    new SqlParameter("@GD_ReceiveOrder", SqlDbType.VarChar,50),
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50)};
            parameters[0].Value = "已投料";
            parameters[1].Value = ReceiveShop;
            parameters[2].Value = "";
            parameters[3].Value = BaseUI.GetServerTime();
            parameters[4].Value = ReceiveOP;
            parameters[5].Value = parameters[2].Value;
            parameters[6].Value = ReceiveShop;
            parameters[7].Value = ReceiveLineCode;
            parameters[8].Value = ReceiveLine;
            parameters[9].Value = DeviceIP;
            parameters[10].Value = Order;
            parameters[11].Value = SN;

            int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 机台投料
        /// </summary>
        /// <param name="SN">序列号</param>
        /// <param name="ReceiveOP">投料人员工号</param>
        /// <param name="ReceiveLine">投料产线名称</param>
        /// <param name="DeviceIP">投料设备IP</param>
        /// <returns></returns>
        public static bool ReceiveGlass(string SN, string ReceiveOP, string ReceiveShop, string ReceiveLineCode, string ReceiveLine, string DeviceIP, string Order)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE TPL_Glass_Delivery SET ");
            strSql.Append("GD_State=@GD_State,");
            strSql.Append("GD_ReceiveOP=@GD_ReceiveOP,");
            strSql.Append("GD_ReceiveTime=@GD_ReceiveTime,");
            strSql.Append("GD_ReceiveShop=@GD_ReceiveShop,");
            strSql.Append("GD_ReceiveLineCode=@GD_ReceiveLineCode,");
            strSql.Append("GD_ReceiveLine=@GD_ReceiveLine,");
            strSql.Append("GD_ReceiveDeviceIP=@GD_ReceiveDeviceIP,");
            strSql.Append("GD_ReceiveOrder=@GD_ReceiveOrder");
            strSql.Append(" WHERE GD_SN=@GD_SN ");
            SqlParameter[] parameters = {
					new SqlParameter("@GD_State", SqlDbType.NVarChar,10),
					new SqlParameter("@GD_ReceiveOP", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveTime", SqlDbType.DateTime),
                    new SqlParameter("@GD_ReceiveShop", SqlDbType.VarChar,50),
                    new SqlParameter("@GD_ReceiveLineCode", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveLine", SqlDbType.VarChar,50),
					new SqlParameter("@GD_ReceiveDeviceIP", SqlDbType.VarChar,50),
                    new SqlParameter("@GD_ReceiveOrder", SqlDbType.VarChar,50),
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50)};
            parameters[0].Value = "已投料";
            parameters[1].Value = ReceiveOP;
            parameters[2].Value = BaseUI.GetServerTime();
            parameters[3].Value = ReceiveShop;
            parameters[4].Value = ReceiveLineCode;
            parameters[5].Value = ReceiveLine;
            parameters[6].Value = DeviceIP;
            parameters[7].Value = Order;
            parameters[8].Value = SN;

            int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// 获取指定班次的投料数据
        ///// </summary>
        ///// <param name="TimeFrom">开始时间</param>
        ///// <param name="TimeTo">结束时间</param>
        ///// <param name="LineCode">产线编码列表</param>
        ///// <returns></returns>
        //public static DataTable GetDailyData(DateTime TimeFrom, DateTime TimeTo, string[] LineCode)
        //{
        //    string line = "'" + string.Join("','", LineCode) + "'";
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM TPL_Glass_Delivery ");
        //    strSql.Append("WHERE GD_DeliveryTime >= @TimeFrom AND GD_DeliveryTime <= @TimeTo ");
        //    strSql.Append("AND GD_DeliveryLineCode IN(" + line + ") OR GD_ReceiveLineCode IN(" + line + ") ");
        //    SqlParameter[] parameters = 
        //    {
        //        new SqlParameter("@TimeFrom", SqlDbType.DateTime),
        //        new SqlParameter("@TimeTo", SqlDbType.DateTime)	
        //    };
        //    parameters[0].Value = TimeFrom;
        //    parameters[1].Value = TimeTo;

        //    DataTable dt = conn.ExecuteDataTable(strSql.ToString(), parameters, "Base");
        //    return dt;
        //}

        /// <summary>
        /// 获取指定班次的投料数据
        /// </summary>
        /// <param name="TimeFrom">开始时间</param>
        /// <param name="TimeTo">结束时间</param>
        /// <returns></returns>
        public static DataTable GetDailyData(DateTime TimeFrom, DateTime TimeTo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM TPL_Glass_Delivery ");
            strSql.Append("WHERE GD_DeliveryTime >= @TimeFrom AND GD_DeliveryTime <= @TimeTo ");
            SqlParameter[] parameters = 
            {
                new SqlParameter("@TimeFrom", SqlDbType.DateTime),
                new SqlParameter("@TimeTo", SqlDbType.DateTime)	
            };
            parameters[0].Value = TimeFrom;
            parameters[1].Value = TimeTo;

            DataTable dt = conn.ExecuteDataTable(strSql.ToString(), parameters, "Base");
            return dt;
        }

        ///// <summary>
        ///// 获取历史投料数据
        ///// </summary>
        ///// <returns></returns>
        //public static DataTable GetDailyData()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM TPL_Glass_Delivery ");
        //    DataTable dt = conn.ExecuteDataTable(strSql.ToString(), "Base");
        //    return dt;
        //}


        /// <summary>
        /// 获取指定车间的投料数据
        /// </summary>
        /// <param name="TimeFrom">开始时间</param>
        /// <param name="TimeTo">结束时间</param>
        /// <param name="ShopName">车间名称</param>
        /// <returns></returns>
        public static DataTable GetDailyData(DateTime TimeFrom, DateTime TimeTo, string ShopName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM TPL_Glass_Delivery ");
            strSql.Append("WHERE GD_DeliveryTime >= @TimeFrom AND GD_DeliveryTime <= @TimeTo ");
            strSql.Append("AND (GD_DeliveryShop = '" + ShopName + "' OR GD_CheckShop = '" + ShopName + "' OR GD_ReceiveShop = '" + ShopName + "')");
            SqlParameter[] parameters = 
            {
				new SqlParameter("@TimeFrom", SqlDbType.DateTime),
			    new SqlParameter("@TimeTo", SqlDbType.DateTime)	
            };
            parameters[0].Value = TimeFrom;
            parameters[1].Value = TimeTo;

            DataTable dt = conn.ExecuteDataTable(strSql.ToString(), parameters, "Base");
            return dt;
        }

        /// <summary>
        /// 获取指定车间的历史投料数据
        /// </summary>
        /// <param name="ShopName">车间名称</param>
        /// <returns></returns>
        public static DataTable GetDailyData(string ShopName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM TPL_Glass_Delivery ");
            strSql.Append("WHERE GD_DeliveryShop = '" + ShopName + "' OR GD_CheckShop = '" + ShopName + "' OR GD_ReceiveShop = '" + ShopName + "'");
            DataTable dt = conn.ExecuteDataTable(strSql.ToString(), "Base");
            return dt;
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public static bool Delete(string GD_SN)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM TPL_Glass_Delivery ");
			strSql.Append(" WHERE GD_SN=@GD_SN ");
			SqlParameter[] parameters = {
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50)			};
			parameters[0].Value = GD_SN;

            int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
        public static bool DeleteList(string GD_SNlist)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("DELETE FROM TPL_Glass_Delivery ");
			strSql.Append(" WHERE GD_SN IN ("+GD_SNlist + ")  ");
			int rows = conn.ExecuteAction(strSql.ToString(), "Base");
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static Utils.Model.TPL_Glass_Delivery GetModel(string GD_SN)
		{
			
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 GD_Tid,GD_SN,GD_ProductCode,GD_ProductModel,GD_ProductionOrder,GD_MaterialCount,GD_ProductionLine,GD_State,GD_DeliveryShop,GD_DeliveryLineCode,GD_DeliveryLine,GD_DeliveryOP,GD_DeliveryIP,GD_DeliveryTime,GD_CheckShop,GD_CheckOP,GD_CheckIP,GD_CheckTime,GD_ReceiveOP,GD_ReceiveTime,GD_ReceiveShop,GD_ReceiveLineCode,GD_ReceiveLine,GD_ReceiveDeviceIP,GD_ReceiveOrder from TPL_Glass_Delivery ");
            strSql.Append(" WHERE GD_SN=@GD_SN ");
			SqlParameter[] parameters = {
					new SqlParameter("@GD_SN", SqlDbType.VarChar,50)			};
			parameters[0].Value = GD_SN;

			Utils.Model.TPL_Glass_Delivery model=new Utils.Model.TPL_Glass_Delivery();
            DataSet ds = conn.ExecuteDataSet(strSql.ToString(), parameters, "Base");
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static Utils.Model.TPL_Glass_Delivery DataRowToModel(DataRow row)
        {
            Utils.Model.TPL_Glass_Delivery model = new Utils.Model.TPL_Glass_Delivery();
            if (row != null)
            {
                if (row["GD_Tid"] != null && row["GD_Tid"].ToString() != "")
                {
                    model.GD_Tid = new Guid(row["GD_Tid"].ToString());
                }
                if (row["GD_SN"] != null)
                {
                    model.GD_SN = row["GD_SN"].ToString();
                }
                if (row["GD_ProductCode"] != null)
                {
                    model.GD_ProductCode = row["GD_ProductCode"].ToString();
                }
                if (row["GD_ProductModel"] != null)
                {
                    model.GD_ProductModel = row["GD_ProductModel"].ToString();
                }
                if (row["GD_ProductionOrder"] != null)
                {
                    model.GD_ProductionOrder = row["GD_ProductionOrder"].ToString();
                }
                if (row["GD_MaterialCount"] != null && row["GD_MaterialCount"].ToString() != "")
                {
                    model.GD_MaterialCount = int.Parse(row["GD_MaterialCount"].ToString());
                }
                if (row["GD_ProductionLine"] != null)
                {
                    model.GD_ProductionLine = row["GD_ProductionLine"].ToString();
                }
                if (row["GD_State"] != null)
                {
                    model.GD_State = row["GD_State"].ToString();
                }
                if (row["GD_DeliveryShop"] != null)
                {
                    model.GD_DeliveryShop = row["GD_DeliveryShop"].ToString();
                }
                if (row["GD_DeliveryLineCode"] != null)
                {
                    model.GD_DeliveryLineCode = row["GD_DeliveryLineCode"].ToString();
                }
                if (row["GD_DeliveryLine"] != null)
                {
                    model.GD_DeliveryLine = row["GD_DeliveryLine"].ToString();
                }
                if (row["GD_DeliveryOP"] != null)
                {
                    model.GD_DeliveryOP = row["GD_DeliveryOP"].ToString();
                }
                if (row["GD_DeliveryIP"] != null)
                {
                    model.GD_DeliveryIP = row["GD_DeliveryIP"].ToString();
                }
                if (row["GD_DeliveryTime"] != null && row["GD_DeliveryTime"].ToString() != "")
                {
                    model.GD_DeliveryTime = DateTime.Parse(row["GD_DeliveryTime"].ToString());
                }
                if (row["GD_CheckShop"] != null)
                {
                    model.GD_CheckShop = row["GD_CheckShop"].ToString();
                }
                if (row["GD_CheckOP"] != null)
                {
                    model.GD_CheckOP = row["GD_CheckOP"].ToString();
                }
                if (row["GD_CheckIP"] != null)
                {
                    model.GD_CheckIP = row["GD_CheckIP"].ToString();
                }
                if (row["GD_CheckTime"] != null && row["GD_CheckTime"].ToString() != "")
                {
                    model.GD_CheckTime = DateTime.Parse(row["GD_CheckTime"].ToString());
                }
                if (row["GD_ReceiveOP"] != null)
                {
                    model.GD_ReceiveOP = row["GD_ReceiveOP"].ToString();
                }
                if (row["GD_ReceiveTime"] != null && row["GD_ReceiveTime"].ToString() != "")
                {
                    model.GD_ReceiveTime = DateTime.Parse(row["GD_ReceiveTime"].ToString());
                }
                if (row["GD_ReceiveShop"] != null)
                {
                    model.GD_ReceiveShop = row["GD_ReceiveShop"].ToString();
                }
                if (row["GD_ReceiveLineCode"] != null)
                {
                    model.GD_ReceiveLineCode = row["GD_ReceiveLineCode"].ToString();
                }
                if (row["GD_ReceiveLine"] != null)
                {
                    model.GD_ReceiveLine = row["GD_ReceiveLine"].ToString();
                }
                if (row["GD_ReceiveDeviceIP"] != null)
                {
                    model.GD_ReceiveDeviceIP = row["GD_ReceiveDeviceIP"].ToString();
                }
                if (row["GD_ReceiveOrder"] != null)
                {
                    model.GD_ReceiveOrder = row["GD_ReceiveOrder"].ToString();
                }
            }
            return model;
        }

        #endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 获取当前所在的班次时间
        /// </summary>
        /// <param name="tNow">指定时间</param>
        /// <param name="ProductDay">生产日期</param>
        /// <param name="ClassType">班次</param>
        private static void GetClassTime(DateTime tNow, out string ProductDay, out string ClassType)
        {
            DateTime time1 = tNow.Date.Add(new TimeSpan(7, 30, 0));
            DateTime time2 = tNow.Date.Add(new TimeSpan(19, 30, 0));
            //DateTime time3 = tNow.Date.Add(new TimeSpan(-1, 19, 30, 0));
            //DateTime time4 = tNow.Date.Add(new TimeSpan(1, 7, 30, 0));
            if (tNow >= time1 && tNow < time2)//当天A班
            {
                ProductDay = tNow.ToString("yyyy-MM-dd");
                ClassType = "白班";
            }
            else if (tNow < time1)//前一天B班
            {
                ProductDay = tNow.Add(new TimeSpan(-1,0,0,0)).ToString("yyyy-MM-dd");
                ClassType = "夜班";
            }
            else//当天B班
            {
                ProductDay = tNow.ToString("yyyy-MM-dd");
                ClassType = "夜班";
            }
        }
//        /// <summary>
//        /// 更新指定工单投料数量，并获取投料数量和扫码数量
//        /// </summary>
//        /// <param name="Order">工单编码</param>
//        /// <param name="Count">本批次投料数量</param>
//        /// <param name="FeedNum">投料数量</param>
//        /// <param name="InputNum">扫码数量</param>
//        public static bool AddFeedNumber(string Order, int Count, out int FeedNum, out int InputNum)
//        {
//            string ProductDay;
//            string Banci;
//            GetClassTime(DateTime.Now, out ProductDay, out Banci);

//            // 查找产量数据表中是否存在此工单
//            string sql = @"IF NOT EXISTS (
//                SELECT SPP_SPOMJobCode,SPP_ProductDay,SPP_BanCi,SPP_Status,SPP_LCDCode,SPP_InPutNum,SPP_AddDate,SPP_OutPutNum,SPP_FeedNum 
//                FROM Store_ProduceOrder_Product 
//                WHERE SPP_SPOMJobCode = @SPP_SPOMJobCode AND SPP_ProductDay = @SPP_ProductDay AND SPP_BanCi = @SPP_BanCi)
//
//                    INSERT INTO Store_ProduceOrder_Product (
//                    SPP_SPOMJobCode,SPP_ProductDay,SPP_BanCi,SPP_Status,SPP_LCDCode,SPP_InPutNum,SPP_AddDate,SPP_OutPutNum,SPP_FeedNum)
//                    VALUES (
//                    @SPP_SPOMJobCode,@SPP_ProductDay,@SPP_BanCi,@SPP_Status,@SPP_LCDCode,@SPP_InPutNum,@SPP_AddDate,@SPP_OutPutNum,@SPP_FeedNum)
//
//                ELSE 
//
//                    UPDATE Store_ProduceOrder_Product SET 
//                    SPP_FeedNum = ISNULL(SPP_FeedNum,0) + @SPP_FeedNum 
//                    WHERE SPP_SPOMJobCode = @SPP_SPOMJobCode AND SPP_ProductDay = @SPP_ProductDay AND SPP_BanCi = @SPP_BanCi";

//            SqlParameter[] parameters = {
//                new SqlParameter("@SPP_SPOMJobCode", SqlDbType.VarChar,50),
//                new SqlParameter("@SPP_ProductDay", SqlDbType.VarChar,50),
//                new SqlParameter("@SPP_BanCi", SqlDbType.VarChar,50),
//                new SqlParameter("@SPP_Status", SqlDbType.VarChar,50),
//                new SqlParameter("@SPP_LCDCode", SqlDbType.VarChar,50),
//                new SqlParameter("@SPP_InPutNum", SqlDbType.Decimal,9),
//                new SqlParameter("@SPP_AddDate", SqlDbType.DateTime),
//                new SqlParameter("@SPP_OutPutNum", SqlDbType.Decimal,9),
//                new SqlParameter("@SPP_FeedNum", SqlDbType.Decimal,9)};
//            parameters[0].Value = Order;
//            parameters[1].Value = ProductDay;
//            parameters[2].Value = Banci;
//            parameters[3].Value = "000";
//            parameters[4].Value = "";
//            parameters[5].Value = 0;
//            parameters[6].Value = DateTime.Now;
//            parameters[7].Value = 0;
//            parameters[8].Value = Count;
//            int rows = conn.ExecuteAction(sql, parameters, "Base");

//            FeedNum = 0;
//            InputNum = 0;
//            string strSql = string.Format("SELECT SPP_FeedNum, SPP_InPutNum FROM Store_ProduceOrder_Product WHERE SPP_SPOMJobCode = '{0}'", Order);
//            DataView dv = conn.ExecuteDataView(strSql, "Base"); 
            
//            if (dv == null || dv.Count == 0)
//            {
//                FeedNum = 0;
//                InputNum = 0;
//            }
//            else
//            {
//                foreach (DataRowView row in dv)
//                {
//                    FeedNum += YJ.Common.Utils.StrToInt(row["SPP_FeedNum"], 0);
//                    InputNum += YJ.Common.Utils.StrToInt(row["SPP_InPutNum"], 0);
//                }
//            }

//            if (rows > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

        /// <summary>
        /// 更新指定工单投料数量，并获取投料数量和扫码数量
        /// </summary>
        /// <param name="order">工单编码</param>
        /// <param name="materialCode">物料编码</param>
        /// <param name="count">本次投料数</param>
        /// <param name="FeedNum">工单投料数</param>
        /// <param name="InputNum">清洗投入数</param>
        public static void AddFeedNumber(string order, string materialCode, int count, out int FeedNum, out int InputNum)
        {
            string sql = string.Format("EXEC TPL_Glass_Delivery_AddFeedNum '{0}', '{1}',{2}", order, materialCode, count);
            DataView dv = conn.ExecuteDataView(sql, "Base"); 
            
            if (dv == null || dv.Count == 0)
            {
                FeedNum = 0;
                InputNum = 0;
            }
            else
            {
                FeedNum = YJ.Common.Utils.StrToInt(dv[0]["FeedNum"], 0);
                InputNum = YJ.Common.Utils.StrToInt(dv[0]["InputNum"], 0);
            }
        }

        /// <summary>
        /// 检查物料编码是否属于本工单物料
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <param name="productOrder">工单编码</param>
        /// <returns></returns>
        public static bool MaterialCodeCheck(string materialCode, string productOrder)
        {
            string strSql = string.Format("SELECT COUNT(SPOS_SPOMTid) FROM Store_ProduceOrder_Sub INNER JOIN Store_ProduceOrder_Main ON SPOM_Tid = SPOS_SPOMTid WHERE SPOS_SMCode = '{0}' AND SPOM_JobCode = '{1}'", materialCode, productOrder);
            object obj = conn.ExecuteScalar(strSql, "Base");
            if (obj != null)
            {
                int n = (int)obj;
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
		#endregion  ExtensionMethod
    }
}
