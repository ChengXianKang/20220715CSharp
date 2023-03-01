using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using Utils;
using Utils.Model;
using Dapper;

namespace MDC
{
    public class BaseUI
    {
        #region 字段
        /// <summary>
        /// 系统版本号
        /// </summary>
        public static string VERNNAME;
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UI_BOOPID;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UI_BOOPNAME;
        /// <summary>
        /// 账号
        /// </summary>
        public static string UI_BOOLOGNAME;
        /// <summary>
        /// 登录日期
        /// </summary>
        public static string UI_BOOLOGDATE;
        /// <summary>
        /// 人员权限Dictionary(rcode, note)
        /// </summary>
        public static Dictionary<string, string> UI_RIGHT;
        /// <summary>
        /// 禁止补扫的工序编码数组
        /// </summary>
        public static List<string> UI_NoAccessProcess;
        /// <summary>
        /// 限定允许补扫的工序编码数组
        /// </summary>
        public static List<string> UI_OnlyAccessProcess;
        /// <summary>
        /// 允许补扫的工序编码列表
        /// </summary>
        public static List<string> UI_AccessProcess;
        /// <summary>
        /// 产线ID
        /// </summary>
        public static int UI_SPLID;
        /// <summary>
        /// 产线编码
        /// </summary>
        public static string UI_SPLJOBCODE;
        /// <summary>
        /// 产线名称
        /// </summary>
        public static string UI_SPLNAME;
        /// <summary>
        /// 上报服务器端口号
        /// </summary>
        public static string UI_SPLPORT;
        /// <summary>
        /// 上报服务器IP地址
        /// </summary>
        public static string UI_SPLIPAddress;
        /// <summary>
        /// 工序ID
        /// </summary>
        public static int UI_SGXID;
        /// <summary>
        /// 工序序号
        /// </summary>
        public static string UI_SGXNO;
        /// <summary>
        /// 工序编码
        /// </summary>
        public static string UI_SGXJOBCODE;
        /// <summary>
        /// 工序名称
        /// </summary>
        public static string UI_SGXNAME;
        /// <summary>
        /// 是否关键工序
        /// </summary>
        public static bool UI_IsBind;
        /// <summary>
        /// 是否允许返检工序
        /// </summary>
        public static bool UI_SGXRework;
        /// <summary>
        /// 是否TP有码
        /// </summary>
        public static bool UI_IsTP;
        ///// <summary>
        ///// 是否允许跨线生产
        ///// </summary>
        //public static bool UI_IsMixedLine;
        /// <summary>
        /// 成品型号编码
        /// </summary>
        public static string UI_SMCODE;
        /// <summary>
        /// 成品规格
        /// </summary>
        public static string UI_SMSPEC;
        /// <summary>
        /// 工单ID
        /// </summary>
        public static int UI_SPOMID;
        /// <summary>
        /// 工单编码
        /// </summary>
        public static string UI_SPOMJobCode;
        /// <summary>
        /// 当前工序编码
        /// </summary>
        public static string UI_CurrentProcedureCode;
        /// <summary>
        /// 当前工序序号
        /// </summary>
        public static string UI_CurrentProcedureNo;
        /// <summary>
        /// 当前工序名称
        /// </summary>
        public static string UI_CurrentProcedureName;
        /// <summary>
        /// 当前站点实际IP地址（针对补扫站点）
        /// </summary>
        public static string UI_EquCardIP;
        /// <summary>
        /// 当前站点设备编码
        /// </summary>
        public static string UI_EquDeviceCode;
        /// <summary>
        /// 设备ID
        /// </summary>
        public static int UI_DeviceTid;
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 数据库类型名称
        /// </summary>
        private static string DBName = "Base";
        /// <summary>
        /// 程序配置文件操作类
        /// </summary>
        private static AppConfig appConfig = new AppConfig();
        #endregion 字段

