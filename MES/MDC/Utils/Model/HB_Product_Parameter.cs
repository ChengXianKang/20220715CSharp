using System;
namespace Utils.Model
{
    /// <summary>
    /// HB_Product_Parameter:实体类(华为数据上传参数)
    /// </summary>
    [Serializable]
    public partial class HB_Product_Parameter
    {
        public HB_Product_Parameter()
        { }
        #region Model
        private string _hpp_guid;
        private string _hpp_code;
        private string _hpp_smcode;
        private string _hpp_smspec;
        private string _hpp_cuscode;
        private string _hpp_spljobcode;
        private string _hpp_sgxjobcode;
        private string _hpp_devicecode;
        private string _hpp_cardip;
        private string _hpp_scanip;
        private string _hpp_productdate;
        private string _hpp_lotcode;
        private DateTime? _hpp_adddate;
        private string _hpp_lcd;
        private string _hpp_fpc;
        private string _hpp_tp;
        private string _hpp_bl;
        private string _hpp_qr;
        private string _hpp_parameter1;
        private string _hpp_parameter2;
        private string _hpp_parameter3;
        private string _hpp_parameter4;
        private string _hpp_parameter5;
        private string _hpp_parameter6;
        private string _hpp_parameter7;
        private string _hpp_parameter8;
        private string _hpp_parameter9;
        private string _hpp_parameter10;
        private bool _hpp_issend = false;
        private DateTime? _hpp_senddate;
        private string _hpp_testresult;
        private string _hpp_testremark;
        /// <summary>
        /// GUID
        /// </summary>
        public string HPP_Guid
        {
            set { _hpp_guid = value; }
            get { return _hpp_guid; }
        }
        /// <summary>
        /// 工单编码
        /// </summary>
        public string HPP_Code
        {
            set { _hpp_code = value; }
            get { return _hpp_code; }
        }
        /// <summary>
        /// 成品编码
        /// </summary>
        public string HPP_SMCode
        {
            set { _hpp_smcode = value; }
            get { return _hpp_smcode; }
        }
        /// <summary>
        /// 成品型号
        /// </summary>
        public string HPP_SMSpec
        {
            set { _hpp_smspec = value; }
            get { return _hpp_smspec; }
        }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string HPP_CusCode
        {
            set { _hpp_cuscode = value; }
            get { return _hpp_cuscode; }
        }
        /// <summary>
        /// 产线编码
        /// </summary>
        public string HPP_SPLJobCode
        {
            set { _hpp_spljobcode = value; }
            get { return _hpp_spljobcode; }
        }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string HPP_SGXJobCode
        {
            set { _hpp_sgxjobcode = value; }
            get { return _hpp_sgxjobcode; }
        }
        /// <summary>
        /// 机台编码
        /// </summary>
        public string HPP_DeviceCode
        {
            set { _hpp_devicecode = value; }
            get { return _hpp_devicecode; }
        }
        /// <summary>
        /// 设备IP
        /// </summary>
        public string HPP_CardIP
        {
            set { _hpp_cardip = value; }
            get { return _hpp_cardip; }
        }
        /// <summary>
        /// 扫描IP
        /// </summary>
        public string HPP_ScanIP
        {
            set { _hpp_scanip = value; }
            get { return _hpp_scanip; }
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string HPP_ProductDate
        {
            set { _hpp_productdate = value; }
            get { return _hpp_productdate; }
        }
        /// <summary>
        /// 生产批号
        /// </summary>
        public string HPP_LotCode
        {
            set { _hpp_lotcode = value; }
            get { return _hpp_lotcode; }
        }
        /// <summary>
        /// 过站时间
        /// </summary>
        public DateTime? HPP_AddDate
        {
            set { _hpp_adddate = value; }
            get { return _hpp_adddate; }
        }
        /// <summary>
        /// 玻璃编码
        /// </summary>
        public string HPP_LCD
        {
            set { _hpp_lcd = value; }
            get { return _hpp_lcd; }
        }
        /// <summary>
        /// FPC编码
        /// </summary>
        public string HPP_FPC
        {
            set { _hpp_fpc = value; }
            get { return _hpp_fpc; }
        }
        /// <summary>
        /// TP编码
        /// </summary>
        public string HPP_TP
        {
            set { _hpp_tp = value; }
            get { return _hpp_tp; }
        }
        /// <summary>
        /// 背光编码
        /// </summary>
        public string HPP_BL
        {
            set { _hpp_bl = value; }
            get { return _hpp_bl; }
        }
        /// <summary>
        /// 成品编码
        /// </summary>
        public string HPP_QR
        {
            set { _hpp_qr = value; }
            get { return _hpp_qr; }
        }
        /// <summary>
        /// 机台参数1
        /// </summary>
        public string HPP_Parameter1
        {
            set { _hpp_parameter1 = value; }
            get { return _hpp_parameter1; }
        }
        /// <summary>
        /// 机台参数2
        /// </summary>
        public string HPP_Parameter2
        {
            set { _hpp_parameter2 = value; }
            get { return _hpp_parameter2; }
        }
        /// <summary>
        /// 机台参数3
        /// </summary>
        public string HPP_Parameter3
        {
            set { _hpp_parameter3 = value; }
            get { return _hpp_parameter3; }
        }
        /// <summary>
        /// 机台参数4
        /// </summary>
        public string HPP_Parameter4
        {
            set { _hpp_parameter4 = value; }
            get { return _hpp_parameter4; }
        }
        /// <summary>
        /// 机台参数5
        /// </summary>
        public string HPP_Parameter5
        {
            set { _hpp_parameter5 = value; }
            get { return _hpp_parameter5; }
        }
        /// <summary>
        /// 机台参数6
        /// </summary>
        public string HPP_Parameter6
        {
            set { _hpp_parameter6 = value; }
            get { return _hpp_parameter6; }
        }
        /// <summary>
        /// 机台参数7
        /// </summary>
        public string HPP_Parameter7
        {
            set { _hpp_parameter7 = value; }
            get { return _hpp_parameter7; }
        }
        /// <summary>
        /// 机台参数8
        /// </summary>
        public string HPP_Parameter8
        {
            set { _hpp_parameter8 = value; }
            get { return _hpp_parameter8; }
        }
        /// <summary>
        /// 机台参数9
        /// </summary>
        public string HPP_Parameter9
        {
            set { _hpp_parameter9 = value; }
            get { return _hpp_parameter9; }
        }
        /// <summary>
        /// 机台参数10
        /// </summary>
        public string HPP_Parameter10
        {
            set { _hpp_parameter10 = value; }
            get { return _hpp_parameter10; }
        }
        /// <summary>
        /// 是否发送
        /// </summary>
        public bool HPP_IsSend
        {
            set { _hpp_issend = value; }
            get { return _hpp_issend; }
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? HPP_SendDate
        {
            set { _hpp_senddate = value; }
            get { return _hpp_senddate; }
        }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string HPP_TestResult
        {
            set { _hpp_testresult = value; }
            get { return _hpp_testresult; }
        }
        /// <summary>
        /// 测试说明
        /// </summary>
        public string HPP_TestRemark
        {
            set { _hpp_testremark = value; }
            get { return _hpp_testremark; }
        }
        #endregion Model

    }
}
