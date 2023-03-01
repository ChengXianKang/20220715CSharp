using System;
namespace Utils.Model
{
    /// <summary>
    /// TPL_Glass_Delivery:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TPL_Glass_Delivery
    {
        public TPL_Glass_Delivery()
        { }
        #region Model
        private Guid _gd_tid;
        private string _gd_sn;
        private string _gd_productcode;
        private string _gd_productmodel;
        private string _gd_productionorder;
        private int? _gd_materialcount;
        private string _gd_productionline;
        private string _gd_state;
        private string _gd_deliveryshop;
        private string _gd_deliverylinecode;
        private string _gd_deliveryline;
        private string _gd_deliveryip;
        private string _gd_deliveryop;
        private DateTime? _gd_deliverytime;
        private string _gd_checkshop;
        private string _gd_checkop;
        private string _gd_checkip;
        private DateTime? _gd_checktime;
        private string _gd_receiveop;
        private DateTime? _gd_receivetime;
        private string _gd_receiveshop;
        private string _gd_receivelinecode;
        private string _gd_receiveline;
        private string _gd_receivedeviceip;
        private string _gd_receiveorder;
        /// <summary>
        /// GUID
        /// </summary>
        public Guid GD_Tid
        {
            set { _gd_tid = value; }
            get { return _gd_tid; }
        }
        /// <summary>
        /// 物料单号
        /// </summary>
        public string GD_SN
        {
            set { _gd_sn = value; }
            get { return _gd_sn; }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string GD_ProductCode
        {
            set { _gd_productcode = value; }
            get { return _gd_productcode; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string GD_ProductModel
        {
            set { _gd_productmodel = value; }
            get { return _gd_productmodel; }
        }
        /// <summary>
        /// 工单编码
        /// </summary>
        public string GD_ProductionOrder
        {
            set { _gd_productionorder = value; }
            get { return _gd_productionorder; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int? GD_MaterialCount
        {
            set { _gd_materialcount = value; }
            get { return _gd_materialcount; }
        }
        /// <summary>
        /// 线别
        /// </summary>
        public string GD_ProductionLine
        {
            set { _gd_productionline = value; }
            get { return _gd_productionline; }
        }
        /// <summary>
        /// 发料状态(0：未发料，1：已发料，2：已收料，3：已投料)
        /// </summary>
        public string GD_State
        {
            set { _gd_state = value; }
            get { return _gd_state; }
        }
        /// <summary>
        /// 发往车间
        /// </summary>
        public string GD_DeliveryShop
        {
            set { _gd_deliveryshop = value; }
            get { return _gd_deliveryshop; }
        }
        /// <summary>
        /// 发到产线编码
        /// </summary>
        public string GD_DeliveryLineCode
        {
            set { _gd_deliverylinecode = value; }
            get { return _gd_deliverylinecode; }
        }
        /// <summary>
        /// 发到产线
        /// </summary>
        public string GD_DeliveryLine
        {
            set { _gd_deliveryline = value; }
            get { return _gd_deliveryline; }
        }
        /// <summary>
        /// 发料IP
        /// </summary>
        public string GD_DeliveryIP
        {
            set { _gd_deliveryip = value; }
            get { return _gd_deliveryip; }
        }
        /// <summary>
        /// 发料人员
        /// </summary>
        public string GD_DeliveryOP
        {
            set { _gd_deliveryop = value; }
            get { return _gd_deliveryop; }
        }
        /// <summary>
        /// 发料时间
        /// </summary>
        public DateTime? GD_DeliveryTime
        {
            set { _gd_deliverytime = value; }
            get { return _gd_deliverytime; }
        }
        /// <summary>
        /// 收料车间
        /// </summary>
        public string GD_CheckShop
        {
            set { _gd_checkshop = value; }
            get { return _gd_checkshop; }
        }
        /// <summary>
        /// 收料IP
        /// </summary>
        public string GD_CheckIP
        {
            set { _gd_checkip = value; }
            get { return _gd_checkip; }
        }
        /// <summary>
        /// 收料人员
        /// </summary>
        public string GD_CheckOP
        {
            set { _gd_checkop = value; }
            get { return _gd_checkop; }
        }
        /// <summary>
        /// 收料时间
        /// </summary>
        public DateTime? GD_CheckTime
        {
            set { _gd_checktime = value; }
            get { return _gd_checktime; }
        }
        /// <summary>
        /// 上料人员
        /// </summary>
        public string GD_ReceiveOP
        {
            set { _gd_receiveop = value; }
            get { return _gd_receiveop; }
        }
        /// <summary>
        /// 上料时间
        /// </summary>
        public DateTime? GD_ReceiveTime
        {
            set { _gd_receivetime = value; }
            get { return _gd_receivetime; }
        }
        /// <summary>
        /// 上料车间
        /// </summary>
        public string GD_ReceiveShop
        {
            set { _gd_receiveshop = value; }
            get { return _gd_receiveshop; }
        }
        /// <summary>
        /// 上料产线编码
        /// </summary>
        public string GD_ReceiveLineCode
        {
            set { _gd_receivelinecode = value; }
            get { return _gd_receivelinecode; }
        }
        /// <summary>
        /// 上料产线
        /// </summary>
        public string GD_ReceiveLine
        {
            set { _gd_receiveline = value; }
            get { return _gd_receiveline; }
        }
        /// <summary>
        /// 上料设备IP地址
        /// </summary>
        public string GD_ReceiveDeviceIP
        {
            set { _gd_receivedeviceip = value; }
            get { return _gd_receivedeviceip; }
        }
        /// <summary>
        /// 上料设备IP地址
        /// </summary>
        public string GD_ReceiveOrder
        {
            set { _gd_receiveorder = value; }
            get { return _gd_receiveorder; }
        }
        #endregion Model

    }
}