        #region 判断按钮权限
        /// <summary>
        /// 判断功能操作权限
        /// </summary>
        /// <param name="Rcode">权限编码</param>
        /// <param name="Pid">人员ID</param>
        /// <returns></returns>
        public static bool CheckRight(string Rcode, string Pid)
        {
            try
            {
                string sqlString = string.Format("SELECT * FROM Base_Op_Right WHERE BOR_RCode='{0}' AND BOR_Pid='{1}'", Rcode, Pid);
                DataView sqlView = BaseUI.conn.ExecuteDataView(sqlString, BaseUI.DBName);
                if (sqlView.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获取人员权限
        /// <summary>
        /// 获取指定用户的四楼工控机权限列表
        /// </summary>
        /// <param name="Pid">用户id</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> GetRight(string Pid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                string sql = "select rcode, note from VW_Base_Op_Right_IPC where pid = '" + Pid + "' ORDER BY rcode";
                DataView dv = BaseUI.conn.ExecuteDataView(sql, BaseUI.DBName);
                foreach (DataRowView row in dv)
                {
                    dic.Add(row["rcode"].ToString(), row["note"].ToString());
                }
            }
            catch
            {
            }
            return dic;
        }

        /// <summary>
        /// 获取补扫工序权限
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        public static List<string> GetRescanRight(string Pid)
        {
            List<string> lst = new List<string>();
            try
            {
                string sql = string.Format(@"SELECT rcode, note FROM VW_Base_Op_Right_IPC 
                                                            WHERE rcode LIKE '021203%' 
                                                            AND rcode NOT IN('021203','02120301','02120302','02120303','02120304')
                                                            AND pid = '{0}' 
                                                            ORDER BY rcode", Pid);
                DataTable dt = BaseUI.conn.ExecuteDataTable(sql, BaseUI.DBName);
                lst = dt.AsEnumerable().Select(d => d.Field<string>("note")).ToList();
            }
            catch
            {
            }
            return lst;
        }
        #endregion

        #region 本机IP获取
        /// <summary>
        /// 获取本机IPv4地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIP()
        {
            string localIp = "";
            try
            {
                if (appConfig.ContainsKey("LocalIP"))
                {
                    localIp = appConfig.GetConfigString("LocalIP");
                    if (!string.IsNullOrWhiteSpace(localIp))
                    {
                        return localIp;
                    }
                }
                //获取本地IP地址
                string name = Dns.GetHostName();
                IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
                foreach (IPAddress ipa in ipadrlist)
                {
                    if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIp = ipa.ToString();
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("本机IP获取失败。" + exp.ToString(), exp);
            }
            return localIp;
        }
        #endregion

        #region 判断当前登录账号是否存在
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LoginState Login(string account, string password)
        {
            LoginState state = LoginState.服务器异常;
            try
            {
                string sql = string.Format(@"SELECT * FROM Base_Op_Operator
                                             WHERE BOO_LogName = '{0}'", account);
                DataTable tb = conn.ExecuteDataTable(sql, "Base");
                if (tb != null && tb.Rows.Count > 0)
                {
                    DataRow rowuser = tb.Rows[0];
                    string name = rowuser["BOO_Pname"].ToString();
                    string psw = rowuser["BOO_PassWord"].ToString();
                    if (YJ.Common.AES.Encode(password, "yj888128") == psw)
                    {
                        //获取人员权限
                        BaseUI.UI_RIGHT = GetRight(rowuser["BOO_Pid"].ToString());

                        if (UI_RIGHT.Count > 0)
                        {
                            state = LoginState.登录成功;

                            //保存登录人员信息
                            BaseUI.UI_BOOPID = rowuser["BOO_Pid"].ToString();
                            BaseUI.UI_BOOPNAME = rowuser["BOO_Pname"].ToString();
                            BaseUI.UI_BOOLOGNAME = rowuser["BOO_LogName"].ToString();
                            BaseUI.UI_BOOLOGDATE = BaseUI.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");

                            BaseUI.UI_NoAccessProcess = null;
                            BaseUI.UI_OnlyAccessProcess = null;
                            // 03禁止补扫OTP烧录+TP测试,复测AOI,FV2外观
                            if (BaseUI.UI_RIGHT.ContainsKey("02120303"))
                            {
                                string note = BaseUI.UI_RIGHT["02120303"];
                                BaseUI.UI_NoAccessProcess = note.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            }

                            // 04仅允许LCD清洗|COG|FOG|FOG AOI|TP贴合|背光贴合
                            if (BaseUI.UI_RIGHT.ContainsKey("02120304"))
                            {
                                string note = BaseUI.UI_RIGHT["02120304"];
                                BaseUI.UI_OnlyAccessProcess = note.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            }

                            // 获取补扫工序权限
                            BaseUI.UI_AccessProcess = GetRescanRight(BaseUI.UI_BOOPID);
                        }//if (BaseUI.GetRight(rowuser["BOO_Pid"].ToString()))
                        else
                        {
                            state = LoginState.账号无权限;
                        }
                    }//if (YJ.Common.AES.Encode(password, "yj888128") == psw)
                    else
                    {
                        state = LoginState.密码错误;
                    }
                }//if (tb != null && tb.Rows.Count > 0)
                else
                {
                    state = LoginState.账号不存在;
                }
            }//try
            catch (Exception exp)
            {
                state = LoginState.服务器异常;
                LogHelper.Error("登录失败" + exp.Message, exp);
            }
            return state;
        }

        /// <summary>
        /// 根据用户ID获取用户账号
        /// </summary>
        /// <param name="Pid">用户ID</param>
        /// <returns>用户账号</returns>
        public static string GetOpName(string Pid)
        {
            if (string.IsNullOrEmpty(Pid)) return null;
            try
            {
                string sql = string.Format(@"SELECT BOO_LogName FROM Base_Op_Operator
                                             WHERE BOO_Pid = {0}", Pid);
                object obj = conn.ExecuteScalar(sql, "Base");
                if (obj != null)
                {
                    return obj.ToString();
                }//if (tb != null)
                else
                {
                    return null;
                }
            }//try
            catch (Exception exp)
            {
                LogHelper.Error("登录失败" + exp.Message, exp);
                return null;
            }
        }

        #endregion

        #region 判断帐号是否已登录
        /// <summary>
        /// 获取帐号登录IP地址
        /// </summary>
        /// <param name="account">登录帐号</param>
        /// <returns></returns>
        public static string GetLoginIP(string account)
        {
            if (string.IsNullOrEmpty(account)) return null;
            try
            {
                string sql = string.Format(@"SELECT BOO_WindfromToken FROM Base_Op_Operator
                                             WHERE BOO_LogName = '{0}'", account);
                object obj = conn.ExecuteScalar(sql, "Base");
                if (obj != null)
                {
                    return obj.ToString();
                }//if (tb != null)
                else
                {
                    return null;
                }
            }//try
            catch (Exception exp)
            {
                LogHelper.Error("帐号登录IP获取失败" + exp.Message, exp);
                return null;
            }

        }

        /// <summary>
        /// 更新帐号登录状态
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="IP">登录IP</param>
        /// <returns></returns>
        public static bool ChangeLoginState(string account, string IP)
        {
            if (string.IsNullOrEmpty(account)) return false;
            try
            {
                string sql = string.Format(@"UPDATE Base_Op_Operator SET BOO_WindfromToken = '{0}' 
                                             WHERE BOO_LogName = '{1}'", IP, account);
                int n = conn.ExecuteAction(sql, "Base");
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }//try
            catch (Exception exp)
            {
                LogHelper.Error("帐号登录状态更新失败" + exp.Message, exp);
                return false;
            }
        }
        #endregion 判断帐号是否已登录

        #region 修改登录密码
        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <param name="password">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public static LoginState ChangePassword(string account, string password, string newPassword)
        {
            LoginState state;
            try
            {
                string sql = string.Format(@"SELECT * FROM Base_Op_Operator
                                             WHERE BOO_LogName = '{0}'", account);
                DataView dv = conn.ExecuteDataView(sql, "Base");
                if (dv != null && dv.Count > 0)
                {
                    DataRowView rowuser = dv[0];
                    string name = rowuser["BOO_Pname"].ToString();
                    string psw = rowuser["BOO_PassWord"].ToString();
                    if (YJ.Common.AES.Encode(password, "yj888128") == psw)
                    {
                        string newPsw = YJ.Common.AES.Encode(newPassword, "yj888128");
                        string pinyin = rowuser["BOO_Pinyin"].ToString();
                        string qr = "Person|" + account + "|" + newPsw + "|" + pinyin + "|" + name;

                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("UPDATE Base_Op_Operator SET ");
                        strSql.Append("BOO_PassWord = @BOO_PassWord, ");
                        strSql.Append("BOO_QRCode = @BOO_QRCode ");
                        strSql.Append("WHERE BOO_LogName = @BOO_LogName ");
                        SqlParameter[] parameters = {
                            new SqlParameter("@BOO_PassWord", SqlDbType.VarChar,80),
                            new SqlParameter("@BOO_QRCode", SqlDbType.VarChar,500),
                            new SqlParameter("@BOO_LogName", SqlDbType.VarChar,50),
                        };
                        parameters[0].Value = newPsw;
                        parameters[1].Value = qr;
                        parameters[2].Value = account;

                        int rows = conn.ExecuteAction(strSql.ToString(), parameters, "Base");
                        if (rows > 0)
                        {
                            state = LoginState.登录成功;
                        }
                        else
                        {
                            state = LoginState.服务器异常;
                        }
                    }//if (YJ.Common.AES.Encode(password, "yj888128") == psw)
                    else
                    {
                        state = LoginState.密码错误;
                    }
                }//if (dv != null && dv.Count > 0)
                else
                {
                    state = LoginState.账号不存在;
                }
            }//try
            catch (Exception exp)
            {
                state = LoginState.服务器异常;
                LogHelper.Error("登录失败" + exp.Message, exp);
            }
            return state;
        }

        #endregion 修改登录密码


        #region  根据本机IP，获取本机IP所在的产线、工序、产线在制品的型号
        /// <summary>
        /// 根据本机IP，获取本机IP所在的产线、工序、产线在制品的型号
        /// </summary>
        /// <param name="ipAddress">本机IP</param>
        public static void GetLineModelProcedure(string ipAddress)
        {
            try
            {
                string sql = string.Format(@"SELECT * FROM TPL_ProductProcess_GetIP_View  WHERE  STW_CardIP='{0}'", ipAddress);
                DataView dv = BaseUI.conn.ExecuteDataView(sql, BaseUI.DBName);
                if (dv.Count == 0)
                {
                    Exception ex = new Exception("本机IP【" + ipAddress + "】未在MES系统中配置");
                    throw ex;
                }
                else
                {
                    BaseUI.UI_SPLJOBCODE = (dv[0]["SPL_JobCode"] ?? "").ToString();//产线编码
                    BaseUI.UI_SPLNAME = (dv[0]["SPL_Name"] ?? "").ToString();//产线名称
                    BaseUI.UI_SPLPORT = (dv[0]["SPL_Port"] ?? "").ToString();//上报服务器端口号
                    BaseUI.UI_SPLIPAddress = (dv[0]["SPL_IPAddress"] ?? "").ToString();//上报服务器IP地址
                    BaseUI.UI_SMCODE = (dv[0]["SPOM_SMCode"] ?? "").ToString();//成品型号编码
                    BaseUI.UI_SMSPEC = (dv[0]["SPOM_SMSpec"] ?? "").ToString();//成品规格
                    BaseUI.UI_SGXJOBCODE = (dv[0]["SGX_JobCode"] ?? "").ToString();//工序编码
                    BaseUI.UI_SGXNAME = (dv[0]["SGX_Name"] ?? "").ToString();//工序名称
                    BaseUI.UI_SPOMJobCode = (dv[0]["SPOM_JobCode"] ?? "").ToString();//工单编码
                    BaseUI.UI_IsTP = Convert.ToBoolean(dv[0]["SPOM_IsTP"] ?? "False");//是否TP有码

                    if (BaseUI.UI_SGXJOBCODE == "018" && !BaseUI.UI_IsTP)
                    {
                        // TP贴合且TP无码，认定为过站工序
                        BaseUI.UI_IsBind = false;
                    }
                    else
                    {
                        BaseUI.UI_IsBind = Convert.ToBoolean(dv[0]["SGX_IsBind"] ?? "Flase");//是否关键工序
                    }
                    //// 是否允许跨线生产
                    //switch (YJ.Common.Utils.StrToInt(dv[0]["SPOM_MixedLine"], 0))
                    //{
                    //    case 0:
                    //        BaseUI.UI_IsMixedLine = false;
                    //        break;
                    //    case 1:
                    //        BaseUI.UI_IsMixedLine = true;
                    //        break;
                    //    default:
                    //        BaseUI.UI_IsMixedLine = false;
                    //        break;
                    //}

                    // 是否允许返检
                    switch ((dv[0]["SGX_Rework"] ?? "").ToString())
                    {
                        case "02":
                            BaseUI.UI_SGXRework = true;
                            break;
                        default:
                            BaseUI.UI_SGXRework = false;
                            break;
                    }

                    BaseUI.UI_SPOMID = YJ.Common.TypeConverter.ObjectToInt(dv[0]["SPOM_Tid"]);//工单ID
                    BaseUI.UI_SPLID = YJ.Common.TypeConverter.ObjectToInt(dv[0]["SPL_Tid"]);//产线ID
                    BaseUI.UI_SGXID = YJ.Common.TypeConverter.ObjectToInt(dv[0]["SGX_Tid"]);//工序ID
                    BaseUI.UI_DeviceTid = YJ.Common.TypeConverter.ObjectToInt(dv[0]["STW_Tid"]);//设备ID
                }
                dv = null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 获取产线在制品的型号、工序数据
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetMaterialProcessData()
        {
            try
            {
                DataSet ds = null;
                if (BaseUI.UI_SGXJOBCODE == "050")
                {
                    string sql = string.Format("EXEC Service_InitMaterialProcessLineNew '{0}','{1}','{2}'", "ProcessRepeat", BaseUI.UI_SPLJOBCODE, BaseUI.UI_SPOMJobCode);
                    ds = conn.ExecuteDataSet(sql, "Base");
                }
                else
                {
                    string sql = string.Format("EXEC Service_InitMaterialProcessLineNew '{0}','{1}','{2}'", "ProcessKey", BaseUI.UI_SPLJOBCODE, BaseUI.UI_SPOMJobCode);
                    ds = conn.ExecuteDataSet(sql, "Base");
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取指定产线工单的工序链信息
        /// </summary>
        /// <param name="ProductOrderCode">工单编码</param>
        /// <param name="LineCode">产线编码</param>
        /// <returns>DataTable</returns>
        public static DataTable GetProduceRouteData(string ProductOrder, string LineCode)
        {
            string sqlString = string.Format(@"SELECT * FROM ProduceOrder_Route_CarIP_VW WITH(NOLOCK) 
                                               WHERE SPOM_JobCode = '{0}'AND SPL_JobCode = '{1}'  
                                               ORDER BY PR_NoSeq ASC", ProductOrder, LineCode);
            DataTable dt = BaseUI.conn.ExecuteDataTable(sqlString, BaseUI.DBName);
            return dt;
        }
        #endregion

        #region 初始化当前已过的工序的Dictionary
        /// <summary>
        /// 初始化当前已过的工序的Dictionary
        /// </summary>
        /// <param name="da"></param>
        /// <returns></returns>
        public static string NowCode = ",,,";
        public static Dictionary<string, string> GetProcedure(DataTable da)
        {
            try
            {
                DataView ProcessLineView = new DataView();
                string process = BaseUI.UI_CurrentProcedureCode;
                int sort = YJ.Common.Utils.StrToInt(BaseUI.UI_CurrentProcedureNo, 0);
                Dictionary<string, string> ProcessDic = new Dictionary<string, string>();
                if (da.Rows.Count > 0)
                {
                    ProcessLineView = new DataView(da);
                    for (int i = 0; i < ProcessLineView.Count; i++)
                    {
                        if (YJ.Common.Utils.StrToInt(ProcessLineView[i]["PR_NoSeq"].ToString(), 0) < sort
                            && !YJ.Common.Utils.StrToBool(ProcessLineView[i]["SGX_IsOptional"].ToString(), false))
                        {
                            NowCode += ProcessLineView[i]["SGX_JobCode"].ToString() + ",";
                            ProcessDic.Add(ProcessLineView[i]["SGX_JobCode"].ToString(), ProcessLineView[i]["SGX_Name"].ToString());
                        }
                    }
                }
                return ProcessDic;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        #endregion

        #region 判断检测产品漏工序 、重复扫描
        /// <summary>
        /// 漏工序检测
        /// </summary>
        /// <param name="logCode">玻璃已过工序集合</param>
        /// <param name="nowCode">玻璃应过工序集合</param>
        /// <param name="processDic">玻璃应过工序集合键值对</param>
        /// <returns>玻璃未过工序集合</returns>
        public static string GetLeakProcedure(string logCode, string nowCode, Dictionary<string, string> processDic)
        {
            try
            {
                string LeakProcedure = "";
                string[] LogCodeList = logCode.Trim(',').Split(',');
                string[] ProcessCodeList = nowCode.Trim(',').Split(',');
                var DiffList = ProcessCodeList.Except(LogCodeList).ToList();
                for (int i = 0; i < DiffList.Count; i++)
                {
                    if (processDic.ContainsKey(DiffList[i]))
                    {
                        LeakProcedure += processDic[DiffList[i]].ToString() + " ,";
                    }
                }
                return LeakProcedure.Trim(',');
            }
            catch (Exception exp)
            {
                return "False" + exp.Message.ToString();
            }
        }
        /// <summary>
        /// 检测当前工序是否重复扫描
        /// </summary>
        /// <param name="logNumber"></param>
        /// <returns></returns>
        public static bool CheckProcedure(string logNumber)
        {
            bool Isrepeat = false;
            try
            {
                if (logNumber.Contains(BaseUI.UI_CurrentProcedureCode))
                {
                    Isrepeat = true;
                }
                return Isrepeat;
            }
            catch
            {
                return Isrepeat;
            }
        }
        #endregion

        #region 获取当前LCD所在的生产线
        /// <summary>
        /// 由产线编码获取产线名称
        /// </summary>
        /// <param name="LineCode">产线编码</param>
        /// <returns></returns>
        public static string GetLineName(string LineCode)
        {
            string LineName = "";
            try
            {
                string sql = string.Format("SELECT SPL_Name FROM Store_Procedure_Line WITH(NOLOCK) WHERE  SPL_JobCode='{0}'", LineCode);
                DataView dv = conn.ExecuteDataView(sql, "Base");
                if (dv.Count > 0)
                {
                    LineName = dv[0]["SPL_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LineName;
        }
        #endregion

        #region 获取工序名称
        /// <summary>
        /// 由工序编码获取工序名称
        /// </summary>
        /// <param name="ProcessCode">工序编码</param>
        /// <returns></returns>
        public static string GetProcessName(string ProcessCode)
        {
            string ProcessName = "";
            try
            {
                string sql = string.Format("SELECT SGX_Name FROM Store_GongXu_Main WHERE  SGX_JobCode='{0}'", ProcessCode);
                DataView dv = conn.ExecuteDataView(sql, "Base");
                if (dv.Count > 0)
                {
                    ProcessName = dv[0]["SGX_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("由工序编码获取工序名称失败", ex);
            }
            return ProcessName;
        }
        #endregion

        /// <summary>
        /// 获取工单的工序信息
        /// </summary>
        /// <param name="productionOrder"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(string productionOrder)
        {
            DataTable dt = null;
            try
            {
                string sql = string.Format(@"SELECT SPL_ShopName
                                              ,SPL_JobCode
	                                          ,SPL_Name
                                              ,SPOM_Tid
                                              ,SPOM_JobCode
	                                          ,SPOM_Status
                                              ,SPOM_SMCode
                                              ,SPOM_SMSpec
	                                          ,SPOM_CustomerCode
	                                          ,SPOM_CustomerOrder
	                                          ,SPOM_IsTP
	                                          ,SPOM_MixedLine
                                              ,SPOM_Nums
	                                          ,SPOM_InNum
                                              ,SPOM_NumsOver
	                                          ,SPOM_NextOrder
                                              ,SPOM_MaterialState
                                            FROM YJ_TXDMES.dbo.Store_ProduceOrder_Main
                                            LEFT JOIN YJ_TXDMES.dbo.Store_Procedure_Line 
                                            ON Store_Procedure_Line.SPL_Tid = Store_ProduceOrder_Main.SPOM_SPLTid
                                            WHERE SPOM_JobCode ='{0}'", productionOrder);
                dt = conn.ExecuteDataTable(sql, "Base");
            }
            catch (Exception ex)
            {
                LogHelper.Error("由工单编码获取工单信息失败", ex);
            }
            return dt;
        }

        #region 不良申报
        /// <summary>
        /// 获取当前工序的默认异常项[TPD_BNCTid, TPD_BNCName]
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<int, string> GetDefaultBNCItem(string ProcessCode)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            try
            {
                //                string sqlString = string.Format(@"IF EXISTS(SELECT TPD_BNCTid,TPD_BNCName FROM TPL_ProcessFail_Define WHERE TPD_ProcessCode = '{0}' AND TPD_BNCCode = '{0}')
                //	                                                  BEGIN
                //		                                                    SELECT TPD_BNCTid,TPD_BNCName FROM TPL_ProcessFail_Define WHERE TPD_ProcessCode = '{0}' AND TPD_BNCCode = '{0}';
                //	                                                  END
                //                                                   ELSE
                //  	                                                  BEGIN
                //	                                                        SELECT TOP 1 TPD_BNCTid,TPD_BNCName FROM TPL_ProcessFail_Define WHERE TPD_ProcessCode = '{0}' AND TPD_BNCCode NOT LIKE 'JT_%';
                //		                                              END", ProcessCode);
                string sqlString = string.Format(@"IF EXISTS(SELECT TPD_BNCTid,TPD_BNCName FROM TPL_ProcessFail_Define WHERE TPD_ProcessCode = '{0}' AND TPD_BNCCode = '{0}')
	                                                  BEGIN
		                                                    SELECT TPD_BNCTid,TPD_BNCName FROM TPL_ProcessFail_Define WHERE TPD_ProcessCode = '{0}' AND TPD_BNCCode = '{0}';
	                                                  END
                                                   ELSE
  	                                                  BEGIN
	                                                        SELECT TPD_BNCTid,TPD_BNCName FROM TPL_ProcessFail_Define WHERE TPD_ProcessCode = '{0}' AND TPD_BNCCode NOT LIKE 'JT_%';
		                                              END", ProcessCode);

                DataView sqlView = BaseUI.conn.ExecuteDataView(sqlString, BaseUI.DBName);
                foreach (DataRowView rv in sqlView)
                {
                    if (!dic.ContainsKey(Convert.ToInt32(rv["TPD_BNCTid"])))
                    {
                        dic.Add(Convert.ToInt32(rv["TPD_BNCTid"]), rv["TPD_BNCName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dic;
        }
        /// <summary>
        /// 获取所有异常项列表[BNC_Tid],[BNC_Flag],[BNC_cCode],[BNC_cName],[BNC_Note],[BNC_RepairSGX]
        /// 排除所有机台不良项和工序默认不良项
        /// </summary>
        /// <returns>Dictionary</returns>
        public static DataTable GetBNCTable()
        {
            try
            {
                string sqlString = "SELECT TPD_ProcessCode,TPD_BNCTid,TPD_BNCCode,TPD_BNCName,BNC_Flag,BNC_Note,BNC_RepairSGX,BNC_RepairNote FROM TPL_ProcessFail_Define INNER JOIN Basal_NCCode ON BNC_Tid = TPD_BNCTid WHERE TPD_BNCCode NOT LIKE 'JT_%' AND TPD_BNCCode <> TPD_ProcessCode";
                //string sqlString = "SELECT TPD_ProcessCode,TPD_BNCTid,TPD_BNCCode,TPD_BNCName,BNC_Flag,BNC_Note,BNC_RepairSGX FROM TPL_ProcessFail_Define INNER JOIN Basal_NCCode ON BNC_Tid = TPD_BNCTid WHERE TPD_BNCCode NOT LIKE 'JT_%'";
                DataTable dt = BaseUI.conn.ExecuteDataTable(sqlString, BaseUI.DBName);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加工序异常提报信息
        /// </summary>
        /// <param name="TFM_Status">提报状态</param>
        /// <param name="TFM_Type">提报类型</param>
        /// <param name="FPC">FPC编码</param>
        /// <param name="LCD">LCD编码</param>
        /// <param name="TIME">过站时间</param>
        /// <param name="BNCTid">不良项ID</param>
        /// <param name="BNCName">不良项名称</param>
        /// <param name="TFS_Type">异常来源</param>
        /// <param name="IP">设备IP</param>
        /// <param name="PID">员工账号ID</param>
        /// <param name="PID">ProductOrderCode</param>
        /// <returns></returns>
        public static bool Add_ProcessFail(string TFM_Status, string TFM_Type, string FPC, string LCD, string TIME, string BNCTid, string BNCName, string TFS_Type, string IP, int PID, string ProductOrderCode)
        {
            try
            {
                string sql = string.Format("EXEC TPL_ProcessFail_Pro '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'", TFM_Status, TFM_Type, FPC, LCD, TIME, BNCTid, BNCName, TFS_Type, IP, PID, ProductOrderCode);
                object rst = conn.ExecuteScalar(sql, "Base");
                if (rst.ToString() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新不良复判信息
        /// </summary>
        /// <param name="NGGlassList">不良玻璃列表</param>
        /// <param name="TFM_Type">提报类型</param>
        /// <param name="BNCTid">不良项ID</param>
        /// <param name="BNCName">不良项名称</param>
        /// <param name="PID">复判员工账号ID</param>
        /// <returns></returns>
        public static bool Check_ProcessFail(string FailIDList, string TFM_Type, string BNCTid, string BNCName, int PID)
        {
            try
            {
                string sql = string.Format("EXEC TPL_ProcessFail_Review '{0}','{1}','{2}','{3}',{4}", FailIDList, TFM_Type, BNCTid, BNCName, PID);
                object rst = conn.ExecuteScalar(sql, "Base");
                if (rst.ToString() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///// <summary>
        ///// 判断扫描的编码是否已提报不良或已复判
        ///// </summary>
        ///// <param name="GlassCode">玻璃编码</param>
        ///// <returns></returns>
        //public static ExceptionState GetFailReviewResult(string GlassCode)
        //{
        //    if (string.IsNullOrEmpty(GlassCode))
        //    {
        //        return ExceptionState.良品;
        //    }
        //    string strSql = string.Format("SELECT TOP 1 TFL_Status FROM TPL_ProcessFail_Log WIITH(NOLOCK) WHERE TFL_GlassCode = '{0}' ORDER BY TFL_TFMTid DESC", GlassCode);
        //    object obj = BaseUI.conn.ExecuteScalar(strSql, BaseUI.DBName);
        //    if (obj == null)
        //    {
        //        return ExceptionState.良品;
        //    }

        //    string status = obj.ToString();
        //    switch (status)
        //    {
        //        case "重工":
        //            return ExceptionState.复判重工;

        //        case "报废":
        //            return ExceptionState.复判报废;

        //        case "误判":
        //            return ExceptionState.复判良品;

        //        default:
        //            return ExceptionState.待复判;
        //    }
        //}

        /// <summary>
        /// 查询指定FPC编码的工序异常提报信息
        /// </summary>
        /// <param name="ScanCode">FPC编码</param>
        /// <returns>Utils.Model.TPL_ProcessFail_Main</returns>
        public static Utils.Model.TPL_ProcessFail_Main GetProcessFail_Main(string ScanCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 TFM_Tid,TFM_Status,TFM_Type,TFM_Describe,TFM_ProductTid,TFM_ProductOrder,TFM_LineTid,TFM_LineCode,TFM_ProcessTid,TFM_ProcessCode,TFM_DeviceTid,TFM_DeviceIP,TFM_ScanCode,TFM_GlassCode,TFM_ScanTime,TFM_Num,TFM_AddPid,TFM_AddDate,TFM_CheckPid,TFM_CheckDate FROM TPL_ProcessFail_Main WITH(NOLOCK)");
            strSql.Append("WHERE TFM_ScanCode = @TFM_ScanCode OR TFM_GlassCode = @TFM_ScanCode ");
            strSql.Append("ORDER BY TFM_AddDate DESC ");
            SqlParameter[] parameters = {
                    new SqlParameter("TFM_ScanCode", SqlDbType.VarChar,50)
            };
            parameters[0].Value = ScanCode;

            Utils.Model.TPL_ProcessFail_Main model = new Utils.Model.TPL_ProcessFail_Main();
            DataSet ds = BaseUI.conn.ExecuteDataSet(strSql.ToString(), parameters, BaseUI.DBName);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return TPL_ProcessFail_Main_DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个TPL_ProcessFail_Main的对象实体
        /// </summary>
        public static Utils.Model.TPL_ProcessFail_Main TPL_ProcessFail_Main_DataRowToModel(DataRow row)
        {
            Utils.Model.TPL_ProcessFail_Main model = new Utils.Model.TPL_ProcessFail_Main();
            if (row != null)
            {
                if (row["TFM_Tid"] != null && row["TFM_Tid"].ToString() != "")
                {
                    model.TFM_Tid = int.Parse(row["TFM_Tid"].ToString());
                }
                if (row["TFM_Status"] != null)
                {
                    model.TFM_Status = row["TFM_Status"].ToString();
                }
                if (row["TFM_Type"] != null)
                {
                    model.TFM_Type = row["TFM_Type"].ToString();
                }
                if (row["TFM_Describe"] != null)
                {
                    model.TFM_Describe = row["TFM_Describe"].ToString();
                }
                if (row["TFM_ProductTid"] != null && row["TFM_ProductTid"].ToString() != "")
                {
                    model.TFM_ProductTid = int.Parse(row["TFM_ProductTid"].ToString());
                }
                if (row["TFM_ProductOrder"] != null)
                {
                    model.TFM_ProductOrder = row["TFM_ProductOrder"].ToString();
                }
                if (row["TFM_LineTid"] != null && row["TFM_LineTid"].ToString() != "")
                {
                    model.TFM_LineTid = int.Parse(row["TFM_LineTid"].ToString());
                }
                if (row["TFM_LineCode"] != null)
                {
                    model.TFM_LineCode = row["TFM_LineCode"].ToString();
                }
                if (row["TFM_ProcessTid"] != null && row["TFM_ProcessTid"].ToString() != "")
                {
                    model.TFM_ProcessTid = int.Parse(row["TFM_ProcessTid"].ToString());
                }
                if (row["TFM_ProcessCode"] != null)
                {
                    model.TFM_ProcessCode = row["TFM_ProcessCode"].ToString();
                }
                if (row["TFM_DeviceTid"] != null && row["TFM_DeviceTid"].ToString() != "")
                {
                    model.TFM_DeviceTid = int.Parse(row["TFM_DeviceTid"].ToString());
                }
                if (row["TFM_DeviceIP"] != null)
                {
                    model.TFM_DeviceIP = row["TFM_DeviceIP"].ToString();
                }
                if (row["TFM_ScanCode"] != null)
                {
                    model.TFM_ScanCode = row["TFM_ScanCode"].ToString();
                }
                if (row["TFM_GlassCode"] != null)
                {
                    model.TFM_GlassCode = row["TFM_GlassCode"].ToString();
                }
                if (row["TFM_ScanTime"] != null && row["TFM_ScanTime"].ToString() != "")
                {
                    model.TFM_ScanTime = DateTime.Parse(row["TFM_ScanTime"].ToString());
                }
                if (row["TFM_Num"] != null && row["TFM_Num"].ToString() != "")
                {
                    model.TFM_Num = int.Parse(row["TFM_Num"].ToString());
                }
                if (row["TFM_AddPid"] != null && row["TFM_AddPid"].ToString() != "")
                {
                    model.TFM_AddPid = int.Parse(row["TFM_AddPid"].ToString());
                }
                if (row["TFM_AddDate"] != null && row["TFM_AddDate"].ToString() != "")
                {
                    model.TFM_AddDate = DateTime.Parse(row["TFM_AddDate"].ToString());
                }
                if (row["TFM_CheckPid"] != null && row["TFM_CheckPid"].ToString() != "")
                {
                    model.TFM_CheckPid = int.Parse(row["TFM_CheckPid"].ToString());
                }
                if (row["TFM_CheckDate"] != null && row["TFM_CheckDate"].ToString() != "")
                {
                    model.TFM_CheckDate = DateTime.Parse(row["TFM_CheckDate"].ToString());
                }
            }
            return model;
        }
        #endregion 不良申报

        #region 自动站点

        /// <summary>
        /// 获取自动站点扫描枪IP
        /// </summary>
        /// <param name="DeviceIP">工控机IP</param>
        /// <returns>string[]（0：1#扫描枪IP，1：2#扫描枪IP）</returns>
        public static string[] GetCodeReaderIP(string DeviceIP)
        {
            string strSql = string.Format("SELECT * FROM TXD_AutoScan_Config WHERE TAC_DeviceIP = '{0}'", DeviceIP);
            DataView dv = BaseUI.conn.ExecuteDataView(strSql, BaseUI.DBName);
            if (dv.Count > 0)
            {
                string[] ip = new string[2];
                ip[0] = dv[0]["TAC_Reader1IP"].ToString();
                ip[1] = dv[0]["TAC_Reader2IP"].ToString();
                return ip;
            }
            else
            {
                return null;
            }
        }
        #endregion 自动站点

        #region 获取产线列表
        /// <summary>
        /// 获取产线列表（产线编码，产线名称, 车间名称）
        /// </summary>
        /// <returns></returns>
        public static DataView GetProcedureLine()
        {
            string strSql = "SELECT SPL_JobCode, SPL_Name, SPL_ShopName FROM Store_Procedure_Line ORDER BY SPL_ShopName, SPL_JobCode";
            DataView dv = BaseUI.conn.ExecuteDataView(strSql, BaseUI.DBName);
            return dv;
        }
        #endregion 获取产线列表

        /// <summary>
        /// 获取指定工单编码的物料清单
        /// </summary>
        /// <param name="ProductionOrder">工单编码</param>
        /// <returns></returns>
        public static DataView GetMaterialTable(string ProductionOrder)
        {
            string strSql = string.Format("SELECT SPOM_Tid,SPOM_JobCode,SPOS_SMCode,SPOS_SMName,SPOS_SMSpec FROM Store_ProduceOrder_Main LEFT JOIN Store_ProduceOrder_Sub ON SPOM_Tid = SPOS_SPOMTid WHERE SPOM_JobCode = '{0}'", ProductionOrder);
            DataView dv = BaseUI.conn.ExecuteDataView(strSql, BaseUI.DBName);
            return dv;
        }
        /// <summary>
        /// 获取工单的计划生产数量
        /// </summary>
        /// <param name="ProductionOrder"></param>
        /// <returns></returns>
        public static int GetOrderNumber(string ProductionOrder)
        {
            string strSql = string.Format("SELECT SPOM_Nums FROM Store_ProduceOrder_Main WHERE SPOM_JobCode = '{0}'", ProductionOrder);
            object num = BaseUI.conn.ExecuteScalar(strSql, BaseUI.DBName);
            if (num == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(num);
            }
        }
        /// <summary>
        /// 获取工单的投料数量，清洗投入数量
        /// </summary>
        /// <param name="ProductionOrder"></param>
        /// <returns></returns>
        public static void GetFeedAndInputNumber(string ProductionOrder, out int FeedNum, out int InputNum)
        {
            FeedNum = 0;
            InputNum = 0;

            string strSql = string.Format("SELECT SPP_FeedNum, SPP_InPutNum FROM Store_ProduceOrder_Product WHERE SPP_SPOMJobCode = '{0}'", ProductionOrder);
            DataView dv = BaseUI.conn.ExecuteDataView(strSql, BaseUI.DBName);
            if (dv == null || dv.Count == 0)
            {
                FeedNum = 0;
                InputNum = 0;
            }
            else
            {
                foreach (DataRowView row in dv)
                {
                    FeedNum += YJ.Common.Utils.StrToInt(row["SPP_FeedNum"], 0);
                    InputNum += YJ.Common.Utils.StrToInt(row["SPP_InPutNum"], 0);
                }
            }
        }

        /// <summary>
        /// 获取指定产线的工单列表
        /// </summary>
        /// <param name="LineCode">产线编码</param>
        /// <returns></returns>
        public static DataTable GetOrderTable(string LineCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT SPOM_Tid,SPOM_JobCode,SPOM_Status,SPOM_Nums,SPOM_SPLTid,SPL_JobCode,SPOM_SMSpec,SPOM_NextOrder,SPOM_MaterialState,SPOM_ShengChengDate,SPOM_OverDate FROM Store_ProduceOrder_Main ");
            strSql.Append("LEFT JOIN Store_Procedure_Line ON SPOM_SPLTid = SPL_Tid ");
            strSql.Append(string.Format("WHERE SPL_JobCode = '{0}' AND SPOM_Status IN ('生成计划', '生产中','已完结','生产挂起')", LineCode));
            strSql.Append("ORDER BY SPOM_OverDate DESC, SPOM_ShengChengDate DESC ");
            DataTable dt = BaseUI.conn.ExecuteDataTable(strSql.ToString(), BaseUI.DBName);
            return dt;
        }

        /// <summary>
        /// 查询指定ID的工单是否存在未完成的退料单
        /// </summary>
        /// <param name="OrderID">工单ID</param>
        /// <returns></returns>
        public static bool IsProductNgBackOver(int OrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(*) FROM TPL_ProductNgBack_Main ");
            strSql.Append(string.Format("WHERE TNM_ProductTid = {0} AND TNM_Status = '已审核' AND TNM_Type = '退料单'", OrderID));
            object obj = BaseUI.conn.ExecuteScalar(strSql.ToString(), BaseUI.DBName);
            int count = YJ.Common.Utils.StrToInt(obj, 0);
            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 确定待产工单
        /// </summary>
        /// <param name="OrderCode">工单编码</param>
        /// <returns></returns>
        public static bool NextOrderCommit(string CurrentOrderCode, int NextOrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_NextOrder = {0} WHERE SPOM_JobCode = '{1}' ;;", NextOrderID, CurrentOrderCode));
            strSql.Append(string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_MaterialState = '待配料', SPOM_Status = '生成计划' WHERE SPOM_Tid = {0} ", NextOrderID));
            int count = BaseUI.conn.ExecuteAction(strSql.ToString(), BaseUI.DBName);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置工单完结信息
        /// </summary>
        /// <param name="OrderCode">工单编码</param>
        /// <param name="Note">完结说明</param>
        /// <returns></returns>
        public static bool SetOrderOverInfo(string OrderCode, string Note)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("UPDATE Store_ProduceOrder_Main SET SPOM_OverNote = '{0}',SPOM_OverPid = {1},SPOM_OverDate = '{2}' WHERE SPOM_JobCode = '{3}' ", Note, UI_BOOPID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), OrderCode));
            int count = BaseUI.conn.ExecuteAction(strSql.ToString(), BaseUI.DBName);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 切换工单
        /// </summary>
        /// <param name="Url">接口地址</param>
        /// <param name="CodeType">类型（01：生产完结，02：生产挂起）</param>
        /// <param name="LineCode">产线编码</param>
        /// <param name="ProductOrder">工单编码</param>
        /// <returns></returns>
        public static string OrderSwitch(string Url, string CodeType, String LineCode, string ProductOrder)
        {
            Dictionary<string, string> dicPara = new Dictionary<string, string>()
            {
                {"op","Get_ProductionOrder"},
                {"CodeType",CodeType},
                {"LineCode",LineCode},
                {"ProductOrder",ProductOrder}
            };

            string postStr = Utils.HttpHelper.BuildQueryStr(dicPara);

            string response = Utils.HttpHelper.ExecutePost(Url, postStr);

            return response;
        }

        /// <summary>
        /// 更新数据库中的工控机程序版本
        /// </summary>
        /// <param name="version"></param>
        public static void DeviceVersionUpdate()
        {
            // 获取本机IP地址
            string ip = GetLocalIP();
            // 当前程序版本
            string version = "数据采集V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            string sql = string.Format("UPDATE Store_TaiWei SET STW_Name = '{0}' WHERE STW_CardIP = '{1}' ", version, ip);
            int count = BaseUI.conn.ExecuteAction(sql, BaseUI.DBName);
        }

        /// <summary>
        /// 获取SQL Server服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            string sql = "SELECT GETDATE()";
            object obj = BaseUI.conn.ExecuteScalar(sql, BaseUI.DBName);
            if (obj != null)
            {
                return DateTime.Parse(obj.ToString());
            }
            else
            {
                return DateTime.Now;
            }
        }


        //        #region 获取工单混线生产设置
        //        /// <summary>
        //        /// 获取工单混线生产设置
        //        /// </summary>
        //        /// <param name="Order">工单编码</param>
        //        /// <returns></returns>
        //        public static DataTable GetOrderMixLineConfig(string Order)
        //        {
        //            string sql = string.Format(@"SELECT Store_ProduceOrder_Main.SPOM_JobCode
        //                                              ,Store_ProduceOrder_Main.SPOM_SMCode
        //                                              ,Store_ProduceOrder_Main.SPOM_SMSpec
        //	                                          ,Store_Procedure_Line.SPL_JobCode
        //	                                          ,Store_Procedure_Line.SPL_Name
        //                                              ,Store_ProduceOrder_Main.SPOM_MixedLine
        //	                                          ,MIX.MixLineCode 
        //	                                          ,MIX.MixLineName
        //                                          FROM Store_ProduceOrder_Main 
        //                                          LEFT JOIN Store_Procedure_Line ON Store_ProduceOrder_Main.SPOM_SPLTid = Store_Procedure_Line.SPL_Tid
        //                                          LEFT JOIN (
        //                                          SELECT Store_ProduceOrder_Line.SPOL_SPOMTid 
        //                                          ,Store_ProduceOrder_Line.SPOL_SPOMJobCode 
        //                                          ,Store_Procedure_Line.SPL_JobCode AS MixLineCode
        //                                          ,Store_Procedure_Line.SPL_Name AS MixLineName
        //                                          FROM Store_ProduceOrder_Line
        //                                          LEFT JOIN Store_Procedure_Line ON Store_ProduceOrder_Line.SPOL_SPLTid = Store_Procedure_Line.SPL_Tid 
        //                                          ) MIX 
        //                                          ON Store_ProduceOrder_Main.SPOM_Tid = MIX.SPOL_SPOMTid
        //                                          WHERE Store_ProduceOrder_Main.SPOM_JobCode = '{0}'", Order);
        //            DataTable dt = conn.ExecuteDataTable(sql, BaseUI.DBName);
        //            return dt;
        //        }

        //        /// <summary>
        //        /// 获取工单可混产线编码列表
        //        /// </summary>
        //        /// <param name="Order">工单编码</param>
        //        /// <returns></returns>
        //        public static List<string> GetMixLine(string Order)
        //        {
        //            List<string> lst = new List<string>();
        //            DataTable dt = GetOrderMixLineConfig(Order);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    string line = (row["MixLineCode"]??"").ToString();
        //                    if (!string.IsNullOrWhiteSpace(line))
        //                    {
        //                        lst.Add(line);
        //                    }                    
        //                }
        //            }
        //            return lst;
        //        }
        //        #endregion 获取工单混线生产设置

        #region 获取指定型号混线生产设置
        /// <summary>
        /// 获取成品型号配置的可混产线编码列表
        /// </summary>
        /// <param name="finishesCode">成品型号编码</param>
        /// <returns></returns>
        public static List<string> GetMixLine(string finishesCode)
        {
            List<string> lst = new List<string>();

            string sql = string.Format(@"SELECT Store_Procedure_Line.SPL_JobCode AS MixLineCode
	                                    FROM Store_ProduceOrder_Line 
	                                    JOIN  Store_Material ON Store_ProduceOrder_Line.SPOL_SMTid = Store_Material.SM_Tid
	                                    JOIN Store_Procedure_Line ON Store_ProduceOrder_Line.SPOL_SPLTid = Store_Procedure_Line.SPL_Tid
	                                    WHERE Store_Material.SM_nbCode = '{0}' 
                                        AND ISNULL(Store_ProduceOrder_Line.SPOL_SPOMJobCode, '') = ''", finishesCode);
            DataTable dt = conn.ExecuteDataTable(sql, BaseUI.DBName);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string line = (row["MixLineCode"] ?? "").ToString();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        lst.Add(line);
                    }
                }
            }
            return lst;
        }
        #endregion 获取指定型号混线生产设置

        /// <summary>
        /// 检查条码是否包含小写字符
        /// </summary>
        /// <param name="code">待检查条码</param>
        /// <returns>True：包含小写，False：不含小写</returns>
        public static bool LowerCheck(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return false;
            }

            char[] cc = code.ToCharArray();
            foreach (char c in cc)
            {
                if (c >= 'a' && c <= 'z')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 检查玻璃是否已包装
        /// </summary>
        /// <param name="GlassCode"></param>
        /// <returns></returns>
        public static bool isInPackage(string GlassCode)
        {
            // 检查是否已包装
            string sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Sub WITH (NOLOCK) WHERE IBS_GlassCode='{0}'", GlassCode);
            DataView dv = conn.ExecuteDataView(sql, "Base");
            if (dv.Count > 0)
            {
                return true;
            }

            sql = string.Format("SELECT * FROM TPL_PackingInnerBox_Small_Sub WITH (NOLOCK) WHERE ISS_GlassCode='{0}'", GlassCode);
            dv = conn.ExecuteDataView(sql, "Base");
            if (dv.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取机台IP设置的弹框提醒类型（bit0:重工良品，bit1:复判良品，bit2:点线类不良，bit3:允许切换型号）
        /// </summary>
        /// <param name="DeviceIP">机台IP地址</param>
        /// <returns>bit0:重工良品，bit1:复判良品，bit2:点线类不良，bit3:允许切换型号</returns>
        public static string GetDeviceWarnType(string DeviceIP)
        {
            try
            {
                string sql = string.Format("SELECT STW_WarnType FROM Store_TaiWei WHERE STW_CardIP = '{0}'", DeviceIP);
                object obj = conn.ExecuteScalar(sql, "Base");
                if (obj != null)
                {
                    return obj.ToString().PadRight(4, '0');
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取相邻工序过站间隔时间
        /// </summary>
        /// <param name="DeviceIP">机台IP</param>
        /// <returns></returns>
        public static int GetProcessInterval(string DeviceIP)
        {
            try
            {
                string sql = string.Format("SELECT STW_Interval FROM Store_TaiWei WHERE STW_CardIP = '{0}'", DeviceIP);
                object obj = conn.ExecuteScalar(sql, "Base");
                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取产线信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPLineList()
        {
            try
            {
                string sql = @" select SPL_ShopName,SPL_JobCode,SPL_Name from Store_Procedure_Line with(nolock) where SPL_Name not like '%(虚拟产线)%' AND SPL_Name not like '%CELL产线%' AND SPL_Name not like '%返修线%' ORDER BY SPL_ShopName, SPL_jobCode asc ";
                DataTable obj = conn.ExecuteDataTable(sql, "Base");
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 老化过站 老化进站出站
        /// </summary>
        /// <returns></returns>
        public static List<StationEntity> GetOldStation()
        {
            List<StationEntity> oldList = new List<StationEntity>() {
            new StationEntity(){Code="01",Name="老化进站" },
            new StationEntity(){Code="02",Name="老化出站" }
            };
            return oldList;
        }
        /// <summary>
        /// 获取岗位列表
        /// </summary>
        /// <param name="WorkD"></param>
        /// <returns></returns>
        public static DataTable GetWorkPost(string WorkD)
        {
            try
            {
                string Sql = "  select Id,ItemName From FaultItem where Remark=1 and parentId in(  select Id From FaultItem where itemName='" + WorkD + "') ";
                DataTable obj = conn.ExecuteDataTable(Sql, "Base");
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取故障列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DataTable GetFault_Dt(string Id)
        {
            try
            {
                string Sql = "   select Id,ItemName From FaultItem where Remark=1 and parentId =" + Id + " ";
                DataTable obj = conn.ExecuteDataTable(Sql, "Base");
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetFaultItemById(string Id)
        {
            try
            {
                string Reobj = "";
                string Sql = "   select Id,ItemName From FaultItem where Id =" + Id + " ";
                DataTable obj = conn.ExecuteDataTable(Sql, "Base");
                if (obj != null && obj.Rows.Count > 0)
                {
                    Reobj = obj.Rows[0]["ItemName"].ToString();
                }
                return Reobj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 通过IP获取所属工序名称
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        public static string GetGongXuByIP(string Ip)
        {
            try
            {
                string GongXuName = "";
                string Sql = " select STW_CardName From Store_GongXu_SheBei_VW where STW_CardIP='" + Ip + "'  ";
                DataTable obj = conn.ExecuteDataTable(Sql, "Base");
                if (obj != null && obj.Rows.Count > 0)
                {
                    GongXuName = obj.Rows[0]["STW_CardName"].ToString();
                }
                return GongXuName;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 设备维修 白夜班
        /// </summary>
        /// <returns></returns>
        public static List<StationEntity> GetByB()
        {
            List<StationEntity> ItemList = new List<StationEntity>() {
            new StationEntity(){Code="01",Name="白班" },
            new StationEntity(){Code="02",Name="夜班" }
            };
            return ItemList;
        }
        /// <summary>
        /// 设备维修 设备类型
        /// </summary>
        /// <returns></returns>
        public static List<StationEntity> GetDeviceType()
        {
            List<StationEntity> ItemList = new List<StationEntity>() {
            new StationEntity(){Code="治具",Name="治具" },
            new StationEntity(){Code="主机",Name="主机" }
            };
            return ItemList;
        }
        /// <summary>
        /// 设备维修 工段
        /// </summary>
        /// <returns></returns>
        public static List<StationEntity> GetWorkD()
        {
            List<StationEntity> oldList = new List<StationEntity>() {
            new StationEntity(){Code="CELL",Name="CELL" },
            new StationEntity(){Code="CFOG",Name="CFOG" },
            new StationEntity(){Code="CFOGTP",Name="CFOGTP" },
            new StationEntity(){Code="DB",Name="DB" },
            new StationEntity(){Code="ASSY",Name="ASSY" },
            new StationEntity(){Code="ASSYTP",Name="ASSYTP" },
            new StationEntity(){Code="ASSYOTP",Name="ASSYOTP" },
            new StationEntity(){Code="OQC",Name="OQC" },
            new StationEntity(){Code="返修",Name="返修" },
            };
            return oldList;
        }

        /// <summary>
        /// 根据FPC码 玻璃码 客户码 查询单条老化玻璃信息
        /// </summary>
        /// <param name="GlassCOde"></param>
        /// <returns></returns>
        public static DataTable GetSingleOldGlass(string GlassCode)
        {
            string SelectSql = @"select Id,LCDCode,FPCCode,ClientCode,StartDate,EndDate,Duration,LineCode,
            LineName,SiteName,CreateId,CreateName,ModifyId,ModifyName,ServerIP,
            ClientIP,ClientPost,WorkOrderCode,Model,InOutFlag From OldStation where LCDCode = '" + GlassCode + "' OR FPCCode = '" + GlassCode + "' OR ClientCode = '" + GlassCode + "'";
            try
            {
                DataTable obj = conn.ExecuteDataTable(SelectSql, "Base");
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable GetSingleFixtureRepair(string FixtureCode)
        {
            string SelectSql = @" select Id,ByB,WorkD,HostCode,FixtureCode,model,SendRepairer_Name,SendRepairer_Remark,FixtureStatus,Line,PostId,FaultId,DeviceType From FixtureRepair where FixtureCode = '" + FixtureCode + "' ";
            try
            {
                DataTable obj = conn.ExecuteDataTable(SelectSql, "Base");
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据工单查询该工单下的所有老化玻璃信息
        /// </summary>
        /// <param name="WorkOrderCode"></param>
        /// <returns></returns>
        public static DataTable GetOldGlassListByGDCode(string WorkOrderCode)
        {
            string SelectSql = @"select Id,LCDCode,FPCCode,ClientCode,StartDate,EndDate,Duration,LineCode,
            LineName,SiteName,CreateId,CreateName,ModifyId,ModifyName,ServerIP,
            ClientIP,ClientPost,WorkOrderCode,Model,IsException From OldStation where WorkOrderCode = '" + WorkOrderCode + "'";
            try
            {
                DataTable obj = conn.ExecuteDataTable(SelectSql, "Base");
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 老化出站修改信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static bool UpdateOldOutStation(string Code, string EndDate, string Duration, string SiteName, string ModifyId, string ModifyName)
        {
            string UpdateSql = string.Format(@"update OldStation set EndDate='{0}',Duration='{1}',SiteName='{2}',ModifyId='{3}',ModifyName='{4}',InOutFlag='1'
            where FPCCOde = '{5}' OR LCDCode = '{5}' OR ClientCode = '{5}'", EndDate, Duration, SiteName, ModifyId, ModifyName, Code);
            try
            {
                int obj = BaseUI.conn.ExecuteAction(UpdateSql, BaseUI.DBName);
                return Convert.ToInt32(obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 重新老化进站
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="StartDate"></param>
        /// <param name="SiteName"></param>
        /// <param name="ModifyId"></param>
        /// <param name="ModifyName"></param>
        /// <returns></returns>
        public static bool RepeatOldOutStation(string Code, string StartDate, string SiteName, string ModifyId, string ModifyName)
        {
            //CreateId = '',CreateName = '',StartDate = '',EndDate = '',Duration = ''

            string UpdateSql = string.Format(@"update OldStation set CreateId = '{0}',CreateName = '{1}',StartDate='{2}',EndDate='',Duration='',SiteName='{3}',ModifyId='',ModifyName='',InOutFlag='0'
            where FPCCOde = '{4}' OR LCDCode = '{4}' OR ClientCode = '{4}'", ModifyId, ModifyName, StartDate, SiteName, Code);
            try
            {
                int obj = BaseUI.conn.ExecuteAction(UpdateSql, BaseUI.DBName);
                return Convert.ToInt32(obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="Byb"></param>
        /// <param name="WorkD"></param>
        /// <param name="WorkX"></param>
        /// <param name="Model"></param>
        /// <param name="SendRepairer_Name"></param>
        /// <param name="SendRepairer_Remark"></param>
        /// <param name="LineName"></param>
        /// <param name="FixtureStatus"></param>
        /// <param name="Code"></param>
        /// <param name="PostId"></param>
        /// <param name="FaultId"></param>
        /// <returns></returns>
        public static bool UpdateFixtureRepair(string Byb, string WorkD, string HostCode, string Model, string SendRepairer_Name, string SendRepairer_Remark, string LineName, string FixtureStatus, string Code, string PostId, string FaultId, string DeviceType)
        {
            string UpdateSql = string.Format(@" update FixtureRepair set ByB='{0}',WorkD='{1}',HostCode='{2}',model='{3}',SendRepairer_Name='{4}',SendRepairer_Remark='{5}',Line='{6}',FixtureStatus='{7}',PostId='{8}',FaultId='{9}',DeviceType='{10}' where FixtureCode='" + Code + "' ",
                                                                         Byb, WorkD, HostCode, Model, SendRepairer_Name, SendRepairer_Remark, LineName, FixtureStatus, PostId, FaultId, DeviceType);
            try
            {
                int obj = BaseUI.conn.ExecuteAction(UpdateSql, BaseUI.DBName);
                return Convert.ToInt32(obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 执行插入语句
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public static bool InsertOldInStation(string sqlStr)
        {
            try
            {
                int obj = BaseUI.conn.ExecuteAction(sqlStr, BaseUI.DBName);
                return Convert.ToInt32(obj) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
