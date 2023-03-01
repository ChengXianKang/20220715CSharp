using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    /// <summary>
    /// 工序异常提报表
    /// </summary>
    [Serializable]
    public partial class TPL_ProcessFail_Main
    {
        public TPL_ProcessFail_Main()
        { }
        #region Model
        private int _tfm_tid;
        private string _tfm_status;
        private string _tfm_type;
        private string _tfm_describe;
        private int? _tfm_producttid;
        private string _tfm_productorder;
        private int? _tfm_linetid;
        private string _tfm_linecode;
        private int? _tfm_processtid;
        private string _tfm_processcode;
        private int? _tfm_devicetid;
        private string _tfm_deviceip;
        private string _tfm_scancode;
        private string _tfm_glasscode;
        private DateTime? _tfm_scantime;
        private int? _tfm_num;
        private int? _tfm_addpid;
        private DateTime? _tfm_adddate;
        private int? _tfm_checkpid;
        private DateTime? _tfm_checkdate;
        /// <summary>
        /// 主键
        /// </summary>
        public int TFM_Tid
        {
            set { _tfm_tid = value; }
            get { return _tfm_tid; }
        }
        /// <summary>
        /// 提报状态
        /// </summary>
        public string TFM_Status
        {
            set { _tfm_status = value; }
            get { return _tfm_status; }
        }
        /// <summary>
        /// 提报类型，默认提报时为重工 复判后 有三种状态 （重工 报废 误判）
        /// </summary>
        public string TFM_Type
        {
            set { _tfm_type = value; }
            get { return _tfm_type; }
        }
        /// <summary>
        /// 异常描述 （将提报及复判后的不良内容合并后更新到此字段）
        /// </summary>
        public string TFM_Describe
        {
            set { _tfm_describe = value; }
            get { return _tfm_describe; }
        }
        /// <summary>
        /// 工单主键
        /// </summary>
        public int? TFM_ProductTid
        {
            set { _tfm_producttid = value; }
            get { return _tfm_producttid; }
        }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string TFM_ProductOrder
        {
            set { _tfm_productorder = value; }
            get { return _tfm_productorder; }
        }
        /// <summary>
        /// 产线主键
        /// </summary>
        public int? TFM_LineTid
        {
            set { _tfm_linetid = value; }
            get { return _tfm_linetid; }
        }
        /// <summary>
        /// 产线编号
        /// </summary>
        public string TFM_LineCode
        {
            set { _tfm_linecode = value; }
            get { return _tfm_linecode; }
        }
        /// <summary>
        /// 工序主键
        /// </summary>
        public int? TFM_ProcessTid
        {
            set { _tfm_processtid = value; }
            get { return _tfm_processtid; }
        }
        /// <summary>
        /// 工序编号
        /// </summary>
        public string TFM_ProcessCode
        {
            set { _tfm_processcode = value; }
            get { return _tfm_processcode; }
        }
        /// <summary>
        /// 设备主键
        /// </summary>
        public int? TFM_DeviceTid
        {
            set { _tfm_devicetid = value; }
            get { return _tfm_devicetid; }
        }
        /// <summary>
        /// 设备IP
        /// </summary>
        public string TFM_DeviceIP
        {
            set { _tfm_deviceip = value; }
            get { return _tfm_deviceip; }
        }
        /// <summary>
        /// 扫描码（一般扫描FPC）
        /// </summary>
        public string TFM_ScanCode
        {
            set { _tfm_scancode = value; }
            get { return _tfm_scancode; }
        }
        /// <summary>
        /// 对应的玻璃码
        /// </summary>
        public string TFM_GlassCode
        {
            set { _tfm_glasscode = value; }
            get { return _tfm_glasscode; }
        }
        /// <summary>
        /// 该片玻璃的过站时间
        /// </summary>
        public DateTime? TFM_ScanTime
        {
            set { _tfm_scantime = value; }
            get { return _tfm_scantime; }
        }
        /// <summary>
        /// 提报数量（默认每次只提报一片）
        /// </summary>
        public int? TFM_Num
        {
            set { _tfm_num = value; }
            get { return _tfm_num; }
        }
        /// <summary>
        /// 提报人员主键
        /// </summary>
        public int? TFM_AddPid
        {
            set { _tfm_addpid = value; }
            get { return _tfm_addpid; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? TFM_AddDate
        {
            set { _tfm_adddate = value; }
            get { return _tfm_adddate; }
        }
        /// <summary>
        /// 复判人员主键
        /// </summary>
        public int? TFM_CheckPid
        {
            set { _tfm_checkpid = value; }
            get { return _tfm_checkpid; }
        }
        /// <summary>
        /// 复判时间
        /// </summary>
        public DateTime? TFM_CheckDate
        {
            set { _tfm_checkdate = value; }
            get { return _tfm_checkdate; }
        }
        #endregion Model

    }
}
