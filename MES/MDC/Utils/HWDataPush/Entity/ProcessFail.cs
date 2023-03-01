using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// 工序异常提报表
    /// </summary>
    [Serializable]
    public partial class ProcessFail
    {
        /// <summary>
        /// TFM_Tid,TFM_Status,TFM_Type,TFM_Describe,TFM_ProductOrder,SPOM_SMCode,SPOM_SMSpec,SPOM_CustomerCode,TFM_LineCode,TFM_ProcessCode,TFM_DeviceIP,TFM_GlassCode,TFM_ScanTime,TFM_CheckDate
        /// </summary>
        public ProcessFail()
        { }
        #region Model
        private int _tid;
        private string _status;
        private string _type;
        private string _describe;
        private string _productorder;
        private string _smcode;
        private string _smspec;
        private string _cuscode;
        private string _linecode;
        private string _processcode;
        private string _deviceip;
        private string _glasscode;
        private DateTime? _scantime;
        private DateTime? _checkdate;
        /// <summary>
        /// 主键
        /// </summary>
        public int TFM_Tid
        {
            set { _tid = value; }
            get { return _tid; }
        }
        /// <summary>
        /// 提报状态
        /// </summary>
        public string TFM_Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 提报类型，默认提报时为重工 复判后 有三种状态 （重工 报废 误判）
        /// </summary>
        public string TFM_Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 异常描述 （将提报及复判后的不良内容合并后更新到此字段）
        /// </summary>
        public string TFM_Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string TFM_ProductOrder
        {
            set { _productorder = value; }
            get { return _productorder; }
        }
        /// <summary>
        /// 型号编码
        /// </summary>
        public string SPOM_SMCode
        {
            set { _smcode = value; }
            get { return _smcode; }
        }
        /// <summary>
        /// 成品型号
        /// </summary>
        public string SPOM_SMSpec
        {
            set { _smspec = value; }
            get { return _smspec; }
        }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string SPOM_CustomerCode
        {
            set { _cuscode = value; }
            get { return _cuscode; }
        }
        /// <summary>
        /// 产线编号
        /// </summary>
        public string TFM_LineCode
        {
            set { _linecode = value; }
            get { return _linecode; }
        }
        /// <summary>
        /// 工序编号
        /// </summary>
        public string TFM_ProcessCode
        {
            set { _processcode = value; }
            get { return _processcode; }
        }
        /// <summary>
        /// 设备IP
        /// </summary>
        public string TFM_DeviceIP
        {
            set { _deviceip = value; }
            get { return _deviceip; }
        }
        /// <summary>
        /// 对应的玻璃码
        /// </summary>
        public string TFM_GlassCode
        {
            set { _glasscode = value; }
            get { return _glasscode; }
        }
        /// <summary>
        /// 该片玻璃的过站时间
        /// </summary>
        public DateTime? TFM_ScanTime
        {
            set { _scantime = value; }
            get { return _scantime; }
        }
        /// <summary>
        /// 复判时间
        /// </summary>
        public DateTime? TFM_CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        #endregion Model

    }
}
