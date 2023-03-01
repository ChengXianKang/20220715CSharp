using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// 华为上传数据类
    /// </summary>
    public class HuaweiPushData
    {
        /// <summary>
        /// 数据类型（tplcdQCVoList）
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// AppKey
        /// </summary>
        public string appKey { get; set; }
        /// <summary>
        /// 测试参数列表
        /// </summary>
        public List<TplcdQCVoListItem> tplcdQCVoList { get; set; }
    }

    /// <summary>
    /// Tplcd测试数据类
    /// </summary>
    public class TplcdQCVoListItem
    {
        /// <summary>
        /// 华为编码
        /// </summary>
        public string customerNumber { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string supplierName { get; set; }
        /// <summary>
        /// 厂别
        /// </summary>
        public string factoryName { get; set; }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string customerModel { get; set; }
        /// <summary>
        /// lot批次号
        /// </summary>
        public string lotNumber { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string serialNumber { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string productNumber { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string line { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public string qcTime { get; set; }
        /// <summary>
        /// 生产工序/测试工站
        /// </summary>
        public string testStation { get; set; }
        /// <summary>
        /// 测试项目
        /// </summary>
        public string testItemName { get; set; }
        /// <summary>
        /// 测试子项
        /// </summary>
        public string testSubItemName { get; set; }
        /// <summary>
        /// 控制上限
        /// </summary>
        public string controlUpperLimit { get; set; }
        /// <summary>
        /// 控制下限
        /// </summary>
        public string controlLowerLimit { get; set; }
        /// <summary>
        /// 测试单位
        /// </summary>
        public string testUnit { get; set; }
        /// <summary>
        /// 测试值
        /// </summary>
        public string testResult { get; set; }
        /// <summary>
        /// 测试结果（PASS/FAIL）
        /// </summary>
        public string testCharResult { get; set; }
        /// <summary>
        /// 是否为验证模式，默认false
        /// </summary>
        public string isVerification { get; set; }
        /// <summary>
        /// 测试组ID
        /// </summary>
        public string rawDataID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
