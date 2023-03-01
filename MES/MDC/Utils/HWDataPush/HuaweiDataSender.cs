using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;
using Dapper;
using Utils.HWDataPush.Entity;

namespace Utils.HWDataPush
{
    /// <summary>
    /// 华为数据发送类
    /// 作者：陈蒋耀 2018/12/26
    /// 功能：读取、写入发送配置，向华为发送AOI测试数据
    /// 备注：1、本类所有方法理论上线程安全；2、本类所有方法都不处理异常。
    /// 
    /// 修改记录：
    ///     第一次重构 2018/12/27：将YJ.Data数据库操作方法移除，引入Dapper 1.40版本，并全部修改为Dapper操作数据库
    ///     第二次重构 2018/12/28：增加文档注释和代码行注释
    /// </summary>
    public class HuaweiDataSender
    {
        /// <summary>
        /// 数据库配置类型，对应数据库配置表Base_Parameter中的Para_Type字段，本类使用固定值HuaweiDataPusher
        /// </summary>
        public const string HUAWEI_PUSHER_PARAMTYPE = "HuaweiDataPusher";
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static HuaweiDataSender _instance;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _instanceLock = new object();
        /// <summary>
        /// 数据库连接字符串，为了保证本类线程安全，不应当在任何非构造方法中修改该字符串
        /// </summary>
        private string _connStr;
        /// <summary>
        /// 系统配置
        /// </summary>
        private HuaweiPushDataConfig _config;
        /// <summary>
        /// 配置参数组名称
        /// </summary>
        private string _configName;

        private TXDPushData _txdPushData;

        /// <summary>
        /// 测试项参数配置表
        /// </summary>
        private DataTable _TestItemConfig;
        /// <summary>
        /// 测试项参数配置表DataView
        /// </summary>
        private DataView _dvTestItemConfig;

        /// <summary>
        /// TXD-HW标准故障代码对应表
        /// </summary>
        private Dictionary<string, string> _dicNCCode;

        private List<string> _HWNCCode;

        #region 属性
        /// <summary>
        /// 获取当前的唯一实例
        /// </summary>
        public static HuaweiDataSender Current
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
                            _instance = new HuaweiDataSender();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 配置参数
        /// </summary>
        public HuaweiPushDataConfig Config
        {
            get
            {
                //第一次检测实例是否已经存在，避免不必要的线程等待
                if (_config == null)
                {
                    _config = GetConfig(_configName);
                }
                return _config;
            }
        }

        #endregion 属性

        /// <summary>
        /// 构造函数
        /// </summary>
        private HuaweiDataSender()
        {
            _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["Base"].ToString();//从配置文件读取数据库连接字符串
            _configName = System.Configuration.ConfigurationManager.AppSettings["ConfigGroup"].ToString();
            _config = GetConfig(_configName);
            _txdPushData = new TXDPushData(_config);
            _TestItemConfig = GetTestItemConfig();
            _dicNCCode = GetNCCode();
            _HWNCCode = "显示贯穿竖线|显示横线、半截线、十字交叉线|黑屏|白屏颜色不均|灰阶颜色不均|显示整屏偏色|大视角色偏|像素亮点/暗点|黑屏亮点|白屏黑点|白屏白点、白斑|黑屏彩斑、碎亮点|白屏色斑|白屏亮度不均|灰阶亮度不均|显示纹路|背光划伤|Cell内划伤|Cell污渍|显示花屏|显示闪屏|液晶屏黑斑、漏液|柔性屏黑斑、封装裂|硬屏OLED紫斑异色、封装裂|Crosstalk|Flicker|周边漏光|LED死灯|白屏底部灯影（Hotspot）|白屏亮度偏低|背光组装偏位|SCF贴附偏位|屏幕玻璃裂|屏幕玻璃崩缺、凸缘|铁框平整度相关不良|Cell薄化外观不良|CG盖板裂|切割尺寸超标|弯折半径超标|COF损伤|FPC破损|FPC金手指不良|FPC连接器变形|FPC元器件不良|FPC翘曲|CG组装偏位|银浆点胶不良|Pad区点胶不良|一线胶点胶不良|背光点胶、粘胶外观不良|弯折区点胶不良|AF镀膜脱落|CG油墨脱落|CG表面划伤|CG油墨印刷不良|IC损伤|POL相关不良|背光焊接不良|贴合气泡|TP容值异常|Checksum Fail|RAMBist Fail|OTP不良|电流检测异常|COF Bonding AOI 偏位|COF Bonding AOI 粒子不良|COF Bonding AOI 异物|COF ACF不良|COG Bonding AOI 偏位|COG Bonding AOI 粒子不良|COG Bonding AOI 异物|COG ACF不良|FOG Bonding AOI 偏位|FOG Bonding AOI 粒子不良|FOG Bonding AOI 异物|FOG ACF不良|COP Bonding AOI 偏位|COP Bonding AOI 粒子不良|COP Bonding AOI 异物|COP ACF不良|FOF Bonding阻抗NG".Split('|').ToList<string>();
        }


