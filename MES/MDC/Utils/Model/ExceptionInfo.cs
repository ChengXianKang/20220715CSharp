using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    public class ExceptionInfo
    {
        /// <summary>
        /// 不良RowKey
        /// </summary>
        public string ExceptionKey { get; set; }
        /// <summary>
        /// 玻璃编码
        /// </summary>
        public string GlassCode { get; set; }
        /// <summary>
        /// 生产工单
        /// </summary>
        public string ProductionOrder { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string FinishesCode { get; set; } 
        /// <summary>
        /// 成品规格型号
        /// </summary>
        public string FinishesModel { get; set; }
        /// <summary>
        /// 产线编码
        /// </summary>
        public string ProductionLineCode { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        public string ProductionLineName { get; set; }
        /// <summary>
        /// 是否返修品
        /// </summary>
        public string Repeat { get; set; }
        /// <summary>
        /// 重工次数
        /// </summary>
        public string RepeatStatus { get; set; }
        /// <summary>
        /// 是否返检
        /// </summary>
        public string ReworkStatus { get; set; }
        /// <summary>
        /// 申报状态（待复判 复判良品 复判报废 复判重工 重工良品 重工报废）
        /// </summary>
        public string ExceptionState { get; set; }
        /// <summary>
        /// 不良流水号
        /// </summary>
        public string LotNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LotNumberSecond { get; set; } 
        /// <summary>
        /// 申报工序编码
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 申报工序名称
        /// </summary>
        public string processName { get; set; }
        /// <summary>
        /// 申报人员
        /// </summary>
        public string ScanNumber { get; set; }
        /// <summary>
        /// 申报时间
        /// </summary>
        public string ScanTime { get; set; }
        /// <summary>
        /// 申报不良设备IP
        /// </summary>
        public string DeviceIp { get; set; }
        /// <summary>
        /// 申报异常项（竖线分隔）
        /// </summary>
        public string ExceptionContent { get; set; } 
        /// <summary>
        /// 复判人员
        /// </summary>
        public string JudgeNumber { get; set; }
        /// <summary>
        /// 复判时间
        /// </summary>
        public string JudgeTime { get; set; } 
        /// <summary>
        /// 复判设备Ip
        /// </summary>
        public string JudgeIp { get; set; }
        /// <summary>
        /// 复判异常项
        /// </summary>
        public string JudgeContent { get; set; }
        /// <summary>
        /// 重工人员
        /// </summary>
        public string ReworkNumber { get; set; } 
        /// <summary>
        /// 重工时间
        /// </summary>
        public string ReworkTime { get; set; } 
        /// <summary>
        /// 重工Ip
        /// </summary>
        public string ReworkIp { get; set; } 
        /// <summary>
        /// 重工异常项
        /// </summary>
        public string ReworkContent { get; set; }
        /// <summary>
        /// 解绑工序
        /// </summary>
        public string ReworkProcess { get; set; } 
    }

    public class ExceptionItem
    {
        /// <summary>
        /// 产线编码
        /// </summary>
        public string productLineCode { get; set; }
        /// <summary>
        /// 玻璃编码
        /// </summary>
        public string glassCode { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        public string productLineName { get; set; }
        /// <summary>
        /// 申报IP
        /// </summary>
        public string deviceIp { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string finishesCode { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string processCode { get; set; }
        /// <summary>
        /// 申报时间
        /// </summary>
        public string scanTime { get; set; }
        /// <summary>
        /// 成品型号
        /// </summary>
        public string finishesModel { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string exceptionState { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string processName { get; set; }
        /// <summary>
        /// 不良批号
        /// </summary>
        public string lotNumber { get; set; }
        /// <summary>
        /// 申报人员
        /// </summary>
        public string scanNumber { get; set; }
        /// <summary>
        /// 申报不良描述
        /// </summary>
        public string exceptionContent { get; set; }
        /// <summary>
        /// 生产工单
        /// </summary>
        public string productionOrder { get; set; }
        /// <summary>
        /// 重工次数
        /// </summary>
        public string repeatStatus { get; set; }
        /// <summary>
        /// 复判人员
        /// </summary>
        public string judgeNumber { get; set; }
        /// <summary>
        /// 复判不良描述
        /// </summary>
        public string judgeContent { get; set; }
        /// <summary>
        /// 复判时间
        /// </summary>
        public string judgeTime { get; set; }
        /// <summary>
        /// 复判IP
        /// </summary>
        public string judgeIp { get; set; }
        /// <summary>
        /// 重工人员
        /// </summary>
        public string reWorkNumber { get; set; }
        /// <summary>
        /// 重工时间
        /// </summary>
        public string reWorkTime { get; set; }
        /// <summary>
        /// 重工IP
        /// </summary>
        public string reWorkIp { get; set; }
        /// <summary>
        /// 重工不良描述
        /// </summary>
        public string reWorkContent { get; set; }
        /// <summary>
        /// 重工解绑工序
        /// </summary>
        public string reWorkProcess { get; set; }
    }
}
