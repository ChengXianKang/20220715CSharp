using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// Cell_MachineInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Cell_MachineInfo
    {
        public Cell_MachineInfo()
        { }
        #region Model
        private int _tcm_tid;
        private string _tcm_deviceip;
        private string _tcm_cuntomernumber;
        private string _tcm_customermodel;
        private string _tcm_productnumber;
        private string _tcm_remark;
        private bool _tcm_enabled;
        /// <summary>
        /// ID
        /// </summary>
        public int TCM_Tid
        {
            set { _tcm_tid = value; }
            get { return _tcm_tid; }
        }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string TCM_DeviceIP
        {
            set { _tcm_deviceip = value; }
            get { return _tcm_deviceip; }
        }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string TCM_CuntomerNumber
        {
            set { _tcm_cuntomernumber = value; }
            get { return _tcm_cuntomernumber; }
        }
        /// <summary>
        /// 客户型号
        /// </summary>
        public string TCM_CustomerModel
        {
            set { _tcm_customermodel = value; }
            get { return _tcm_customermodel; }
        }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string TCM_ProductNumber
        {
            set { _tcm_productnumber = value; }
            get { return _tcm_productnumber; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string TCM_Remark
        {
            set { _tcm_remark = value; }
            get { return _tcm_remark; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool TCM_Enabled
        {
            set { _tcm_enabled = value; }
            get { return _tcm_enabled; }
        }
        #endregion Model

    }
}