        /// <summary>
        /// 读取系统配置
        /// </summary>
        /// <param name="configSuffix">配置组合名称（不同的产品编码使用不同的配置组合）</param>
        /// <returns></returns>
        public HuaweiPushDataConfig GetConfig(string configName)
        {
            HuaweiPushDataConfig ret = new HuaweiPushDataConfig();//返回值
            ret.GroupName = configName;
            //string sql = "SELECT Para_Name, Para_Value FROM Base_Parameters WHERE Para_Type = @type AND Para_Name LIKE @pn";
            string sql = "SELECT Para_Name, Para_Value FROM Base_Parameters WHERE Para_Type = @type AND Para_Group = @group";
            using (IDbConnection conn = new SqlConnection(_connStr))
            {
                var list = conn.Query<EntConfig>(sql, new { type = HUAWEI_PUSHER_PARAMTYPE, group = configName });
                foreach (EntConfig dr in list)
                {
                    //逐行读取数据
                    if (dr.Para_Name.ToString() == "DataType")
                    {
                        ret.DataType = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "SupplierName")
                    {
                        ret.SupplierName = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "FactoryName")
                    {
                        ret.FactoryName = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "AppKey")
                    {
                        ret.AppKey = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "TokenURL")
                    {
                        ret.TokenURL = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "TokenKey")
                    {
                        ret.TokenKey = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "TokenSecur")
                    {
                        ret.TokenSecur = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "ApiURL")
                    {
                        ret.ApiURL = dr.Para_Value.ToString();
                    }
                    else if (dr.Para_Name.ToString() == "CustomerNumber")
                    {
                        ret.CustomerNumber = dr.Para_Value.ToString();
                    }
                }
                //检查配置，发现有不存在的配置则报错
                StringBuilder lostConfig = new StringBuilder("配置组[" + configName + "]缺少以下配置项：\n");
                bool lackConfig = false;//是否缺少配置
                if (string.IsNullOrEmpty(ret.DataType))
                {
                    lackConfig = true;
                    lostConfig.Append("DataType").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.SupplierName))
                {
                    lackConfig = true;
                    lostConfig.Append("SupplierName").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.FactoryName))
                {
                    lackConfig = true;
                    lostConfig.Append("FactoryName").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.AppKey))
                {
                    lackConfig = true;
                    lostConfig.Append("AppKey").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.TokenURL))
                {
                    lackConfig = true;
                    lostConfig.Append("TokenURL").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.TokenKey))
                {
                    lackConfig = true;
                    lostConfig.Append("TokenKey").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.TokenSecur))
                {
                    lackConfig = true;
                    lostConfig.Append("TokenSecur").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.ApiURL))
                {
                    lackConfig = true;
                    lostConfig.Append("ApiURL").Append(", ");
                }
                if (string.IsNullOrEmpty(ret.CustomerNumber))
                {
                    lackConfig = true;
                    lostConfig.Append("CustomerNumber").Append(", ");
                }
                //汇总检测结果
                if (lackConfig)
                {
                    lostConfig.Remove(lostConfig.Length - 1, 1).AppendLine().Append("可能需要先进行配置设置。");
                    throw new HuaweiConfigExecption(lostConfig.ToString());
                }
            }
            return ret;
        }

        /// <summary>
        /// 保存系统配置
        /// </summary>
        /// <param name="configName">配置组名称</param>
        /// <param name="configdata">配置值</param>
        public void SaveConfig(string configName, HuaweiPushDataConfig configdata)
        {
            Dictionary<string, string> dataParse = new Dictionary<string, string>() 
            {
                {"DataType", configdata.DataType},
                {"SupplierName", configdata.SupplierName},
                {"FactoryName", configdata.FactoryName},
                {"AppKey", configdata.AppKey},
                {"TokenURL", configdata.TokenURL},
                {"TokenKey", configdata.TokenKey},
                {"TokenSecur", configdata.TokenSecur},
                {"ApiURL", configdata.ApiURL},
                {"CustomerNumber", configdata.CustomerNumber},
            };//组织数据

            //逐行更新数据
            foreach (var kv in dataParse)
            {
                string sql = "if not exists (select [GUID] from Base_Parameters where Para_Type = @pt And Para_Group = @pg And Para_Name = @pn) " +
                        "INSERT INTO Base_Parameters([GUID], Para_Type, Para_Group, Para_Name, Para_Value) VALUES(NEWID(),@pt,@pg,@pn,@pv) " +
                        "else update Base_Parameters set Para_Value = @pv where Para_Type = @pt And Para_Group = @pg And Para_Name = @pn";//如果该配置项已存在则更新，否则新增
                using (IDbConnection conn = new SqlConnection(_connStr))
                {
                    int res = conn.Execute(sql, new { pt = HUAWEI_PUSHER_PARAMTYPE, pg = configName, pn = kv.Key, pv = kv.Value });
                }
            }
        }

        /// <summary>
        /// 从数据库获取测试项信息配置表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTestItemConfig()
        {
            //实例化DataTable，用于装载查询结果集
            DataTable data = new DataTable();
            string sql = "SELECT * FROM HB_Product_TestItemConfig";
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

        /// <summary>
        /// 发送一批数据给华为
        /// </summary>
        /// <param name="Count">玻璃数</param>
        /// <param name="ProcessCode">工序编码</param>
        /// <returns></returns>
        public HuaweiPushResult PushData(int Count, string ProcessCode)
        {
            HuaweiPushResult ret = new HuaweiPushResult();
            List<EntData> tb = new List<EntData>();
            using (IDbConnection conn = new SqlConnection(_connStr))
            {
                // 从数据库中标无效数据
                int nDel = conn.Execute("UPDATE HB_Product_Parameter SET HPP_IsSend = 1 WHERE HPP_IsSend = 0 AND HPP_CusCode NOT IN (SELECT DISTINCT HPT_CustomerNumber FROM HB_Product_TestItemConfig)");
                
                string sql;
                if (string.IsNullOrEmpty(ProcessCode))
                {
                    sql = string.Format("SELECT TOP {0} * FROM HB_Product_Parameter WHERE HPP_IsSend = 0 ORDER BY HPP_AddDate", Count);
                }
                else
                {
                    sql = string.Format("SELECT TOP {0} * FROM HB_Product_Parameter " +
                    "WHERE HPP_SGXJobCode = '{1}' AND HPP_IsSend = 0 ORDER BY HPP_AddDate", Count, ProcessCode);
                }

                tb = conn.Query<EntData>(sql).ToList();
                int count = tb.Count;//本次获得的记录数
                if (count <= 0)
                {
                    ret.ResultCode = 0;
                    ret.ResultMessage = "成功";
                    conn.Close();
                    return ret;
                    //break;//没有数据了 结束
                }
                HuaweiPushData data = new HuaweiPushData();
                StringBuilder SendIds = new StringBuilder();//记录所有被发送的ID
                StringBuilder InvalidIds = new StringBuilder();//记录所有无效数据ID

                //组织数据给对象
                data.dataType = _config.DataType;
                data.appKey = _config.AppKey;
                data.tplcdQCVoList = new List<TplcdQCVoListItem>();

                for (int i = 0; i < count; i++)
                {
                    // 待发送数据行
                    EntData row = tb[i];

                    // 所有参数数组
                    string[] paraString = new string[]
                    {
                        row.HPP_Parameter1,
                        row.HPP_Parameter2,
                        row.HPP_Parameter3,
                        row.HPP_Parameter4,
                        row.HPP_Parameter5,
                        row.HPP_Parameter6,
                        row.HPP_Parameter7,
                        row.HPP_Parameter8,
                        row.HPP_Parameter9,
                        row.HPP_Parameter10
                    };

                    // 该行数据是否有效
                    bool isValidPara = false;

                    this._dvTestItemConfig = new DataView(this._TestItemConfig);
                    this._dvTestItemConfig.RowFilter = string.Format("HPT_SGXJobCode = '{0}' AND HPT_CustomerNumber = '{1}'", row.HPP_SGXJobCode, row.HPP_CusCode);
                    // 遍历该工序的所有测试子项
                    foreach (DataRowView subitem in this._dvTestItemConfig)
                    {
                        // 华为测试数据类
                        TplcdQCVoListItem tl = new TplcdQCVoListItem();

                        //HPT_ParameterIdx:参数序号
                        int idx = Convert.ToInt32(subitem["HPT_ParameterIdx"]);
                        // 测试值
                        if (subitem["HPT_ParameterPosition"] == null || subitem["HPT_ParameterPosition"].ToString() == "" || (!paraString[idx].Contains("|") && !paraString[idx].Contains(":")))
                        {
                            tl.testResult = paraString[idx];
                        }
                        else
                        {
                            try
                            {
                                string str = "";
                                string[] para = paraString[idx].Split(new char[] { '|', ':' });
                                string[] position = subitem["HPT_ParameterPosition"].ToString().Split(',');
                                if (position.Length == 1)
                                {
                                    str = para[Convert.ToInt32(position[0])];
                                }
                                else
                                {
                                    foreach (string pos in position)
                                    {
                                        if (str == "")
                                        {
                                            str = para[Convert.ToInt32(pos)];
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(para[Convert.ToInt32(pos)]) < Convert.ToInt32(str))
                                            {
                                                str = para[Convert.ToInt32(pos)];
                                            }
                                        }
                                    }
                                }
                                if (str.Trim() == "#") continue;
                                tl.testResult = str.Trim();
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error("华为待上传数据无效", ex);
                                // 无效数据，跳过
                                continue;
                            }
                        }//if (subitem["HPT_ParameterPosition"] == null ...

                        // 控制上限
                        tl.controlUpperLimit = subitem["HPT_ControlUpperLimit"].ToString();
                        // 控制下限
                        tl.controlLowerLimit = subitem["HPT_ControlLowerLimit"].ToString();

                        // 根据不同的测试工序，获取测试子项，测试值，测试结果,是否为验证模式
                        try
                        {
                            if (row.HPP_SGXJobCode == "011" || row.HPP_SGXJobCode == "031" || row.HPP_SGXJobCode == "034")
                            {
                                // LCD前测、复测AOI、FV2，人工检测站点的良品和不良品测试数据
                                switch (row.HPP_TestResult)
                                {
                                    case "PASS":
                                        // 测试子项
                                        tl.testSubItemName = "";
                                        // 测试值
                                        tl.testResult = "";
                                        // 测试结果
                                        tl.testCharResult = "PASS";
                                        break;

                                    case "FAIL":
                                        // 转换华为不良描述，生成测试子项和测试值
                                        if (this._dicNCCode.ContainsKey(tl.testResult))
                                        {
                                            // 测试子项，华为故障小类信息
                                            tl.testSubItemName = this._dicNCCode[tl.testResult];
                                            // 测试值
                                            tl.testResult = "";
                                            if (!_HWNCCode.Contains(tl.testSubItemName))
                                            {
                                                // 无效数据，跳过
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            // 无效数据，跳过
                                            continue;
                                        }
                                        // 测试结果
                                        tl.testCharResult = "FAIL";
                                        break;
                                }//switch (row.HPP_TestResult)

                                // 是否为验证模式
                                tl.isVerification = "人工测试数据";

                            }//if (row.HPP_SGXJobCode == "011" || row.HPP_SGXJobCode == "031" || row.HPP_SGXJobCode == "034")
                            else if (row.HPP_SGXJobCode == "080" || row.HPP_SGXJobCode == "082" || (row.HPP_SGXJobCode == "081" && subitem["HPT_TestItemName"].ToString() == "外观检测结果"))
                            {
                                // 封胶机良品和不良品测试数据
                                switch (row.HPP_TestResult)
                                {
                                    case "PASS":
                                        // 测试子项
                                        tl.testSubItemName = "";
                                        // 测试值
                                        tl.testResult = "";
                                        // 测试结果
                                        tl.testCharResult = "PASS";
                                        break;

                                    case "FAIL":
                                        if (string.IsNullOrEmpty(tl.testResult) || tl.testResult == "--") continue;
                                        // 测试子项
                                        tl.testSubItemName = subitem["HPT_TestSubItemName"].ToString();
                                        // 测试结果
                                        tl.testCharResult = "FAIL";
                                        break;
                                }//switch (row.HPP_TestResult)

                                // 是否为验证模式
                                tl.isVerification = "自动测试数据";

                            }//else if (row.HPP_SGXJobCode == "080")
                            else
                            {
                                // 机台测试数据
                                if (string.IsNullOrEmpty(tl.testResult) || tl.testResult == "--" || tl.testResult == "#" || (subitem["HPT_TestStation"].ToString() == "Panel切割" && tl.testResult == "-1")) continue;
                                // 测试子项
                                tl.testSubItemName = subitem["HPT_TestSubItemName"].ToString();
                                // 判断上下限，生成测试结果
                                if (string.IsNullOrEmpty(tl.controlUpperLimit) && string.IsNullOrEmpty(tl.controlLowerLimit))//未设置上下限
                                {
                                    // 测试结果
                                    tl.testCharResult = row.HPP_TestResult;
                                }
                                else if (!string.IsNullOrEmpty(tl.controlUpperLimit) && string.IsNullOrEmpty(tl.controlLowerLimit))//仅设置上限
                                {
                                    if (Convert.ToSingle(tl.testResult) <= Convert.ToSingle(tl.controlUpperLimit))
                                    {
                                        // 测试结果
                                        tl.testCharResult = "PASS";
                                    }
                                    else
                                    {
                                        // 测试结果
                                        tl.testCharResult = "FAIL";
                                    }
                                }
                                else if (string.IsNullOrEmpty(tl.controlUpperLimit) && !string.IsNullOrEmpty(tl.controlLowerLimit))//仅设置下限
                                {
                                    if (Convert.ToSingle(tl.testResult) >= Convert.ToSingle(tl.controlLowerLimit))
                                    {
                                        // 测试结果
                                        tl.testCharResult = "PASS";
                                    }
                                    else
                                    {
                                        // 测试结果
                                        tl.testCharResult = "FAIL";
                                    }
                                }
                                else if (!string.IsNullOrEmpty(tl.controlUpperLimit) && !string.IsNullOrEmpty(tl.controlLowerLimit))//同时设置上下限
                                {
                                    if ((Convert.ToSingle(tl.testResult) >= Convert.ToSingle(tl.controlLowerLimit)) && (Convert.ToSingle(tl.testResult) <= Convert.ToSingle(tl.controlUpperLimit)))
                                    {
                                        // 测试结果
                                        tl.testCharResult = "PASS";
                                    }
                                    else
                                    {
                                        // 测试结果
                                        tl.testCharResult = "FAIL";
                                    }
                                }

                                // 是否为验证模式
                                tl.isVerification = "自动测试数据";
                            }//else
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("华为待上传数据无效", ex);
                            // 无效数据，跳过
                            continue;
                        }

                        //填写必填项
                        // 华为编码
                        tl.customerNumber = row.HPP_CusCode;
                        // 供应商
                        tl.supplierName = _config.SupplierName;
                        // 厂别
                        tl.factoryName = _config.FactoryName;
                        // 客户型号
                        tl.customerModel = subitem["HPT_CustomerModel"].ToString();
                        //switch (row.HPP_CusCode)
                        //{
                        //    case "23020730":
                        //        tl.customerModel = "Dubai";
                        //        break;
                        //    case "23020712":
                        //        tl.customerModel = "Jakarta";
                        //        break;
                        //    case "23020737":
                        //        tl.customerModel = "Madirid";
                        //        break;
                        //}
                        // lot号
                        tl.lotNumber = row.HPP_LotCode;
                        // 序列号
                        if (string.IsNullOrEmpty(row.HPP_LCD))
                        {
                            tl.serialNumber = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            tl.serialNumber = row.HPP_LCD;
                        }
                        // 供应商编码
                        tl.productNumber = row.HPP_SMCode;
                        // 生产线
                        tl.line = row.HPP_SPLJobCode;
                        // 测试时间戳
                        tl.qcTime = GetTimeStamp(Convert.ToDateTime(row.HPP_AddDate));
                        // 生产工序/测试工站
                        tl.testStation = subitem["HPT_TestStation"].ToString();
                        // 测试项目
                        tl.testItemName = subitem["HPT_TestItemName"].ToString();
                        // 测试单位
                        tl.testUnit = subitem["HPT_TestUnit"].ToString();
                        // 测试组ID
                        tl.rawDataID = tl.customerNumber + "_" + tl.supplierName + "_" + tl.factoryName + "_" + tl.line + "_" + tl.qcTime + "_" + tl.testItemName;
                        // 备注
                        tl.remark = (row.HPP_TestRemark == null ? "" : row.HPP_TestRemark);

                        data.tplcdQCVoList.Add(tl);

                        // 数据有效
                        isValidPara = true;
                    }//foreach (DataRowView subitem in this._dvTestItemConfig)

                    // 无效数据处理
                    if (!isValidPara)
                    {
                        //// 从数据库中删除无效数据
                        //string sqlDel = string.Format("DELETE FROM HB_Product_Parameter WHERE HPP_Guid = '{0}'", row.HPP_Guid);
                        //conn.Execute(sqlDel);
                        InvalidIds.Append("'").Append(row.HPP_Guid).Append("',");//记录无效数据Id
                        continue;
                    }

                    // 记录有效数据ID
                    SendIds.Append("'").Append(row.HPP_Guid).Append("',");//记录Id
                }//for (int i = 0; i < count; i++)

                if (data.tplcdQCVoList.Count == 0)
                {
                    // 标记无效数据
                    if (InvalidIds.ToString().TrimEnd(',') != "")
                    {
                        string sqlDel = string.Format("UPDATE HB_Product_Parameter SET HPP_IsSend = 1 WHERE HPP_Guid IN ({0})", InvalidIds.ToString().TrimEnd(','));
                        conn.Execute(sqlDel, null);
                    }
                    ret.ResultCode = 1;
                    ret.ResultMessage = string.Format("该{0}行数据中不包含任何有效参数", count);
                }
                else
                {
                    // 待发送Json字符串
                    string toSend = JsonConvert.SerializeObject(data);
                    // 添加日志
                    LogHelper.Info(toSend);

                    // 记录发送时间
                    DateTime sendTime = DateTime.Now;
                    // 发送数据并获取返回值
                    string res = _txdPushData.sendPushData(toSend);
                    // 添加日志
                    LogHelper.Info(res);

                    // 发送结果
                    PushResult pr = JsonConvert.DeserializeObject<PushResult>(res);
                    if (!pr.status)
                    {
                        //服务器返回错误
                        ret.ResultCode = 1;
                        ret.ResultMessage = "发送已发送数据时出错，华为接口返回：" + res;
                        //break;
                    }
                    else
                    {
                        ret.SuccessedCount += data.tplcdQCVoList.Count;
                        ret.SentTimes++;
                        conn.Open();
                        IDbTransaction trans = conn.BeginTransaction();//启用事务，如果事务失败将会导致后续给华为发送重复的数据，所以当此处事务失败时报错并停止运行
                        ////将本次发送成功的记录转入历史表
                        //string sqlHist = "INSERT INTO HB_Product_Parameter_History SELECT * FROM HB_Product_Parameter WHERE HPP_Guid IN (" + SendIds.ToString().TrimEnd(',') + ")";
                        ////_conn.ExecuteAction(sqlHist, trans, "Base");
                        //conn.Execute(sqlHist, null, trans);

                        List<HuaweiUpdate> lstHuaweiUpdate = new List<HuaweiUpdate>();
                        foreach (TplcdQCVoListItem tl in data.tplcdQCVoList)
                        {
                            HuaweiUpdate HuaweiUpdateData = new HuaweiUpdate()
                            {
                                HW_Guid = Guid.NewGuid(),
                                HW_CustomerNumber = tl.customerNumber,
                                HW_SupplierName = tl.supplierName,
                                HW_FactoryName = tl.factoryName,
                                HW_CustomerModel = tl.customerModel,
                                HW_LotNumber = tl.lotNumber,
                                HW_SerialNumber = tl.serialNumber,
                                HW_ProductNumber = tl.productNumber,
                                HW_Line = tl.line,
                                HW_QcTime = tl.qcTime,
                                HW_TestStation = tl.testStation,
                                HW_TestItemName = tl.testItemName,
                                HW_TestSubItemName = tl.testSubItemName,
                                HW_ControlUpperLimit = tl.controlUpperLimit,
                                HW_ControlLowerLimit = tl.controlLowerLimit,
                                HW_TestUnit = tl.testUnit,
                                HW_TestResult = tl.testResult,
                                HW_TestCharResult = tl.testCharResult,
                                HW_IsVerification = tl.isVerification,
                                HW_RawDataID = tl.rawDataID,
                                HW_Remark  = tl.remark,
                                HW_UpdateTime = sendTime
                            };

                            lstHuaweiUpdate.Add(HuaweiUpdateData);
                        }

                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("INSERT INTO TXD_HuaweiUpdate(");
                        strSql.Append("HW_Guid,HW_CustomerNumber,HW_SupplierName,HW_FactoryName,HW_CustomerModel,HW_LotNumber,HW_SerialNumber,HW_ProductNumber,HW_Line,HW_QcTime,HW_TestStation,HW_TestItemName,HW_TestSubItemName,HW_ControlUpperLimit,HW_ControlLowerLimit,HW_TestUnit,HW_TestResult,HW_TestCharResult,HW_IsVerification,HW_RawDataID,HW_Remark,HW_UpdateTime)");
                        strSql.Append(" VALUES (");
                        strSql.Append("@HW_Guid,@HW_CustomerNumber,@HW_SupplierName,@HW_FactoryName,@HW_CustomerModel,@HW_LotNumber,@HW_SerialNumber,@HW_ProductNumber,@HW_Line,@HW_QcTime,@HW_TestStation,@HW_TestItemName,@HW_TestSubItemName,@HW_ControlUpperLimit,@HW_ControlLowerLimit,@HW_TestUnit,@HW_TestResult,@HW_TestCharResult,@HW_IsVerification,@HW_RawDataID,@HW_Remark,@HW_UpdateTime)");

                        conn.Execute(strSql.ToString(), lstHuaweiUpdate, trans);

                        ////将本次发送成功的记录从表中删除
                        //string sqlDel = "DELETE FROM HB_Product_Parameter WHERE HPP_Guid IN (" + SendIds.ToString().TrimEnd(',') + ")";
                        //conn.Execute(sqlDel, null, trans);

                        // 标记已发送数据
                        if (SendIds.ToString().TrimEnd(',') != "")
                        {
                            string sqlDel = string.Format("UPDATE HB_Product_Parameter SET HPP_IsSend = 1, HPP_SendDate = '{0}' WHERE HPP_Guid IN ({1})", sendTime.ToString("yyyy-MM-dd HH:mm:ss"), SendIds.ToString().TrimEnd(','));
                            conn.Execute(sqlDel, null, trans);
                        }

                        // 标记无效数据
                        if (InvalidIds.ToString().TrimEnd(',') != "")
                        {
                            string sqlDel = string.Format("UPDATE HB_Product_Parameter SET HPP_IsSend = 1 WHERE HPP_Guid IN ({0})", InvalidIds.ToString().TrimEnd(','));
                            conn.Execute(sqlDel, null, trans);
                        }

                        try
                        {
                            trans.Commit();
                            ret.ResultCode = 0;
                            ret.ResultMessage = "成功";
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            ret.ResultCode = 2;
                            ret.ResultMessage = "归档已发送数据时出错，请检查数据库连接";
                            //break;//停止继续运行
                        }
                    }//if (!pr.status)
                }// if (data.tplcdQCVoList.Count == 0)
            }//using (IDbConnection conn = new SqlConnection(_connStr))
            return ret;
        }

        /// <summary>
        /// 获取TXD-HW标准故障代码对应表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetNCCode()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT BNC_cName,BNC_Note FROM Basal_NCCode ");
            strSql.Append("WHERE BNC_AddDate > '2019-05-05 00:00:00.000' ");

            //实例化DataTable，用于装载查询结果集
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                using (SqlCommand command = new SqlCommand(strSql.ToString(), connection))
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

            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow row in data.Rows)
            {
                string name = row["BNC_cName"].ToString();
                string note = row["BNC_Note"].ToString();

                if (!dic.ContainsKey(name))
                {
                    dic.Add(name, note);
                }
            }

            return dic;

            //using (IDbConnection conn = new SqlConnection(_connStr))
            //{
            //    var dict = conn.Query(strSql.ToString()).ToDictionary(
            //        row => (string)row.BNC_cName,
            //        row => (string)row.BNC_Note);
            //    return dict;
            //}
        }
        
        ///// <summary>
        ///// 获取时间戳
        ///// </summary>
        ///// <returns></returns>
        //public string GetTimeStamp(DateTime dt)
        //{
        //    TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    return Convert.ToInt64(ts.TotalSeconds).ToString();
        //}

        /// <summary>
        /// Time转换为实际戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            //long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位   
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long t = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
            return t;
        }

        /// <summary>
        /// JavaScript时间戳转换为C# DateTime
        /// </summary>
        /// <param name="TimeStamp">13位时间戳字符串</param>
        /// <returns></returns>
        public static DateTime ConvertTimeStampToDateTime(string TimeStamp)
        {
            long jsTimeStamp = Convert.ToInt64(TimeStamp); ;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(jsTimeStamp);
            return dt;
        }
    }

    /// <summary>
    /// 自定义异常类型，用于抛出指定错误
    /// </summary>
    public class HuaweiConfigExecption : Exception
    {
        public HuaweiConfigExecption(string message) : base(message) { }
    }
}
