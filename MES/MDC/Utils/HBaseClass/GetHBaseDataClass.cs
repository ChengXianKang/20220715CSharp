using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Data;
using Utils.Model;

namespace Utils.HBaseClass
{
    public class GetHBaseDataClass
    {
        //data_01 扫码关联表 {"tpCode":"","productLineCode":"01","glassCode":"T-GZ180629-2151-9199","fpcCode":"","backCode":"","productionOrder":"S1807190008"}
        //data_02 生产总表
        //data_03 物料总表
        //data_04 生产Lot表
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static GetHBaseDataClass _instance;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _instanceLock = new object();
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();


        /// <summary>
        /// 获取当前的唯一实例
        /// </summary>
        public static GetHBaseDataClass Instance
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
                            _instance = new GetHBaseDataClass();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 重连HBase数据库
        /// </summary>
        public void Reconnect()
        {
            GetDataFromHBase.Instance.OpenConnect();
        }

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
                throw ex;
            }
            return ProcessName;
        }
        #endregion

        ///// <summary>
        ///// 获取单片玻璃追溯信息
        ///// </summary>
        ///// <param name="CheckCode">查询的编码</param>
        ///// <returns></returns>
        ////string productRowKey, productRowValue, glassRowKey, glassRowValue, glassCode;
        //public DataSet GetDataTableBySingleGlass(string searchGlassCode)
        //{
        //    try
        //    {
        //        #region  初始化过站工序信息
        //        DataSet dataSet = new DataSet();
        //        DataTable parameterTable = new DataTable("parameterTable");
        //        DataTable productTable = new DataTable("productTable");

        //        dataSet.Tables.Add(productTable);
        //        dataSet.Tables.Add(parameterTable);

        //        productTable.Columns.Add("product_ProductOrder");//生产订单编码
        //        productTable.Columns.Add("product_destroyOrder");//销售订单编码
        //        productTable.Columns.Add("product_LineTid");//产线ID
        //        productTable.Columns.Add("product_LineCode");//产线编码
        //        productTable.Columns.Add("product_LineName");//产线名称
        //        productTable.Columns.Add("product_finishesCode");//成品的编码
        //        productTable.Columns.Add("product_finishesModel");//成品型号
        //        productTable.Columns.Add("product_finishesProductNo");//成品的料号
        //        productTable.Columns.Add("product_clientProductNo");//客户料号
        //        productTable.Columns.Add("product_reworkFlag");//是否重工解绑

        //        parameterTable.Columns.Add("parameter_RowKey");//RowKey
        //        parameterTable.Columns.Add("parameter_ProcessTid");//工序ID
        //        parameterTable.Columns.Add("parameter_ProcessCode");//工序编码
        //        parameterTable.Columns.Add("parameter_ProcessName");//工序名称
        //        parameterTable.Columns.Add("parameter_ProcessNumber", typeof(int));//工序序号
        //        parameterTable.Columns.Add("parameter_CardTid");//机台ID
        //        parameterTable.Columns.Add("parameter_CardIP");//机台IP  
        //        parameterTable.Columns.Add("parameter_CardName");//机台名称
        //        parameterTable.Columns.Add("parameter_ScanIP");//扫描IP
        //        parameterTable.Columns.Add("parameter_ScanOP");//扫描人员
        //        parameterTable.Columns.Add("parameter_ScanNumber");//扫描人员
        //        parameterTable.Columns.Add("parameter_MouldNumber");//治具编码
        //        parameterTable.Columns.Add("parameter_MouldName");//治具名称
        //        parameterTable.Columns.Add("parameter_EventCode");//机台事件
        //        parameterTable.Columns.Add("parameter_deviceOP");//机台负责人
        //        parameterTable.Columns.Add("parameter_LineCode");//产线编码
        //        parameterTable.Columns.Add("parameter_LotNo");//生产批次号
        //        parameterTable.Columns.Add("parameter_DateTime");//采集时间
        //        parameterTable.Columns.Add("parameter_trackNumber");//扫描码
        //        parameterTable.Columns.Add("parameter_oppositeNumber");//对应码
        //        parameterTable.Columns.Add("parameter_bfFlag");//是否补扫
        //        parameterTable.Columns.Add("parameter_exceptionState");//是否异常
        //        parameterTable.Columns.Add("parameter_exceptionContent");//异常原因
        //        parameterTable.Columns.Add("parameter_deviceCallState");//是否报警
        //        parameterTable.Columns.Add("parameter_deviceCallContent");//报警内容
        //        parameterTable.Columns.Add("parameter_Type");//参数类型
        //        parameterTable.Columns.Add("parameter_Value");//参数值
        //        parameterTable.Columns.Add("parameter_Unit");//参数单位
        //        #endregion
        //        //先用查询编码去扫描关联表里面找到对应的玻璃编码（data_01）
        //        List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchGlassCode, "data_01");
        //        string glassCode = "";
        //        foreach (var glassKey in glassRowResult)
        //        {
        //            //获取rowKey
        //            string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
        //            foreach (var key in glassKey.Columns)
        //            {
        //                try
        //                {
        //                    //获取rowValue
        //                    string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
        //                    //反序列化rowValue
        //                    JObject jsonStr = JObject.Parse(glassRowValue);
        //                    //获取里面的玻璃编码
        //                    glassCode = jsonStr["glassCode"].ToString();
        //                }
        //                catch (Exception exp)
        //                {
        //                    LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message, exp);
        //                }
        //            }
        //        }
        //        //获取到了玻璃编码之后需要去机台生产信息表查询详细信息
        //        if (string.IsNullOrEmpty(glassCode))
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            #region  获取当前玻璃重工信息
        //            string reworkFlag = "否";
        //            string sql = string.Format("SELECT TOP 1 * FROM HB_Product_RepeatLog WITH(NOLOCK) WHERE PRL_GlassCode='{0}' ORDER BY PRL_Tid DESC", glassCode);
        //            DataView dv = conn.ExecuteDataView(sql, YJ.Config.UserConfig.GetDbName());
        //            if (dv.Count > 0)
        //            {
        //                reworkFlag = "是";
        //            }
        //            #endregion
        //            string SearchGlass = "$G" + glassCode;
        //            List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(SearchGlass, "data_02");
        //            foreach (var productKey in ProductRowResult)
        //            {
        //                //获取rowKey
        //                string productRowKey = Encoding.UTF8.GetString(productKey.Row);
        //                foreach (var key in productKey.Columns)
        //                {
        //                    try
        //                    {
        //                        //获取rowValue
        //                        string productRowValue = Encoding.UTF8.GetString(key.Value.Value);
        //                        //初始化采集表的数据
        //                        //反序列化rowValue
        //                        JObject jsonStr = JObject.Parse(productRowValue);
        //                        string processId = jsonStr["processId"].ToString();//工序ID
        //                        string processCode = jsonStr["processCode"].ToString();//工序编码
        //                        string processName = jsonStr["processName"].ToString();//工序名称
        //                        int processNumber = Convert.ToInt32(jsonStr["processNumber"].ToString());//工序序号
        //                        string deviceId = jsonStr["deviceId"].ToString(); //机台ID
        //                        string deviceIp = jsonStr["deviceIp"].ToString(); //机台IP
        //                        string deviceName = jsonStr["deviceName"].ToString();//机台名称
        //                        string scanIP = jsonStr["scanIp"].ToString(); //扫描IP
        //                        string scanNumber = jsonStr["scanNumber"].ToString();//扫描人编码
        //                        string scanOP = jsonStr["scanOP"].ToString();//扫描人员
        //                        string mouldNumber = jsonStr["mouldNumber"].ToString();//治具编码
        //                        string mouldName = jsonStr["mouldName"].ToString();//治具名称
        //                        string deviceEventCode = jsonStr["deviceEventCode"].ToString(); //机台事件
        //                        string deviceOP = jsonStr["deviceOP"].ToString() == "" ? jsonStr["scanOP"].ToString() : jsonStr["deviceOP"].ToString(); //jsonStr["scanOP"].ToString(); //机台负责人
        //                        string productLineId = jsonStr["productLineId"].ToString(); //产线ID
        //                        string productLineCode = jsonStr["productLineCode"].ToString(); //产线编码
        //                        string productLineName = jsonStr["productLineName"].ToString(); //产线名称
        //                        string collectionTime = jsonStr["InTime"].ToString(); //采集时间
        //                        string trackNumber = jsonStr["trackNumber"].ToString(); //扫描码
        //                        string oppositeNumber = jsonStr["oppositeNumber"].ToString(); //对应码
        //                        string productionOrder = jsonStr["productionOrder"].ToString(); //生产订单编码
        //                        string destroyOrder = jsonStr["destroyOrder"].ToString(); //销售订单编码
        //                        string finishesCode = (jsonStr["finishesCode"] ?? "").ToString();// jsonStr["finishesCode"].ToString(); //成品的编码
        //                        string finishesModel = (jsonStr["finishesModel"] ?? "").ToString() == "" ? jsonStr["destroyOrder"].ToString() : jsonStr["finishesModel"].ToString();//jsonStr["finishesModel"].ToString(); //成品型号
        //                        string finishesProductNo = (jsonStr["finishesProductNo"] ?? "").ToString();//jsonStr["finishesProductNo"].ToString(); //成品的料号
        //                        string clientProductNo = (jsonStr["clientProductNo"] ?? "").ToString();//jsonStr["clientProductNo"].ToString(); //客户料号
        //                        string exceptionState = jsonStr["exceptionState"].ToString() == "0" ? "否" : "是"; //是否异常  
        //                        string exceptionContent = jsonStr["exceptionContent"].ToString().Replace('#', ' '); //异常原因    
        //                        string deviceCallState = jsonStr["deviceCallState"].ToString() == "0" ? "否" : "是"; //设备报警  
        //                        string deviceCallContent = jsonStr["deviceCallContent"].ToString(); //设备报警内容     
        //                        string lotNumber = jsonStr["lotNumber"].ToString(); //生产批次号
        //                        string bfFlag = (jsonStr["bfFlag"] ?? "0").ToString() == "1" ? "是" : "否"; //是否补扫

        //                        if (productTable.Rows.Count <= 0)
        //                        {
        //                            DataRow productRow = productTable.NewRow();
        //                            productRow["product_ProductOrder"] = productionOrder;//生产订单
        //                            productRow["product_destroyOrder"] = destroyOrder;//销售订单
        //                            productRow["product_LineTid"] = productLineId;//产线ID
        //                            productRow["product_LineCode"] = productLineCode; //产线编码
        //                            productRow["product_LineName"] = productLineName; //产线名称

        //                            productRow["product_finishesCode"] = finishesCode;//成品的编码
        //                            productRow["product_finishesModel"] = finishesModel;//成品型号
        //                            productRow["product_finishesProductNo"] = finishesProductNo;//成品的料号
        //                            productRow["product_clientProductNo"] = clientProductNo;//客户料号
        //                            productRow["product_reworkFlag"] = reworkFlag;//是否重工解绑
        //                            productTable.Rows.Add(productRow);
        //                        }
        //                        var parameterJson = jsonStr["Parameter"].AsEnumerable();
        //                        foreach (var parameterItem in parameterJson)
        //                        {
        //                            //更新实际采集表的数据
        //                            DataRow parameterRow = parameterTable.NewRow();
        //                            parameterRow["parameter_RowKey"] = productRowKey;
        //                            parameterRow["parameter_ProcessTid"] = processId;//工序ID
        //                            parameterRow["parameter_ProcessCode"] = processCode;//工序编码
        //                            parameterRow["parameter_ProcessName"] = processName;//工序名称
        //                            parameterRow["parameter_processNumber"] = processNumber;//工序序号
        //                            parameterRow["parameter_CardTid"] = deviceId; //机台ID
        //                            parameterRow["parameter_CardIP"] = deviceIp; //机台IP
        //                            parameterRow["parameter_CardName"] = deviceName;//机台名称
        //                            parameterRow["parameter_ScanIP"] = scanIP; //扫描IP
        //                            parameterRow["parameter_ScanNumber"] = scanNumber;//扫描人编码
        //                            parameterRow["parameter_ScanOP"] = scanOP;//扫描人员
        //                            parameterRow["parameter_MouldNumber"] = mouldNumber;//治具编码
        //                            parameterRow["parameter_MouldName"] = mouldName;//治具名称
        //                            parameterRow["parameter_EventCode"] = deviceEventCode; //机台事件
        //                            parameterRow["parameter_deviceOP"] = deviceOP; //机台负责人
        //                            parameterRow["parameter_LineCode"] = productLineCode; //产线编码
        //                            parameterRow["parameter_DateTime"] = collectionTime; //采集时间
        //                            parameterRow["parameter_trackNumber"] = trackNumber; //扫描码
        //                            parameterRow["parameter_oppositeNumber"] = oppositeNumber; //对应码
        //                            parameterRow["parameter_LotNo"] = lotNumber.Substring(0, lotNumber.Length - 1); //生产批次号
        //                            parameterRow["parameter_bfFlag"] = bfFlag; //是否补扫
        //                            parameterRow["parameter_exceptionState"] = exceptionState; //是否异常
        //                            parameterRow["parameter_exceptionContent"] = exceptionContent; //异常内容
        //                            parameterRow["parameter_deviceCallState"] = deviceCallState; //是否报警
        //                            parameterRow["parameter_deviceCallContent"] = deviceCallContent; //报警内容
        //                            parameterRow["parameter_Type"] = parameterItem["parameterType"].ToString(); //参数类型
        //                            parameterRow["parameter_Value"] = parameterItem["parameterValue"].ToString(); //参数值
        //                            parameterRow["parameter_Unit"] = parameterItem["parameterUnit"].ToString(); //参数单位
        //                            parameterTable.Rows.Add(parameterRow);
        //                        }
        //                    }
        //                    catch (Exception exp)
        //                    {
        //                        LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message, exp);
        //                    }
        //                }
        //            }
        //            return dataSet;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        LogHelper.Error("rowKey范围查询失败.|" + exp.Message, exp);
        //        Reconnect();
        //        return null;
        //    }
        //}


        ///// <summary>
        /////单片玻璃信息
        ///// </summary>
        ///// <param name="CheckCode">查询的编码</param>
        ///// <returns></returns>
        ////string productRowKey, productRowValue, glassRowKey, glassRowValue, glassCode;
        //public DataSet newGetDataTableBySingleGlass(string searchGlassCode)
        //{
        //    DataSet dataSet = new DataSet();
        //    try
        //    {
        //        #region  初始化工序与原材料表
        //        DataTable materialTable = new DataTable("materialTable");
        //        DataTable parameterTable = new DataTable("parameterTable");
        //        DataTable productTable = new DataTable("productTable");

        //        dataSet.Tables.Add(productTable);
        //        dataSet.Tables.Add(materialTable);
        //        dataSet.Tables.Add(parameterTable);

        //        productTable.Columns.Add("product_ProductOrder");//生产订单编码
        //        productTable.Columns.Add("product_destroyOrder");//销售订单编码
        //        productTable.Columns.Add("product_LineTid");//产线ID
        //        productTable.Columns.Add("product_LineCode");//产线编码
        //        productTable.Columns.Add("product_LineName");//产线名称
        //        productTable.Columns.Add("product_finishesCode");//成品的编码
        //        productTable.Columns.Add("product_finishesModel");//成品型号
        //        productTable.Columns.Add("product_finishesProductNo");//成品的料号
        //        productTable.Columns.Add("product_clientProductNo");//客户料号
        //        productTable.Columns.Add("product_reworkFlag");//是否重工

        //        materialTable.Columns.Add("material_ProcessTid");//工序ID
        //        materialTable.Columns.Add("material_ProcessCode");//工序编码
        //        materialTable.Columns.Add("material_ProcessName");//工序名称
        //        materialTable.Columns.Add("material_CardTid");//机台ID
        //        materialTable.Columns.Add("material_CardIP");//机台IP
        //        materialTable.Columns.Add("material_CardName");//机台名称
        //        materialTable.Columns.Add("material_ScanIP");//扫描IP
        //        materialTable.Columns.Add("material_ScanOP");//扫描人编码
        //        materialTable.Columns.Add("material_ScanNumber");//扫描人员
        //        materialTable.Columns.Add("material_MouldNumber");//治具编码
        //        materialTable.Columns.Add("material_MouldName");//治具名称
        //        materialTable.Columns.Add("material_LineCode");//产线编码
        //        materialTable.Columns.Add("material_Code"); //原料编码
        //        materialTable.Columns.Add("material_Name"); //原料名称
        //        materialTable.Columns.Add("material_Spec");//原料型号
        //        materialTable.Columns.Add("material_Batch");//原料入库批次
        //        materialTable.Columns.Add("material_Vonder");//原料供应商
        //        materialTable.Columns.Add("material_SCDate");//原料来料生产日期
        //        materialTable.Columns.Add("material_BoxCode");//原料入库纸箱编码
        //        materialTable.Columns.Add("material_FeedingOP");//上料人

        //        parameterTable.Columns.Add("parameter_RowKey");//RowKey
        //        parameterTable.Columns.Add("parameter_ProcessTid");//工序ID
        //        parameterTable.Columns.Add("parameter_ProcessCode");//工序编码
        //        parameterTable.Columns.Add("parameter_ProcessName");//工序名称
        //        parameterTable.Columns.Add("parameter_ProcessNumber");//工序序号
        //        parameterTable.Columns.Add("parameter_CardTid");//机台ID
        //        parameterTable.Columns.Add("parameter_CardIP");//机台IP  
        //        parameterTable.Columns.Add("parameter_CardName");//机台名称
        //        parameterTable.Columns.Add("parameter_ScanIP");//扫描IP
        //        parameterTable.Columns.Add("parameter_ScanOP");//扫描人员
        //        parameterTable.Columns.Add("parameter_ScanNumber");//扫描人员
        //        parameterTable.Columns.Add("parameter_MouldNumber");//治具编码
        //        parameterTable.Columns.Add("parameter_MouldName");//治具名称
        //        parameterTable.Columns.Add("parameter_EventCode");//机台事件
        //        parameterTable.Columns.Add("parameter_deviceOP");//机台负责人
        //        parameterTable.Columns.Add("parameter_LineCode");//产线编码

        //        parameterTable.Columns.Add("parameter_LotNo");//生产批次号
        //        parameterTable.Columns.Add("parameter_DateTime");//采集时间
        //        parameterTable.Columns.Add("parameter_trackNumber");//扫描码
        //        parameterTable.Columns.Add("parameter_oppositeNumber");//对应码
        //        parameterTable.Columns.Add("parameter_bfFlag");//是否补扫
        //        parameterTable.Columns.Add("parameter_exceptionState");//是否异常
        //        parameterTable.Columns.Add("parameter_exceptionContent");//异常原因
        //        parameterTable.Columns.Add("parameter_deviceCallState");//是否报警
        //        parameterTable.Columns.Add("parameter_deviceCallContent");//报警内容
        //        parameterTable.Columns.Add("parameter_Type");//参数类型
        //        parameterTable.Columns.Add("parameter_Value");//参数值
        //        parameterTable.Columns.Add("parameter_Unit");//参数单位
        //        #endregion
        //        //先用查询编码去扫描关联表里面找到对应的玻璃编码（data_01）
        //        List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchGlassCode, "data_01");
        //        //List<TRowResult> glassRowResult = gdfh.GetRowsWithPrefix("$G" + searchGlassCode, "exception_01");
        //        //YJ.Log.FileLog.log.Debug("扫码关联表查询：" + searchGlassCode + "返回的数据共 " + glassRowResult.Count.ToString() + "条。解析开始");
        //        string glassCode = "";
        //        foreach (var glassKey in glassRowResult)
        //        {
        //            //获取rowKey
        //            string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
        //            foreach (var key in glassKey.Columns)
        //            {
        //                try
        //                {
        //                    //获取rowValue
        //                    string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
        //                    //反序列化rowValue
        //                    JObject jsonStr = JObject.Parse(glassRowValue);
        //                    //获取里面的玻璃编码
        //                    glassCode = jsonStr["glassCode"].ToString();
        //                    //YJ.Log.FileLog.log.Debug("玻璃号" + jsonStr["glassCode"].ToString() + "   生产订单编码" + jsonStr["productionOrder"].ToString() + "   产线编码" + jsonStr["productLineCode"].ToString() + "------Json" + glassRowValue);
        //                }
        //                catch (Exception exp)
        //                {
        //                    LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message.ToString(), exp);
        //                }
        //            }
        //        }
        //        //获取到了玻璃编码之后需要去机台生产信息表查询详细信息
        //        if (string.IsNullOrEmpty(glassCode))
        //        {
        //            return null;
        //        }
        //        else
        //        {
        //            #region  获取当前玻璃重工信息
        //            string reworkFlag = "否";
        //            string sql = string.Format("SELECT TOP 1 * from HB_Product_RepeatLog WHERE PRL_GlassCode='{0}' ORDER BY PRL_Tid DESC", glassCode);
        //            DataView dv = conn.ExecuteDataView(sql, YJ.Config.UserConfig.GetDbName());
        //            if (dv.Count > 0)
        //            {
        //                reworkFlag = "是";
        //            }
        //            #endregion
        //            string SearchGlass = "$G" + glassCode;
        //            List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(SearchGlass, "data_02");
        //            foreach (var productKey in ProductRowResult)
        //            {
        //                //获取rowKey
        //                string productRowKey = Encoding.UTF8.GetString(productKey.Row);
        //                foreach (var key in productKey.Columns)
        //                {
        //                    try
        //                    {
        //                        //获取rowValue
        //                        string productRowValue = Encoding.UTF8.GetString(key.Value.Value);
        //                        //初始化采集表的数据
        //                        //反序列化rowValue
        //                        JObject jsonStr = JObject.Parse(productRowValue);
        //                        string processId = jsonStr["processId"].ToString();//工序ID
        //                        string processCode = jsonStr["processCode"].ToString();//工序编码
        //                        string processName = jsonStr["processName"].ToString();//工序名称
        //                        string processNumber = jsonStr["processNumber"].ToString();//工序序号
        //                        string deviceId = jsonStr["deviceId"].ToString(); //机台ID
        //                        string deviceIp = jsonStr["deviceIp"].ToString(); //机台IP
        //                        string deviceName = jsonStr["deviceName"].ToString();//机台名称
        //                        string scanIP = jsonStr["scanIp"].ToString(); //扫描IP
        //                        string scanNumber = jsonStr["scanNumber"].ToString();//扫描人编码
        //                        string scanOP = jsonStr["scanOP"].ToString();//扫描人员
        //                        string mouldNumber = jsonStr["mouldNumber"].ToString();//治具编码
        //                        string mouldName = jsonStr["mouldName"].ToString();//治具名称
        //                        string deviceEventCode = jsonStr["deviceEventCode"].ToString(); //机台事件
        //                        string deviceOP = jsonStr["deviceOP"].ToString() == "" ? jsonStr["scanOP"].ToString() : jsonStr["deviceOP"].ToString(); //jsonStr["scanOP"].ToString(); //机台负责人
        //                        string productLineId = jsonStr["productLineId"].ToString(); //产线ID
        //                        string productLineCode = jsonStr["productLineCode"].ToString(); //产线编码
        //                        string productLineName = jsonStr["productLineName"].ToString(); //产线名称
        //                        string collectionTime = jsonStr["InTime"].ToString(); //采集时间
        //                        string trackNumber = jsonStr["trackNumber"].ToString(); //扫描码
        //                        string oppositeNumber = jsonStr["oppositeNumber"].ToString(); //对应码
        //                        string productionOrder = jsonStr["productionOrder"].ToString(); //生产订单编码
        //                        string destroyOrder = jsonStr["destroyOrder"].ToString(); //销售订单编码
        //                        string finishesCode = (jsonStr["finishesCode"] ?? "").ToString();// jsonStr["finishesCode"].ToString(); //成品的编码
        //                        string finishesModel = (jsonStr["finishesModel"] ?? "").ToString() == "" ? jsonStr["destroyOrder"].ToString() : jsonStr["finishesModel"].ToString();//jsonStr["finishesModel"].ToString(); //成品型号
        //                        string finishesProductNo = (jsonStr["finishesProductNo"] ?? "").ToString();//jsonStr["finishesProductNo"].ToString(); //成品的料号
        //                        string clientProductNo = (jsonStr["clientProductNo"] ?? "").ToString();//jsonStr["clientProductNo"].ToString(); //客户料号
        //                        string exceptionState = jsonStr["exceptionState"].ToString() == "0" ? "否" : "是"; //是否异常  
        //                        string exceptionContent = jsonStr["exceptionContent"].ToString().Replace('#', ' '); //异常原因    
        //                        string deviceCallState = jsonStr["deviceCallState"].ToString() == "0" ? "否" : "是"; //设备报警  
        //                        string deviceCallContent = jsonStr["deviceCallContent"].ToString(); //设备报警内容     
        //                        string lotNumber = jsonStr["lotNumber"].ToString(); //生产批次号
        //                        string bfFlag = (jsonStr["bfFlag"] ?? "0").ToString() == "1" ? "是" : "否"; //是否补扫

        //                        if (productTable.Rows.Count <= 0)
        //                        {
        //                            DataRow productRow = productTable.NewRow();
        //                            productRow["product_ProductOrder"] = productionOrder;//生产订单
        //                            productRow["product_destroyOrder"] = destroyOrder;//销售订单
        //                            productRow["product_LineTid"] = productLineId;//产线ID
        //                            productRow["product_LineCode"] = productLineCode; //产线编码
        //                            productRow["product_LineName"] = productLineCode.Substring(0, 2) + "线"; // productLineName; //产线名称

        //                            productRow["product_finishesCode"] = finishesCode;//成品的编码
        //                            productRow["product_finishesModel"] = finishesModel;//成品型号
        //                            productRow["product_finishesProductNo"] = finishesProductNo;//成品的料号
        //                            productRow["product_clientProductNo"] = clientProductNo;//客户料号
        //                            productRow["product_reworkFlag"] = reworkFlag;
        //                            productTable.Rows.Add(productRow);
        //                        }

        //                        var materialJson = jsonStr["Material"].AsEnumerable();
        //                        foreach (var materialItem in materialJson)
        //                        {
        //                            //更新实际采集表的数据
        //                            if (materialItem["materialInfo"].ToString() != "")
        //                            {
        //                                DataRow materialRow = materialTable.NewRow();

        //                                materialRow["material_ProcessTid"] = processId;//工序ID
        //                                materialRow["material_ProcessCode"] = processCode;//工序编码
        //                                materialRow["material_ProcessName"] = processName;//工序名称
        //                                materialRow["material_CardTid"] = deviceId; //机台ID
        //                                materialRow["material_CardIP"] = deviceIp; //机台IP
        //                                materialRow["material_CardName"] = deviceName;//机台名称
        //                                materialRow["material_ScanIP"] = scanIP; //扫描IP
        //                                materialRow["material_ScanNumber"] = scanNumber;//扫描人编码
        //                                materialRow["material_ScanOP"] = scanOP;//扫描人员
        //                                materialRow["material_MouldNumber"] = mouldNumber;//治具编码
        //                                materialRow["material_MouldName"] = mouldName;//治具名称
        //                                materialRow["material_LineCode"] = productLineCode; //产线编码
        //                                materialRow["material_Code"] = materialItem["materialInfo"].ToString();//原料编码
        //                                materialRow["material_Name"] = materialItem["materialName"].ToString();//原料名称
        //                                materialRow["material_Spec"] = materialItem["materialSpecification"].ToString();//原料型号
        //                                materialRow["material_Batch"] = materialItem["materialBatch"].ToString();//原料入库批次
        //                                materialRow["material_Vonder"] = materialItem["materialManufacturers"].ToString(); //原料供应商
        //                                materialRow["material_SCDate"] = materialItem["materialProductionDate"].ToString(); //原料来料生产日期
        //                                materialRow["material_BoxCode"] = materialItem["materialBoxNumber"].ToString();//原料入库纸箱编码
        //                                materialRow["material_FeedingOP"] = materialItem["loadMaterialPerson"].ToString(); //上料人
        //                                materialTable.Rows.Add(materialRow);
        //                            }
        //                        }
        //                        var parameterJson = jsonStr["Parameter"].AsEnumerable();
        //                        foreach (var parameterItem in parameterJson)
        //                        {
        //                            //更新实际采集表的数据
        //                            DataRow parameterRow = parameterTable.NewRow();
        //                            parameterRow["parameter_RowKey"] = productRowKey;
        //                            parameterRow["parameter_ProcessTid"] = processId;//工序ID
        //                            parameterRow["parameter_ProcessCode"] = processCode;//工序编码
        //                            parameterRow["parameter_ProcessName"] = processName;//工序名称
        //                            parameterRow["parameter_processNumber"] = processNumber;//工序序号
        //                            parameterRow["parameter_CardTid"] = deviceId; //机台ID
        //                            parameterRow["parameter_CardIP"] = deviceIp; //机台IP
        //                            parameterRow["parameter_CardName"] = deviceName;//机台名称
        //                            parameterRow["parameter_ScanIP"] = scanIP; //扫描IP
        //                            parameterRow["parameter_ScanNumber"] = scanNumber;//扫描人编码
        //                            parameterRow["parameter_ScanOP"] = scanOP;//扫描人员
        //                            parameterRow["parameter_MouldNumber"] = mouldNumber;//治具编码
        //                            parameterRow["parameter_MouldName"] = mouldName;//治具名称
        //                            parameterRow["parameter_EventCode"] = deviceEventCode; //机台事件
        //                            parameterRow["parameter_deviceOP"] = deviceOP; //机台负责人
        //                            parameterRow["parameter_LineCode"] = productLineCode; //产线编码
        //                            parameterRow["parameter_DateTime"] = collectionTime; //采集时间
        //                            parameterRow["parameter_trackNumber"] = trackNumber; //扫描码
        //                            parameterRow["parameter_oppositeNumber"] = oppositeNumber; //对应码
        //                            parameterRow["parameter_LotNo"] = lotNumber.Substring(0, lotNumber.Length - 1); //生产批次号
        //                            parameterRow["parameter_bfFlag"] = bfFlag; //是否补扫
        //                            parameterRow["parameter_exceptionState"] = exceptionState; //是否异常
        //                            parameterRow["parameter_exceptionContent"] = exceptionContent; //异常内容
        //                            parameterRow["parameter_deviceCallState"] = deviceCallState; //是否报警
        //                            parameterRow["parameter_deviceCallContent"] = deviceCallContent; //报警内容
        //                            parameterRow["parameter_Type"] = parameterItem["parameterType"].ToString(); //参数类型
        //                            parameterRow["parameter_Value"] = parameterItem["parameterValue"].ToString(); //参数值
        //                            parameterRow["parameter_Unit"] = parameterItem["parameterUnit"].ToString(); //参数单位
        //                            parameterTable.Rows.Add(parameterRow);
        //                        }
        //                    }
        //                    catch (Exception exp)
        //                    {
        //                        LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message.ToString(), exp);
        //                    }
        //                }
        //            }

        //            dataSet.Tables.Add(Instance.GetExceptionInfo(glassCode));
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        LogHelper.Error("rowKey范围查询" + exp.Message.ToString(), exp);
        //    }
        //    return dataSet;
        //}

        /// <summary>
        /// 从HBase数据库查询单片玻璃基本信息、当前状态、不良记录
        /// </summary>
        /// <param name="searchCode">查询编码</param>
        /// <returns>GlassInfo对象</returns>
        public GlassInfo GetGlassInfo(string searchCode)
        {
            try
            {
                GlassInfo glassInfo = new GlassInfo();
                List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_01");
                if (glassRowResult.Count <= 0)
                {
                    return glassInfo;
                }

                ProductInfo productInfo = new ProductInfo();
                foreach (var glassKey in glassRowResult)
                {
                    //获取rowKey
                    string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
                    foreach (var key in glassKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //System.Diagnostics.Debug.Print(glassRowValue);

                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(glassRowValue);

                            System.Diagnostics.Debug.Print(jsonStr.ToString());

                            //获取里面的玻璃编码
                            productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                            productInfo.ProductionOrder = (jsonStr["productionOrder"] ?? "").ToString();//订单编码
                            productInfo.ProductionLineCode = (jsonStr["productLineCode"] ?? "").ToString();//产线编码
                            productInfo.ProductionDate = (jsonStr["productDate"] ?? "").ToString();//生产日期
                            productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                            productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                            productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                            productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                            productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                            productInfo.FinishesCode = (jsonStr["finishesCode"] ?? "").ToString();//成品型号编码
                            productInfo.FinishesModel = (jsonStr["finishesModel"] ?? "").ToString();//成品规格型号
                            productInfo.CustomerNumber = (jsonStr["clientProductNo"] ?? "").ToString();//客户料号
                            productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                            glassInfo.ProductInfo = productInfo;
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败：" + exp.Message, exp);
                        }
                    }//foreach (var key in glassKey.Columns)
                }//foreach (var glassKey in glassRowResult)

                List<TRowResult> processWIPResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(glassInfo.ProductInfo.LCDCode, "data_06");
                if (processWIPResult.Count <= 0)
                {
                    return glassInfo;
                }
                GlassState glassState = new GlassState();
                foreach (var processWIP in processWIPResult)
                {
                    //获取rowKey
                    string processWIPKey = Encoding.UTF8.GetString(processWIP.Row);
                    foreach (var key in processWIP.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string processWIPValue = Encoding.UTF8.GetString(key.Value.Value);
                            //System.Diagnostics.Debug.Print(processWIPValue);
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(processWIPValue);
                            System.Diagnostics.Debug.Print(jsonStr.ToString());
                            //获取里面的工序信息
                            glassState.LogNumber = (jsonStr["logNumber"] ?? "").ToString();//产品已过工序的序号
                            glassState.LogCode = (jsonStr["logCode"] ?? "").ToString();//产品已过工序的编码
                            glassState.HistoryCode = (jsonStr["HistoryCode"] ?? "").ToString();
                            glassState.LCDState = (jsonStr["lcdState"] ?? "").ToString();
                            if (glassState.LCDState == "")
                            {
                                glassState.LCDState = "良品";
                            }
                            glassState.Repeat = (jsonStr["Repeat"] ?? "").ToString();
                            glassInfo.GlassState = glassState;
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从工序移动关联表中获取工序已过工序失败失败" + exp.Message, exp);
                        }
                    }//foreach (var key in processWIP.Columns)
                }//foreach (var processWIP in processWIPResult)

                List<TRowResult> exceptionRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + glassInfo.ProductInfo.LCDCode, "exception_01");
                if (exceptionRowResult.Count <= 0)
                {
                    return glassInfo;
                }
                glassInfo.Exception = new Dictionary<string, ExceptionInfo>();
                foreach (var exceptionKey in exceptionRowResult)
                { //获取rowKey
                    string exceptionRowKey = Encoding.UTF8.GetString(exceptionKey.Row);
                    foreach (var key in exceptionKey.Columns)
                    {
                        ExceptionInfo exceptionInfo = new ExceptionInfo();
                        //获取rowValue
                        string exceptionRowValue = Encoding.UTF8.GetString(key.Value.Value);
                        //System.Diagnostics.Debug.Print(exceptionRowValue);
                        //反序列化rowValue
                        JObject jsonStr = JObject.Parse(exceptionRowValue);
                        System.Diagnostics.Debug.Print(jsonStr.ToString());
                        exceptionInfo.ExceptionKey = exceptionRowKey;//不良RowKey
                        exceptionInfo.GlassCode = (jsonStr["glassCode"] ?? "").ToString();//成品料号
                        exceptionInfo.FinishesCode = (jsonStr["finishesCode"] ?? "").ToString();//成品料号
                        exceptionInfo.FinishesModel = (jsonStr["finishesModel"] ?? "").ToString();//成品型号
                        exceptionInfo.ProductionLineCode = (jsonStr["productLineCode"] ?? "").ToString();//产线编码
                        exceptionInfo.ProductionLineName = (jsonStr["productLineName"] ?? "").ToString();//产线名称
                        exceptionInfo.ProductionOrder = (jsonStr["productionOrder"] ?? "").ToString();//工单编码

                        exceptionInfo.Repeat = (jsonStr["Repeat"] ?? "").ToString();//是否返修品
                        exceptionInfo.RepeatStatus = (jsonStr["repeatStatus"] ?? "").ToString();//重工次数
                        exceptionInfo.ReworkStatus = (jsonStr["reworkStatus"] ?? "").ToString();//是否返检
                        exceptionInfo.ExceptionState = (jsonStr["exceptionState"] ?? "").ToString();//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                        if (exceptionInfo.ExceptionState == "" || exceptionInfo.ExceptionState.Trim() == "1")
                        {
                            exceptionInfo.ExceptionState = "待复判";
                        }

                        exceptionInfo.LotNumber = (jsonStr["lotNumber"] ?? "").ToString();//不良流水号
                        exceptionInfo.LotNumberSecond = (jsonStr["lotNumberSecond"] ?? "").ToString();//
                        exceptionInfo.processName = (jsonStr["processName"] ?? "").ToString();//申报工序名称
                        exceptionInfo.ProcessCode = (jsonStr["processCode"] ?? "").ToString();//申报工序编号

                        exceptionInfo.ScanTime = (jsonStr["scanTime"] ?? "").ToString(); //申报时间
                        if (exceptionInfo.ScanTime != "" && !exceptionInfo.ScanTime.Contains("-"))
                        {
                            string t = exceptionInfo.ScanTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.ScanTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.ScanNumber = (jsonStr["scanNumber"] ?? "").ToString();//申报人员	
                        exceptionInfo.DeviceIp = (jsonStr["deviceIp"] ?? "").ToString();//申报IP
                        exceptionInfo.ExceptionContent = (jsonStr["exceptionContent"] ?? "").ToString();//申报异常项（竖线分隔）

                        exceptionInfo.JudgeNumber = (jsonStr["judgeNumber"] ?? "").ToString();//复判人员 
                        exceptionInfo.JudgeContent = (jsonStr["judgeContent"] ?? "").ToString();//复判异常项
                        exceptionInfo.JudgeTime = (jsonStr["judgeTime"] ?? "").ToString();//复判时间
                        if (exceptionInfo.JudgeTime != "" && !exceptionInfo.JudgeTime.Contains("-"))
                        {
                            string t = exceptionInfo.JudgeTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.JudgeTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.JudgeIp = (jsonStr["judgeIp"] ?? "").ToString();//复判Ip

                        exceptionInfo.ReworkNumber = (jsonStr["reWorkNumber"] ?? "").ToString();//重工人员
                        exceptionInfo.ReworkTime = (jsonStr["reWorkTime"] ?? "").ToString();//重工时间
                        if (exceptionInfo.ReworkTime != "" && !exceptionInfo.ReworkTime.Contains("-"))
                        {
                            string t = exceptionInfo.ScanTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.ReworkTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.ReworkIp = (jsonStr["reWorkIp"] ?? "").ToString();//重工Ip
                        exceptionInfo.ReworkContent = (jsonStr["reWorkContent"] ?? "").ToString();//重工异常项
                        exceptionInfo.ReworkProcess = (jsonStr["reWorkProcess"] ?? "").ToString();//解绑工序
                        glassInfo.Exception.Add(exceptionInfo.ExceptionKey, exceptionInfo);
                        break;
                    }//foreach (var key in exceptionKey.Columns)
                }//foreach (var exceptionKey in exceptionRowResult)
                return glassInfo;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 从HBase数据库查询单片玻璃基本信息和当前状态
        /// </summary>
        /// <param name="searchCode">查询编码</param>
        /// <returns>GlassInfo对象</returns>
        public GlassInfo GetGlassState(string searchCode)
        {
            try
            {
                GlassInfo glassInfo = new GlassInfo();
                List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_01");
                if (glassRowResult.Count <= 0)
                {
                    return glassInfo;
                }

                ProductInfo productInfo = new ProductInfo();
                foreach (var glassKey in glassRowResult)
                {
                    //获取rowKey
                    string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
                    foreach (var key in glassKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(glassRowValue);
                            System.Diagnostics.Debug.Print(jsonStr.ToString());
                            //获取里面的玻璃编码
                            productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                            productInfo.ProductionOrder = (jsonStr["productionOrder"] ?? "").ToString();//订单编码
                            productInfo.ProductionLineCode = (jsonStr["productLineCode"] ?? "").ToString();//产线编码
                            productInfo.ProductionDate = (jsonStr["productDate"] ?? "").ToString();//生产日期
                            productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                            productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                            productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                            productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                            productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                            productInfo.FinishesCode = (jsonStr["finishesCode"] ?? "").ToString();//成品型号编码
                            productInfo.FinishesModel = (jsonStr["finishesModel"] ?? "").ToString();//成品规格型号
                            productInfo.CustomerNumber = (jsonStr["clientProductNo"] ?? "").ToString();//客户料号
                            productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                            glassInfo.ProductInfo = productInfo;
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败：" + exp.Message, exp);
                        }
                    }//foreach (var key in glassKey.Columns)
                }//foreach (var glassKey in glassRowResult)

                List<TRowResult> processWIPResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(glassInfo.ProductInfo.LCDCode, "data_06");
                if (processWIPResult.Count <= 0)
                {
                    return glassInfo;
                }
                GlassState glassState = new GlassState();
                foreach (var processWIP in processWIPResult)
                {
                    //获取rowKey
                    string processWIPKey = Encoding.UTF8.GetString(processWIP.Row);
                    foreach (var key in processWIP.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string processWIPValue = Encoding.UTF8.GetString(key.Value.Value);
                            //System.Diagnostics.Debug.Print(processWIPValue);
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(processWIPValue);
                            System.Diagnostics.Debug.Print(jsonStr.ToString());
                            //获取里面的工序信息
                            glassState.LogNumber = (jsonStr["logNumber"] ?? "").ToString();//产品已过工序的序号
                            glassState.LogCode = (jsonStr["logCode"] ?? "").ToString();//产品已过工序的编码
                            glassState.HistoryCode = (jsonStr["HistoryCode"] ?? "").ToString();
                            glassState.LCDState = (jsonStr["lcdState"] ?? "").ToString();
                            if (glassState.LCDState == "")
                            {
                                glassState.LCDState = "良品";
                            }
                            glassState.Repeat = (jsonStr["Repeat"] ?? "").ToString();
                            glassInfo.GlassState = glassState;
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从工序移动关联表中获取工序已过工序失败失败" + exp.Message, exp);
                        }
                    }//foreach (var key in processWIP.Columns)
                }//foreach (var processWIP in processWIPResult)
                return glassInfo;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }


        /// <summary>
        /// 从HBase数据库查询单片玻璃当前状态
        /// </summary>
        /// <param name="lcdCode">玻璃编码</param>
        /// <returns>GlassState对象</returns>
        public GlassState GetData06(string lcdCode)
        {
            try
            {
                List<TRowResult> processWIPResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(lcdCode, "data_06");
                if (processWIPResult.Count <= 0)
                {
                    return null;
                }
                GlassState glassState = new GlassState();
                foreach (var processWIP in processWIPResult)
                {
                    //获取rowKey
                    string processWIPKey = Encoding.UTF8.GetString(processWIP.Row);
                    foreach (var key in processWIP.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string processWIPValue = Encoding.UTF8.GetString(key.Value.Value);
                            //System.Diagnostics.Debug.Print(processWIPValue);
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(processWIPValue);
                            System.Diagnostics.Debug.Print(jsonStr.ToString());
                            //获取里面的工序信息
                            glassState.LogNumber = (jsonStr["logNumber"] ?? "").ToString();//产品已过工序的序号
                            glassState.LogCode = (jsonStr["logCode"] ?? "").ToString();//产品已过工序的编码
                            glassState.HistoryCode = (jsonStr["HistoryCode"] ?? "").ToString();
                            glassState.LCDState = (jsonStr["lcdState"] ?? "").ToString();
                            if (glassState.LCDState == "")
                            {
                                glassState.LCDState = "良品";
                            }
                            glassState.Repeat = (jsonStr["Repeat"] ?? "").ToString();
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从data_06中获取玻璃当前状态失败" + exp.Message, exp);
                        }
                    }//foreach (var key in processWIP.Columns)
                }//foreach (var processWIP in processWIPResult)
                return glassState;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 从HBase数据库查询单片玻璃基本信息
        /// </summary>
        /// <param name="searchCode">查询编码</param>
        /// <returns>ProductInfo对象</returns>
        public ProductInfo GetProductInfo(string searchCode)
        {
            try
            {
                ProductInfo productInfo = new ProductInfo();
                List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_01");
                if (glassRowResult.Count <= 0)
                {
                    return productInfo;
                }

                foreach (var glassKey in glassRowResult)
                {
                    //获取rowKey
                    string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
                    foreach (var key in glassKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(glassRowValue);
                            System.Diagnostics.Debug.Print(jsonStr.ToString());
                            //获取里面的玻璃编码
                            productInfo.LCDCode = (jsonStr["glassCode"] ?? "").ToString(); //玻璃码
                            productInfo.ProductionOrder = (jsonStr["productionOrder"] ?? "").ToString();//订单编码
                            productInfo.ProductionLineCode = (jsonStr["productLineCode"] ?? "").ToString();//产线编码
                            productInfo.ProductionDate = (jsonStr["productDate"] ?? "").ToString();//生产日期
                            productInfo.BLCode = (jsonStr["backCode"] ?? "").ToString();//BL编码
                            productInfo.FPCCode = (jsonStr["fpcCode"] ?? "").ToString();//fpc编码
                            productInfo.TPCode = (jsonStr["tpCode"] ?? "").ToString();//tp编码
                            productInfo.ICCode = (jsonStr["icCode"] ?? "").ToString();
                            productInfo.QRCode = (jsonStr["intactCode"] ?? "").ToString();//54位产品编码
                            productInfo.FinishesCode = (jsonStr["finishesCode"] ?? "").ToString();//成品型号编码
                            productInfo.FinishesModel = (jsonStr["finishesModel"] ?? "").ToString();//成品规格型号
                            productInfo.CustomerNumber = (jsonStr["clientProductNo"] ?? "").ToString();//客户料号
                            productInfo.TPState = (jsonStr["tpState"] ?? "0").ToString();//TP是否有码
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败：" + exp.Message, exp);
                        }
                    }//foreach (var key in glassKey.Columns)
                }//foreach (var glassKey in glassRowResult)
                return productInfo;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 查询指定玻璃码的不良信息列表
        /// </summary>
        /// <param name="glassCode">玻璃编码</param>
        /// <returns>Dictionary<string, ExceptionInfo></returns>
        public Dictionary<string, ExceptionInfo> GetException(string glassCode)
        {
            try
            {
                List<TRowResult> exceptionRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + glassCode, "exception_01");
                if (exceptionRowResult.Count <= 0)
                {
                    return null;
                }
                Dictionary<string, ExceptionInfo> Exception = new Dictionary<string, ExceptionInfo>();
                foreach (var exceptionKey in exceptionRowResult)
                { //获取rowKey
                    string exceptionRowKey = Encoding.UTF8.GetString(exceptionKey.Row);
                    foreach (var key in exceptionKey.Columns)
                    {
                        ExceptionInfo exceptionInfo = new ExceptionInfo();
                        //获取rowValue
                        string exceptionRowValue = Encoding.UTF8.GetString(key.Value.Value);
                        //System.Diagnostics.Debug.Print(exceptionRowValue);
                        //反序列化rowValue
                        JObject jsonStr = JObject.Parse(exceptionRowValue);
                        System.Diagnostics.Debug.Print(jsonStr.ToString());
                        exceptionInfo.ExceptionKey = exceptionRowKey;//不良RowKey
                        exceptionInfo.GlassCode = (jsonStr["glassCode"] ?? "").ToString();//成品料号
                        exceptionInfo.FinishesCode = (jsonStr["finishesCode"] ?? "").ToString();//成品料号
                        exceptionInfo.FinishesModel = (jsonStr["finishesModel"] ?? "").ToString();//成品型号
                        exceptionInfo.ProductionLineCode = (jsonStr["productLineCode"] ?? "").ToString();//产线编码
                        exceptionInfo.ProductionLineName = (jsonStr["productLineName"] ?? "").ToString();//产线名称
                        exceptionInfo.ProductionOrder = (jsonStr["productionOrder"] ?? "").ToString();//工单编码

                        exceptionInfo.Repeat = (jsonStr["Repeat"] ?? "").ToString();//是否返修品
                        exceptionInfo.RepeatStatus = (jsonStr["repeatStatus"] ?? "").ToString();//重工次数
                        exceptionInfo.ReworkStatus = (jsonStr["reworkStatus"] ?? "").ToString();//是否返检
                        exceptionInfo.ExceptionState = (jsonStr["exceptionState"] ?? "").ToString();//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                        if (exceptionInfo.ExceptionState == "" || exceptionInfo.ExceptionState.Trim() == "1")
                        {
                            exceptionInfo.ExceptionState = "待复判";
                        }

                        exceptionInfo.LotNumber = (jsonStr["lotNumber"] ?? "").ToString();//不良流水号
                        exceptionInfo.LotNumberSecond = (jsonStr["lotNumberSecond"] ?? "").ToString();//
                        exceptionInfo.processName = (jsonStr["processName"] ?? "").ToString();//申报工序名称
                        exceptionInfo.ProcessCode = (jsonStr["processCode"] ?? "").ToString();//申报工序编号

                        exceptionInfo.ScanTime = (jsonStr["scanTime"] ?? "").ToString(); //申报时间
                        if (exceptionInfo.ScanTime != "" && !exceptionInfo.ScanTime.Contains("-"))
                        {
                            string t = exceptionInfo.ScanTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.ScanTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.ScanNumber = (jsonStr["scanNumber"] ?? "").ToString();//申报人员	
                        exceptionInfo.DeviceIp = (jsonStr["deviceIp"] ?? "").ToString();//申报IP
                        exceptionInfo.ExceptionContent = (jsonStr["exceptionContent"] ?? "").ToString();//申报异常项（竖线分隔）

                        exceptionInfo.JudgeNumber = (jsonStr["judgeNumber"] ?? "").ToString();//复判人员 
                        exceptionInfo.JudgeContent = (jsonStr["judgeContent"] ?? "").ToString();//复判异常项
                        exceptionInfo.JudgeTime = (jsonStr["judgeTime"] ?? "").ToString();//复判时间
                        if (exceptionInfo.JudgeTime != "" && !exceptionInfo.JudgeTime.Contains("-"))
                        {
                            string t = exceptionInfo.JudgeTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.JudgeTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.JudgeIp = (jsonStr["judgeIp"] ?? "").ToString();//复判Ip

                        exceptionInfo.ReworkNumber = (jsonStr["reWorkNumber"] ?? "").ToString();//重工人员
                        exceptionInfo.ReworkTime = (jsonStr["reWorkTime"] ?? "").ToString();//重工时间
                        if (exceptionInfo.ReworkTime != "" && !exceptionInfo.ReworkTime.Contains("-"))
                        {
                            string t = exceptionInfo.ScanTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.ReworkTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.ReworkIp = (jsonStr["reWorkIp"] ?? "").ToString();//重工Ip
                        exceptionInfo.ReworkContent = (jsonStr["reWorkContent"] ?? "").ToString();//重工异常项
                        exceptionInfo.ReworkProcess = (jsonStr["reWorkProcess"] ?? "").ToString();//解绑工序

                        Exception.Add(exceptionInfo.ExceptionKey, exceptionInfo);
                        break;
                    }//foreach (var key in exceptionKey.Columns)
                }//foreach (var exceptionKey in exceptionRowResult)
                return Exception;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 查询指定玻璃码的不良信息列表
        /// </summary>
        /// <param name="glassCode">玻璃编码</param>
        /// <returns></returns>
        public DataTable GetExceptionInfo(string glassCode)
        {
            DataTable exceptionTable = new DataTable("exceptionTable");

            exceptionTable.Columns.Add("ex_rowKey");//不良RowKey
            exceptionTable.Columns.Add("ex_glassCode");//玻璃编码
            exceptionTable.Columns.Add("ex_finishesCode");//成品料号
            exceptionTable.Columns.Add("ex_finishesModel");//成品型号
            exceptionTable.Columns.Add("ex_productionOrder");//工单编码
            exceptionTable.Columns.Add("ex_lotNumber");//不良流水号
            exceptionTable.Columns.Add("ex_lotNumberSecond");//不良流水号2
            exceptionTable.Columns.Add("ex_Repeat");//是否返修品
            exceptionTable.Columns.Add("ex_repeatStatus");//重工次数
            exceptionTable.Columns.Add("ex_reworkStatus");//是否返检申报

            exceptionTable.Columns.Add("ex_lineCode");//产线编码
            exceptionTable.Columns.Add("ex_lineName");//产线名称
            exceptionTable.Columns.Add("ex_processName");//工序名称
            exceptionTable.Columns.Add("ex_processCode");//工序编号
            exceptionTable.Columns.Add("ex_type");//操作类型（申报，复判，重工）
            exceptionTable.Columns.Add("ex_state");//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
            exceptionTable.Columns.Add("ex_content");//不良描述
            exceptionTable.Columns.Add("ex_name");//操作人员
            exceptionTable.Columns.Add("ex_ip");//机台IP
            exceptionTable.Columns.Add("ex_time");//时间
            exceptionTable.Columns.Add("ex_reworkProcess");//重投站点

            try
            {
                List<TRowResult> exceptionRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + glassCode, "exception_01");
                if (exceptionRowResult.Count <= 0)
                {
                    return null;
                }
                foreach (var exceptionKey in exceptionRowResult)
                { //获取rowKey
                    string exceptionRowKey = Encoding.UTF8.GetString(exceptionKey.Row);
                    foreach (var key in exceptionKey.Columns)
                    {
                        ExceptionInfo exceptionInfo = new ExceptionInfo();
                        //获取rowValue
                        string exceptionRowValue = Encoding.UTF8.GetString(key.Value.Value);
                        //System.Diagnostics.Debug.Print(exceptionRowValue);
                        //反序列化rowValue
                        JObject jsonStr = JObject.Parse(exceptionRowValue);
                        System.Diagnostics.Debug.Print(jsonStr.ToString());
                        //System.Diagnostics.Debug.Print(exceptionRowValue);
                        ////反序列化rowValue
                        //dynamic reObj = JObject.Parse(exceptionRowValue);

                        exceptionInfo.ExceptionKey = exceptionRowKey;//不良RowKey
                        exceptionInfo.GlassCode = (jsonStr["glassCode"] ?? "").ToString();//玻璃编码
                        exceptionInfo.FinishesCode = (jsonStr["finishesCode"] ?? "").ToString();//成品料号
                        exceptionInfo.FinishesModel = (jsonStr["finishesModel"] ?? "").ToString();//成品型号
                        exceptionInfo.ProductionLineCode = (jsonStr["productLineCode"] ?? "").ToString();//产线编码
                        exceptionInfo.ProductionLineName = (jsonStr["productLineName"] ?? "").ToString();//产线名称
                        exceptionInfo.ProductionOrder = (jsonStr["productionOrder"] ?? "").ToString();//工单编码

                        exceptionInfo.Repeat = (jsonStr["Repeat"] ?? "").ToString();//是否返修品
                        exceptionInfo.RepeatStatus = (jsonStr["repeatStatus"] ?? "").ToString();//重工次数
                        exceptionInfo.ReworkStatus = (jsonStr["reworkStatus"] ?? "").ToString();//是否返检
                        exceptionInfo.ExceptionState = (jsonStr["exceptionState"] ?? "").ToString();//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                        if (exceptionInfo.ExceptionState == "" || exceptionInfo.ExceptionState.Trim() == "1")
                        {
                            exceptionInfo.ExceptionState = "待复判";
                        }

                        exceptionInfo.LotNumber = (jsonStr["lotNumber"] ?? "").ToString();//不良流水号
                        exceptionInfo.LotNumberSecond = (jsonStr["lotNumberSecond"] ?? "").ToString();//
                        exceptionInfo.processName = (jsonStr["processName"] ?? "").ToString();//申报工序名称
                        exceptionInfo.ProcessCode = (jsonStr["processCode"] ?? "").ToString();//申报工序编号

                        exceptionInfo.ScanTime = (jsonStr["scanTime"] ?? "").ToString(); //申报时间
                        if (exceptionInfo.ScanTime != "" && !exceptionInfo.ScanTime.Contains("-"))
                        {
                            string t = exceptionInfo.ScanTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.ScanTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.ScanNumber = (jsonStr["scanNumber"] ?? "").ToString();//申报人员	
                        exceptionInfo.DeviceIp = (jsonStr["deviceIp"] ?? "").ToString();//申报IP
                        exceptionInfo.ExceptionContent = (jsonStr["exceptionContent"] ?? "").ToString();//申报异常项（竖线分隔）

                        exceptionInfo.JudgeNumber = (jsonStr["judgeNumber"] ?? "").ToString();//复判人员 
                        exceptionInfo.JudgeContent = (jsonStr["judgeContent"] ?? "").ToString();//复判异常项
                        exceptionInfo.JudgeTime = (jsonStr["judgeTime"] ?? "").ToString();//复判时间
                        if (exceptionInfo.JudgeTime != "" && !exceptionInfo.JudgeTime.Contains("-"))
                        {
                            string t = exceptionInfo.JudgeTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.JudgeTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.JudgeIp = (jsonStr["judgeIp"] ?? "").ToString();//复判Ip

                        exceptionInfo.ReworkNumber = (jsonStr["reWorkNumber"] ?? "").ToString();//重工人员
                        exceptionInfo.ReworkTime = (jsonStr["reWorkTime"] ?? "").ToString();//重工时间
                        if (exceptionInfo.ReworkTime != "" && !exceptionInfo.ReworkTime.Contains("-"))
                        {
                            string t = exceptionInfo.ScanTime.PadRight(14, '0').Substring(0, 14);
                            exceptionInfo.ReworkTime = DateTime.ParseExact(t, "yyyyMMddHHmmss", null).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        exceptionInfo.ReworkIp = (jsonStr["reWorkIp"] ?? "").ToString();//重工Ip
                        exceptionInfo.ReworkContent = (jsonStr["reWorkContent"] ?? "").ToString();//重工异常项
                        exceptionInfo.ReworkProcess = (jsonStr["reWorkProcess"] ?? "").ToString();//解绑工序

                        foreach (string type in new string[] { "申报", "复判", "重工" })
                        {
                            DataRow exRow = exceptionTable.NewRow();
                            exRow["ex_rowKey"] = exceptionInfo.ExceptionKey;//不良RowKey
                            exRow["ex_glassCode"] = exceptionInfo.GlassCode;//玻璃编码
                            exRow["ex_finishesCode"] = exceptionInfo.FinishesCode;//成品料号
                            exRow["ex_finishesModel"] = exceptionInfo.FinishesModel;//成品型号
                            exRow["ex_productionOrder"] = exceptionInfo.ProductionOrder;//工单编码
                            exRow["ex_lotNumber"] = exceptionInfo.LotNumber;//不良流水号
                            exRow["ex_lotNumberSecond"] = exceptionInfo.LotNumberSecond;//不良流水号2
                            exRow["ex_Repeat"] = exceptionInfo.Repeat;//是否返修品
                            exRow["ex_repeatStatus"] = exceptionInfo.RepeatStatus;//重工次数
                            exRow["ex_reworkStatus"] = exceptionInfo.ReworkStatus;//是否返检申报
                            exRow["ex_lineCode"] = exceptionInfo.ProductionLineCode;//产线编码
                            exRow["ex_lineName"] = exceptionInfo.ProductionLineName;//产线名称
                            exRow["ex_processName"] = exceptionInfo.processName;//工序名称
                            exRow["ex_processCode"] = exceptionInfo.ProcessCode;//工序编码
                            exRow["ex_type"] = type;//操作类型（申报，复判，重工）
                            switch (type)
                            {
                                case "申报":
                                    exRow["ex_state"] = "待复判";//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                    exRow["ex_content"] = exceptionInfo.ExceptionContent;//不良描述
                                    exRow["ex_name"] = exceptionInfo.ScanNumber;//操作人员
                                    exRow["ex_ip"] = exceptionInfo.DeviceIp;//机台IP
                                    exRow["ex_time"] = exceptionInfo.ScanTime;//时间
                                    exRow["ex_reworkProcess"] = "";//重投站点
                                    break;
                                case "复判":
                                    if (exceptionInfo.ExceptionState == "待复判")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        exRow["ex_state"] = (exceptionInfo.ExceptionState == "复判良品" || exceptionInfo.ExceptionState == "复判报废") ? exceptionInfo.ExceptionState : "复判重工";//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                        exRow["ex_content"] = exceptionInfo.JudgeContent;//不良描述
                                        exRow["ex_name"] = exceptionInfo.JudgeNumber;//操作人员
                                        exRow["ex_ip"] = exceptionInfo.JudgeIp;//机台IP
                                        exRow["ex_time"] = exceptionInfo.JudgeTime;//时间
                                        exRow["ex_reworkProcess"] = "";//重投站点
                                    }
                                    break;
                                case "重工":
                                    if (exceptionInfo.ExceptionState != "重工良品" && exceptionInfo.ExceptionState != "重工报废")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        exRow["ex_state"] = exceptionInfo.ExceptionState;//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                        exRow["ex_content"] = exceptionInfo.ReworkContent;//不良描述
                                        exRow["ex_name"] = exceptionInfo.ReworkNumber;//操作人员
                                        exRow["ex_ip"] = exceptionInfo.ReworkIp;//机台IP
                                        exRow["ex_time"] = exceptionInfo.ReworkTime;//时间
                                        exRow["ex_reworkProcess"] = exceptionInfo.ReworkProcess;//重投站点
                                    }
                                    break;
                            }//switch (type)
                            exceptionTable.Rows.Add(exRow);
                        }//foreach (string type in new string[] { "申报", "复判", "重工" })
                    }//foreach (var key in exceptionKey.Columns)
                }//foreach (var exceptionKey in exceptionRowResult)

                if (exceptionTable.Rows.Count == 0)
                {
                    string sqlBadness = string.Format(@"select TFM_Describe,TFM_LineCode,SGX_Name,AddOperator.BOO_Pname AS AddPname,TFM_AddDate,TFM_GlassCode,TFM_DeviceIP,CheckOperator.BOO_Pname AS CheckPname,TFM_CheckDate,TFM_Status,TFM_Type
                                                        from TPL_ProcessFail_Main WITH (NOLOCK)
                                                        LEFT JOIN Store_GongXu_Main ON TFM_ProcessTid=SGX_Tid
                                                        LEFT JOIN Base_Op_Operator AddOperator ON TFM_AddPid=AddOperator.BOO_Pid
                                                        LEFT JOIN Base_Op_Operator CheckOperator ON TFM_CheckPid=CheckOperator.BOO_Pid
                                                        WHERE  TFM_GlassCode='{0}'", glassCode);
                    DataView dvBadness = conn.ExecuteDataView(sqlBadness, "Base");
                    foreach (DataRowView row in dvBadness)
                    {
                        foreach (string type in new string[] { "申报", "复判" })
                        {
                            DataRow exRow = exceptionTable.NewRow();
                            exRow["ex_rowKey"] = "";//不良RowKey
                            exRow["ex_glassCode"] = (row["TFM_GlassCode"] ?? "").ToString();//玻璃编码
                            exRow["ex_finishesCode"] = "";//成品料号
                            exRow["ex_finishesModel"] = "";//成品型号
                            exRow["ex_productionOrder"] = "";//工单编码
                            exRow["ex_lotNumber"] = "";//不良流水号
                            exRow["ex_lotNumberSecond"] = "";//不良流水号2
                            exRow["ex_repeatStatus"] = "";//重工次数
                            exRow["ex_reworkStatus"] = "";//是否返检申报
                            exRow["ex_lineCode"] = (row["TFM_LineCode"] ?? "").ToString();//产线编码
                            exRow["ex_lineName"] = "";//产线名称
                            exRow["ex_processName"] = (row["SGX_Name"] ?? "").ToString();//工序名称SGX_Name
                            exRow["ex_processCode"] = "";//工序编码
                            exRow["ex_type"] = type;//操作类型（申报，复判，重工）
                            switch (type)
                            {
                                case "申报":
                                    exRow["ex_state"] = "待复判";//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                    exRow["ex_content"] = (row["TFM_Describe"] ?? "").ToString();//不良描述
                                    exRow["ex_name"] = (row["AddPname"] ?? "").ToString();//操作人员
                                    exRow["ex_ip"] = (row["TFM_DeviceIP"] ?? "").ToString();//机台IP
                                    exRow["ex_time"] = (row["TFM_AddDate"] ?? "").ToString();//时间
                                    exRow["ex_reworkProcess"] = "";//重投站点
                                    break;
                                case "复判":
                                    if ((row["TFM_Status"] ?? "").ToString() != "已复判")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        switch ((row["TFM_Type "] ?? "").ToString())
                                        {
                                            case "误判":
                                                exRow["ex_state"] = "复判良品";//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                                break;
                                            case "报废":
                                                exRow["ex_state"] = "复判报废";//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                                break;
                                            case "重工":
                                                exRow["ex_state"] = "复判重工";//不良状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
                                                break;
                                        }
                                        exRow["ex_content"] = (row["TFM_Describe"] ?? "").ToString();//不良描述
                                        exRow["ex_name"] = (row["CheckPname"] ?? "").ToString();//操作人员
                                        exRow["ex_ip"] = "";//机台IP
                                        exRow["ex_time"] = (row["TFM_CheckDate"] ?? "").ToString();//时间
                                        exRow["ex_reworkProcess"] = "";//重投站点
                                    }
                                    break;
                            }//switch (type)
                            exceptionTable.Rows.Add(exRow);
                        }//foreach (string type in new string[] { "申报", "复判", "重工" })
                    }//foreach (DataRowView row in dvBadness)
                }//if (exceptionTable.Rows.Count == 0)

                exceptionTable.DefaultView.Sort = "ex_time";
                exceptionTable = exceptionTable.DefaultView.ToTable();
                return exceptionTable;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 查询指定玻璃码的不良信息列表
        /// </summary>
        /// <param name="glassCode">玻璃编码</param>
        /// <returns>Dictionary<string, ExceptionInfo></returns>
        public Dictionary<string, dynamic> GetExceptionDynamic(string glassCode)
        {
            try
            {
                List<TRowResult> exceptionRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + glassCode, "exception_01");
                if (exceptionRowResult.Count <= 0)
                {
                    return null;
                }
                Dictionary<string, dynamic> Exception = new Dictionary<string, dynamic>();
                foreach (var exceptionKey in exceptionRowResult)
                { //获取rowKey
                    string exceptionRowKey = Encoding.UTF8.GetString(exceptionKey.Row);
                    foreach (var key in exceptionKey.Columns)
                    {
                        //获取rowValue
                        string exceptionRowValue = Encoding.UTF8.GetString(key.Value.Value);
                        //System.Diagnostics.Debug.Print(exceptionRowValue);
                        //反序列化rowValue
                        dynamic jsonObj = JObject.Parse(exceptionRowValue);
                        Exception.Add(exceptionRowKey, jsonObj);
                        break;
                    }//foreach (var key in exceptionKey.Columns)
                }//foreach (var exceptionKey in exceptionRowResult)
                return Exception;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 不良申报
        /// </summary>
        /// <param name="glassInfo">玻璃信息</param>
        /// <returns></returns>
        public string ExceptionAdd(ref GlassInfo glassInfo)
        {
            // 最新不良记录Key
            string exKey = glassInfo.LastExceptionKey;
            // 不良信息
            ExceptionInfo exInfo = glassInfo.Exception[exKey];

            //更新exception_01
            try
            {
                bool UpdateSuccess = UpdateException01(exKey, exInfo);
                if (!UpdateSuccess)
                {
                    return "申报失败";
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("单一RowKey申报exception_01.|" + exp.Message, exp);
                Reconnect();
                return "申报失败";
            }

            // 更新exception_05
            try
            {
                //// 时段内exception_01的key值列表
                //List<string> exception01searchCodes = new List<string>();
                string exception05RowValue = "";
                // 从exception_05获取key值列表
                List<TRowResult> exception05RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(exInfo.LotNumberSecond, "exception_05");
                foreach (var exception05Row in exception05RowResult)
                {
                    //获取rowKey
                    string exception05RowKey = Encoding.UTF8.GetString(exception05Row.Row);
                    foreach (var exception05key in exception05Row.Columns)
                    {
                        //获取rowValue
                        exception05RowValue = Encoding.UTF8.GetString(exception05key.Value.Value);
                        System.Diagnostics.Debug.Print(exception05RowValue);
                        //exception01searchCodes = (exception05RowValue.Split(',')).ToList<string>();
                    }
                }
                //// 将本次复判的不良项的key值加入列表
                //exception01searchCodes.Add(glassInfo.LastExceptionKey);
                //// 转换成","分隔的字符串
                //string value = string.Join(",", exception01searchCodes);
                if (string.IsNullOrEmpty(exception05RowValue))
                {
                    exception05RowValue = exKey;
                }
                else
                {
                    exception05RowValue = exception05RowValue + "," + exKey;
                }

                // 回写exception_05
                bool UpdateSuccess = UpdateHBaseKeyValue("exception_05", exInfo.LotNumberSecond, exception05RowValue);
                if (!UpdateSuccess)
                {
                    return "申报失败";
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("更新exception_05失败.|" + exp.Message, exp);
                Reconnect();
                return "申报失败";
            }

            try
            {
                //更新lcdState
                glassInfo.GlassState.LCDState = "待复判";

                //更新Data_06
                bool UpdateSuccess = UpdateData06(glassInfo.ProductInfo.LCDCode, glassInfo.GlassState);
                if (!UpdateSuccess)
                {
                    return "申报失败";
                }
                return "申报成功";
            }
            catch (Exception exp)
            {
                LogHelper.Error("更新Data_06失败.|" + exp.Message, exp);
                Reconnect();
                return "申报失败";
            }
        }

        /// <summary>
        /// 不良复判
        /// </summary>
        /// <param name="glassInfo">玻璃信息对象</param>
        /// <param name="judgeState">复判结果(复判重工,复判良品,复判报废)</param>
        /// <param name="judgeNumber">复判人员</param>
        /// <param name="judgeContent">复判不良项(多个不良以|分割)</param>
        /// <param name="judgeTime">复判时间 格式(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="judgeIP">复判电脑IP</param>
        /// <returns></returns>
        public string ExceptionJudge(ref GlassInfo glassInfo, string judgeState, string judgeNumber, string judgeContent, string judgeTime, string judgeIP)
        {
            //更新exception_01
            try
            {
                //获取产品提交的不良信息exception_01
                ExceptionInfo exceptionInfo = glassInfo.Exception[glassInfo.LastExceptionKey];

                exceptionInfo.ExceptionState = judgeState;//不良状态 
                exceptionInfo.JudgeNumber = judgeNumber;//复判人员 
                exceptionInfo.JudgeContent = judgeContent;//复判异常项
                exceptionInfo.JudgeTime = judgeTime;//复判时间
                exceptionInfo.JudgeIp = judgeIP;//复判Ip

                if (string.IsNullOrEmpty(exceptionInfo.Repeat))
                {
                    exceptionInfo.Repeat = glassInfo.GlassState.Repeat;
                }

                bool UpdateSuccess = UpdateException01(exceptionInfo.ExceptionKey, exceptionInfo);
                if (!UpdateSuccess)
                {
                    return "复判失败";
                }
                glassInfo.Exception[glassInfo.LastExceptionKey] = exceptionInfo;
            }
            catch (Exception exp)
            {
                LogHelper.Error("单一RowKey复判exception_01.|" + exp.Message, exp);
                Reconnect();
                return "复判失败";
            }

            // 更新exception_03
            try
            {
                // 复判时间
                DateTime tJudge = DateTime.Parse(judgeTime);
                // 确定复判时间段（1：前半小时，2：后半小时）
                int part = tJudge.Minute < 30 ? 1 : 2;
                // 产线编码
                string lineCode = glassInfo.Exception[glassInfo.LastExceptionKey].ProductionLineCode;
                // 工序编码
                string processCode = glassInfo.Exception[glassInfo.LastExceptionKey].ProcessCode;
                // RowKey
                string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, tJudge.ToString("yyyyMMddHH"), part);
                // 时段内exception_01的key值列表
                List<string> exception01searchCodes = new List<string>();
                // 从exception_03获取key值列表
                List<TRowResult> exception03RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_03");
                foreach (var exception03Row in exception03RowResult)
                {
                    //获取rowKey
                    string exception03RowKey = Encoding.UTF8.GetString(exception03Row.Row);
                    foreach (var exception03key in exception03Row.Columns)
                    {
                        //获取rowValue
                        string exception03RowValue = Encoding.UTF8.GetString(exception03key.Value.Value);
                        System.Diagnostics.Debug.Print(exception03RowValue);
                        exception01searchCodes = (exception03RowValue.Split(',')).ToList<string>();
                    }
                }
                // 将本次复判的不良项的key值加入列表
                exception01searchCodes.Add(glassInfo.LastExceptionKey);
                // 转换成","分隔的字符串
                string value = string.Join(",", exception01searchCodes);
                // 回写exception_03
                bool UpdateSuccess = UpdateHBaseKeyValue("exception_03", searchCode, value);
                if (!UpdateSuccess)
                {
                    return "复判失败";
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("更新exception_03失败.|" + exp.Message, exp);
                Reconnect();
                return "复判失败";
            }

            try
            {
                //获取data_06
                GlassState glassState = GetData06(glassInfo.ProductInfo.LCDCode);

                //对于重工过的复判良品，data_06状态仍是重工良品
                if (judgeState == "复判良品")
                {
                    foreach (ExceptionInfo item in glassInfo.Exception.Values)
                    {
                        if (item.ExceptionState == "重工良品")
                        {
                            judgeState = "重工良品";
                            break;
                        }
                    }
                }
                //更新lcdState
                glassState.LCDState = judgeState;

                //更新Data_06
                bool UpdateSuccess = UpdateData06(glassInfo.ProductInfo.LCDCode, glassState);
                if (!UpdateSuccess)
                {
                    return "更新产品过站信息失败";
                }
                glassInfo.GlassState = glassState;
                return "复判成功";
            }
            catch (Exception exp)
            {
                LogHelper.Error("更新Data_06失败.|" + exp.Message, exp);
                Reconnect();
                return "复判失败";
            }
        }

        /// <summary>
        /// 不良重工
        /// </summary>
        /// <param name="glassInfo">玻璃信息对象GlassInfo</param>
        /// <param name="reworkState">重工结果(重工良品,重工报废)</param>
        /// <param name="reworkNumber">重工人员</param>
        /// <param name="reworkContent">重工不良项(多个不良以|分割)</param>
        /// <param name="reworkTime">重工时间 格式(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="reworkIP">重工电脑IP</param>
        /// <param name="reworkProcess">解绑工序</param>
        /// <returns></returns>
        public string ExceptionRework(GlassInfo glassInfo, string reworkState, string reworkNumber, string reworkContent, string reworkTime, string reworkIP, string reworkProcess, bool isRoot = false)
        {
            try
            {
                ExceptionInfo exceptionInfo = null;

                if (!isRoot)
                {
                    // 第一步：判断产品状态
                    // --------------------------------------------------------------------------------------------
                    exceptionInfo = glassInfo.Exception[glassInfo.LastExceptionKey];
                    //if (exceptionInfo.ExceptionState != "复判重工" && exceptionInfo.ExceptionState != "复判报废")
                    if (exceptionInfo.ExceptionState != "复判重工" && exceptionInfo.ExceptionState != "复判报废" && exceptionInfo.ExceptionState != "重工良品" && exceptionInfo.ExceptionState != "重工报废")
                    {
                        return "当前状态为" + exceptionInfo.ExceptionState + ",无法重工";
                    }

                    // 如果是"复判报废"品，则重工状态强制为"重工报废"，允许解绑
                    if (exceptionInfo.ExceptionState == "复判报废")
                    {
                        reworkState = "重工报废";
                    }
                }

                // 第二步：更新data_06产品过站信息和状态
                // --------------------------------------------------------------------------------------------
                string productCode = glassInfo.ProductInfo.ProductionOrder;
                //获取工单型号的工序链信息
                string processString = string.Format(@"SELECT SPOM_JobCode AS productCode,SGX_JobCode AS processCode,SGX_Name AS processName,PR_NoSeq AS processSort 
                FROM TPL_ProductProcess_Route LEFT JOIN Store_ProduceOrder_Main ON PR_SPOMTid=SPOM_Tid LEFT JOIN Store_GongXu_Main ON PR_SGXTid=SGX_Tid
                WHERE SPOM_JobCode='{0}' ORDER BY PR_NoSeq ASC", productCode);
                DataView processView = conn.ExecuteDataView(processString, "Base");
                if (processView.Count <= 0)
                {
                    return "未获取到当前条码对应工单的工序链信息";
                }
                string newLogCode = "", processLogCode = "";
                // 产品应过工序字符串
                for (int i = 0; i < processView.Count; i++)
                {
                    processLogCode += processView[i]["processCode"].ToString() + ",";
                }
                //需要保留的工序信息
                if (!string.IsNullOrEmpty(reworkProcess))
                {
                    newLogCode = processLogCode.TrimEnd(',').Substring(0, processLogCode.TrimEnd(',').IndexOf(reworkProcess)).TrimEnd(',');
                }
                else
                {
                    newLogCode = processLogCode.TrimEnd(',');
                }
                //获取已经过的工序信息data_06
                GlassState glassState = GetData06(glassInfo.ProductInfo.LCDCode);
                string oldLogCode = glassState.LogCode;
                string HistoryCode = glassState.HistoryCode;
                //更新已过工序信息
                string ComparerResult = "";
                string[] newList = newLogCode.Trim(',').Split(',');
                string[] oldList = oldLogCode.Trim(',').Split(',');
                // 当前已过的最后一道工序
                string lastProcess = oldList[oldList.Length - 1];

                // 取当前已过工序和应保留工序链工序的交集，作为新的已过工序
                var DiffList = newList.Intersect(oldList).ToList();
                for (int i = 0; i < DiffList.Count; i++)
                {
                    ComparerResult += DiffList[i] + ",";
                }
                if (DiffList.Count > 0)
                {
                    glassState.LogNumber = DiffList[DiffList.Count - 1].ToString();
                }
                else
                {
                    glassState.LogNumber = "";
                }
                glassState.LogCode = ComparerResult.TrimEnd(',');
                if (ComparerResult.TrimEnd(',') != "")
                {
                    glassState.HistoryCode = HistoryCode + oldLogCode.Replace(ComparerResult.TrimEnd(','), "");
                }
                else
                {
                    glassState.HistoryCode = HistoryCode + oldLogCode;
                }

                glassState.Repeat = "True";
                glassState.LCDState = reworkState;

                //更新Data_06
                try
                {
                    bool UpdateSuccess = UpdateData06(glassInfo.ProductInfo.LCDCode, glassState);
                    if (!UpdateSuccess)
                    {
                        return "更新产品过站信息失败";
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("单一RowKey更新Data_06失败.|" + exp.Message, exp);
                    Reconnect();
                    return "单一RowKey更新Data_06失败";
                }

                // 第三步：删除data_01中的绑定条码
                // --------------------------------------------------------------------------------------------
                //删除条码绑定
                try
                {
                    //不包含LCD投入需要解绑LCD、FPC、TP、背光、成品喷码
                    if (!newLogCode.Contains("005"))
                    {
                        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.LCDCode, "logValue:");
                        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.FPCCode, "logValue:");
                        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.TPCode, "logValue:");
                        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                    }
                    //不包含FOG需要解绑FPC、TP、背光、成品喷码
                    else
                    {
                        dynamic objLCD = GetHBaseDataClass.Instance.GetData01(glassInfo.ProductInfo.LCDCode);
                        string fpc = objLCD.fpcCode;
                        string tp = objLCD.tpCode;
                        string bl = objLCD.backCode;
                        string qr = objLCD.intactCode;

                        if (!newLogCode.Contains("008"))
                        {
                            objLCD.fpcCode = "";
                            objLCD.tpCode = "";
                            objLCD.backCode = "";
                            objLCD.intactCode = "";
                            try
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", glassInfo.ProductInfo.LCDCode, objLCD);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error("玻璃" + glassInfo.ProductInfo.LCDCode + "修改data_01失败", ex);
                            }
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.FPCCode, "logValue:");
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.TPCode, "logValue:");
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                        } //不包含TP贴合需要解绑TP、背光、成品喷码
                        else if (!newLogCode.Contains("018"))
                        {
                            objLCD.tpCode = "";
                            objLCD.backCode = "";
                            objLCD.intactCode = "";
                            try
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", glassInfo.ProductInfo.LCDCode, objLCD);
                                if (!string.IsNullOrWhiteSpace(fpc))
                                {
                                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objLCD);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error("玻璃" + glassInfo.ProductInfo.LCDCode + "修改data_01失败", ex);
                            }
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.TPCode, "logValue:");
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                        }
                        //不包含背光绑定需要解绑背光、成品喷码
                        else if (!newLogCode.Contains("024"))
                        {
                            objLCD.backCode = "";
                            objLCD.intactCode = "";
                            try
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", glassInfo.ProductInfo.LCDCode, objLCD);
                                if (!string.IsNullOrWhiteSpace(fpc))
                                {
                                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objLCD);
                                }
                                if (!string.IsNullOrWhiteSpace(tp))
                                {
                                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objLCD);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error("玻璃" + glassInfo.ProductInfo.LCDCode + "修改data_01失败", ex);
                            }
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                        }
                        //不包含成品喷码需要解绑成品喷码
                        else if (!newLogCode.Contains("032") && !newLogCode.Contains("039"))
                        {
                            objLCD.intactCode = "";
                            try
                            {
                                GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", glassInfo.ProductInfo.LCDCode, objLCD);
                                if (!string.IsNullOrWhiteSpace(fpc))
                                {
                                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", fpc, objLCD);
                                }
                                if (!string.IsNullOrWhiteSpace(tp))
                                {
                                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", tp, objLCD);
                                }
                                if (!string.IsNullOrWhiteSpace(bl))
                                {
                                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", bl, objLCD);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error("玻璃" + glassInfo.ProductInfo.LCDCode + "修改data_01失败", ex);
                            }
                            GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                        }
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("编码解绑失败|" + exp.Message, exp);
                    Reconnect();
                    return "编码解绑失败";
                }

                //// 第四步：SQL Server中添加解绑记录
                //// --------------------------------------------------------------------------------------------
                //try
                //{
                //    string logString = string.Format(@"INSERT INTO HB_Product_RepeatLog(PRL_GlassCode,PRL_ProductCode,PRL_ProductSpec,PRL_LineCode,PRL_RepeatProcess,PRL_AddDate,PRL_LastProcess)
                //    VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                //    glassInfo.ProductInfo.LCDCode,
                //    productCode,
                //    glassInfo.ProductInfo.FinishesModel,
                //    glassInfo.ProductInfo.ProductionLineCode,
                //    reworkProcess,
                //    GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"),
                //    lastProcess);
                //    conn.ExecuteAction(logString, "Base");
                //}
                //catch (Exception exp)
                //{
                //    LogHelper.Error("sql添加解绑记录失败|" + exp.Message, exp);
                //}



                //第五步：更新exception_01产品不良信息
                // --------------------------------------------------------------------------------------------
                //获取产品提交的不良信息exception_01
                //List<TRowResult> exceptionRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(exceptionInfo.ExceptionKey, "exception_01");
                //if (exceptionRowResult.Count == 0)
                //{
                //    return "RowKeyCode不存在";
                //}
                ////获取产品提交的不良信息exception_01
                //ExceptionInfo exceptionInfo = glassInfo.Exception[glassInfo.LastExceptionKey];
                if (isRoot)
                {
                    ///"productLineCode": "5301",
                    ///"glassCode": "P393D207TA02",
                    ///"productLineName": "TXD生产53线",
                    ///"deviceIp": "172.16.8.216",
                    ///"finishesCode": "711-3062",
                    ///"processCode": "031",
                    ///"scanTime": "2019-10-30 07:37:55",
                    ///"finishesModel": "TXDY610SAXPUB-1V3[A5]",
                    ///"exceptionState": "复判良品",
                    ///"processName": "复测AOI",
                    //"lotNumber": "5393201910300712",
                    //"lotNumberSecond": "5301031201910300712",
                    ///"scanNumber": "",
                    ///"exceptionContent": "亮团",
                    ///"productionOrder": "SCGZ20190402017",
                    //"repeatStatus": "0",
                    //"reworkStatus": "1",
                    ///"judgeNumber": "G10799",
                    ///"judgeContent": "",
                    ///"judgeTime": "2019-10-30 15:59:37",
                    ///"judgeIp": "172.16.9.159",
                    ///"reWorkNumber": "",
                    ///"reWorkTime": "",
                    ///"reWorkIp": "",
                    ///"reWorkContent": "",
                    ///"reWorkProcess": ""
                    exceptionInfo = new ExceptionInfo()
                    {
                        ExceptionKey = string.Format("$G{0}$T{1}$P{2}", glassInfo.ProductInfo.LCDCode, GetServerTime().ToString("yyyyMMddHHmmss"), lastProcess),
                        GlassCode = glassInfo.ProductInfo.LCDCode,
                        ProductionOrder = glassInfo.ProductInfo.ProductionOrder,
                        FinishesCode = glassInfo.ProductInfo.FinishesCode,
                        FinishesModel = glassInfo.ProductInfo.FinishesModel,
                        ProductionLineCode = glassInfo.ProductInfo.ProductionLineCode,
                        ProductionLineName = glassInfo.ProductInfo.ProductionLineCode,
                        ProcessCode = reworkProcess,
                        processName = GetProcessName(reworkProcess),
                        ScanNumber = reworkNumber,
                        ScanTime = reworkTime,
                        DeviceIp = reworkIP,
                        ExceptionContent = "强制解绑",
                        JudgeNumber = reworkNumber,
                        JudgeContent = "强制解绑",
                        JudgeTime = reworkTime,
                        JudgeIp = reworkIP,
                        ReworkStatus = "0"
                    };
                }

                exceptionInfo.ExceptionState = reworkState;
                exceptionInfo.ReworkNumber = reworkNumber;
                exceptionInfo.ReworkContent = reworkContent;
                exceptionInfo.ReworkTime = reworkTime;
                exceptionInfo.ReworkIp = reworkIP;
                exceptionInfo.ReworkProcess = reworkProcess;

                try
                {
                    bool UpdateSuccess = UpdateException01(exceptionInfo.ExceptionKey, exceptionInfo);
                    if (!UpdateSuccess)
                    {
                        return "更新产品重工信息失败";
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("单一RowKey重工exception_01.|" + exp.Message, exp);
                    Reconnect();
                    return "单一RowKey重工exception_01失败";
                }


                // 更新exception_04
                try
                {
                    if (!isRoot)
                    {
                        // 复判时间
                        DateTime tRework = DateTime.Parse(reworkTime);
                        // 确定复判时间段（1：前半小时，2：后半小时）
                        int part = tRework.Minute < 30 ? 1 : 2;
                        // 产线编码
                        string lineCode = glassInfo.Exception[glassInfo.LastExceptionKey].ProductionLineCode;
                        // 工序编码
                        string processCode = glassInfo.Exception[glassInfo.LastExceptionKey].ProcessCode;
                        // RowKey
                        string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, tRework.ToString("yyyyMMddHH"), part);
                        // 时段内exception_01的key值列表
                        List<string> exception01searchCodes = new List<string>();
                        // 从exception_04获取key值列表
                        List<TRowResult> exception04RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_04");
                        foreach (var exception04Row in exception04RowResult)
                        {
                            //获取rowKey
                            string exception04RowKey = Encoding.UTF8.GetString(exception04Row.Row);
                            foreach (var exception04key in exception04Row.Columns)
                            {
                                //获取rowValue
                                string exception04RowValue = Encoding.UTF8.GetString(exception04key.Value.Value);
                                System.Diagnostics.Debug.Print(exception04RowValue);
                                exception01searchCodes = (exception04RowValue.Split(',')).ToList<string>();
                            }
                        }
                        // 将本次重工的不良项的key值加入列表
                        exception01searchCodes.Add(glassInfo.LastExceptionKey);
                        // 转换成","分隔的字符串
                        string value = string.Join(",", exception01searchCodes);
                        // 回写exception_04
                        bool UpdateSuccess = UpdateHBaseKeyValue("exception_04", searchCode, value);
                        if (!UpdateSuccess)
                        {
                            return "重工失败";
                        }
                    }

                    return "重工成功";
                }
                catch (Exception exp)
                {
                    LogHelper.Error("更新exception_04失败.|" + exp.Message, exp);
                    Reconnect();
                    return "重工失败";
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("单一RowKey重工exception_01.|" + exp.Message, exp);
                return "重工失败";
            }
        }


        /// <summary>
        /// 解绑精简画面间的OQC的工序
        /// </summary>
        /// <param name="glassInfo">玻璃信息对象GlassInfo</param>
        /// <param name="reworkProcess">解绑工序</param>
        /// <returns></returns>
        public string UnBound039(GlassInfo glassInfo, string reworkProcess = "029")
        {
            try
            {
                // 第一步：更新data_06产品过站信息和状态
                // --------------------------------------------------------------------------------------------
                string productCode = glassInfo.ProductInfo.ProductionOrder;
                //获取工单型号的工序链信息
                string processString = string.Format(@"SELECT SPOM_JobCode AS productCode,SGX_JobCode AS processCode,SGX_Name AS processName,PR_NoSeq AS processSort 
                FROM TPL_ProductProcess_Route LEFT JOIN Store_ProduceOrder_Main ON PR_SPOMTid=SPOM_Tid LEFT JOIN Store_GongXu_Main ON PR_SGXTid=SGX_Tid
                WHERE SPOM_JobCode='{0}' ORDER BY PR_NoSeq ASC", productCode);
                DataView processView = conn.ExecuteDataView(processString, "Base");
                if (processView.Count <= 0)
                {
                    return "未获取到当前条码对应工单的工序链信息";
                }
                string newLogCode = "", processLogCode = "";
                string UnBoundCode = string.Empty;
                // 产品应过工序字符串
                for (int i = 0; i < processView.Count; i++)
                {
                    processLogCode += processView[i]["processCode"].ToString() + ",";
                }
                //需要保留的工序信息
                if (!string.IsNullOrEmpty(reworkProcess))
                {
                    newLogCode = processLogCode.TrimEnd(',').Substring(0, processLogCode.TrimEnd(',').IndexOf(reworkProcess)).TrimEnd(',');
                    UnBoundCode= processLogCode.TrimEnd(',').Substring(processLogCode.TrimEnd(',').IndexOf("029")).TrimEnd(',');
                }
                else
                {
                    newLogCode = processLogCode.TrimEnd(',');
                }
                //获取已经过的工序信息data_06
                GlassState glassState = GetData06(glassInfo.ProductInfo.LCDCode);
                string oldLogCode = glassState.LogCode;
                string HistoryCode = glassState.HistoryCode;
                //更新已过工序信息
                string ComparerResult = "";
                string[] newList = newLogCode.Trim(',').Split(',');
                string[] oldList = oldLogCode.Trim(',').Split(',');
                // 当前已过的最后一道工序
                string lastProcess = oldList[oldList.Length - 1];

                // 取当前已过工序和应保留工序链工序的交集，作为新的已过工序
                var DiffList = newList.Intersect(oldList).ToList();
                for (int i = 0; i < DiffList.Count; i++)
                {
                    ComparerResult += DiffList[i] + ",";
                }
                if (DiffList.Count > 0)
                {
                    glassState.LogNumber = DiffList[DiffList.Count - 1].ToString();
                }
                else
                {
                    glassState.LogNumber = "";
                }
                glassState.LogCode = ComparerResult.TrimEnd(',');
                if (ComparerResult.TrimEnd(',') != "")
                {
                    glassState.HistoryCode = HistoryCode + oldLogCode.Replace(ComparerResult.TrimEnd(','), "");
                }
                else
                {
                    glassState.HistoryCode = HistoryCode + oldLogCode;
                }

                //glassState.Repeat = "True";
                //glassState.LCDState = reworkState;

                //更新Data_06
                try
                {
                    bool UpdateSuccess = UpdateData06(glassInfo.ProductInfo.LCDCode, glassState);
                    if (!UpdateSuccess)
                    {
                        return "更新产品过站信息失败";
                    }
                    else
                    {
                        return "更新产品过站信息成功";
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("单一RowKey更新Data_06失败.|" + exp.Message, exp);
                    Reconnect();
                    return "单一RowKey更新Data_06失败";
                }
                // 第三步：删除data_01中的绑定条码
                try
                {
                    //如果新码中包含 039 则 解除客户码绑定
                    if (UnBoundCode.Contains("039"))
                    {
                        GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("编码解绑失败|" + exp.Message, exp);
                    Reconnect();
                    return "编码解绑失败";
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("单一RowKey重工exception_01.|" + exp.Message, exp);
                return "解除失败";
            }
        }

        /// <summary>
        /// HBase回写Exception_01
        /// </summary>
        /// <param name="rowKey"></param>
        /// <param name="exceptionInfo"></param>
        /// <returns></returns>
        public bool UpdateException01(string rowKey, ExceptionInfo exceptionInfo)
        {
            bool UpdateSuccess = false;
            //更新exception_01
            string exceptionRowValue =
                "{\"productLineCode\":\"" + exceptionInfo.ProductionLineCode +
                "\",\"glassCode\":\"" + exceptionInfo.GlassCode +
                "\",\"productLineName\":\"" + exceptionInfo.ProductionLineName +
                "\",\"deviceIp\":\"" + exceptionInfo.DeviceIp +
                "\",\"finishesCode\":\"" + exceptionInfo.FinishesCode +
                "\",\"processCode\":\"" + exceptionInfo.ProcessCode +
                "\",\"scanTime\":\"" + exceptionInfo.ScanTime +
                "\",\"finishesModel\":\"" + exceptionInfo.FinishesModel +
                "\",\"exceptionState\":\"" + exceptionInfo.ExceptionState +
                "\",\"processName\":\"" + exceptionInfo.processName +
                "\",\"lotNumber\":\"" + exceptionInfo.LotNumber +
                "\",\"lotNumberSecond\":\"" + exceptionInfo.LotNumberSecond +
                "\",\"scanNumber\":\"" + exceptionInfo.ScanNumber +
                "\",\"exceptionContent\":\"" + exceptionInfo.ExceptionContent +
                "\",\"productionOrder\":\"" + exceptionInfo.ProductionOrder +
                "\",\"Repeat\":\"" + exceptionInfo.Repeat +
                "\",\"repeatStatus\":\"" + exceptionInfo.RepeatStatus +
                "\",\"reworkStatus\":\"" + exceptionInfo.ReworkStatus +
                "\",\"judgeNumber\":\"" + exceptionInfo.JudgeNumber +
                "\",\"judgeContent\":\"" + exceptionInfo.JudgeContent +
                "\",\"judgeTime\":\"" + exceptionInfo.JudgeTime +
                "\",\"judgeIp\":\"" + exceptionInfo.JudgeIp +
                "\",\"reWorkNumber\":\"" + exceptionInfo.ReworkNumber +//重工人员 
                "\",\"reWorkTime\":\"" + exceptionInfo.ReworkTime + //重工时间
                "\",\"reWorkIp\":\"" + exceptionInfo.ReworkIp +//重工Ip
                "\",\"reWorkContent\":\"" + exceptionInfo.ReworkContent +//重工异常项
                "\",\"reWorkProcess\":\"" + exceptionInfo.ReworkProcess +//重工工序
                "\"}";
            System.Diagnostics.Debug.Print(exceptionRowValue);
            Mutation _mutation = new Mutation();
            _mutation.Column = Encoding.UTF8.GetBytes("logValue:");
            _mutation.Value = Encoding.UTF8.GetBytes(exceptionRowValue);
            UpdateSuccess = GetDataFromHBase.Instance.MutateRowHBase("exception_01", rowKey, new List<Mutation> { _mutation });
            return UpdateSuccess;
        }

        /// <summary>
        /// HBase回写data_06
        /// </summary>
        /// <param name="glassCode">玻璃编码</param>
        /// <param name="glassState">状态类</param>
        /// <returns></returns>
        public bool UpdateData06(string glassCode, GlassState glassState)
        {
            bool UpdateSuccess = false;
            //更新Data_06
            string logCodeRowValue =
                "{\"HistoryCode\":\"" + glassState.HistoryCode.Trim(',') +
                "\",\"logNumber\":\"" + glassState.LogNumber +
                "\",\"logCode\":\"" + glassState.LogCode.Trim(',') +
                "\",\"lcdState\":\"" + glassState.LCDState +
                "\",\"Repeat\":\"" + glassState.Repeat +
                "\"}";
            System.Diagnostics.Debug.Print(logCodeRowValue);
            Mutation _mutation = new Mutation();
            _mutation.Column = Encoding.UTF8.GetBytes("logValue:");
            _mutation.Value = Encoding.UTF8.GetBytes(logCodeRowValue);
            UpdateSuccess = GetDataFromHBase.Instance.MutateRowHBase("data_06", glassCode, new List<Mutation> { _mutation });
            return UpdateSuccess;
        }

        /// <summary>
        /// 回写HBase数据
        /// </summary>
        /// <param name="tableName">HBase数据表名</param>
        /// <param name="key">查询关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool UpdateHBaseKeyValue(string tableName, string key, string value)
        {
            bool UpdateSuccess = false;
            Mutation _mutation = new Mutation();
            _mutation.Column = Encoding.UTF8.GetBytes("logValue:");
            _mutation.Value = Encoding.UTF8.GetBytes(value);
            UpdateSuccess = GetDataFromHBase.Instance.MutateRowHBase(tableName, key, new List<Mutation> { _mutation });
            return UpdateSuccess;
        }

        /// <summary>
        /// 回写HBase数据表键值对
        /// </summary>
        /// <param name="tableName">HBase数据表名</param>
        /// <param name="key">查询关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool UpdateHBaseKeyValue(string tableName, string key, dynamic jobject)
        {
            bool UpdateSuccess = false;
            Mutation _mutation = new Mutation();
            _mutation.Column = Encoding.UTF8.GetBytes("logValue:");
            string value = jobject.ToString();
            _mutation.Value = Encoding.UTF8.GetBytes(value);
            UpdateSuccess = GetDataFromHBase.Instance.MutateRowHBase(tableName, key, new List<Mutation> { _mutation });
            return UpdateSuccess;
        }

        ///// <summary>
        ///// 获取指定时间段的当前机台过站数据
        ///// </summary>
        ///// <param name="deviceCode">机台编码</param>
        ///// <param name="time">时间（精确到小时）</param>
        ///// <param name="time">时间段（1：前半小时，2：后半小时）</param>
        ///// <returns></returns>
        //public List<dynamic> GetGlassByTime(string deviceCode, DateTime time, int part)
        //{
        //    List<dynamic> lstProcessInfo = new List<dynamic>();
        //    for (int i = 2 * part - 1; i <= 2 * part; i++)
        //    {
        //        string searchCode = string.Format("{0}{1}1{2}", deviceCode, time.ToString("yyyyMMddHH"), i);
        //        List<TRowResult> date04RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_04");
        //        if (date04RowResult.Count <= 0)
        //        {
        //            continue;
        //        }
        //        foreach (var date04Row in date04RowResult)
        //        {
        //            //获取rowKey
        //            string date04RowKey = Encoding.UTF8.GetString(date04Row.Row);
        //            foreach (var date04key in date04Row.Columns)
        //            {
        //                try
        //                {
        //                    //获取rowValue
        //                    string date04RowValue = Encoding.UTF8.GetString(date04key.Value.Value);
        //                    System.Diagnostics.Debug.Print(date04RowValue);
        //                    string[] data02searchCodes = date04RowValue.Split(',');
        //                    foreach (string data02searchCode in data02searchCodes)
        //                    {
        //                        List<TRowResult> data02RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(data02searchCode, "data_02");
        //                        if (data02RowResult.Count <= 0)
        //                        {
        //                            continue;
        //                        }
        //                        foreach (var data02Row in data02RowResult)
        //                        {
        //                            //获取rowKey
        //                            string data02RowKey = Encoding.UTF8.GetString(data02Row.Row);
        //                            foreach (var key in data02Row.Columns)
        //                            {
        //                                try
        //                                {
        //                                    //获取rowValue
        //                                    string data02RowValue = Encoding.UTF8.GetString(key.Value.Value);
        //                                    System.Diagnostics.Debug.Print(data02RowValue);
        //                                    //反序列化rowValue
        //                                    dynamic reObj = JObject.Parse(data02RowValue);
        //                                    lstProcessInfo.Add(reObj);
        //                                    //JObject jsonStr = JObject.Parse(data02RowValue);
        //                                    //System.Diagnostics.Debug.Print(jsonStr.ToString());

        //                                }
        //                                catch (Exception exp)
        //                                {
        //                                    LogHelper.Error("从data_02获取过站信息失败：" + exp.Message, exp);
        //                                }
        //                            }//foreach (var key in glassKey.Columns)
        //                        }//foreach (var glassKey in glassRowResult)
        //                    }
        //                }
        //                catch (Exception exp)
        //                {
        //                    LogHelper.Error("获取过站信息失败：" + exp.Message, exp);
        //                }
        //            }//foreach (var key in glassKey.Columns)
        //        }//foreach (var glassKey in glassRowResult)
        //    }
        //    return lstProcessInfo;
        //}

        ///// <summary>
        ///// 获取指定时间段的当前机台不良数据
        ///// </summary>
        ///// <param name="deviceCode">机台编码</param>
        ///// <param name="time">时间（精确到小时）</param>
        ///// <param name="time">时间段（1：前半小时，2：后半小时）</param>
        ///// <returns></returns>
        //public List<dynamic> GetExceptionByTime(string deviceCode, DateTime time, int part)
        //{
        //    List<dynamic> lstExceptionInfo = new List<dynamic>();
        //    string searchCode = string.Format("{0}{1}1{2}", deviceCode, time.ToString("yyyyMMddHH"), part);
        //    List<TRowResult> exception02RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_02");
        //    foreach (var exception02Row in exception02RowResult)
        //    {
        //        //获取rowKey
        //        string exception02RowKey = Encoding.UTF8.GetString(exception02Row.Row);
        //        foreach (var exception02key in exception02Row.Columns)
        //        {
        //            try
        //            {
        //                //获取rowValue
        //                string exception02RowValue = Encoding.UTF8.GetString(exception02key.Value.Value);
        //                System.Diagnostics.Debug.Print(exception02RowValue);
        //                string[] exception01searchCodes = exception02RowValue.Split(',');
        //                foreach (string exception01searchCode in exception01searchCodes)
        //                {
        //                    List<TRowResult> exception01RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(exception01searchCode, "exception_01");
        //                    if (exception01RowResult.Count <= 0)
        //                    {
        //                        continue;
        //                    }
        //                    foreach (var exception01Row in exception01RowResult)
        //                    {
        //                        //获取rowKey
        //                        string exception01RowKey = Encoding.UTF8.GetString(exception01Row.Row);
        //                        foreach (var key in exception01Row.Columns)
        //                        {
        //                            try
        //                            {
        //                                //获取rowValue
        //                                string exception01RowValue = Encoding.UTF8.GetString(key.Value.Value);
        //                                System.Diagnostics.Debug.Print(exception01RowValue);
        //                                //反序列化rowValue
        //                                dynamic reObj = JObject.Parse(exception01RowValue);
        //                                lstExceptionInfo.Add(reObj);
        //                                //JObject jsonStr = JObject.Parse(data02RowValue);
        //                                //System.Diagnostics.Debug.Print(jsonStr.ToString());

        //                            }
        //                            catch (Exception exp)
        //                            {
        //                                LogHelper.Error("从exception_01中获取不良申报信息失败：" + exp.Message, exp);
        //                            }
        //                        }//foreach (var key in glassKey.Columns)
        //                    }//foreach (var glassKey in glassRowResult)
        //                }
        //            }
        //            catch (Exception exp)
        //            {
        //                LogHelper.Error("获取不良申报信息失败：" + exp.Message, exp);
        //            }
        //        }//foreach (var key in glassKey.Columns)
        //    }//foreach (var glassKey in glassRowResult)
        //    return lstExceptionInfo;
        //}

        /// <summary>
        /// 获取指定时间段、产线、工序的过站玻璃码
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="time">时间（精确到小时）</param>
        /// <param name="part">时间段（1：前半小时，2：后半小时）</param>
        /// <returns></returns>
        public List<string> GetGlassCodeByTime(string lineCode, string processCode, DateTime time, int part)
        {
            List<string> lstGlass = new List<string>();
            string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, time.ToString("yyyyMMddHH"), part);
            List<TRowResult> date08RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_08");
            foreach (var date08Row in date08RowResult)
            {
                //获取rowKey
                string date08RowKey = Encoding.UTF8.GetString(date08Row.Row);
                foreach (var date08key in date08Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string date08RowValue = Encoding.UTF8.GetString(date08key.Value.Value);
                        System.Diagnostics.Debug.Print(date08RowValue);
                        string[] data02searchCodes = date08RowValue.Split(',');
                        foreach (string key in data02searchCodes)
                        {
                            string lcd = key.Split('$')[1].Substring(1);
                            lstGlass.Add(lcd);
                        }
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取玻璃码列表失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)
            return lstGlass;
        }

        /// <summary>
        /// 获取指定时间段、产线、工序的过站数据
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="time">时间（精确到小时）</param>
        /// <param name="part">时间段（1：前半小时，2：后半小时）</param>
        /// <returns></returns>
        public List<dynamic> GetGlassByTime(string lineCode, string processCode, DateTime time, int part)
        {
            List<dynamic> lstProcessInfo = new List<dynamic>();
            string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, time.ToString("yyyyMMddHH"), part);
            List<TRowResult> date08RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_08");
            foreach (var date08Row in date08RowResult)
            {
                //获取rowKey
                string date08RowKey = Encoding.UTF8.GetString(date08Row.Row);
                foreach (var date08key in date08Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string date08RowValue = Encoding.UTF8.GetString(date08key.Value.Value);
                        System.Diagnostics.Debug.Print(date08RowValue);
                        string[] data02searchCodes = date08RowValue.Split(',');
                        foreach (string data02searchCode in data02searchCodes)
                        {
                            List<TRowResult> data02RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(data02searchCode, "data_02");
                            if (data02RowResult.Count <= 0)
                            {
                                continue;
                            }
                            foreach (var data02Row in data02RowResult)
                            {
                                //获取rowKey
                                string data02RowKey = Encoding.UTF8.GetString(data02Row.Row);
                                foreach (var key in data02Row.Columns)
                                {
                                    try
                                    {
                                        //获取rowValue
                                        string data02RowValue = Encoding.UTF8.GetString(key.Value.Value);
                                        System.Diagnostics.Debug.Print(data02RowValue);
                                        //反序列化rowValue
                                        dynamic reObj = JObject.Parse(data02RowValue);
                                        lstProcessInfo.Add(reObj);
                                        //JObject jsonStr = JObject.Parse(data02RowValue);
                                        //System.Diagnostics.Debug.Print(jsonStr.ToString());

                                    }
                                    catch (Exception exp)
                                    {
                                        LogHelper.Error("从data_02获取过站信息失败：" + exp.Message, exp);
                                    }
                                }//foreach (var key in glassKey.Columns)
                            }//foreach (var glassKey in glassRowResult)
                        }
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取过站信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)
            return lstProcessInfo;
        }

        /// <summary>
        /// 获取指定时间段的产线工序所有申报、复判、重工的不良数据
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="time">时间（精确到小时）</param>
        /// <param name="part">时间段（1：前半小时，2：后半小时）</param>
        /// <returns></returns>
        public List<dynamic> GetExceptionByTime(string lineCode, string processCode, DateTime time, int part)
        {
            List<dynamic> lstExceptionInfo = new List<dynamic>();
            string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, time.ToString("yyyyMMddHH"), part);

            string[] keys01 = new string[0];    //用于查询exception_01的key值集合
            string[] keys05 = new string[0];    //exception_05保存所有申报不良的key值集合
            string[] keys03 = new string[0];    //exception_03保存所有复判不良的key值集合
            string[] keys04 = new string[0];    //exception_04保存所有重工不良的key值集合

            // 获取exception_05保存所有申报不良的key值集合
            List<TRowResult> exception05RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_05");
            foreach (var exception05Row in exception05RowResult)
            {
                //获取rowKey
                string exception05RowKey = Encoding.UTF8.GetString(exception05Row.Row);
                foreach (var exception05key in exception05Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string exception05RowValue = Encoding.UTF8.GetString(exception05key.Value.Value);
                        System.Diagnostics.Debug.Print(exception05RowValue);
                        keys05 = exception05RowValue.Split(',');
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取不良申报信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)

            //exception_03保存所有复判不良的key值集合
            List<TRowResult> exception03RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_03");
            foreach (var exception03Row in exception03RowResult)
            {
                //获取rowKey
                string exception03RowKey = Encoding.UTF8.GetString(exception03Row.Row);
                foreach (var exception03key in exception03Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string exception03RowValue = Encoding.UTF8.GetString(exception03key.Value.Value);
                        System.Diagnostics.Debug.Print(exception03RowValue);
                        keys03 = exception03RowValue.Split(',');
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取不良复判信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)

            //exception_04保存所有重工不良的key值集合
            List<TRowResult> exception04RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_04");
            foreach (var exception04Row in exception04RowResult)
            {
                //获取rowKey
                string exception04RowKey = Encoding.UTF8.GetString(exception04Row.Row);
                foreach (var exception04key in exception04Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string exception04RowValue = Encoding.UTF8.GetString(exception04key.Value.Value);
                        System.Diagnostics.Debug.Print(exception04RowValue);
                        keys04 = exception04RowValue.Split(',');
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取不良重工信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)

            // 将三个集合合并去重
            keys01 = keys05.Union(keys03).Union(keys04).ToArray<string>();

            // 从exception_01中查询所有不良申报、复判、重工信息
            foreach (string exception01searchCode in keys01)
            {
                List<TRowResult> exception01RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(exception01searchCode, "exception_01");
                if (exception01RowResult.Count <= 0)
                {
                    continue;
                }
                foreach (var exception01Row in exception01RowResult)
                {
                    //获取rowKey
                    string exception01RowKey = Encoding.UTF8.GetString(exception01Row.Row);
                    foreach (var key in exception01Row.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string exception01RowValue = Encoding.UTF8.GetString(key.Value.Value);
                            System.Diagnostics.Debug.Print(exception01RowValue);
                            //反序列化rowValue
                            dynamic reObj = JObject.Parse(exception01RowValue);
                            lstExceptionInfo.Add(reObj);
                            //JObject jsonStr = JObject.Parse(data02RowValue);
                            //System.Diagnostics.Debug.Print(jsonStr.ToString());
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从exception_01中获取不良申报信息失败：" + exp.Message, exp);
                        }
                    }//foreach (var key in glassKey.Columns)
                }//foreach (var glassKey in glassRowResult)
            }
            return lstExceptionInfo;
        }

        /// <summary>
        /// 获取指定时间段的产线工序申报不良数据
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="time">时间（精确到小时）</param>
        /// <param name="part">时间段（1：前半小时，2：后半小时）</param>
        /// <returns></returns>
        public List<dynamic> GetReportExceptionByTime(string lineCode, string processCode, DateTime time, int part)
        {
            List<dynamic> lstExceptionInfo = new List<dynamic>();
            string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, time.ToString("yyyyMMddHH"), part);
            List<TRowResult> exception05RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_05");
            foreach (var exception05Row in exception05RowResult)
            {
                //获取rowKey
                string exception05RowKey = Encoding.UTF8.GetString(exception05Row.Row);
                foreach (var exception05key in exception05Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string exception05RowValue = Encoding.UTF8.GetString(exception05key.Value.Value);
                        System.Diagnostics.Debug.Print(exception05RowValue);
                        string[] exception01searchCodes = exception05RowValue.Split(',');
                        foreach (string exception01searchCode in exception01searchCodes)
                        {
                            List<TRowResult> exception01RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(exception01searchCode, "exception_01");
                            if (exception01RowResult.Count <= 0)
                            {
                                continue;
                            }
                            foreach (var exception01Row in exception01RowResult)
                            {
                                //获取rowKey
                                string exception01RowKey = Encoding.UTF8.GetString(exception01Row.Row);
                                foreach (var key in exception01Row.Columns)
                                {
                                    try
                                    {
                                        //获取rowValue
                                        string exception01RowValue = Encoding.UTF8.GetString(key.Value.Value);
                                        System.Diagnostics.Debug.Print(exception01RowValue);
                                        //反序列化rowValue
                                        dynamic reObj = JObject.Parse(exception01RowValue);
                                        lstExceptionInfo.Add(reObj);
                                        //JObject jsonStr = JObject.Parse(data02RowValue);
                                        //System.Diagnostics.Debug.Print(jsonStr.ToString());

                                    }
                                    catch (Exception exp)
                                    {
                                        LogHelper.Error("从exception_01中获取不良申报信息失败：" + exp.Message, exp);
                                    }
                                }//foreach (var key in glassKey.Columns)
                            }//foreach (var glassKey in glassRowResult)
                        }
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取不良申报信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)
            return lstExceptionInfo;
        }

        /// <summary>
        /// 获取指定时间段的产线工序复判不良数据
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="time">时间（精确到小时）</param>
        /// <param name="part">时间段（1：前半小时，2：后半小时）</param>
        /// <returns></returns>
        public List<dynamic> GetJudgeExceptionByTime(string lineCode, string processCode, DateTime time, int part)
        {
            List<dynamic> lstExceptionInfo = new List<dynamic>();
            string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, time.ToString("yyyyMMddHH"), part);
            List<TRowResult> exception03RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_03");
            foreach (var exception03Row in exception03RowResult)
            {
                //获取rowKey
                string exception03RowKey = Encoding.UTF8.GetString(exception03Row.Row);
                foreach (var exception03key in exception03Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string exception03RowValue = Encoding.UTF8.GetString(exception03key.Value.Value);
                        System.Diagnostics.Debug.Print(exception03RowValue);
                        string[] exception01searchCodes = exception03RowValue.Split(',');
                        foreach (string exception01searchCode in exception01searchCodes)
                        {
                            List<TRowResult> exception01RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(exception01searchCode, "exception_01");
                            if (exception01RowResult.Count <= 0)
                            {
                                continue;
                            }
                            foreach (var exception01Row in exception01RowResult)
                            {
                                //获取rowKey
                                string exception01RowKey = Encoding.UTF8.GetString(exception01Row.Row);
                                foreach (var key in exception01Row.Columns)
                                {
                                    try
                                    {
                                        //获取rowValue
                                        string exception01RowValue = Encoding.UTF8.GetString(key.Value.Value);
                                        System.Diagnostics.Debug.Print(exception01RowValue);
                                        //反序列化rowValue
                                        dynamic reObj = JObject.Parse(exception01RowValue);
                                        lstExceptionInfo.Add(reObj);
                                        //JObject jsonStr = JObject.Parse(data02RowValue);
                                        //System.Diagnostics.Debug.Print(jsonStr.ToString());

                                    }
                                    catch (Exception exp)
                                    {
                                        LogHelper.Error("从exception_01中获取不良复判信息失败：" + exp.Message, exp);
                                    }
                                }//foreach (var key in glassKey.Columns)
                            }//foreach (var glassKey in glassRowResult)
                        }
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取不良复判信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)
            return lstExceptionInfo;
        }

        /// <summary>
        /// 获取指定时间段的产线工序重工不良数据
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="time">时间（精确到小时）</param>
        /// <param name="part">时间段（1：前半小时，2：后半小时）</param>
        /// <returns></returns>
        public List<dynamic> GetReworkExceptionByTime(string lineCode, string processCode, DateTime time, int part)
        {
            List<dynamic> lstExceptionInfo = new List<dynamic>();
            string searchCode = string.Format("{0}{1}{2}1{3}", lineCode, processCode, time.ToString("yyyyMMddHH"), part);
            List<TRowResult> exception04RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "exception_04");
            foreach (var exception04Row in exception04RowResult)
            {
                //获取rowKey
                string exception04RowKey = Encoding.UTF8.GetString(exception04Row.Row);
                foreach (var exception04key in exception04Row.Columns)
                {
                    try
                    {
                        //获取rowValue
                        string exception04RowValue = Encoding.UTF8.GetString(exception04key.Value.Value);
                        System.Diagnostics.Debug.Print(exception04RowValue);
                        string[] exception01searchCodes = exception04RowValue.Split(',');
                        foreach (string exception01searchCode in exception01searchCodes)
                        {
                            List<TRowResult> exception01RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(exception01searchCode, "exception_01");
                            if (exception01RowResult.Count <= 0)
                            {
                                continue;
                            }
                            foreach (var exception01Row in exception01RowResult)
                            {
                                //获取rowKey
                                string exception01RowKey = Encoding.UTF8.GetString(exception01Row.Row);
                                foreach (var key in exception01Row.Columns)
                                {
                                    try
                                    {
                                        //获取rowValue
                                        string exception01RowValue = Encoding.UTF8.GetString(key.Value.Value);
                                        System.Diagnostics.Debug.Print(exception01RowValue);
                                        //反序列化rowValue
                                        dynamic reObj = JObject.Parse(exception01RowValue);
                                        lstExceptionInfo.Add(reObj);
                                        //JObject jsonStr = JObject.Parse(data02RowValue);
                                        //System.Diagnostics.Debug.Print(jsonStr.ToString());

                                    }
                                    catch (Exception exp)
                                    {
                                        LogHelper.Error("从exception_01中获取不良重工信息失败：" + exp.Message, exp);
                                    }
                                }//foreach (var key in glassKey.Columns)
                            }//foreach (var glassKey in glassRowResult)
                        }
                    }
                    catch (Exception exp)
                    {
                        LogHelper.Error("获取不良重工信息失败：" + exp.Message, exp);
                    }
                }//foreach (var key in glassKey.Columns)
            }//foreach (var glassKey in glassRowResult)
            return lstExceptionInfo;
        }

        /// <summary>
        /// 获取SQL Server服务器时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime()
        {
            string sql = "SELECT GETDATE()";
            object obj = conn.ExecuteScalar(sql, "Base");
            if (obj != null)
            {
                return DateTime.Parse(obj.ToString());
            }
            else
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 查询工单Wip
        /// </summary>
        /// <param name="searchId">工单Id</param>
        /// <param name="searchOrder">工单编号</param>
        /// <returns></returns>
        /// GetHBaseDataClass.SummaryScanByTime(lineView[line]["lineCode"].ToString(), day, "07", "02", "07:30");
        public DataSet SummaryScanByWip(string searchId, string searchOrder)
        {
            try
            {
                #region 构建查询返回数据Table
                DataTable byWipTable = new DataTable();
                byWipTable.Columns.Add("wip_productionOrder");//工单编号
                byWipTable.Columns.Add("wip_processSeq");//工序序号
                byWipTable.Columns.Add("wip_processCode");//工序编号
                byWipTable.Columns.Add("wip_processName");//工序名称
                byWipTable.Columns.Add("wip_glassCode");//Lcd条码
                byWipTable.Columns.Add("wip_firstWip", typeof(int));//Wip
                byWipTable.Columns.Add("wip_judgeWip", typeof(int));//复判Wip
                byWipTable.Columns.Add("wip_repeatWip", typeof(int));//重工Wip
                byWipTable.Columns.Add("wip_judgeScrap", typeof(int));//复判报废
                byWipTable.Columns.Add("wip_rwScrap", typeof(int));//重工报废
                byWipTable.Columns.Add("wip_except", typeof(int));//异常
                byWipTable.Columns.Add("wip_key", typeof(string));//key

                string prosCodeSql = @"SELECT DISTINCT SGX_JobCode AS prosCode,SGX_Name AS prosName,ISNULL(PR_NoSeq,100) AS prosSeq FROM Store_GongXu_Main WITH(NOLOCK)
                                       INNER JOIN  TPL_ProductProcess_Route  WITH(NOLOCK) ON  PR_SGXTid=SGX_Tid AND PR_SPOMTid='" + searchId + "'ORDER BY prosSeq ASC";
                DataView prosCodeView = conn.ExecuteDataView(prosCodeSql, "Base");
                #endregion

                #region 查找工序数据。
                //工单扫描Lcd集合  Key  工单编号+年(4位)+月(2位)+日(2位)
                List<TRowResult> wipLotRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix(searchOrder, "data_07");
                //循环工单在Lcd清洗投料扫描到的所有Lcd
                foreach (var wipLotRow in wipLotRowCount)
                {
                    //获取dtta_07表的rowkey(工单编号+年(4位)+月(2位)+日(2位))
                    string wipLotRowKey = Encoding.UTF8.GetString(wipLotRow.Row);
                    foreach (var wipLotkey in wipLotRow.Columns)
                    {
                        //获取dtta_07表的rowValue(Lcd编号按照逗号[,]分隔)
                        string wipLcdLotKey = Encoding.UTF8.GetString(wipLotkey.Value.Value);
                        //以多RowKey的方式在data_06表中查询Lcd的扫描记录及玻璃的当前状态 Key lcd编号
                        List<TRowResult> wipLcdRowCount = GetDataFromHBase.Instance.GetRowsWithManyRowKey((wipLcdLotKey.Trim(',')), "data_06");
                        if (wipLcdRowCount == null)
                        {
                            continue;
                        }
                        foreach (var wipLcdRow in wipLcdRowCount)
                        {
                            //获取dtta_06表的rowkey(lcd编号)
                            string lcdCode = Encoding.UTF8.GetString(wipLcdRow.Row);
                            //初始化投入产出等数据
                            int wip_firstWip = 0, wip_judgeWip = 0, wip_repeatWip = 0, wip_judgeScrap = 0, wip_rwScrap = 0, wip_except = 0;
                            string logCode = "", lcdState = "", prosSeq = "", prosCode = "", prosName = "";
                            foreach (var wipLcdkey in wipLcdRow.Columns)
                            {
                                //获取dtta_06表的rowValue(lcd 状态 扫描记录 解绑记录等信息)
                                string wipLcdValue = Encoding.UTF8.GetString(wipLcdkey.Value.Value);
                                //反序列化rowValue
                                JObject jsonStr = JObject.Parse(wipLcdValue);
                                logCode = "," + ((jsonStr["logCode"] ?? "").ToString().Trim(',')) + ",";
                                lcdState = (jsonStr["lcdState"] ?? "").ToString();
                                //循环工序信息（防止工单生产中更换工序链，循环全部的工序）
                                string earlyProsSeq = "", earlyProsCode = "", earlyProsName = "";
                                for (int pros = 0; pros < prosCodeView.Count; pros++)
                                {
                                    if (lcdState == "良品" || lcdState == "复判良品" || lcdState == "重工良品")//哪道工序没扫描就是哪道工序的Wip
                                    {
                                        if (!logCode.Contains("," + prosCodeView[pros]["prosCode"].ToString() + ","))
                                        {
                                            //表示产品在本道工序尚未扫描
                                            prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                            prosCode = prosCodeView[pros]["prosCode"].ToString();
                                            prosName = prosCodeView[pros]["prosName"].ToString();
                                            wip_firstWip = 1; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 0;
                                            break;
                                        }
                                    }
                                    else if (lcdState == "待复判")//Wip在扫描到的工序内。
                                    {
                                        if (logCode.Contains("," + prosCodeView[pros]["prosCode"].ToString() + ","))
                                        {
                                            earlyProsSeq = prosCodeView[pros]["prosSeq"].ToString();
                                            earlyProsCode = prosCodeView[pros]["prosCode"].ToString();
                                            earlyProsName = prosCodeView[pros]["prosName"].ToString();
                                        }
                                        else
                                        {
                                            prosSeq = earlyProsSeq;
                                            prosCode = earlyProsCode;
                                            prosName = earlyProsName;
                                            wip_firstWip = 0; wip_judgeWip = 1; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 0;
                                            break;
                                        }
                                    }
                                    else if (lcdState == "复判报废")//复判良品表示产品申报不良复判为报废,Wip清除,需要知道是在哪道工序申报的不良
                                    {
                                        //判断产品当前工序在exception_01表是否申报了不良
                                        List<TRowResult> expLcdRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + lcdCode, "exception_01");
                                        if (expLcdRowCount == null)
                                        {
                                            continue;
                                        }
                                        foreach (var expLcdRow in expLcdRowCount)
                                        {
                                            string expLcdCode = Encoding.UTF8.GetString(expLcdRow.Row);
                                            if (expLcdCode.Contains("$P" + prosCodeView[pros]["prosCode"].ToString()))
                                            {
                                                foreach (var expLcdKey in expLcdRow.Columns)
                                                {
                                                    //获取rowValue
                                                    string expLcdValue = Encoding.UTF8.GetString(expLcdKey.Value.Value);
                                                    JObject jsonExpStr = JObject.Parse(expLcdValue);
                                                    string exceptionState = (jsonExpStr["exceptionState"] ?? "").ToString();
                                                    if (exceptionState == "复判报废")
                                                    {
                                                        //表示产品在本道工序申报了不良复判为报废
                                                        prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                                        prosCode = prosCodeView[pros]["prosCode"].ToString();
                                                        prosName = prosCodeView[pros]["prosName"].ToString();
                                                        wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 1; wip_rwScrap = 0; wip_except = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (lcdState == "复判重工")
                                    {
                                        if (logCode.Contains("," + prosCodeView[pros]["prosCode"].ToString() + ","))
                                        {
                                            earlyProsSeq = prosCodeView[pros]["prosSeq"].ToString();
                                            earlyProsCode = prosCodeView[pros]["prosCode"].ToString();
                                            earlyProsName = prosCodeView[pros]["prosName"].ToString();
                                        }
                                        else
                                        {
                                            prosSeq = earlyProsSeq;
                                            prosCode = earlyProsCode;
                                            prosName = earlyProsName;
                                            wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 1; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 0;
                                            break;
                                        }
                                    }
                                    else if (lcdState == "重工报废")
                                    {
                                        //判断产品当前工序在exception_01表是否申报了不良
                                        List<TRowResult> expLcdRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + lcdCode, "exception_01");
                                        if (expLcdRowCount == null)
                                        {
                                            continue;
                                        }
                                        foreach (var expLcdRow in expLcdRowCount)
                                        {
                                            string expLcdCode = Encoding.UTF8.GetString(expLcdRow.Row);
                                            if (expLcdCode.Contains("$P" + prosCodeView[pros]["prosCode"].ToString()))
                                            {
                                                foreach (var expLcdKey in expLcdRow.Columns)
                                                {
                                                    //获取rowValue
                                                    string expLcdValue = Encoding.UTF8.GetString(expLcdKey.Value.Value);
                                                    JObject jsonExpStr = JObject.Parse(expLcdValue);
                                                    string exceptionState = (jsonExpStr["exceptionState"] ?? "").ToString();
                                                    if (exceptionState == "重工报废")
                                                    {
                                                        //表示产品在本道工序申报了不良重工为报废
                                                        prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                                        prosCode = prosCodeView[pros]["prosCode"].ToString();
                                                        prosName = prosCodeView[pros]["prosName"].ToString();
                                                        wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 1; wip_except = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //表示产品在本道工序申报了不良重工为报废
                                        prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                        prosCode = prosCodeView[pros]["prosCode"].ToString();
                                        prosName = prosCodeView[pros]["prosName"].ToString();
                                        wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 1;
                                        break;
                                    }
                                }

                                //更新实际数据
                                DataRow wipRow = byWipTable.NewRow();
                                wipRow["wip_productionOrder"] = searchOrder;//工单编号
                                wipRow["wip_processSeq"] = prosSeq;//工序序号
                                wipRow["wip_processCode"] = prosCode;//工序编号
                                wipRow["wip_processName"] = prosName;//工序名称
                                wipRow["wip_glassCode"] = lcdCode;//Lcd条码
                                wipRow["wip_firstWip"] = wip_firstWip;//Wip产出
                                wipRow["wip_judgeWip"] = wip_judgeWip;//复判Wip
                                wipRow["wip_repeatWip"] = wip_repeatWip;//重工Wip
                                wipRow["wip_judgeScrap"] = wip_judgeScrap;//复判报废
                                wipRow["wip_rwScrap"] = wip_rwScrap;//重工报废
                                wipRow["wip_except"] = wip_except;//异常
                                wipRow["wip_key"] = lcdCode + prosCode;//key
                                byWipTable.Rows.Add(wipRow);
                            }
                        }
                    }
                }
                #endregion

                #region 扫描元数据写入表格作分析
                //for (int i = 0; i < byWipTable.DefaultView.Count; i++)
                //{
                //    string resultString = @"INSERT INTO testtable1 (A,B,C,D,E,F,G,H,I,J,K,L)VALUES( '"
                //               + byWipTable.DefaultView[i]["wip_productionOrder"].ToString() + "','" + byWipTable.DefaultView[i]["wip_processSeq"].ToString() + "','" + byWipTable.DefaultView[i]["wip_processCode"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_processName"].ToString() + "','" + byWipTable.DefaultView[i]["wip_glassCode"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_firstWip"].ToString() + "', '" + byWipTable.DefaultView[i]["wip_judgeWip"].ToString() + "','" + byWipTable.DefaultView[i]["wip_repeatWip"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_judgeScrap"].ToString() + "','" + byWipTable.DefaultView[i]["wip_rwScrap"].ToString() + "','" + byWipTable.DefaultView[i]["wip_except"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_key"].ToString() + "')";
                //    conn.ExecuteAction(resultString, "ReportBase");
                //}
                #endregion

                #region 获取已打印标签的包装数据
                byWipTable.DefaultView.Sort = "wip_glassCode ASC"; //按照lcd排序
                byWipTable = byWipTable.DefaultView.ToTable();
                DataTable unRepeattable = byWipTable.Copy();//copy一个新的Table处理数据
                DataTable dtResult = unRepeattable.Clone();//按照 工单 工序分组合计数据
                DataTable dtName = unRepeattable.DefaultView.ToTable(true, "wip_productionOrder", "wip_processSeq", "wip_processCode", "wip_processName");
                unRepeattable.PrimaryKey = new DataColumn[] { unRepeattable.Columns["wip_key"] };//添加一个Key的主键

                string packSql = @"SELECT IBS_GlassCode AS IBS_GlassCode,IBS_productOrder FROM TPL_PackingInnerBox_Sub WHERE IBS_productOrder='" + searchOrder + "' AND ISNULL(IBS_IsPrint,0)=1";
                DataTable packTable = conn.ExecuteDataTable(packSql, "Base");
                for (int i = unRepeattable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow[] unRepeatRows = packTable.Select("IBS_GlassCode='" + unRepeattable.Rows[i]["wip_glassCode"] + "'");
                    if (unRepeatRows.Length > 0)
                    {
                        unRepeattable.Rows.RemoveAt(i);
                    }
                }
                #endregion

                #region 扫描元数据写入表格作分析(已过滤已包装数据)
                //for (int i = 0; i < unRepeattable.DefaultView.Count; i++)
                //{
                //    string resultString = @"INSERT INTO testtable1 (A,B,C,D,E,F,G,H,I,J,K,L,M)VALUES( '"
                //               + unRepeattable.DefaultView[i]["wip_productionOrder"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_processSeq"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_processCode"].ToString() + "','" +
                //               unRepeattable.DefaultView[i]["wip_processName"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_glassCode"].ToString() + "','"
                //               + unRepeattable.DefaultView[i]["wip_firstWip"].ToString() + "', '" + unRepeattable.DefaultView[i]["wip_judgeWip"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_repeatWip"].ToString() + "','"
                //               + unRepeattable.DefaultView[i]["wip_judgeScrap"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_rwScrap"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_except"].ToString() + "','"
                //               + unRepeattable.DefaultView[i]["wip_key"].ToString() + "','Wip')";
                //    conn.ExecuteAction(resultString, "ReportBase");
                //}
                #endregion

                #region 合计Wip
                for (int i = 0; i < dtName.Rows.Count; i++)
                {
                    DataRow[] rows = unRepeattable.Select("wip_productionOrder='" + dtName.Rows[i]["wip_productionOrder"] + "'and wip_processCode='" + dtName.Rows[i]["wip_processCode"] + "'");
                    //temp用来存储筛选出来的数据
                    DataTable temp = dtResult.Clone();
                    foreach (DataRow row in rows)
                    {
                        temp.Rows.Add(row.ItemArray);
                        //string resultString = @"INSERT INTO testtable1 (A,B,C,D)VALUES( '" + row.ItemArray[2].ToString() + "', '" + row.ItemArray[1].ToString() + "', '" + row.ItemArray[4].ToString() + "','temp')";
                        //conn.ExecuteAction(resultString, "ReportBase");
                    }
                    DataRow dr = dtResult.NewRow();
                    dr["wip_productionOrder"] = dtName.Rows[i]["wip_productionOrder"].ToString();//工单编号
                    dr["wip_processSeq"] = dtName.Rows[i]["wip_processSeq"].ToString();//工序序号
                    dr["wip_processCode"] = dtName.Rows[i]["wip_processCode"].ToString();//工序编号
                    dr["wip_processName"] = dtName.Rows[i]["wip_processName"].ToString();//工序名称
                    dr["wip_firstWip"] = temp.Compute("sum(wip_firstWip)", "");//Wip
                    dr["wip_judgeWip"] = temp.Compute("sum(wip_judgeWip)", "");//复判Wip
                    dr["wip_repeatWip"] = temp.Compute("sum(wip_repeatWip)", "");///重工Wip
                    dr["wip_judgeScrap"] = temp.Compute("sum(wip_judgeScrap)", "");//复判报废
                    dr["wip_rwScrap"] = temp.Compute("sum(wip_rwScrap)", "");////重工报废
                    dr["wip_except"] = temp.Compute("sum(wip_except)", "");//异常
                    dtResult.Rows.Add(dr);
                }
                #endregion

                #region  返回数据
                DataSet ds = new DataSet();
                ds.Tables.Add(dtResult);
                ds.Tables.Add(unRepeattable);
                return ds;
                #endregion
            }
            catch (Exception exp)
            {
                LogHelper.Error("工单Wip统计失败.", exp);
                return null;
            }
        }


        /// <summary>
        /// 查询工单Wip
        /// </summary>
        /// <param name="searchId">工单Id</param>
        /// <param name="searchOrder">工单编号</param>
        /// <returns></returns>
        /// GetHBaseDataClass.SummaryScanByTime(lineView[line]["lineCode"].ToString(), day, "07", "02", "07:30");
        public DataSet GetWipByOrder(string searchId, string searchOrder)
        {
            try
            {
                #region 构建查询返回数据Table
                DataTable byWipTable = new DataTable();
                byWipTable.Columns.Add("wip_productionOrder");//工单编号
                byWipTable.Columns.Add("wip_processSeq");//工序序号
                byWipTable.Columns.Add("wip_processCode");//工序编号
                byWipTable.Columns.Add("wip_processName");//工序名称
                byWipTable.Columns.Add("wip_glassCode");//Lcd条码
                byWipTable.Columns.Add("wip_input", typeof(int));//投入
                byWipTable.Columns.Add("wip_firstWip", typeof(int));//Wip
                byWipTable.Columns.Add("wip_judgeWip", typeof(int));//复判Wip
                byWipTable.Columns.Add("wip_repeatWip", typeof(int));//重工Wip
                byWipTable.Columns.Add("wip_output", typeof(int));//产出
                byWipTable.Columns.Add("wip_judgeScrap", typeof(int));//复判报废
                byWipTable.Columns.Add("wip_rwScrap", typeof(int));//重工报废
                byWipTable.Columns.Add("wip_except", typeof(int));//异常
                byWipTable.Columns.Add("wip_key", typeof(string));//key

                string prosCodeSql = @"SELECT DISTINCT SGX_JobCode AS prosCode,SGX_Name AS prosName,ISNULL(PR_NoSeq,100) AS prosSeq FROM Store_GongXu_Main WITH(NOLOCK)
                                       INNER JOIN  TPL_ProductProcess_Route  WITH(NOLOCK) ON  PR_SGXTid=SGX_Tid AND PR_SPOMTid='" + searchId + "'ORDER BY prosSeq ASC";
                DataView prosCodeView = conn.ExecuteDataView(prosCodeSql, "Base");
                #endregion

                #region 查找工序数据。
                //工单扫描Lcd集合  Key  工单编号+年(4位)+月(2位)+日(2位)
                List<TRowResult> wipLotRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix(searchOrder, "data_07");
                //循环工单在Lcd清洗投料扫描到的所有Lcd
                foreach (var wipLotRow in wipLotRowCount)
                {
                    //获取dtta_07表的rowkey(工单编号+年(4位)+月(2位)+日(2位))
                    string wipLotRowKey = Encoding.UTF8.GetString(wipLotRow.Row);
                    foreach (var wipLotkey in wipLotRow.Columns)
                    {
                        //获取dtta_07表的rowValue(Lcd编号按照逗号[,]分隔)
                        string wipLcdLotKey = Encoding.UTF8.GetString(wipLotkey.Value.Value);
                        //以多RowKey的方式在data_06表中查询Lcd的扫描记录及玻璃的当前状态 Key lcd编号
                        List<TRowResult> wipLcdRowCount = GetDataFromHBase.Instance.GetRowsWithManyRowKey((wipLcdLotKey.Trim(',')), "data_06");
                        if (wipLcdRowCount == null)
                        {
                            continue;
                        }
                        foreach (var wipLcdRow in wipLcdRowCount)
                        {
                            //获取dtta_06表的rowkey(lcd编号)
                            string lcdCode = Encoding.UTF8.GetString(wipLcdRow.Row);
                            //初始化投入产出等数据
                            int wip_firstWip = 0, wip_judgeWip = 0, wip_repeatWip = 0, wip_judgeScrap = 0, wip_rwScrap = 0, wip_except = 0;
                            string logCode = "", lcdState = "", prosSeq = "", prosCode = "", prosName = "";
                            foreach (var wipLcdkey in wipLcdRow.Columns)
                            {
                                //获取dtta_06表的rowValue(lcd 状态 扫描记录 解绑记录等信息)
                                string wipLcdValue = Encoding.UTF8.GetString(wipLcdkey.Value.Value);
                                //反序列化rowValue
                                JObject jsonStr = JObject.Parse(wipLcdValue);
                                logCode = "," + ((jsonStr["logCode"] ?? "").ToString().Trim(',')) + ",";
                                lcdState = (jsonStr["lcdState"] ?? "").ToString();
                                //循环工序信息（防止工单生产中更换工序链，循环全部的工序）
                                string earlyProsSeq = "", earlyProsCode = "", earlyProsName = "";
                                for (int pros = 0; pros < prosCodeView.Count; pros++)
                                {
                                    if (lcdState == "良品" || lcdState == "复判良品" || lcdState == "重工良品")//哪道工序没扫描就是哪道工序的Wip
                                    {
                                        if (!logCode.Contains("," + prosCodeView[pros]["prosCode"].ToString() + ","))
                                        {
                                            //表示产品在本道工序尚未扫描
                                            prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                            prosCode = prosCodeView[pros]["prosCode"].ToString();
                                            prosName = prosCodeView[pros]["prosName"].ToString();
                                            wip_firstWip = 1; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 0;
                                            break;
                                        }
                                    }
                                    else if (lcdState == "待复判")//Wip在扫描到的工序内。
                                    {
                                        if (logCode.Contains("," + prosCodeView[pros]["prosCode"].ToString() + ","))
                                        {
                                            earlyProsSeq = prosCodeView[pros]["prosSeq"].ToString();
                                            earlyProsCode = prosCodeView[pros]["prosCode"].ToString();
                                            earlyProsName = prosCodeView[pros]["prosName"].ToString();
                                        }
                                        else
                                        {
                                            prosSeq = earlyProsSeq;
                                            prosCode = earlyProsCode;
                                            prosName = earlyProsName;
                                            wip_firstWip = 0; wip_judgeWip = 1; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 0;
                                            break;
                                        }
                                    }
                                    else if (lcdState == "复判报废")//复判良品表示产品申报不良复判为报废,Wip清除,需要知道是在哪道工序申报的不良
                                    {
                                        //判断产品当前工序在exception_01表是否申报了不良
                                        List<TRowResult> expLcdRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + lcdCode, "exception_01");
                                        if (expLcdRowCount == null)
                                        {
                                            continue;
                                        }
                                        foreach (var expLcdRow in expLcdRowCount)
                                        {
                                            string expLcdCode = Encoding.UTF8.GetString(expLcdRow.Row);
                                            if (expLcdCode.Contains("$P" + prosCodeView[pros]["prosCode"].ToString()))
                                            {
                                                foreach (var expLcdKey in expLcdRow.Columns)
                                                {
                                                    //获取rowValue
                                                    string expLcdValue = Encoding.UTF8.GetString(expLcdKey.Value.Value);
                                                    JObject jsonExpStr = JObject.Parse(expLcdValue);
                                                    string exceptionState = (jsonExpStr["exceptionState"] ?? "").ToString();
                                                    if (exceptionState == "复判报废")
                                                    {
                                                        //表示产品在本道工序申报了不良复判为报废
                                                        prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                                        prosCode = prosCodeView[pros]["prosCode"].ToString();
                                                        prosName = prosCodeView[pros]["prosName"].ToString();
                                                        wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 1; wip_rwScrap = 0; wip_except = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (lcdState == "复判重工")
                                    {
                                        if (logCode.Contains("," + prosCodeView[pros]["prosCode"].ToString() + ","))
                                        {
                                            earlyProsSeq = prosCodeView[pros]["prosSeq"].ToString();
                                            earlyProsCode = prosCodeView[pros]["prosCode"].ToString();
                                            earlyProsName = prosCodeView[pros]["prosName"].ToString();
                                        }
                                        else
                                        {
                                            prosSeq = earlyProsSeq;
                                            prosCode = earlyProsCode;
                                            prosName = earlyProsName;
                                            wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 1; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 0;
                                            break;
                                        }
                                    }
                                    else if (lcdState == "重工报废")
                                    {
                                        //判断产品当前工序在exception_01表是否申报了不良
                                        List<TRowResult> expLcdRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + lcdCode, "exception_01");
                                        if (expLcdRowCount == null)
                                        {
                                            continue;
                                        }
                                        foreach (var expLcdRow in expLcdRowCount)
                                        {
                                            string expLcdCode = Encoding.UTF8.GetString(expLcdRow.Row);
                                            if (expLcdCode.Contains("$P" + prosCodeView[pros]["prosCode"].ToString()))
                                            {
                                                foreach (var expLcdKey in expLcdRow.Columns)
                                                {
                                                    //获取rowValue
                                                    string expLcdValue = Encoding.UTF8.GetString(expLcdKey.Value.Value);
                                                    JObject jsonExpStr = JObject.Parse(expLcdValue);
                                                    string exceptionState = (jsonExpStr["exceptionState"] ?? "").ToString();
                                                    if (exceptionState == "重工报废")
                                                    {
                                                        //表示产品在本道工序申报了不良重工为报废
                                                        prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                                        prosCode = prosCodeView[pros]["prosCode"].ToString();
                                                        prosName = prosCodeView[pros]["prosName"].ToString();
                                                        wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 1; wip_except = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //表示产品在本道工序申报了不良重工为报废
                                        prosSeq = prosCodeView[pros]["prosSeq"].ToString();
                                        prosCode = prosCodeView[pros]["prosCode"].ToString();
                                        prosName = prosCodeView[pros]["prosName"].ToString();
                                        wip_firstWip = 0; wip_judgeWip = 0; wip_repeatWip = 0; wip_judgeScrap = 0; wip_rwScrap = 0; wip_except = 1;
                                        break;
                                    }
                                }

                                //更新实际数据
                                DataRow wipRow = byWipTable.NewRow();
                                wipRow["wip_productionOrder"] = searchOrder;//工单编号
                                wipRow["wip_processSeq"] = prosSeq;//工序序号
                                wipRow["wip_processCode"] = prosCode;//工序编号
                                wipRow["wip_processName"] = prosName;//工序名称
                                wipRow["wip_glassCode"] = lcdCode;//Lcd条码
                                wipRow["wip_firstWip"] = wip_firstWip;//Wip产出
                                wipRow["wip_judgeWip"] = wip_judgeWip;//复判Wip
                                wipRow["wip_repeatWip"] = wip_repeatWip;//重工Wip
                                wipRow["wip_judgeScrap"] = wip_judgeScrap;//复判报废
                                wipRow["wip_rwScrap"] = wip_rwScrap;//重工报废
                                wipRow["wip_except"] = wip_except;//异常
                                wipRow["wip_key"] = lcdCode + prosCode;//key
                                byWipTable.Rows.Add(wipRow);
                            }
                        }
                    }
                }
                #endregion

                #region 扫描元数据写入表格作分析
                //for (int i = 0; i < byWipTable.DefaultView.Count; i++)
                //{
                //    string resultString = @"INSERT INTO testtable1 (A,B,C,D,E,F,G,H,I,J,K,L)VALUES( '"
                //               + byWipTable.DefaultView[i]["wip_productionOrder"].ToString() + "','" + byWipTable.DefaultView[i]["wip_processSeq"].ToString() + "','" + byWipTable.DefaultView[i]["wip_processCode"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_processName"].ToString() + "','" + byWipTable.DefaultView[i]["wip_glassCode"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_firstWip"].ToString() + "', '" + byWipTable.DefaultView[i]["wip_judgeWip"].ToString() + "','" + byWipTable.DefaultView[i]["wip_repeatWip"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_judgeScrap"].ToString() + "','" + byWipTable.DefaultView[i]["wip_rwScrap"].ToString() + "','" + byWipTable.DefaultView[i]["wip_except"].ToString() + "','"
                //               + byWipTable.DefaultView[i]["wip_key"].ToString() + "')";
                //    conn.ExecuteAction(resultString, "ReportBase");
                //}
                #endregion

                #region 获取已打印标签的包装数据
                byWipTable.DefaultView.Sort = "wip_glassCode ASC"; //按照lcd排序
                byWipTable = byWipTable.DefaultView.ToTable();
                DataTable unRepeattable = byWipTable.Copy();//copy一个新的Table处理数据
                DataTable dtResult = unRepeattable.Clone();//按照 工单 工序分组合计数据
                DataTable dtName = unRepeattable.DefaultView.ToTable(true, "wip_productionOrder", "wip_processSeq", "wip_processCode", "wip_processName");
                unRepeattable.PrimaryKey = new DataColumn[] { unRepeattable.Columns["wip_key"] };//添加一个Key的主键

                string packSql = @"SELECT IBS_GlassCode AS IBS_GlassCode,IBS_productOrder FROM TPL_PackingInnerBox_Sub WHERE IBS_productOrder='" + searchOrder + "' AND ISNULL(IBS_IsPrint,0)=1";
                DataTable packTable = conn.ExecuteDataTable(packSql, "Base");
                for (int i = unRepeattable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow[] unRepeatRows = packTable.Select("IBS_GlassCode='" + unRepeattable.Rows[i]["wip_glassCode"] + "'");
                    if (unRepeatRows.Length > 0)
                    {
                        unRepeattable.Rows.RemoveAt(i);
                    }
                }
                #endregion

                #region 扫描元数据写入表格作分析(已过滤已包装数据)
                //for (int i = 0; i < unRepeattable.DefaultView.Count; i++)
                //{
                //    string resultString = @"INSERT INTO testtable1 (A,B,C,D,E,F,G,H,I,J,K,L,M)VALUES( '"
                //               + unRepeattable.DefaultView[i]["wip_productionOrder"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_processSeq"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_processCode"].ToString() + "','" +
                //               unRepeattable.DefaultView[i]["wip_processName"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_glassCode"].ToString() + "','"
                //               + unRepeattable.DefaultView[i]["wip_firstWip"].ToString() + "', '" + unRepeattable.DefaultView[i]["wip_judgeWip"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_repeatWip"].ToString() + "','"
                //               + unRepeattable.DefaultView[i]["wip_judgeScrap"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_rwScrap"].ToString() + "','" + unRepeattable.DefaultView[i]["wip_except"].ToString() + "','"
                //               + unRepeattable.DefaultView[i]["wip_key"].ToString() + "','Wip')";
                //    conn.ExecuteAction(resultString, "ReportBase");
                //}
                #endregion

                #region 合计Wip
                for (int i = 0; i < dtName.Rows.Count; i++)
                {
                    DataRow[] rows = unRepeattable.Select("wip_productionOrder='" + dtName.Rows[i]["wip_productionOrder"] + "'and wip_processCode='" + dtName.Rows[i]["wip_processCode"] + "'");
                    //temp用来存储筛选出来的数据
                    DataTable temp = dtResult.Clone();
                    foreach (DataRow row in rows)
                    {
                        temp.Rows.Add(row.ItemArray);
                        //string resultString = @"INSERT INTO testtable1 (A,B,C,D)VALUES( '" + row.ItemArray[2].ToString() + "', '" + row.ItemArray[1].ToString() + "', '" + row.ItemArray[4].ToString() + "','temp')";
                        //conn.ExecuteAction(resultString, "ReportBase");
                    }
                    DataRow dr = dtResult.NewRow();
                    dr["wip_productionOrder"] = dtName.Rows[i]["wip_productionOrder"].ToString();//工单编号
                    dr["wip_processSeq"] = dtName.Rows[i]["wip_processSeq"].ToString();//工序序号
                    dr["wip_processCode"] = dtName.Rows[i]["wip_processCode"].ToString();//工序编号
                    dr["wip_processName"] = dtName.Rows[i]["wip_processName"].ToString();//工序名称
                    dr["wip_firstWip"] = temp.Compute("sum(wip_firstWip)", "");//Wip
                    dr["wip_judgeWip"] = temp.Compute("sum(wip_judgeWip)", "");//复判Wip
                    dr["wip_repeatWip"] = temp.Compute("sum(wip_repeatWip)", "");///重工Wip
                    dr["wip_judgeScrap"] = temp.Compute("sum(wip_judgeScrap)", "");//复判报废
                    dr["wip_rwScrap"] = temp.Compute("sum(wip_rwScrap)", "");////重工报废
                    dr["wip_except"] = temp.Compute("sum(wip_except)", "");//异常
                    dtResult.Rows.Add(dr);
                }
                #endregion

                #region  返回数据
                DataSet ds = new DataSet();
                ds.Tables.Add(dtResult);
                ds.Tables.Add(unRepeattable);
                return ds;
                #endregion
            }
            catch (Exception exp)
            {
                LogHelper.Error("工单Wip统计失败.", exp);
                return null;
            }
        }

        /// <summary>
        /// 查询工单扫描的所有玻璃码
        /// </summary>
        /// <param name="searchOrder"></param>
        /// <returns></returns>
        public List<string> GetLCDByOrder(string searchOrder)
        {
            List<string> lst = new List<string>();
            try
            {
                //工单扫描Lcd集合  Key  工单编号+年(4位)+月(2位)+日(2位)
                List<TRowResult> wipLotRowCount = GetDataFromHBase.Instance.GetRowsWithPrefix(searchOrder, "data_07");
                //循环工单在Lcd清洗投料扫描到的所有Lcd
                foreach (var wipLotRow in wipLotRowCount)
                {
                    //获取dtta_07表的rowkey(工单编号+年(4位)+月(2位)+日(2位))
                    string wipLotRowKey = Encoding.UTF8.GetString(wipLotRow.Row);
                    foreach (var wipLotkey in wipLotRow.Columns)
                    {
                        //获取dtta_07表的rowValue(Lcd编号按照逗号[,]分隔)
                        string wipLcdLotKey = Encoding.UTF8.GetString(wipLotkey.Value.Value);
                        string[] keys = wipLcdLotKey.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        lst.AddRange(keys);
                    }
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("工单Wip统计失败.", exp);
            }
            return lst;
        }

        /// <summary>
        /// 获取Data_01数据
        /// </summary>
        /// <param name="searchCode"></param>
        /// <returns></returns>
        public dynamic GetData01(string searchCode)
        {
            dynamic reObj = null;
            try
            {
                List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, "data_01");
                if (glassRowResult.Count <= 0)
                {
                    return null;
                }

                foreach (var glassKey in glassRowResult)
                {
                    //获取rowKey
                    string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
                    foreach (var key in glassKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            System.Diagnostics.Debug.Print(glassRowValue);
                            //反序列化rowValue
                            reObj = JObject.Parse(glassRowValue);
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败：" + exp.Message, exp);
                            return null;
                        }
                    }//foreach (var key in glassKey.Columns)
                }//foreach (var glassKey in glassRowResult)
                return reObj;
            }
            catch (Exception ex)
            {
                LogHelper.Error("查询HBase数据库失败", ex);
                Reconnect();
                return null;
            }
        }

        /// <summary>
        /// 查询data_02过站信息
        /// </summary>
        /// <param name="GlassCode">玻璃编码</param>
        /// <returns>DataSet（parameterTable, materialTable）</returns>
        public DataSet GetData02(string GlassCode)
        {
            DataSet dataSet = new DataSet();
            try
            {
                #region  初始化工序与原材料表
                DataTable materialTable = new DataTable("materialTable");
                DataTable parameterTable = new DataTable("parameterTable");

                dataSet.Tables.Add(materialTable);
                dataSet.Tables.Add(parameterTable);

                materialTable.Columns.Add("material_ProcessTid");//工序ID
                materialTable.Columns.Add("material_ProcessCode");//工序编码
                materialTable.Columns.Add("material_ProcessName");//工序名称
                materialTable.Columns.Add("material_CardTid");//机台ID
                materialTable.Columns.Add("material_CardIP");//机台IP
                materialTable.Columns.Add("material_CardName");//机台名称
                materialTable.Columns.Add("material_ScanIP");//扫描IP
                materialTable.Columns.Add("material_ScanOP");//扫描人编码
                materialTable.Columns.Add("material_ScanNumber");//扫描人员
                materialTable.Columns.Add("material_MouldNumber");//治具编码
                materialTable.Columns.Add("material_MouldName");//治具名称
                materialTable.Columns.Add("material_LineCode");//产线编码
                materialTable.Columns.Add("material_Code"); //原料编码
                materialTable.Columns.Add("material_Name"); //原料名称
                materialTable.Columns.Add("material_Spec");//原料型号
                materialTable.Columns.Add("material_Batch");//原料入库批次
                materialTable.Columns.Add("material_Vonder");//原料供应商
                materialTable.Columns.Add("material_SCDate");//原料来料生产日期
                materialTable.Columns.Add("material_BoxCode");//原料入库纸箱编码
                materialTable.Columns.Add("material_FeedingOP");//上料人

                parameterTable.Columns.Add("parameter_RowKey");//RowKey
                parameterTable.Columns.Add("parameter_ProcessTid");//工序ID
                parameterTable.Columns.Add("parameter_ProcessCode");//工序编码
                parameterTable.Columns.Add("parameter_ProcessName");//工序名称
                parameterTable.Columns.Add("parameter_ProcessNumber");//工序序号
                parameterTable.Columns.Add("parameter_CardTid");//机台ID
                parameterTable.Columns.Add("parameter_CardIP");//机台IP  
                parameterTable.Columns.Add("parameter_CardName");//机台名称
                parameterTable.Columns.Add("parameter_ScanIP");//扫描IP
                parameterTable.Columns.Add("parameter_ScanOP");//扫描人员
                parameterTable.Columns.Add("parameter_ScanNumber");//扫描人员
                parameterTable.Columns.Add("parameter_MouldNumber");//治具编码
                parameterTable.Columns.Add("parameter_MouldName");//治具名称
                parameterTable.Columns.Add("parameter_EventCode");//机台事件
                parameterTable.Columns.Add("parameter_deviceOP");//机台负责人
                parameterTable.Columns.Add("parameter_LineCode");//产线编码

                parameterTable.Columns.Add("parameter_LotNo");//生产批次号
                parameterTable.Columns.Add("parameter_DateTime");//采集时间
                parameterTable.Columns.Add("parameter_trackNumber");//扫描码
                parameterTable.Columns.Add("parameter_oppositeNumber");//对应码
                parameterTable.Columns.Add("parameter_bfFlag");//是否补扫
                parameterTable.Columns.Add("parameter_exceptionState");//是否异常
                parameterTable.Columns.Add("parameter_exceptionContent");//异常原因
                parameterTable.Columns.Add("parameter_deviceCallState");//是否报警
                parameterTable.Columns.Add("parameter_deviceCallContent");//报警内容
                parameterTable.Columns.Add("parameter_Type");//参数类型
                parameterTable.Columns.Add("parameter_Value");//参数值
                parameterTable.Columns.Add("parameter_Unit");//参数单位
                #endregion



                string SearchGlass = "$G" + GlassCode;
                List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(SearchGlass, "data_02");
                foreach (var productKey in ProductRowResult)
                {
                    //获取rowKey
                    string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                    foreach (var key in productKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string productRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //初始化采集表的数据
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(productRowValue);
                            string processId = jsonStr["processId"].ToString();//工序ID
                            string processCode = jsonStr["processCode"].ToString();//工序编码
                            string processName = jsonStr["processName"].ToString();//工序名称
                            string processNumber = jsonStr["processNumber"].ToString();//工序序号
                            string deviceId = jsonStr["deviceId"].ToString(); //机台ID
                            string deviceIp = jsonStr["deviceIp"].ToString(); //机台IP
                            string deviceName = jsonStr["deviceName"].ToString();//机台名称
                            string scanIP = jsonStr["scanIp"].ToString(); //扫描IP
                            string scanNumber = jsonStr["scanNumber"].ToString();//扫描人编码
                            string scanOP = jsonStr["scanOP"].ToString();//扫描人员
                            string mouldNumber = jsonStr["mouldNumber"].ToString();//治具编码
                            string mouldName = jsonStr["mouldName"].ToString();//治具名称
                            string deviceEventCode = jsonStr["deviceEventCode"].ToString(); //机台事件
                            string deviceOP = jsonStr["deviceOP"].ToString() == "" ? jsonStr["scanOP"].ToString() : jsonStr["deviceOP"].ToString(); //jsonStr["scanOP"].ToString(); //机台负责人
                            string productLineId = jsonStr["productLineId"].ToString(); //产线ID
                            string productLineCode = jsonStr["productLineCode"].ToString(); //产线编码
                            string productLineName = jsonStr["productLineName"].ToString(); //产线名称
                            string collectionTime = jsonStr["InTime"].ToString(); //采集时间
                            string trackNumber = jsonStr["trackNumber"].ToString(); //扫描码
                            string oppositeNumber = jsonStr["oppositeNumber"].ToString(); //对应码
                            string productionOrder = jsonStr["productionOrder"].ToString(); //生产订单编码
                            string destroyOrder = jsonStr["destroyOrder"].ToString(); //销售订单编码
                            string finishesCode = (jsonStr["finishesCode"] ?? "").ToString();// jsonStr["finishesCode"].ToString(); //成品的编码
                            string finishesModel = (jsonStr["finishesModel"] ?? "").ToString() == "" ? jsonStr["destroyOrder"].ToString() : jsonStr["finishesModel"].ToString();//jsonStr["finishesModel"].ToString(); //成品型号
                            string finishesProductNo = (jsonStr["finishesProductNo"] ?? "").ToString();//jsonStr["finishesProductNo"].ToString(); //成品的料号
                            string clientProductNo = (jsonStr["clientProductNo"] ?? "").ToString();//jsonStr["clientProductNo"].ToString(); //客户料号
                            string exceptionState = jsonStr["exceptionState"].ToString() == "1" ? "是" : "否"; //是否异常  
                            string exceptionContent = jsonStr["exceptionContent"].ToString().Replace('#', ' '); //异常原因    
                            string deviceCallState = jsonStr["deviceCallState"].ToString() == "0" ? "否" : "是"; //设备报警  
                            string deviceCallContent = jsonStr["deviceCallContent"].ToString(); //设备报警内容     
                            string lotNumber = jsonStr["lotNumber"].ToString(); //生产批次号
                            string bfFlag = (jsonStr["bfFlag"] ?? "0").ToString() == "1" ? "是" : "否"; //是否补扫

                            var materialJson = jsonStr["Material"].AsEnumerable();
                            foreach (var materialItem in materialJson)
                            {
                                //更新实际采集表的数据
                                if (materialItem["materialInfo"].ToString() != "")
                                {
                                    DataRow materialRow = materialTable.NewRow();

                                    materialRow["material_ProcessTid"] = processId;//工序ID
                                    materialRow["material_ProcessCode"] = processCode;//工序编码
                                    materialRow["material_ProcessName"] = processName;//工序名称
                                    materialRow["material_CardTid"] = deviceId; //机台ID
                                    materialRow["material_CardIP"] = deviceIp; //机台IP
                                    materialRow["material_CardName"] = deviceName;//机台名称
                                    materialRow["material_ScanIP"] = scanIP; //扫描IP
                                    materialRow["material_ScanNumber"] = scanNumber;//扫描人编码
                                    materialRow["material_ScanOP"] = scanOP;//扫描人员
                                    materialRow["material_MouldNumber"] = mouldNumber;//治具编码
                                    materialRow["material_MouldName"] = mouldName;//治具名称
                                    materialRow["material_LineCode"] = productLineCode; //产线编码
                                    materialRow["material_Code"] = materialItem["materialInfo"].ToString();//原料编码
                                    materialRow["material_Name"] = materialItem["materialName"].ToString();//原料名称
                                    materialRow["material_Spec"] = materialItem["materialSpecification"].ToString();//原料型号
                                    materialRow["material_Batch"] = materialItem["materialBatch"].ToString();//原料入库批次
                                    materialRow["material_Vonder"] = materialItem["materialManufacturers"].ToString(); //原料供应商
                                    materialRow["material_SCDate"] = materialItem["materialProductionDate"].ToString(); //原料来料生产日期
                                    materialRow["material_BoxCode"] = materialItem["materialBoxNumber"].ToString();//原料入库纸箱编码
                                    materialRow["material_FeedingOP"] = materialItem["loadMaterialPerson"].ToString(); //上料人
                                    materialTable.Rows.Add(materialRow);
                                }
                            }
                            var parameterJson = jsonStr["Parameter"].AsEnumerable();
                            if (parameterJson.Count() == 0)
                            {
                                //更新实际采集表的数据
                                DataRow parameterRow = parameterTable.NewRow();
                                parameterRow["parameter_RowKey"] = productRowKey;
                                parameterRow["parameter_ProcessTid"] = processId;//工序ID
                                parameterRow["parameter_ProcessCode"] = processCode;//工序编码
                                parameterRow["parameter_ProcessName"] = processName;//工序名称
                                parameterRow["parameter_processNumber"] = processNumber;//工序序号
                                parameterRow["parameter_CardTid"] = deviceId; //机台ID
                                parameterRow["parameter_CardIP"] = deviceIp; //机台IP
                                parameterRow["parameter_CardName"] = deviceName;//机台名称
                                parameterRow["parameter_ScanIP"] = scanIP; //扫描IP
                                parameterRow["parameter_ScanNumber"] = scanNumber;//扫描人编码
                                parameterRow["parameter_ScanOP"] = scanOP;//扫描人员
                                parameterRow["parameter_MouldNumber"] = mouldNumber;//治具编码
                                parameterRow["parameter_MouldName"] = mouldName;//治具名称
                                parameterRow["parameter_EventCode"] = deviceEventCode; //机台事件
                                parameterRow["parameter_deviceOP"] = deviceOP; //机台负责人
                                parameterRow["parameter_LineCode"] = productLineCode; //产线编码
                                parameterRow["parameter_DateTime"] = collectionTime; //采集时间
                                parameterRow["parameter_trackNumber"] = trackNumber; //扫描码
                                parameterRow["parameter_oppositeNumber"] = oppositeNumber; //对应码
                                parameterRow["parameter_LotNo"] = lotNumber.Substring(0, lotNumber.Length - 1); //生产批次号
                                parameterRow["parameter_bfFlag"] = bfFlag; //是否补扫
                                parameterRow["parameter_exceptionState"] = exceptionState; //是否异常
                                parameterRow["parameter_exceptionContent"] = exceptionContent; //异常内容
                                parameterRow["parameter_deviceCallState"] = deviceCallState; //是否报警
                                parameterRow["parameter_deviceCallContent"] = deviceCallContent; //报警内容
                                parameterRow["parameter_Type"] = ""; //参数类型
                                parameterRow["parameter_Value"] = ""; //参数值
                                parameterRow["parameter_Unit"] = ""; //参数单位
                                parameterTable.Rows.Add(parameterRow);
                            }
                            else
                            {
                                foreach (var parameterItem in parameterJson)
                                {
                                    //更新实际采集表的数据
                                    DataRow parameterRow = parameterTable.NewRow();
                                    parameterRow["parameter_RowKey"] = productRowKey;
                                    parameterRow["parameter_ProcessTid"] = processId;//工序ID
                                    parameterRow["parameter_ProcessCode"] = processCode;//工序编码
                                    parameterRow["parameter_ProcessName"] = processName;//工序名称
                                    parameterRow["parameter_processNumber"] = processNumber;//工序序号
                                    parameterRow["parameter_CardTid"] = deviceId; //机台ID
                                    parameterRow["parameter_CardIP"] = deviceIp; //机台IP
                                    parameterRow["parameter_CardName"] = deviceName;//机台名称
                                    parameterRow["parameter_ScanIP"] = scanIP; //扫描IP
                                    parameterRow["parameter_ScanNumber"] = scanNumber;//扫描人编码
                                    parameterRow["parameter_ScanOP"] = scanOP;//扫描人员
                                    parameterRow["parameter_MouldNumber"] = mouldNumber;//治具编码
                                    parameterRow["parameter_MouldName"] = mouldName;//治具名称
                                    parameterRow["parameter_EventCode"] = deviceEventCode; //机台事件
                                    parameterRow["parameter_deviceOP"] = deviceOP; //机台负责人
                                    parameterRow["parameter_LineCode"] = productLineCode; //产线编码
                                    parameterRow["parameter_DateTime"] = collectionTime; //采集时间
                                    parameterRow["parameter_trackNumber"] = trackNumber; //扫描码
                                    parameterRow["parameter_oppositeNumber"] = oppositeNumber; //对应码
                                    parameterRow["parameter_LotNo"] = lotNumber.Substring(0, lotNumber.Length - 1); //生产批次号
                                    parameterRow["parameter_bfFlag"] = bfFlag; //是否补扫
                                    parameterRow["parameter_exceptionState"] = exceptionState; //是否异常
                                    parameterRow["parameter_exceptionContent"] = exceptionContent; //异常内容
                                    parameterRow["parameter_deviceCallState"] = deviceCallState; //是否报警
                                    parameterRow["parameter_deviceCallContent"] = deviceCallContent; //报警内容
                                    parameterRow["parameter_Type"] = parameterItem["parameterType"].ToString(); //参数类型
                                    parameterRow["parameter_Value"] = parameterItem["parameterValue"].ToString(); //参数值
                                    parameterRow["parameter_Unit"] = parameterItem["parameterUnit"].ToString(); //参数单位
                                    parameterTable.Rows.Add(parameterRow);
                                }
                            }
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message.ToString(), exp);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("rowKey范围查询" + exp.Message.ToString(), exp);
            }
            return dataSet;
        }

        /// <summary>
        /// 查询data_02过站信息
        /// </summary>
        /// <param name="GlassCode">玻璃编码</param>
        /// <returns>DataSet（parameterTable, materialTable）</returns>
        public DataSet GetData02(string GlassCode, Dictionary<string, string> dicMaterialProcess)
        {
            DataSet dataSet = new DataSet();
            try
            {
                #region  初始化工序与原材料表
                DataTable materialTable = new DataTable("materialTable");
                DataTable parameterTable = new DataTable("parameterTable");

                dataSet.Tables.Add(materialTable);
                dataSet.Tables.Add(parameterTable);

                materialTable.Columns.Add("material_ProcessTid");//工序ID
                materialTable.Columns.Add("material_ProcessCode");//工序编码
                materialTable.Columns.Add("material_ProcessName");//工序名称
                materialTable.Columns.Add("material_CardTid");//机台ID
                materialTable.Columns.Add("material_CardIP");//机台IP
                materialTable.Columns.Add("material_CardName");//机台名称
                materialTable.Columns.Add("material_ScanIP");//扫描IP
                materialTable.Columns.Add("material_ScanOP");//扫描人编码
                materialTable.Columns.Add("material_ScanNumber");//扫描人员
                materialTable.Columns.Add("material_MouldNumber");//治具编码
                materialTable.Columns.Add("material_MouldName");//治具名称
                materialTable.Columns.Add("material_LineCode");//产线编码
                materialTable.Columns.Add("material_Code"); //原料编码
                materialTable.Columns.Add("material_Name"); //原料名称
                materialTable.Columns.Add("material_Spec");//原料型号
                materialTable.Columns.Add("material_Batch");//原料入库批次
                materialTable.Columns.Add("material_Vonder");//原料供应商
                materialTable.Columns.Add("material_SCDate");//原料来料生产日期
                materialTable.Columns.Add("material_BoxCode");//原料入库纸箱编码
                materialTable.Columns.Add("material_FeedingOP");//上料人

                parameterTable.Columns.Add("parameter_RowKey");//RowKey
                parameterTable.Columns.Add("parameter_ProcessTid");//工序ID
                parameterTable.Columns.Add("parameter_ProcessCode");//工序编码
                parameterTable.Columns.Add("parameter_ProcessName");//工序名称
                parameterTable.Columns.Add("parameter_ProcessNumber");//工序序号
                parameterTable.Columns.Add("parameter_CardTid");//机台ID
                parameterTable.Columns.Add("parameter_CardIP");//机台IP  
                parameterTable.Columns.Add("parameter_CardName");//机台名称
                parameterTable.Columns.Add("parameter_ScanIP");//扫描IP
                parameterTable.Columns.Add("parameter_ScanOP");//扫描人员
                parameterTable.Columns.Add("parameter_ScanNumber");//扫描人员
                parameterTable.Columns.Add("parameter_MouldNumber");//治具编码
                parameterTable.Columns.Add("parameter_MouldName");//治具名称
                parameterTable.Columns.Add("parameter_EventCode");//机台事件
                parameterTable.Columns.Add("parameter_deviceOP");//机台负责人
                parameterTable.Columns.Add("parameter_LineCode");//产线编码

                parameterTable.Columns.Add("parameter_LotNo");//生产批次号
                parameterTable.Columns.Add("parameter_DateTime");//采集时间
                parameterTable.Columns.Add("parameter_trackNumber");//扫描码
                parameterTable.Columns.Add("parameter_oppositeNumber");//对应码
                parameterTable.Columns.Add("parameter_bfFlag");//是否补扫
                parameterTable.Columns.Add("parameter_exceptionState");//是否异常
                parameterTable.Columns.Add("parameter_exceptionContent");//异常原因
                parameterTable.Columns.Add("parameter_deviceCallState");//是否报警
                parameterTable.Columns.Add("parameter_deviceCallContent");//报警内容
                parameterTable.Columns.Add("parameter_Type");//参数类型
                parameterTable.Columns.Add("parameter_Value");//参数值
                parameterTable.Columns.Add("parameter_Unit");//参数单位
                #endregion



                string SearchGlass = "$G" + GlassCode;
                List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(SearchGlass, "data_02");
                foreach (var productKey in ProductRowResult)
                {
                    //获取rowKey
                    string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                    foreach (var key in productKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string productRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //初始化采集表的数据
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(productRowValue);
                            string processId = jsonStr["processId"].ToString();//工序ID
                            string processCode = jsonStr["processCode"].ToString();//工序编码
                            string processName = jsonStr["processName"].ToString();//工序名称
                            string processNumber = jsonStr["processNumber"].ToString();//工序序号
                            string deviceId = jsonStr["deviceId"].ToString(); //机台ID
                            string deviceIp = jsonStr["deviceIp"].ToString(); //机台IP
                            string deviceName = jsonStr["deviceName"].ToString();//机台名称
                            string scanIP = jsonStr["scanIp"].ToString(); //扫描IP
                            string scanNumber = jsonStr["scanNumber"].ToString();//扫描人编码
                            string scanOP = jsonStr["scanOP"].ToString();//扫描人员
                            string mouldNumber = jsonStr["mouldNumber"].ToString();//治具编码
                            string mouldName = jsonStr["mouldName"].ToString();//治具名称
                            string deviceEventCode = jsonStr["deviceEventCode"].ToString(); //机台事件
                            string deviceOP = jsonStr["deviceOP"].ToString() == "" ? jsonStr["scanOP"].ToString() : jsonStr["deviceOP"].ToString(); //jsonStr["scanOP"].ToString(); //机台负责人
                            string productLineId = jsonStr["productLineId"].ToString(); //产线ID
                            string productLineCode = jsonStr["productLineCode"].ToString(); //产线编码
                            string productLineName = jsonStr["productLineName"].ToString(); //产线名称
                            string collectionTime = jsonStr["InTime"].ToString(); //采集时间
                            string trackNumber = jsonStr["trackNumber"].ToString(); //扫描码
                            string oppositeNumber = jsonStr["oppositeNumber"].ToString(); //对应码
                            string productionOrder = jsonStr["productionOrder"].ToString(); //生产订单编码
                            string destroyOrder = jsonStr["destroyOrder"].ToString(); //销售订单编码
                            string finishesCode = (jsonStr["finishesCode"] ?? "").ToString();// jsonStr["finishesCode"].ToString(); //成品的编码
                            string finishesModel = (jsonStr["finishesModel"] ?? "").ToString() == "" ? jsonStr["destroyOrder"].ToString() : jsonStr["finishesModel"].ToString();//jsonStr["finishesModel"].ToString(); //成品型号
                            string finishesProductNo = (jsonStr["finishesProductNo"] ?? "").ToString();//jsonStr["finishesProductNo"].ToString(); //成品的料号
                            string clientProductNo = (jsonStr["clientProductNo"] ?? "").ToString();//jsonStr["clientProductNo"].ToString(); //客户料号
                            string exceptionState = jsonStr["exceptionState"].ToString() == "1" ? "是" : "否"; //是否异常  
                            string exceptionContent = jsonStr["exceptionContent"].ToString().Replace('#', ' '); //异常原因    
                            string deviceCallState = jsonStr["deviceCallState"].ToString() == "0" ? "否" : "是"; //设备报警  
                            string deviceCallContent = jsonStr["deviceCallContent"].ToString(); //设备报警内容     
                            string lotNumber = jsonStr["lotNumber"].ToString(); //生产批次号
                            string bfFlag = (jsonStr["bfFlag"] ?? "0").ToString() == "1" ? "是" : "否"; //是否补扫

                            var materialJson = jsonStr["Material"].AsEnumerable();
                            foreach (var materialItem in materialJson)
                            {
                                //更新实际采集表的数据
                                if (materialItem["materialInfo"].ToString() != "" && dicMaterialProcess.ContainsKey(materialItem["materialInfo"].ToString()) && dicMaterialProcess[materialItem["materialInfo"].ToString()] == processCode)
                                {
                                    DataRow materialRow = materialTable.NewRow();

                                    materialRow["material_ProcessTid"] = processId;//工序ID
                                    materialRow["material_ProcessCode"] = processCode;//工序编码
                                    materialRow["material_ProcessName"] = processName;//工序名称
                                    materialRow["material_CardTid"] = deviceId; //机台ID
                                    materialRow["material_CardIP"] = deviceIp; //机台IP
                                    materialRow["material_CardName"] = deviceName;//机台名称
                                    materialRow["material_ScanIP"] = scanIP; //扫描IP
                                    materialRow["material_ScanNumber"] = scanNumber;//扫描人编码
                                    materialRow["material_ScanOP"] = scanOP;//扫描人员
                                    materialRow["material_MouldNumber"] = mouldNumber;//治具编码
                                    materialRow["material_MouldName"] = mouldName;//治具名称
                                    materialRow["material_LineCode"] = productLineCode; //产线编码
                                    materialRow["material_Code"] = materialItem["materialInfo"].ToString();//原料编码
                                    materialRow["material_Name"] = materialItem["materialName"].ToString();//原料名称
                                    materialRow["material_Spec"] = materialItem["materialSpecification"].ToString();//原料型号
                                    materialRow["material_Batch"] = materialItem["materialBatch"].ToString();//原料入库批次
                                    materialRow["material_Vonder"] = materialItem["materialManufacturers"].ToString(); //原料供应商
                                    materialRow["material_SCDate"] = materialItem["materialProductionDate"].ToString(); //原料来料生产日期
                                    materialRow["material_BoxCode"] = materialItem["materialBoxNumber"].ToString();//原料入库纸箱编码
                                    materialRow["material_FeedingOP"] = materialItem["loadMaterialPerson"].ToString(); //上料人
                                    materialTable.Rows.Add(materialRow);
                                }
                            }
                            var parameterJson = jsonStr["Parameter"].AsEnumerable();
                            if (parameterJson.Count() == 0)
                            {
                                //更新实际采集表的数据
                                DataRow parameterRow = parameterTable.NewRow();
                                parameterRow["parameter_RowKey"] = productRowKey;
                                parameterRow["parameter_ProcessTid"] = processId;//工序ID
                                parameterRow["parameter_ProcessCode"] = processCode;//工序编码
                                parameterRow["parameter_ProcessName"] = processName;//工序名称
                                parameterRow["parameter_processNumber"] = processNumber;//工序序号
                                parameterRow["parameter_CardTid"] = deviceId; //机台ID
                                parameterRow["parameter_CardIP"] = deviceIp; //机台IP
                                parameterRow["parameter_CardName"] = deviceName;//机台名称
                                parameterRow["parameter_ScanIP"] = scanIP; //扫描IP
                                parameterRow["parameter_ScanNumber"] = scanNumber;//扫描人编码
                                parameterRow["parameter_ScanOP"] = scanOP;//扫描人员
                                parameterRow["parameter_MouldNumber"] = mouldNumber;//治具编码
                                parameterRow["parameter_MouldName"] = mouldName;//治具名称
                                parameterRow["parameter_EventCode"] = deviceEventCode; //机台事件
                                parameterRow["parameter_deviceOP"] = deviceOP; //机台负责人
                                parameterRow["parameter_LineCode"] = productLineCode; //产线编码
                                parameterRow["parameter_DateTime"] = collectionTime; //采集时间
                                parameterRow["parameter_trackNumber"] = trackNumber; //扫描码
                                parameterRow["parameter_oppositeNumber"] = oppositeNumber; //对应码
                                parameterRow["parameter_LotNo"] = lotNumber.Substring(0, lotNumber.Length - 1); //生产批次号
                                parameterRow["parameter_bfFlag"] = bfFlag; //是否补扫
                                parameterRow["parameter_exceptionState"] = exceptionState; //是否异常
                                parameterRow["parameter_exceptionContent"] = exceptionContent; //异常内容
                                parameterRow["parameter_deviceCallState"] = deviceCallState; //是否报警
                                parameterRow["parameter_deviceCallContent"] = deviceCallContent; //报警内容
                                parameterRow["parameter_Type"] = ""; //参数类型
                                parameterRow["parameter_Value"] = ""; //参数值
                                parameterRow["parameter_Unit"] = ""; //参数单位
                                parameterTable.Rows.Add(parameterRow);
                            }
                            else
                            {
                                foreach (var parameterItem in parameterJson)
                                {
                                    //更新实际采集表的数据
                                    DataRow parameterRow = parameterTable.NewRow();
                                    parameterRow["parameter_RowKey"] = productRowKey;
                                    parameterRow["parameter_ProcessTid"] = processId;//工序ID
                                    parameterRow["parameter_ProcessCode"] = processCode;//工序编码
                                    parameterRow["parameter_ProcessName"] = processName;//工序名称
                                    parameterRow["parameter_processNumber"] = processNumber;//工序序号
                                    parameterRow["parameter_CardTid"] = deviceId; //机台ID
                                    parameterRow["parameter_CardIP"] = deviceIp; //机台IP
                                    parameterRow["parameter_CardName"] = deviceName;//机台名称
                                    parameterRow["parameter_ScanIP"] = scanIP; //扫描IP
                                    parameterRow["parameter_ScanNumber"] = scanNumber;//扫描人编码
                                    parameterRow["parameter_ScanOP"] = scanOP;//扫描人员
                                    parameterRow["parameter_MouldNumber"] = mouldNumber;//治具编码
                                    parameterRow["parameter_MouldName"] = mouldName;//治具名称
                                    parameterRow["parameter_EventCode"] = deviceEventCode; //机台事件
                                    parameterRow["parameter_deviceOP"] = deviceOP; //机台负责人
                                    parameterRow["parameter_LineCode"] = productLineCode; //产线编码
                                    parameterRow["parameter_DateTime"] = collectionTime; //采集时间
                                    parameterRow["parameter_trackNumber"] = trackNumber; //扫描码
                                    parameterRow["parameter_oppositeNumber"] = oppositeNumber; //对应码
                                    parameterRow["parameter_LotNo"] = lotNumber.Substring(0, lotNumber.Length - 1); //生产批次号
                                    parameterRow["parameter_bfFlag"] = bfFlag; //是否补扫
                                    parameterRow["parameter_exceptionState"] = exceptionState; //是否异常
                                    parameterRow["parameter_exceptionContent"] = exceptionContent; //异常内容
                                    parameterRow["parameter_deviceCallState"] = deviceCallState; //是否报警
                                    parameterRow["parameter_deviceCallContent"] = deviceCallContent; //报警内容
                                    parameterRow["parameter_Type"] = parameterItem["parameterType"].ToString(); //参数类型
                                    parameterRow["parameter_Value"] = parameterItem["parameterValue"].ToString(); //参数值
                                    parameterRow["parameter_Unit"] = parameterItem["parameterUnit"].ToString(); //参数单位
                                    parameterTable.Rows.Add(parameterRow);
                                }
                            }
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message.ToString(), exp);
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogHelper.Error("rowKey范围查询" + exp.Message.ToString(), exp);
            }
            return dataSet;
        }

        /// <summary>
        /// 查询玻璃的所有HBase信息
        /// </summary>
        /// <param name="GlassCode"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetAllData(string searchCode)
        {
            dynamic data_01 = null, data_02 = null, data_03 = null, data_04 = null, data_05 = null, data_06 = null, data_07 = null, data_08 = null;
            dynamic exception_01 = null, exception_02 = null, exception_03 = null, exception_04 = null, exception_05 = null;
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>()
            {
                {"data_01", data_01},
                {"data_02", data_02},
                {"data_03", data_03},
                {"data_04", data_04},
                {"data_05", data_05},
                {"data_06", data_06},
                {"data_07", data_07},
                {"data_08", data_08},
                {"exception_01", exception_01},
                {"exception_02", exception_02},
                {"exception_03", exception_03},
                {"exception_04", exception_04},
                {"exception_05", exception_05}
            };
            List<string> keys = new List<string>()
            {
                "data_01",
                "data_02",
                "data_03",
                "data_04",
                "data_05",
                "data_06",
                "data_07",
                "data_08",
                "exception_01",
                "exception_02",
                "exception_03",
                "exception_04",
                "exception_05",
            };
            foreach (string table in keys)
            {
                try
                {
                    List<TRowResult> glassRowResult;
                    //if (table == "data_01" || table == "data_06" || table == "exception_03" || table == "exception_04" || table == "exception_05")
                    //{
                    glassRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, table);
                    //}
                    //else
                    //{
                    //    glassRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + searchCode, table);
                    //}
                    if (glassRowResult.Count == 0)
                    {
                        glassRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + searchCode, table);
                    }
                    if (glassRowResult.Count > 0)
                    {
                        foreach (var glassKey in glassRowResult)
                        {
                            //获取rowKey
                            string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
                            foreach (var key in glassKey.Columns)
                            {
                                try
                                {
                                    //获取rowValue
                                    string glassRowValue = Encoding.UTF8.GetString(key.Value.Value);
                                    System.Diagnostics.Debug.Print(glassRowValue);
                                    //反序列化rowValue
                                    dic[table] = JObject.Parse(glassRowValue);
                                    if (table == "data_01")
                                    {
                                        searchCode = dic[table].glassCode;
                                    }
                                }
                                catch (Exception exp)
                                {
                                    LogHelper.Error("从扫描关联表中获取玻璃编码失败：" + exp.Message, exp);
                                }
                            }//foreach (var key in glassKey.Columns)
                        }//foreach (var glassKey in glassRowResult)
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("查询HBase数据库失败", ex);
                    Reconnect();
                }
            }//foreach (string table in dic.Keys)
            return dic;
        }

        /// <summary>
        /// 获取指定编码的所有Key值
        /// </summary>
        /// <param name="glassCode">玻璃码</param>
        /// <returns></returns>
        public List<string> GetRowKeys(string searchCode, string tableName)
        {
            List<string> lst = new List<string>();
            List<TRowResult> ProductRowResult;
            if (tableName == "data_02" || tableName == "exception_01")
            {
                searchCode = "$G" + searchCode;
                ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(searchCode, tableName);
            }
            else
            {
                ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(searchCode, tableName);
                //ProductRowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(searchCode, tableName);
            }

            foreach (var productKey in ProductRowResult)
            {
                //获取rowKey
                string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                lst.Add(productRowKey);
            }
            return lst;
        }//

        /// <summary>
        /// 获取指定玻璃码的data_02中的所有Key值
        /// </summary>
        /// <param name="glassCode">玻璃码</param>
        /// <returns></returns>
        public List<string> GetData02Keys(string glassCode)
        {
            List<string> lst = new List<string>();
            string SearchGlass = "$G" + glassCode;
            List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(SearchGlass, "data_02");
            foreach (var productKey in ProductRowResult)
            {
                //获取rowKey
                string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                lst.Add(productRowKey);
            }
            return lst;
        }//

        /// <summary>
        /// 根据Key获取data_02的value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public dynamic GetDataValueByKey(string key, string table)
        {
            dynamic json = null;
            //List<TRowResult> RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(key, table);
            List<TRowResult> RowResult;
            //if (table == "data_02" || table == "exception_01")
            //{
            //    key = "$G" + key;
            //    RowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(key, table);
            //}
            //else
            //{
            //    RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(key, table);
            //}
            RowResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(key, table);
            foreach (var productKey in RowResult)
            {
                //获取rowKey
                string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                foreach (var RowKey in productKey.Columns)
                {
                    //获取rowValue
                    string productRowValue = Encoding.UTF8.GetString(RowKey.Value.Value);
                    //初始化采集表的数据
                    //反序列化rowValue
                    json = JValue.Parse(productRowValue);
                }
            }
            return json;
        }//

        /// <summary>
        /// 删除HBase键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool DeleteRowKey(string key, string table)
        {
            return GetDataFromHBase.Instance.DeleteRowHBase(table, key, "logValue:");
        }


        /// <summary>
        /// 查询最后一次的过站信息
        /// </summary>
        /// <param name="glassCode">玻璃编码</param>
        /// <param name="logNumber">最后一次过站的工序编码（data_06中的logNumber）</param>
        /// <returns>dynamic</returns>
        public dynamic GetLastProcessInfo(string glassCode, string logNumber)
        {
            dynamic reObj = null;
            try
            {
                string SearchGlass = "$G" + glassCode;
                List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix(SearchGlass, "data_02");
                foreach (var productKey in ProductRowResult)
                {
                    //获取rowKey
                    string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                    foreach (var key in productKey.Columns)
                    {
                        try
                        {
                            reObj = null;
                            //获取rowValue
                            string productRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //初始化采集表的数据
                            //反序列化rowValue
                            reObj = JObject.Parse(productRowValue);

                            if (reObj.processCode == logNumber)
                            {
                                return reObj;
                            }
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败" + exp.Message.ToString(), exp);
                        }
                    }//foreach (var key in productKey.Columns)
                }//foreach (var productKey in ProductRowResult)
            }
            catch (Exception exp)
            {
                LogHelper.Error("rowKey范围查询" + exp.Message.ToString(), exp);
            }
            return reObj;
        }


        /// <summary>
        /// 客退重工投入
        /// </summary>
        /// <param name="glassInfo">玻璃信息对象GlassInfo</param>
        /// <param name="order">新工单编码</param>
        /// <param name="line">新产线编码</param>
        /// <returns></returns>
        public string ReworkInput(GlassInfo glassInfo, string order, string line, string reworkNumber, string reworkTime, string reworkIP)
        {
            try
            {
                // 第三步：删除data_01中的绑定条码
                // --------------------------------------------------------------------------------------------
                //删除条码绑定
                try
                {
                    GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.TPCode, "logValue:");
                    GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.BLCode, "logValue:");
                    GetDataFromHBase.Instance.DeleteRowHBase("data_01", glassInfo.ProductInfo.QRCode, "logValue:");
                }
                catch (Exception exp)
                {
                    LogHelper.Error("编码解绑失败|" + exp.Message, exp);
                    Reconnect();
                    return "编码解绑失败";
                }

                // 第三步：更新data_01信息
                // --------------------------------------------------------------------------------------------
                try
                {
                    dynamic objLCD = GetHBaseDataClass.Instance.GetData01(glassInfo.ProductInfo.LCDCode);
                    objLCD.tpCode = "";
                    objLCD.productLineCode = line;
                    objLCD.backCode = "";
                    objLCD.intactCode = "";
                    objLCD.productionOrder = order;
                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", glassInfo.ProductInfo.LCDCode, objLCD);

                    dynamic objFPC = GetHBaseDataClass.Instance.GetData01(glassInfo.ProductInfo.FPCCode);
                    objFPC.tpCode = "";
                    objFPC.productLineCode = line;
                    objFPC.backCode = "";
                    objFPC.intactCode = "";
                    objFPC.productionOrder = order;
                    GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_01", glassInfo.ProductInfo.FPCCode, objFPC);
                }
                catch (Exception ex)
                {
                    LogHelper.Error("玻璃信息更新失败", ex);
                    Reconnect();
                    return "玻璃信息更新失败";
                }

                //更新Data_06
                try
                {
                    // 第二步：更新data_06产品过站信息和状态
                    // --------------------------------------------------------------------------------------------
                    dynamic objData06 = GetHBaseDataClass.Instance.GetDataValueByKey(glassInfo.ProductInfo.LCDCode, "data_06");
                    string logcode = objData06.logCode;
                    if (logcode.Contains("008,"))
                    {
                        objData06.HistoryCode = logcode.Substring(logcode.IndexOf("008") + 4).TrimEnd(',');
                    }
                    else
                    {
                        objData06.HistoryCode = logcode.TrimEnd(',');
                    }

                    objData06.logNumber = "008";//052
                    objData06.logCode = "005,008";//052
                    objData06.lcdState = "重工良品";
                    objData06.Repeat = "True";
                    bool UpdateSuccess = GetHBaseDataClass.Instance.UpdateHBaseKeyValue("data_06", glassInfo.ProductInfo.LCDCode, objData06);

                    if (!UpdateSuccess)
                    {
                        return "更新Data_06失败";
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("单一RowKey更新Data_06失败.|" + exp.Message, exp);
                    Reconnect();
                    return "更新Data_06失败";
                }




                //第五步：更新exception_01产品不良信息
                // --------------------------------------------------------------------------------------------
                ExceptionInfo exceptionInfo = new ExceptionInfo()
                {
                    ExceptionKey = string.Format("$G{0}$T{1}$P{2}", glassInfo.ProductInfo.LCDCode, GetServerTime().ToString("yyyyMMddHHmmss"), "052"),
                    GlassCode = glassInfo.ProductInfo.LCDCode,
                    ProductionOrder = order,
                    FinishesCode = glassInfo.ProductInfo.FinishesCode,
                    FinishesModel = glassInfo.ProductInfo.FinishesModel,
                    ProductionLineCode = line,
                    ProductionLineName = "",
                    ProcessCode = "052",
                    processName = "重工投入",
                    ScanNumber = reworkNumber,
                    ScanTime = reworkTime,
                    DeviceIp = reworkIP,
                    ExceptionContent = "客退重工投入",
                    JudgeNumber = reworkNumber,
                    JudgeContent = "客退重工投入",
                    JudgeTime = reworkTime,
                    JudgeIp = reworkIP,
                    ReworkStatus = "0",
                    ExceptionState = "重工良品",
                    ReworkNumber = reworkNumber,
                    ReworkContent = "客退重工投入",
                    ReworkTime = reworkTime,
                    ReworkIp = reworkIP,
                    ReworkProcess = "052",
                };

                try
                {
                    bool UpdateSuccess = UpdateException01(exceptionInfo.ExceptionKey, exceptionInfo);
                    if (!UpdateSuccess)
                    {
                        return "更新产品重工信息失败";
                    }
                }
                catch (Exception exp)
                {
                    LogHelper.Error("重工exception_01.|" + exp.Message, exp);
                    Reconnect();
                    return "重工exception_01失败";
                }

                return "成功";
            }
            catch (Exception exp)
            {
                LogHelper.Error("重工投入失败.|" + exp.Message, exp);
                return "重工投入失败";
            }
        }


        /// <summary>
        /// 物料批次查询玻璃信息
        /// </summary>
        /// <param name="material_Code">物料编码</param>
        /// <param name="BatchCode">物料批次</param>
        /// <param name="orderCode">工单编码</param>
        /// <returns></returns>
        public DataTable GetBatchGlass(string material_Code, string BatchCode, string orderCode)
        {
            #region  初始化数据表
            DataTable parameterTable = new DataTable();
            parameterTable.Columns.Add("产品型号");//产品型号
            parameterTable.Columns.Add("玻璃编码");//玻璃编码
            parameterTable.Columns.Add("FPC编码");//FPC编码
            parameterTable.Columns.Add("TP编码");//TP编码
            parameterTable.Columns.Add("BL编码");//BL编码
            parameterTable.Columns.Add("客户编码");//Cp编码
            parameterTable.Columns.Add("内箱编码"); //内箱编码
            parameterTable.Columns.Add("外箱编码"); //外箱编码

            //物料上料工序对照表
            string sqlRackProcess = string.Format("SELECT DISTINCT SPOS_SMCode,SPOS_SMName,SPOS_RackProcess FROM Store_ProduceOrder_Sub LEFT JOIN Store_ProduceOrder_Main ON SPOS_SPOMTid=SPOM_Tid WHERE  SPOM_JobCode='{0}'  AND ISNULL(SPOS_RackProcess,'')<>''", orderCode);
            DataTable dtRackProcess = conn.ExecuteDataTable(sqlRackProcess,"Base");
            Dictionary<string, string> dicRackProcess = dtRackProcess.Rows.Cast<DataRow>().ToDictionary(x => x["SPOS_SMCode"].ToString(), x => x["SPOS_RackProcess"].ToString());

            DataView dv = dtRackProcess.DefaultView;
            if (dv.Count > 0)
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    string colName = string.Format("{0}[{1}]", dv[i]["SPOS_SMName"].ToString().Trim().Replace("\r", "").Replace("\n", ""), dv[i]["SPOS_SMCode"].ToString().Trim().Replace("\r", "").Replace("\n", ""));
                    parameterTable.Columns.Add(colName); //原料名称
                }
            }
            //parameterTable.PrimaryKey = new DataColumn[] { parameterTable.Columns["玻璃编码"] };//将玻璃码设置为主键

            #endregion 初始化数据表

            try
            {
                #region  查询物料批次下的所有玻璃码
                //先用查询编码去扫描关联表里面找到对应的玻璃编码（data_09）
                List<TRowResult> glassRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$M" + material_Code + "$P" + BatchCode, "data_09");
                //YJ.Log.FileLog.log.Debug(glassRowResult);
                if (glassRowResult.Count <= 0)
                {
                    return null;
                }
                // 玻璃列表
                List<string> lstGlassCode = new List<string>();
                foreach (var glassKey in glassRowResult)
                {
                    //获取rowKey
                    string glassRowKey = Encoding.UTF8.GetString(glassKey.Row);
                    foreach (var glasskey in glassKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string glassRowValue = Encoding.UTF8.GetString(glasskey.Value.Value);
                            //获取里面的玻璃编码
                            string[] str = glassRowValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            // 合并玻璃码列表
                            lstGlassCode = lstGlassCode.Union(str.ToList<string>()).ToList<string>();
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从扫描关联表中获取玻璃编码失败；" + exp.Message, exp);
                        }
                    }//foreach (var glasskey in glassKey.Columns)
                }//foreach (var glassKey in glassRowResult)
                #endregion 查询物料批次下的所有玻璃码

                #region  从SQL Server查询包装信息
                string option = "'" + string.Join("','", lstGlassCode) + "'";
                string sqlpack = "SELECT DISTINCT IBS_GlassCode, IBM_JobCode, OBM_JobCode FROM TPL_PackingInnerBox_Sub LEFT JOIN TPL_PackingInnerBox_Main ON IBS_IBMTid=IBM_Tid LEFT JOIN TPL_PackingOuterBox_Sub ON OBS_IBMTid=IBM_Tid LEFT JOIN TPL_PackingOuterBox_Main ON OBS_OBMTid=OBM_Tid  WHERE IBS_GlassCode IN(" + option + ")";
                //DataView dvpack = conn.ExecuteDataView(sqlpack, YJ.Config.UserConfig.GetDbName());
                DataTable dtpack = conn.ExecuteDataTable(sqlpack, "Base");
                // 内箱编码表
                Dictionary<string, string> dicInnerBox = dtpack.Rows.Cast<DataRow>().ToDictionary(x => x["IBS_GlassCode"].ToString(), x => x["IBS_GlassCode"].ToString());
                // 外箱编码表
                Dictionary<string, string> dicOuterBox = dtpack.Rows.Cast<DataRow>().ToDictionary(x => x["IBS_GlassCode"].ToString(), x => x["OBM_JobCode"].ToString());
                #endregion 从SQL Server查询包装信息

                #region 从data_01查询玻璃基本信息
                string manyRowkey = string.Join(",", lstGlassCode);
                List<TRowResult> ProductRowResult = GetDataFromHBase.Instance.GetRowsWithManyRowKey(manyRowkey, "data_01");
                foreach (var productKey in ProductRowResult)
                {
                    //获取rowKey
                    string productRowKey = Encoding.UTF8.GetString(productKey.Row);
                    foreach (var key in productKey.Columns)
                    {
                        try
                        {
                            //获取rowValue
                            string productRowValue = Encoding.UTF8.GetString(key.Value.Value);
                            //初始化采集表的数据
                            //反序列化rowValue
                            JObject jsonStr = JObject.Parse(productRowValue);
                            string glasscode = jsonStr["glassCode"].ToString();
                            string fpcCode = jsonStr["fpcCode"].ToString();
                            string backCode = jsonStr["backCode"].ToString();
                            string tpCode = jsonStr["tpCode"].ToString();
                            string intactCode = jsonStr["intactCode"].ToString();
                            string finishesCode = jsonStr["finishesCode"].ToString();

                            DataRow productRow = parameterTable.NewRow();
                            productRow["玻璃编码"] = glasscode;//玻璃编码
                            productRow["FPC编码"] = fpcCode;//FPC编码
                            productRow["TP编码"] = tpCode;//TP编码
                            productRow["BL编码"] = backCode;//BL编码
                            productRow["客户编码"] = intactCode;//Cp编码
                            productRow["产品型号"] = finishesCode;//产品型号

                            if (dicInnerBox.ContainsKey(glasscode))
                            {
                                productRow["内箱编码"] = dicInnerBox[glasscode];//内箱编码
                                productRow["外箱编码"] = dicOuterBox[glasscode];//内箱编码
                            }

                            #region  查询产品生产物料信息
                            List<TRowResult> materialRowResult = GetDataFromHBase.Instance.GetRowsWithPrefix("$G" + glasscode, "data_02");
                            foreach (var materialKey in materialRowResult)
                            {
                                //获取rowKey
                                string materialRowKey = Encoding.UTF8.GetString(materialKey.Row);
                                foreach (var material in materialKey.Columns)
                                {
                                    try
                                    {
                                        //获取rowValue
                                        string materialRowValue = Encoding.UTF8.GetString(material.Value.Value);
                                        //初始化采集表的数据
                                        //反序列化rowValue
                                        JObject materialjsonStr = JObject.Parse(materialRowValue);
                                        string processCode = materialjsonStr["processCode"].ToString();//工序编码

                                        var materialJson = materialjsonStr["Material"].AsEnumerable();

                                        foreach (var materialItem in materialJson)
                                        {
                                            //更新实际采集表的数据
                                            if (materialItem["materialInfo"].ToString() != "" && dicRackProcess.ContainsKey(materialItem["materialInfo"].ToString()) && dicRackProcess[materialItem["materialInfo"].ToString()] == processCode)
                                            {
                                                string materialCode = materialItem["materialInfo"].ToString().Trim().Replace("\r", "").Replace("\n", "");//原料编码
                                                string materialName = materialItem["materialName"].ToString().Trim().Replace("\r", "").Replace("\n", "");//原料名称
                                                string materialBatch = materialItem["materialBatch"].ToString().Trim().Replace("\r", "").Replace("\n", "");//原料入库批次
                                                string colName = string.Format("{0}[{1}]", materialName, materialCode);
                                                if (parameterTable.Columns.Contains(colName))
                                                {
                                                    productRow[colName] = materialBatch;
                                                }
                                            }// if (materialItem["materialInfo"].ToString() != "" && dicRackProcess.ContainsKey(materialItem["materialInfo"].ToString()) && dicRackProcess[materialItem["materialInfo"].ToString()] == processCode)
                                        }//foreach (var materialItem in materialJson)
                                    }//try
                                    catch (Exception exp)
                                    {
                                        LogHelper.Error("从data_02中获取物料信息失败；" + exp.Message, exp);
                                    }
                                }//foreach (var material in materialKey.Columns)
                            }//foreach (var materialKey in materialRowResult)
                            #endregion 查询产品生产物料信息

                            parameterTable.Rows.Add(productRow);
                        }
                        catch (Exception exp)
                        {
                            LogHelper.Error("从data_01获取玻璃信息失败；" + exp.Message, exp);
                        }
                    }//foreach (var key in productKey.Columns)
                }//foreach (var productKey in ProductRowResult)
                #endregion 从data_01查询玻璃基本信息

                return parameterTable;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 获取工序玻璃过站数据
        /// </summary>
        /// <param name="lineCode">产线编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="StartTime">开始时间</param>
        /// <param name="StopTime">结束时间</param>
        /// <returns>DataTable</returns>
        public DataTable GetProcessGlassInfo(string lineCode, string processCode, DateTime StartTime, DateTime StopTime)
        {
            DataTable dtGlass = new DataTable();
            dtGlass.TableName = "GlassTable";
            dtGlass.Columns.Add("glassCode");//Lcd条码
            dtGlass.Columns.Add("fpcCode");//FPC条码
            dtGlass.Columns.Add("blCode");//背光条码
            dtGlass.Columns.Add("qrCode");//客户条码
            dtGlass.Columns.Add("finishesModel");//产品型号
            dtGlass.Columns.Add("productionOrder");//工单编号
            dtGlass.Columns.Add("productLineCode");//产线编号
            dtGlass.Columns.Add("productLineName");//产线名称
            dtGlass.Columns.Add("processCode");//工序编号
            dtGlass.Columns.Add("processName");//工序名称
            dtGlass.Columns.Add("inTime");//过站时间
            
            try
            {
                #region 构建查询范围
                string StartLot = "", StopLot = "";
                //获取时段（前半小时或后半小时）
                StartLot = string.Format("{0}{1}{2}1{3}", lineCode, processCode, StartTime.ToString("yyyyMMddHH"), (StartTime.Minute < 30) ? "1" : "2");
                StopLot = string.Format("{0}{1}{2}1{3}", lineCode, processCode, StopTime.ToString("yyyyMMddHH"), (StopTime.Minute < 30) ? "1" : "2");
                #endregion

                #region 查找工序数据。
                //生产lot信息表  Key  产线编号+工序编号+年(4位)+月(2位)+日(2位)+小时(2位)+生产/物料(1位, 0表示物料,1表示生产)+分钟类型(1位 1表示前半小时 2表示后半小时)
                List<TRowResult> LotRowCount = GetDataFromHBase.Instance.GetRowsWithRowKeyRange(StartLot, StopLot, "data_08");

                //循环lot中为数据,获取这道工序的扫描记录
                foreach (var LotRow in LotRowCount)
                {
                    //获取lot表的rowkey 
                    string LotRowKey = Encoding.UTF8.GetString(LotRow.Row);
                    //循环lot表的rowValue
                    if (LotRowKey.Substring(LotRowKey.Length - 2, 1) == "1")
                    {
                        foreach (var Lotkey in LotRow.Columns)
                        {
                            //获取lot表的rowValue 
                            string GlassLotKey = Encoding.UTF8.GetString(Lotkey.Value.Value);
                            //生产扫描记录表
                            List<TRowResult> GlassRowCount = GetDataFromHBase.Instance.GetRowsWithManyRowKey((GlassLotKey.Trim(',')), "data_02");
                            if (GlassRowCount == null)
                            {
                                LogHelper.Error("key=" + GlassLotKey + "，在data_02中未查询到数据.", new Exception());
                                continue;
                            }
                            foreach (var GlassRow in GlassRowCount)
                            {
                                //获取rowKey
                                string GlassRowKey = Encoding.UTF8.GetString(GlassRow.Row);
                                foreach (var Glasskey in GlassRow.Columns)
                                {
                                    //获取rowValue
                                    string GlassValue = Encoding.UTF8.GetString(Glasskey.Value.Value);
                                    //反序列化rowValue
                                    JObject jsonStr = JObject.Parse(GlassValue);
                                    //更新实际数据
                                    DataRow newRow = dtGlass.NewRow();
                                    string lcd = (jsonStr["glassCode"] ?? "").ToString();//Lcd条码
                                    newRow["glassCode"] = lcd;//Lcd条码
                                    
                                    newRow["finishesModel"] = (jsonStr["finishesModel"] ?? "").ToString(); //产品型号
                                    newRow["productionOrder"] = (jsonStr["productionOrder"] ?? "").ToString();//工单编号

                                    newRow["productLineCode"] = (jsonStr["productLineCode"] ?? "").ToString();//产线编号
                                    newRow["productLineName"] = (jsonStr["productLineName"] ?? "").ToString();//产线名称
                                    newRow["processCode"] = (jsonStr["processCode"] ?? "").ToString(); //工序编号 
                                    newRow["processName"] = (jsonStr["processName"] ?? "").ToString(); //工序名称
                                    newRow["inTime"] = (jsonStr["InTime"] ?? "").ToString(); //过站时间
                                    //从data_06获取玻璃状态
                                    List<TRowResult> processWIPResult = GetDataFromHBase.Instance.GetRowsWithSingleRowKey(lcd, "data_01");
                                    foreach (var processWIP in processWIPResult)
                                    {
                                        //获取rowKey
                                        string processWIPKey = Encoding.UTF8.GetString(processWIP.Row);
                                        foreach (var key in processWIP.Columns)
                                        {
                                            try
                                            {
                                                //获取rowValue
                                                string processWIPValue = Encoding.UTF8.GetString(key.Value.Value);
                                                //System.Diagnostics.Debug.Print(processWIPValue);
                                                //反序列化rowValue
                                                JObject jsonState = JObject.Parse(processWIPValue);
                                                System.Diagnostics.Debug.Print(jsonState.ToString());
                                                //获取里面的工序信息
                                                newRow["fpcCode"] = (jsonState["fpcCode"] ?? "").ToString();
                                                newRow["blCode"] = (jsonState["backCode"] ?? "").ToString();
                                                newRow["qrCode"] = (jsonState["intactCode"] ?? "").ToString();
                                            }
                                            catch (Exception exp)
                                            {
                                                LogHelper.Error("从data_01获取玻璃信息失败：" + exp.Message, exp);
                                            }
                                        }//foreach (var key in processWIP.Columns)
                                    }//foreach (var processWIP in processWIPResult)

                                    dtGlass.Rows.Add(newRow);
                                }//foreach (var Glasskey in GlassRow.Columns)
                            }//foreach (var GlassRow in GlassRowCount)
                        }//foreach (var Lotkey in LotRow.Columns)
                    }//if (LotRowKey.Substring(LotRowKey.Length - 2, 1) == "1")
                }//foreach (var LotRow in LotRowCount)
                #endregion

                return dtGlass;
            }
            catch (Exception ex)
            {
                LogHelper.Error("从HBase获取工序过站数据失败：" + ex.Message, ex);
                throw (ex);
            }

        }

    }
}