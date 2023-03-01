using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    /// <summary>
    /// 玻璃当前已过工序信息
    /// </summary>
    public class GlassState
    {
        /// <summary>
        /// 产品当前已过工序的编码
        /// </summary>
        public string LogNumber { get; set; }
        /// <summary>
        /// 产品已过所有工序的编码
        /// </summary>
        public string LogCode { get; set; }
        /// <summary>
        /// 是否重工
        /// </summary>
        public string Repeat { get; set; }
        /// <summary>
        /// 历史已过工序编码
        /// </summary>
        public string HistoryCode { get; set; }
        /// <summary>
        /// 玻璃当前状态
        /// </summary>
        public string LCDState { get; set; }
//{
//  "HistoryCode": "",
//  "logNumber": "034",
//  "logCode": "005,008,011,024,029,031,032,034",
//  "lcdState": "待复判",
//  "Repeat": ""
//}
    }
}
