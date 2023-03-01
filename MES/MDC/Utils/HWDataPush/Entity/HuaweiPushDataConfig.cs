using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    public class HuaweiPushDataConfig
    {
        /// <summary>
        /// 回传数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 参数组名称
        /// </summary>
        public string GroupName { get; set; }
        ///// <summary>
        ///// 器件名称
        ///// </summary>
        //public string Part { get; set; }
        ///// <summary>
        ///// 供应商名称
        ///// </summary>
        //public string Supplier { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 厂别
        /// </summary>
        public string FactoryName { get; set; }
        ///// <summary>
        ///// 部件
        ///// </summary>
        //public string Parts { get; set; }
        /// <summary>
        /// appKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// tokenURL
        /// </summary>
        public string TokenURL { get; set; }
        /// <summary>
        /// tokenKey
        /// </summary>
        public string TokenKey { get; set; }
        /// <summary>
        /// tokenSecur
        /// </summary>
        public string TokenSecur { get; set; }
        /// <summary>
        /// apiURL
        /// </summary>
        public string ApiURL { get; set; }
        /// <summary>
        /// customerNumber
        /// </summary>
        public string CustomerNumber { get; set; }
    }
}
