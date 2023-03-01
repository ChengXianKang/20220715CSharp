using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using Utils.HWDataPush.Entity;

namespace Utils.HWDataPush
{
    /// <summary>
    /// 数据访问类:HuaweiUpdate
    /// </summary>
    public partial class DAL_HuaweiUpdate
    {
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static DAL_HuaweiUpdate _instance;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _instanceLock = new object();
        /// <summary>
        /// MES数据库连接字符串，为了保证本类线程安全，不应当在任何非构造方法中修改该字符串
        /// </summary>
        private string _connStr;
        /// <summary>
        /// 封胶机错误编码
        /// </summary>
        private static List<string> FJ_NG_Code = new List<string>()
        {
            "没找到左Mark点",
            "没找到右Mark点",
            "没找到IC区域",
            "未点胶",
            "气泡",
            "缺胶",
            "IC溢胶",
            "CF溢胶",
            "找不到空白模板",
            "AOI平台拼图出错",
            "移除模板出错",
            "银胶面积过小",
            "银胶面积过大",
            "银胶偏位"
        };

        /// <summary>
        /// 获取当前的唯一实例
        /// </summary>
        public static DAL_HuaweiUpdate Current
        {
            get
            {
                //第一次检测实例是否已经存在，避免不必要的线程等待
                if (_instance == null)
                {
                    //锁住线程
                    lock (_instanceLock)
                    {
                        //第二次检测实例是否已经存在，避免通过了第一次检测的线程重复创建实例
                        if (_instance == null)
                        {
                            _instance = new DAL_HuaweiUpdate();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private DAL_HuaweiUpdate()
        {
            _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ToString();//从配置文件读取数据库连接字符串
        }

        /// <summary>
        /// 查询指定日期的上传历史记录表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataTable GetUpdateHistory(string date)
        {
            //实例化DataTable，用于装载查询结果集
            DataTable data = new DataTable();
            string t1 = date + " 00:00:00";
            string t2 = date + " 23:59:59";
            string sql = string.Format("SELECT * FROM TXD_HuaweiUpdate WHERE HW_UpdateTime >= '{0}' AND HW_UpdateTime <= '{1}' ORDER BY HW_UpdateTime DESC", t1, t2);
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        //指定CommandType
                        command.CommandType = CommandType.Text;
                        //实例化SqlDataAdapter
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        //填充DataTable
                        adapter.Fill(data);
                    }
                    catch (SqlException)
                    {
                        connection.Close();
                    }
                }
            }
            return data;
        }

        private DateTime lastTimeAir;
        /// <summary>
        /// 获取设备内洁净度参数并转存至待发送参数表
        /// </summary>
        /// <returns></returns>
        public int GetAirParticleTest()
        {
            using (IDbConnection conn = new SqlConnection(_connStr))
            {
                // 获取设备内洁净度参数
                List<AirParticleTest> lstAirParticle;
                if (lastTimeAir == new DateTime())
                {
                    // 查询最后获取的数据时间
                    object tObject = conn.ExecuteScalar("SELECT MAX(HPP_AddDate) FROM HB_Product_Parameter WHERE HPP_Parameter10 <> '--'");
                    if (tObject == null)
                    {
                        // 获取已发送记录中的最后数据测试时间戳
                        tObject = conn.ExecuteScalar("SELECT MAX(HW_QcTime) FROM TXD_HuaweiUpdate WHERE HW_TestItemName = '设备内洁净度'");
                        if (tObject == null) return 0;
                        // 将时间戳转换成时间
                        lastTimeAir = HuaweiDataSender.ConvertTimeStampToDateTime(tObject.ToString());
                        //string sql = "SELECT Id,LineNum,StationName,ParticleTwo,EventTime FROM View_AirParticleTest WHERE StationName IN ( 'COG', 'FOG', '背光装配①')";
                        //lstAirParticle = conn.Query<AirParticleTest>(sql).ToList();
                    }
                    else
                    {
                        lastTimeAir = Convert.ToDateTime(tObject);
                    }
                }
                string sql = "SELECT LineNum,StationName,ParticleTwo,EventTime FROM View_AirParticleTest WHERE StationName IN ( 'COG', 'FOG', '背光装配①') AND EventTime > @EventTime";
                lstAirParticle = conn.Query<AirParticleTest>(sql, new { EventTime = lastTimeAir }).ToList();

                if (lstAirParticle.Count == 0) return 0;
                // 待上传参数表
                List<Utils.Model.HB_Product_Parameter> lstPara = new List<Model.HB_Product_Parameter>();
                foreach (AirParticleTest item in lstAirParticle)
                {
                    if ((DateTime)item.EventTime > lastTimeAir)
                    {
                        lastTimeAir = (DateTime)item.EventTime;
                    }

                    // 待上传参数
                    Utils.Model.HB_Product_Parameter para = new Utils.Model.HB_Product_Parameter();
                    // GUID
                    para.HPP_Guid = Guid.NewGuid().ToString();
                    // 产线编码
                    para.HPP_SPLJobCode = item.LineNum.Substring(5, 2) + "01";
                    // 工序编码
                    switch (item.StationName)
                    {
                        case "COG":
                            para.HPP_SGXJobCode = "006";
                            break;
                        case "FOG":
                            para.HPP_SGXJobCode = "008";
                            break;
                        case "背光装配①":
                            para.HPP_SGXJobCode = "024";
                            break;
                    }
                    // 获取产线在制工单信息
                    string sqlProcess = string.Format(@"SELECT * FROM TPL_ProductProcess_GetIP_View WHERE SPL_JobCode = '{0}' AND SGX_JobCode = '{1}'", para.HPP_SPLJobCode, para.HPP_SGXJobCode);
                    List<TPL_ProductProcess> lstProductProcess = conn.Query<TPL_ProductProcess>(sqlProcess).ToList();
                    if (lstProductProcess == null || lstProductProcess.Count == 0)
                    {
                        continue;
                    }
                    // 工单编码
                    para.HPP_Code = lstProductProcess[0].SPOM_JobCode;
                    // 产品编码
                    para.HPP_SMCode = lstProductProcess[0].SPOM_SMCode;
                    // 成品型号
                    para.HPP_SMSpec = lstProductProcess[0].SPOM_SMSpec;
                    // 客户料号
                    para.HPP_CusCode = lstProductProcess[0].SPOM_CustomerCode;             
                    // 机台编码
                    para.HPP_DeviceCode = "";
                    // 设备IP
                    para.HPP_CardIP = lstProductProcess[0].STW_CardIP;
                    // 过站时间
                    para.HPP_AddDate = item.EventTime;
                    // 扫描IP
                    para.HPP_ScanIP = "";
                    // 生产日期
                    para.HPP_ProductDate = "";             
                    // 批次号
                    para.HPP_LotCode = "";                   
                    // 玻璃码
                    para.HPP_LCD = item.Id;                      
                    // FPC码
                    para.HPP_FPC = "";                        
                    // TP码
                    para.HPP_TP = "";                          
                    // 背光码
                    para.HPP_BL = "";                          
                    // 成品码
                    para.HPP_QR = "";                           
                    // 测试参数1-9
                    para.HPP_Parameter1 = "--";                                       
                    para.HPP_Parameter2 = "--";                                          
                    para.HPP_Parameter3 = "--";                                          
                    para.HPP_Parameter4 = "--";                                          
                    para.HPP_Parameter5 = "--";                                         
                    para.HPP_Parameter6 = "--";                                      
                    para.HPP_Parameter7 = "--";                                      
                    para.HPP_Parameter8 = "--";                                   
                    para.HPP_Parameter9 = "--";                            
                    // 测试参数(ParticleTwo)
                    para.HPP_Parameter10 = item.ParticleTwo.ToString();   
                    // 测试结果
                    para.HPP_TestResult = "PASS";                                        
                    para.HPP_IsSend = false;

                    lstPara.Add(para);
                }

                if (lstPara.Count == 0) return 0;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("INSERT INTO HB_Product_Parameter(");
                strSql.Append("HPP_Guid,HPP_Code,HPP_SMCode,HPP_SMSpec,HPP_CusCode,HPP_SPLJobCode,HPP_SGXJobCode,HPP_DeviceCode,HPP_CardIP,HPP_ScanIP,HPP_ProductDate,HPP_LotCode,HPP_AddDate,HPP_LCD,HPP_FPC,HPP_TP,HPP_BL,HPP_QR,HPP_Parameter1,HPP_Parameter2,HPP_Parameter3,HPP_Parameter4,HPP_Parameter5,HPP_Parameter6,HPP_Parameter7,HPP_Parameter8,HPP_Parameter9,HPP_Parameter10,HPP_IsSend,HPP_TestResult)");
                strSql.Append(" VALUES (");
                strSql.Append("@HPP_Guid,@HPP_Code,@HPP_SMCode,@HPP_SMSpec,@HPP_CusCode,@HPP_SPLJobCode,@HPP_SGXJobCode,@HPP_DeviceCode,@HPP_CardIP,@HPP_ScanIP,@HPP_ProductDate,@HPP_LotCode,@HPP_AddDate,@HPP_LCD,@HPP_FPC,@HPP_TP,@HPP_BL,@HPP_QR,@HPP_Parameter1,@HPP_Parameter2,@HPP_Parameter3,@HPP_Parameter4,@HPP_Parameter5,@HPP_Parameter6,@HPP_Parameter7,@HPP_Parameter8,@HPP_Parameter9,@HPP_Parameter10,@HPP_IsSend,@HPP_TestResult)");
                int count = conn.Execute(strSql.ToString(), lstPara);
                return count;
            }
        }

        private DateTime lastTimeGlue;
        /// <summary>
        /// 获取机台参数并转存至待发送参数表
        /// </summary>
        /// <returns></returns>
        public int GetGlueTest()
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(_connStr))
                {
                    // 获取机台参数
                    List<CDL_AppFrame_RcvMsgs> lstRcvMsgs;
                    if (lastTimeGlue == new DateTime())
                    {
                        // 获取最后发送时间
                        object tObject = conn.ExecuteScalar("SELECT MAX(HPP_AddDate) FROM HB_Product_Parameter WHERE HPP_SGXJobCode IN ('080', '081', '082') ");
                        if (tObject == null)
                        {
                            // 获取已发送记录中的最后数据测试时间戳
                            tObject = conn.ExecuteScalar("SELECT MAX(HW_QcTime) FROM TXD_HuaweiUpdate WHERE HW_TestStation IN('点胶外观检测', 'Panel切割', 'Panel 电测')");
                            if (tObject == null) return 0;
                            // 将时间戳转换成时间
                            lastTimeGlue = HuaweiDataSender.ConvertTimeStampToDateTime(tObject.ToString());
                        }
                        else
                        {
                            lastTimeGlue = Convert.ToDateTime(tObject);
                        }
                    }
                    //lastTime = new DateTime(2019, 7, 9, 7, 30, 0);
                    string sql = "SELECT EventTime,MsgData FROM View_CDL_AppFrame WHERE EventTime > @EventTime";
                    lstRcvMsgs = conn.Query<CDL_AppFrame_RcvMsgs>(sql, new { EventTime = lastTimeGlue }).ToList();

                    if (lstRcvMsgs.Count == 0) return 0;
                    // 待上传参数表
                    List<Utils.Model.HB_Product_Parameter> lstPara = new List<Model.HB_Product_Parameter>();
                    foreach (CDL_AppFrame_RcvMsgs item in lstRcvMsgs)
                    {
                        if ((DateTime)item.EventTime > lastTimeGlue)
                        {
                            lastTimeGlue = (DateTime)item.EventTime;
                        }

                        try
                        {
                            // 解析数据字符串
                            string[] data = item.MsgData.Replace("{","").Replace("}","").Split(';');
                            // 过滤无效长度数据
                            if (data.Length < 10) continue;

                            // 新建待上传参数
                            Utils.Model.HB_Product_Parameter para = new Utils.Model.HB_Product_Parameter();

                            // GUID
                            para.HPP_Guid = Guid.NewGuid().ToString();
                            // 产线编码
                            para.HPP_SPLJobCode = data[2];
                            // 过站时间
                            para.HPP_AddDate = item.EventTime;
                            // 设备IP
                            para.HPP_CardIP = data[3];
                            // 玻璃码
                            para.HPP_LCD = data[6];
                            // 生产日期
                            para.HPP_ProductDate = DateTime.ParseExact(data[9], "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.None).ToString();
                            // 机台编码
                            para.HPP_DeviceCode = "";
                            // 扫描IP
                            para.HPP_ScanIP = "";
                            // 批次号
                            para.HPP_LotCode = "";
                            // FPC码
                            para.HPP_FPC = "";
                            // TP码
                            para.HPP_TP = "";
                            // 背光码
                            para.HPP_BL = "";
                            // 成品码
                            para.HPP_QR = "";
                            // 已发送
                            para.HPP_IsSend = false;

                            // 测试状态（0：OK，1：NG）
                            string state = data[7];
                            // 测试结果
                            para.HPP_TestResult = state == "0" ? "PASS" : "FAIL";
                            
                            // 测试项目
                            if (data[0] == "JT" && data[1] == "FJ")// 点胶机参数
                            {
                                #region 点胶机参数
                                // 工序编码
                                para.HPP_SGXJobCode = "080";
                                // 获取产线在制工单信息
                                string sqlProcess = string.Format(@"SELECT * FROM TPL_ProductProcess_GetIP_View WHERE SPL_JobCode = '{0}'", para.HPP_SPLJobCode);
                                List<TPL_ProductProcess> lstProductProcess = conn.Query<TPL_ProductProcess>(sqlProcess).ToList();
                                if (lstProductProcess == null || lstProductProcess.Count == 0)
                                {
                                    continue;
                                }
                                // 工单编码
                                para.HPP_Code = lstProductProcess[0].SPOM_JobCode;
                                // 产品编码
                                para.HPP_SMCode = lstProductProcess[0].SPOM_SMCode;
                                // 成品型号
                                para.HPP_SMSpec = lstProductProcess[0].SPOM_SMSpec;
                                // 客户料号
                                para.HPP_CusCode = lstProductProcess[0].SPOM_CustomerCode;

                                // 测试参数
                                switch (state)
                                {
                                    case "0":
                                        para.HPP_Parameter2 = "--";
                                        para.HPP_Parameter3 = "--";
                                        break;

                                    case "1":
                                        // 测试值
                                        string testResult = data[8];
                                        if (string.IsNullOrEmpty(testResult)) continue;
                                        int NG_Number = Convert.ToInt32(testResult.Replace("NG_", ""));
                                        if (NG_Number <= 11)
                                        {
                                            para.HPP_Parameter2 = FJ_NG_Code[NG_Number - 1];
                                            para.HPP_Parameter3 = "--";
                                        }
                                        else if (NG_Number <= FJ_NG_Code.Count)
                                        {
                                            para.HPP_Parameter2 = "--";
                                            para.HPP_Parameter3 = FJ_NG_Code[NG_Number - 1];
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                        break;
                                }

                                // 测试参数4-10
                                para.HPP_Parameter1 = "--";
                                para.HPP_Parameter4 = "--";
                                para.HPP_Parameter5 = "--";
                                para.HPP_Parameter6 = "--";
                                para.HPP_Parameter7 = "--";
                                para.HPP_Parameter8 = "--";
                                para.HPP_Parameter9 = "--";
                                para.HPP_Parameter10 = "--";
                                #endregion 点胶机参数
                            }
                            else if (data[0] == "HY" && data[1] == "CELL")// Panel 电测
                            {
                                #region Panel 电测
                                // 工序编码
                                para.HPP_SGXJobCode = "082";

                                // 获取CELL机台生产信息表，用于匹配获取华为编码、客户型号、供应商编码
                                string sqlInfo = string.Format(@"SELECT * FROM TXD_Cell_MachineInfo WHERE TCM_DeviceIP = '{0}'", para.HPP_CardIP);
                                List<Cell_MachineInfo> lstMachine = conn.Query<Cell_MachineInfo>(sqlInfo).ToList();
                                if (lstMachine == null || lstMachine.Count == 0)
                                {
                                    continue;
                                }
                                // 工单编码
                                para.HPP_Code = "";
                                // 产品编码
                                para.HPP_SMCode = lstMachine[0].TCM_ProductNumber;
                                // 成品型号
                                para.HPP_SMSpec = lstMachine[0].TCM_CustomerModel;
                                // 客户料号
                                para.HPP_CusCode = lstMachine[0].TCM_CuntomerNumber;

                                // 测试参数
                                switch (state)
                                {
                                    case "0":
                                        para.HPP_Parameter1 = "--";
                                        break;

                                    case "1":
                                        // 测试值
                                        string testResult = data[8];
                                        if (string.IsNullOrEmpty(testResult)) continue;
                                        para.HPP_Parameter1 = testResult.Split('|')[0];//不良代码，多个不良代码取第一个，发华为前转换为“华为标准故障代码”
                                        break;
                                }

                                // 测试参数2-10
                                para.HPP_Parameter2 = "--";
                                para.HPP_Parameter3 = "--";
                                para.HPP_Parameter4 = "--";
                                para.HPP_Parameter5 = "--";
                                para.HPP_Parameter6 = "--";
                                para.HPP_Parameter7 = "--";
                                para.HPP_Parameter8 = "--";
                                para.HPP_Parameter9 = "--";
                                para.HPP_Parameter10 = "--";
                                #endregion Panel 电测
                            }
                            else if (data[0] == "HANS" && data[1] == "HANS_CUT_AOI")// Panel切割
                            {
                                #region Panel切割
                                // 工序编码
                                para.HPP_SGXJobCode = "081";

                                // 获取CELL机台生产信息表，用于匹配获取华为编码、客户型号、供应商编码
                                string sqlInfo = string.Format(@"SELECT * FROM TXD_Cell_MachineInfo WHERE TCM_DeviceIP = '{0}'", para.HPP_CardIP);
                                List<Cell_MachineInfo> lstMachine = conn.Query<Cell_MachineInfo>(sqlInfo).ToList();
                                if (lstMachine == null || lstMachine.Count == 0)
                                {
                                    continue;
                                }
                                // 工单编码
                                para.HPP_Code = "";
                                // 产品编码
                                para.HPP_SMCode = lstMachine[0].TCM_ProductNumber;
                                // 成品型号
                                para.HPP_SMSpec = lstMachine[0].TCM_CustomerModel;
                                // 客户料号
                                para.HPP_CusCode = lstMachine[0].TCM_CuntomerNumber;

                                // 测试参数
                                // "Panel外观AOI"检测结果
                                switch (state)
                                {
                                    case "0":
                                        para.HPP_Parameter1 = "--";
                                        break;

                                    case "1":
                                        // 测试值
                                        string testResult = data[8];
                                        if (string.IsNullOrEmpty(testResult)) continue;
                                        para.HPP_Parameter1 = testResult == "#"? "Cell薄化外观不良":testResult.Split('|')[0];//不良代码，多个不良代码取第一个，发华为前转换为“华为标准故障代码”
                                        break;
                                }

                                // 切割精度量测试项
                                string[] result = data[10].Split('|');
                                Dictionary<string, string> parameter = new Dictionary<string, string>();
                                foreach (string rst in result)
                                {
                                    string[] r = rst.Split(':');
                                    parameter.Add(r[0], r[1]);
                                }

                                // 测试参数2-10 RL_X:975.413|RL_Y:-1|LU_X:4542.589|LU_Y:-1|RU_X:-1|RU_Y:-1|LL_X:-1|C_B1_B2:91|C_B3_B4:93|R_D1_D2:127|R_D3_D4:86|Notch_E1_E5:136|Notch_E2_E4:125|Notch_E3:118|Chipping_1:-1|Chipping_2:-1
                                para.HPP_Parameter2 = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", parameter["RL_X"], parameter["RL_Y"], parameter["LU_X"], parameter["LU_Y"], parameter["RU_X"], parameter["RU_Y"], parameter["LL_X"]);
                                para.HPP_Parameter3 = string.Format("{0}|{1}", parameter["C_B1_B2"], parameter["C_B3_B4"]);
                                para.HPP_Parameter4 = string.Format("{0}|{1}", parameter["R_D1_D2"], parameter["R_D3_D4"]);
                                para.HPP_Parameter5 = string.Format("{0}|{1}|{2}", parameter["Notch_E1_E5"], parameter["Notch_E2_E4"], parameter["Notch_E3"]);
                                para.HPP_Parameter6 = string.Format("{0}|{1}", parameter["Chipping_1"], parameter["Chipping_2"]);
                                para.HPP_Parameter7 = "--";
                                para.HPP_Parameter8 = "--";
                                para.HPP_Parameter9 = "--";
                                para.HPP_Parameter10 = "--";
                                #endregion Panel切割
                            }
                            else
                            {
                                continue;
                            }

                            lstPara.Add(para);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("View_CDL_AppFrame机台检测数据无效.", ex);
                            continue;
                        }
                    }

                    if (lstPara.Count == 0) return 0;

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO HB_Product_Parameter(");
                    strSql.Append("HPP_Guid,HPP_Code,HPP_SMCode,HPP_SMSpec,HPP_CusCode,HPP_SPLJobCode,HPP_SGXJobCode,HPP_DeviceCode,HPP_CardIP,HPP_ScanIP,HPP_ProductDate,HPP_LotCode,HPP_AddDate,HPP_LCD,HPP_FPC,HPP_TP,HPP_BL,HPP_QR,HPP_Parameter1,HPP_Parameter2,HPP_Parameter3,HPP_Parameter4,HPP_Parameter5,HPP_Parameter6,HPP_Parameter7,HPP_Parameter8,HPP_Parameter9,HPP_Parameter10,HPP_IsSend,HPP_TestResult)");
                    strSql.Append(" VALUES (");
                    strSql.Append("@HPP_Guid,@HPP_Code,@HPP_SMCode,@HPP_SMSpec,@HPP_CusCode,@HPP_SPLJobCode,@HPP_SGXJobCode,@HPP_DeviceCode,@HPP_CardIP,@HPP_ScanIP,@HPP_ProductDate,@HPP_LotCode,@HPP_AddDate,@HPP_LCD,@HPP_FPC,@HPP_TP,@HPP_BL,@HPP_QR,@HPP_Parameter1,@HPP_Parameter2,@HPP_Parameter3,@HPP_Parameter4,@HPP_Parameter5,@HPP_Parameter6,@HPP_Parameter7,@HPP_Parameter8,@HPP_Parameter9,@HPP_Parameter10,@HPP_IsSend,@HPP_TestResult)");
                    int count = conn.Execute(strSql.ToString(), lstPara);
                    return count;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取机台参数失败", ex);
                return 0;
            }
        }

        private DateTime lastTimeProcessFail;
        /// <summary>
        /// 获取复判不良玻璃数据并转存至待发送参数表
        /// </summary>
        /// <returns></returns>
        public int GetProcessFail()
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(_connStr))
                {
                    // 获取机台参数
                    List<ProcessFail> lstProcessFail;
                    if (lastTimeProcessFail == new DateTime())
                    {
                        // 获取最后发送时间
                        object tObject = conn.ExecuteScalar("SELECT MAX(HPP_AddDate) FROM HB_Product_Parameter WHERE HPP_SGXJobCode IN ('011', '031', '034') AND HPP_TestResult = 'FAIL' AND HPP_Parameter1 NOT IN('#','--','')");
                        if (tObject == null)
                        {
                            // 获取已发送记录中的最后数据测试时间戳
                            tObject = conn.ExecuteScalar("SELECT MAX(HW_QcTime) FROM TXD_HuaweiUpdate WHERE HW_TestStation IN ('CFOG后ET电测','组装后ET电测','组装外观检测') AND HW_TestCharResult = 'FAIL'");
                            if (tObject == null) return 0;
                            // 将时间戳转换成时间
                            lastTimeProcessFail = HuaweiDataSender.ConvertTimeStampToDateTime(tObject.ToString());
                        }
                        else
                        {
                            lastTimeProcessFail = Convert.ToDateTime(tObject);
                        }
                    }
                    //lastTime = new DateTime(2019, 7, 9, 7, 30, 0);
                    string sql = "SELECT TFM_Tid,TFM_Status,TFM_Type,TFM_Describe,TFM_ProductOrder,SPOM_SMCode,SPOM_SMSpec,SPOM_CustomerCode,TFM_LineCode,TFM_ProcessCode,TFM_DeviceIP,TFM_GlassCode,TFM_ScanTime,TFM_CheckDate FROM TPL_ProcessFail_Main LEFT JOIN Store_ProduceOrder_Main ON TFM_ProductOrder = SPOM_JobCode WHERE TFM_CheckDate > @TFM_CheckDate AND TFM_Type IN ('报废','重工') ORDER BY TFM_CheckDate DESC;";
                    lstProcessFail = conn.Query<ProcessFail>(sql, new { TFM_CheckDate = lastTimeProcessFail }).ToList();

                    if (lstProcessFail.Count == 0) return 0;
                    // 待上传参数表
                    List<Utils.Model.HB_Product_Parameter> lstPara = new List<Model.HB_Product_Parameter>();
                    foreach (ProcessFail item in lstProcessFail)
                    {
                        if ((DateTime)item.TFM_CheckDate > lastTimeProcessFail)
                        {
                            lastTimeProcessFail = (DateTime)item.TFM_CheckDate;
                        }

                        try
                        {
                            // 将不良参数添加到华为数据上传参数表HB_Product_Parameter
                            Utils.Model.HB_Product_Parameter para = new Utils.Model.HB_Product_Parameter()
                            {
                                HPP_Guid = Guid.NewGuid().ToString(),
                                HPP_Code = item.TFM_ProductOrder,                                                           //工单编码
                                HPP_SMCode = item.SPOM_SMCode,                                                               //产品编码
                                HPP_SMSpec = item.SPOM_SMSpec,                                                               //成品型号
                                HPP_CusCode = item.SPOM_CustomerCode,                                                             //客户料号
                                HPP_SPLJobCode = item.TFM_LineCode,                                                         //产线编码
                                HPP_SGXJobCode = item.TFM_ProcessCode,                                                      //工序编码
                                HPP_DeviceCode = "",                                                                    //机台编码
                                HPP_CardIP = item.TFM_DeviceIP,                                                             //设备IP
                                HPP_ScanIP = "",                                                                        //扫描IP
                                HPP_ProductDate = ((DateTime)item.TFM_ScanTime).ToString("yyyy-MM-dd HH:mm:ss"),            //生产日期
                                HPP_LotCode = "",                                                                       //批次号
                                HPP_AddDate = item.TFM_CheckDate,                                                           //过站时间
                                HPP_LCD = item.TFM_GlassCode,                                                               //玻璃码
                                HPP_FPC = "",                                                                           //FPC码
                                HPP_TP = "",                                                                            //TP码
                                HPP_BL = "",                                                                            //背光码
                                HPP_QR = "",                                                                            //成品码
                                HPP_Parameter1 = item.TFM_Describe.Split('|').Length > 0 ? item.TFM_Describe.Split('|')[0]:"--",//测试参数
                                HPP_Parameter2 = "--",                                                                  //测试参数
                                HPP_Parameter3 = "--",                                                                  //测试参数
                                HPP_Parameter4 = "--",                                                                  //测试参数
                                HPP_Parameter5 = "--",                                                                  //测试参数
                                HPP_Parameter6 = "--",                                                                  //测试参数
                                HPP_Parameter7 = "--",                                                                  //测试参数
                                HPP_Parameter8 = "--",                                                                  //测试参数
                                HPP_Parameter9 = "--",                                                                  //测试参数
                                HPP_Parameter10 = "--",                                                                 //测试参数
                                HPP_TestResult = "FAIL",                                                                //测试结果
                                HPP_IsSend = false
                            };

                            lstPara.Add(para);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("ProcessFail数据无效.", ex);
                            continue;
                        }
                    }

                    if (lstPara.Count == 0) return 0;

                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO HB_Product_Parameter(");
                    strSql.Append("HPP_Guid,HPP_Code,HPP_SMCode,HPP_SMSpec,HPP_CusCode,HPP_SPLJobCode,HPP_SGXJobCode,HPP_DeviceCode,HPP_CardIP,HPP_ScanIP,HPP_ProductDate,HPP_LotCode,HPP_AddDate,HPP_LCD,HPP_FPC,HPP_TP,HPP_BL,HPP_QR,HPP_Parameter1,HPP_Parameter2,HPP_Parameter3,HPP_Parameter4,HPP_Parameter5,HPP_Parameter6,HPP_Parameter7,HPP_Parameter8,HPP_Parameter9,HPP_Parameter10,HPP_IsSend,HPP_TestResult)");
                    strSql.Append(" VALUES (");
                    strSql.Append("@HPP_Guid,@HPP_Code,@HPP_SMCode,@HPP_SMSpec,@HPP_CusCode,@HPP_SPLJobCode,@HPP_SGXJobCode,@HPP_DeviceCode,@HPP_CardIP,@HPP_ScanIP,@HPP_ProductDate,@HPP_LotCode,@HPP_AddDate,@HPP_LCD,@HPP_FPC,@HPP_TP,@HPP_BL,@HPP_QR,@HPP_Parameter1,@HPP_Parameter2,@HPP_Parameter3,@HPP_Parameter4,@HPP_Parameter5,@HPP_Parameter6,@HPP_Parameter7,@HPP_Parameter8,@HPP_Parameter9,@HPP_Parameter10,@HPP_IsSend,@HPP_TestResult)");
                    int count = conn.Execute(strSql.ToString(), lstPara);
                    return count;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取不良玻璃数据失败", ex);
                return 0;
            }
        }
    }
}
