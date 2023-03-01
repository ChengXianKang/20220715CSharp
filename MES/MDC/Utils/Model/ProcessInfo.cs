using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    /// <summary>
    /// 物料信息类
    /// </summary>
    public class MaterialItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string productLineId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productLineCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string turnoverNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialBatch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialManufacturers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialBoxNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string holdNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string loadMaterialPerson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string stockOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serialNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialSpecification { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string materialProductionDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productionOrder { get; set; }
    }

    /// <summary>
    /// 过站参数类
    /// </summary>
    public class ParameterItem
    {
        /// <summary>
        /// 图档名称
        /// </summary>
        public string parameterType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parameterValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parameterUnit { get; set; }
    }

    /// <summary>
    /// 玻璃过站信息类
    /// </summary>
    public class ProcessInfo
    {
        /// <summary>
        /// 是否补扫
        /// </summary>
        public string bfFlag { get; set; }
        /// <summary>
        /// 工序ID
        /// </summary>
        public string processId { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string processCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string processName { get; set; }
        /// <summary>
        /// 工序序号
        /// </summary>
        public string processNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isCollection { get; set; }
        /// <summary>
        /// 机台ID
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 机台IP
        /// </summary>
        public string deviceIp { get; set; }
        /// <summary>
        /// 机台名称
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 机台负责人
        /// </summary>
        public string deviceOP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceEventCode { get; set; }
        /// <summary>
        /// 产线ID
        /// </summary>
        public string productLineId { get; set; }
        /// <summary>
        /// 产线编码
        /// </summary>
        public string productLineCode { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        public string productLineName { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string finishesCode { get; set; }
        /// <summary>
        /// 成品型号
        /// </summary>
        public string finishesModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string finishesProductNo { get; set; }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string clientProductNo { get; set; }
        /// <summary>
        /// 进站时间
        /// </summary>
        public string InTime { get; set; }
        /// <summary>
        /// 出站时间
        /// </summary>
        public string outTime { get; set; }
        /// <summary>
        /// 扫描码
        /// </summary>
        public string trackNumber { get; set; }
        /// <summary>
        /// 对应码
        /// </summary>
        public string oppositeNumber { get; set; }
        /// <summary>
        /// 异常状态
        /// </summary>
        public string exceptionState { get; set; }
        /// <summary>
        /// 异常描述
        /// </summary>
        public string exceptionContent { get; set; }
        /// <summary>
        /// 生产工单
        /// </summary>
        public string productionOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string destroyOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceCallState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deviceCallContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scanIp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scanNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scanOP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mouldNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mouldName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customData { get; set; }
        /// <summary>
        /// 物料信息列表
        /// </summary>
        public List<MaterialItem> Material { get; set; }
        /// <summary>
        /// 机台参数列表
        /// </summary>
        public List<ParameterItem> Parameter { get; set; }
        /// <summary>
        /// 玻璃码
        /// </summary>
        public string glassCode { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string lotNumber { get; set; }
        /// <summary>
        /// 重工次数
        /// </summary>
        public string repeatStatus { get; set; }
        /// <summary>
        /// 是否返检
        /// </summary>
        public string reworkStatus { get; set; }
    }
}
