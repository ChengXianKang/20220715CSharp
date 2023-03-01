using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    /// <summary>
    /// 玻璃信息
    /// </summary>
    [Serializable]
    public partial class GlassInfo
    {
        /// <summary>
        /// 产品基本信息
        /// </summary>
        public ProductInfo ProductInfo { get; set; }
        /// <summary>
        /// 产品过站信息
        /// </summary>
        public GlassState GlassState { get; set; }
        /// <summary>
        /// 不良信息列表
        /// </summary>
        public Dictionary<string, ExceptionInfo> Exception { get; set; }
        /// <summary>
        /// 最近一次不良信息Key
        /// </summary>
        public string LastExceptionKey { get; set; }
//tableRow["Process_LCDCode"] = glassCode;//玻璃码
//                        tableRow["Process_productionOrder"] = productionOrder;//生产订单
//                        tableRow["Process_productLineCode"] = productLineCode;//data_01产线编码
//                        tableRow["Process_productDate"] = "";//生产日期
//                        tableRow["Process_number"] = "";//工序序号
//                        tableRow["Process_name"] = "";//工序名称
//                        tableRow["Process_BLCode"] = blCode;//背光编码
//                        tableRow["Process_FPCCode"] = fpcCode;//FPC编码
//                        tableRow["Process_QRCode"] = intactCode;//成品编码
//                        tableRow["Process_TPCode"] = tpCode;//TP编码
//                        tableRow["Process_ICCode"] = icCode;//IC编码
//                        tableRow["Process_finishesCode"] = finishesCode;//成品料号
//                        tableRow["Process_finishesModel"] = finishesModel;//成品规格型号
//                        tableRow["Process_clientProductNo"] = clientProductNo;//客户料号
//                        tableRow["Process_logNumber"] = logNumber;//产品已过工序的序号
//                        tableRow["Process_logCode"] = logCode;//产品已过工序的编码


//                {"ProductOrder",""},              //工单编码
//                {"FinishesCode",""},              //产品编码
//                {"FinishesModel",""},             //成品型号
//                {"ClientProductNo",""},           //客户料号
//                {"LineCode",""},                  //产线编码
//                {"ProcessCode",""},               //工序编码
//                {"ProcessName",""},               //工序名称
//                {"DeviceIp",""},                  //设备IP
//                {"ProductDate",""},               //生产日期
//                {"LotNumber",""},                 //批次号
//                {"DateTime",""},                  //过站时间
//                {"GlassCode",""},                 //玻璃码
//                {"FPCCode",""},                   //FPC码
//                {"TPCode",""},                    //TP码
//                {"BLCode",""},                    //背光码
//                {"QRCode",""},                    //成品码
//                {"TrackNumber",""},               //扫描码
//                {"OppositeNumber",""}             //对应码


//                productTable.Columns.Add("product_ProductOrder");//生产订单编码
//                productTable.Columns.Add("product_destroyOrder");//销售订单编码
//                productTable.Columns.Add("product_LineTid");//产线ID
//                productTable.Columns.Add("product_LineCode");//产线编码
//                productTable.Columns.Add("product_LineName");//产线名称
//                productTable.Columns.Add("product_finishesCode");//成品的编码
//                productTable.Columns.Add("product_finishesModel");//成品型号
//                productTable.Columns.Add("product_finishesProductNo");//成品的料号
//                productTable.Columns.Add("product_clientProductNo");//客户料号
//                productTable.Columns.Add("product_reworkFlag");//是否重工解绑

//                parameterTable.Columns.Add("parameter_RowKey");//RowKey
//                parameterTable.Columns.Add("parameter_ProcessTid");//工序ID
//                parameterTable.Columns.Add("parameter_ProcessCode");//工序编码
//                parameterTable.Columns.Add("parameter_ProcessName");//工序名称
//                parameterTable.Columns.Add("parameter_ProcessNumber", typeof(int));//工序序号
//                parameterTable.Columns.Add("parameter_CardTid");//机台ID
//                parameterTable.Columns.Add("parameter_CardIP");//机台IP  
//                parameterTable.Columns.Add("parameter_CardName");//机台名称
//                parameterTable.Columns.Add("parameter_ScanIP");//扫描IP
//                parameterTable.Columns.Add("parameter_ScanOP");//扫描人员
//                parameterTable.Columns.Add("parameter_ScanNumber");//扫描人员
//                parameterTable.Columns.Add("parameter_MouldNumber");//治具编码
//                parameterTable.Columns.Add("parameter_MouldName");//治具名称
//                parameterTable.Columns.Add("parameter_EventCode");//机台事件
//                parameterTable.Columns.Add("parameter_deviceOP");//机台负责人
//                parameterTable.Columns.Add("parameter_LineCode");//产线编码

//                parameterTable.Columns.Add("parameter_LotNo");//生产批次号
//                parameterTable.Columns.Add("parameter_DateTime");//采集时间
//                parameterTable.Columns.Add("parameter_trackNumber");//扫描码
//                parameterTable.Columns.Add("parameter_oppositeNumber");//对应码
//                parameterTable.Columns.Add("parameter_bfFlag");//是否补扫
//                parameterTable.Columns.Add("parameter_exceptionState");//是否异常
//                parameterTable.Columns.Add("parameter_exceptionContent");//异常原因
//                parameterTable.Columns.Add("parameter_deviceCallState");//是否报警
//                parameterTable.Columns.Add("parameter_deviceCallContent");//报警内容
//                parameterTable.Columns.Add("parameter_Type");//参数类型
//                parameterTable.Columns.Add("parameter_Value");//参数值
//                parameterTable.Columns.Add("parameter_Unit");//参数单位
    }
}
