using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    /// <summary>
    /// 玻璃的基本产品信息
    /// </summary>
    [Serializable]
    public partial class ProductInfo
    {
        /// <summary>
        /// 玻璃码
        /// </summary>
        public string LCDCode { get; set; }
        /// <summary>
        /// FPC编码
        /// </summary>
        public string FPCCode { get; set; }
        /// <summary>
        /// TP编码
        /// </summary>
        public string TPCode { get; set; }
        /// <summary>
        /// 背光编码
        /// </summary>
        public string BLCode { get; set; }
        /// <summary>
        /// 成品编码
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        /// IC编码
        /// </summary>
        public string ICCode { get; set; }
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
        /// 生产日期
        /// </summary>
        public string ProductionDate { get; set; } 
        /// <summary>
        /// 客户料号
        /// </summary>
        public string CustomerNumber { get; set; }
        /// <summary>
        /// TP是否有码
        /// </summary>
        public string TPState { get; set; }

    }
}
