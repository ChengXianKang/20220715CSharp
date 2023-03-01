using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// 华为数据上传记录
    /// </summary>
    [Serializable]
    public partial class HuaweiUpdate
    {
        public HuaweiUpdate()
        { }
        #region Model
        private Guid _hw_guid;
        private string _hw_customernumber;
        private string _hw_suppliername;
        private string _hw_factoryname;
        private string _hw_customermodel;
        private string _hw_lotnumber;
        private string _hw_serialnumber;
        private string _hw_productnumber;
        private string _hw_line;
        private string _hw_qctime;
        private string _hw_teststation;
        private string _hw_testitemname;
        private string _hw_testsubitemname;
        private string _hw_controlupperlimit;
        private string _hw_controllowerlimit;
        private string _hw_testunit;
        private string _hw_testresult;
        private string _hw_testcharresult;
        private string _hw_isverification;
        private string _hw_rawdataid;
        private string _hw_remark;
        private DateTime? _hw_updatetime;
        /// <summary>
        /// GUID
        /// </summary>
        public Guid HW_Guid
        {
            set { _hw_guid = value; }
            get { return _hw_guid; }
        }
        /// <summary>
        /// 华为编码
        /// </summary>
        public string HW_CustomerNumber
        {
            set { _hw_customernumber = value; }
            get { return _hw_customernumber; }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public string HW_SupplierName
        {
            set { _hw_suppliername = value; }
            get { return _hw_suppliername; }
        }
        /// <summary>
        /// 产别
        /// </summary>
        public string HW_FactoryName
        {
            set { _hw_factoryname = value; }
            get { return _hw_factoryname; }
        }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string HW_CustomerModel
        {
            set { _hw_customermodel = value; }
            get { return _hw_customermodel; }
        }
        /// <summary>
        /// lot批次号
        /// </summary>
        public string HW_LotNumber
        {
            set { _hw_lotnumber = value; }
            get { return _hw_lotnumber; }
        }
        /// <summary>
        /// 序列号
        /// </summary>
        public string HW_SerialNumber
        {
            set { _hw_serialnumber = value; }
            get { return _hw_serialnumber; }
        }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string HW_ProductNumber
        {
            set { _hw_productnumber = value; }
            get { return _hw_productnumber; }
        }
        /// <summary>
        /// 生产线
        /// </summary>
        public string HW_Line
        {
            set { _hw_line = value; }
            get { return _hw_line; }
        }
        /// <summary>
        /// 测试时间(时间戳)
        /// </summary>
        public string HW_QcTime
        {
            set { _hw_qctime = value; }
            get { return _hw_qctime; }
        }
        /// <summary>
        /// 生产工序/测试工站
        /// </summary>
        public string HW_TestStation
        {
            set { _hw_teststation = value; }
            get { return _hw_teststation; }
        }
        /// <summary>
        /// 测试项目
        /// </summary>
        public string HW_TestItemName
        {
            set { _hw_testitemname = value; }
            get { return _hw_testitemname; }
        }
        /// <summary>
        /// 测试子项
        /// </summary>
        public string HW_TestSubItemName
        {
            set { _hw_testsubitemname = value; }
            get { return _hw_testsubitemname; }
        }
        /// <summary>
        /// 控制上限
        /// </summary>
        public string HW_ControlUpperLimit
        {
            set { _hw_controlupperlimit = value; }
            get { return _hw_controlupperlimit; }
        }
        /// <summary>
        /// 控制下限
        /// </summary>
        public string HW_ControlLowerLimit
        {
            set { _hw_controllowerlimit = value; }
            get { return _hw_controllowerlimit; }
        }
        /// <summary>
        /// 测试单位
        /// </summary>
        public string HW_TestUnit
        {
            set { _hw_testunit = value; }
            get { return _hw_testunit; }
        }
        /// <summary>
        /// 测试值
        /// </summary>
        public string HW_TestResult
        {
            set { _hw_testresult = value; }
            get { return _hw_testresult; }
        }
        /// <summary>
        /// 测试结果（PASS/FAIL）
        /// </summary>
        public string HW_TestCharResult
        {
            set { _hw_testcharresult = value; }
            get { return _hw_testcharresult; }
        }
        /// <summary>
        /// 是否为验证模式，默认""
        /// </summary>
        public string HW_IsVerification
        {
            set { _hw_isverification = value; }
            get { return _hw_isverification; }
        }
        /// <summary>
        /// 测试组ID
        /// </summary>
        public string HW_RawDataID
        {
            set { _hw_rawdataid = value; }
            get { return _hw_rawdataid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string HW_Remark
        {
            set { _hw_remark = value; }
            get { return _hw_remark; }
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? HW_UpdateTime
        {
            set { _hw_updatetime = value; }
            get { return _hw_updatetime; }
        }
        #endregion Model

    }
}
