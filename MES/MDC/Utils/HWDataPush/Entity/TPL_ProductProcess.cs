using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// TPL_ProductProcess:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TPL_ProductProcess
    {
        public TPL_ProductProcess()
        { }
        #region Model
        private string _spl_jobcode;
        private string _spl_name;
        private string _spl_port;
        private string _spom_jobcode;
        private string _spom_smcode;
        private string _spom_smspec;
        private string _spom_customercode;
        private int? _sgx_tid;
        private string _sgx_jobcode;
        private string _sgx_name;
        private bool _sgx_isbind;
        private string _stw_cardip;
        private string _spl_ipaddress;
        private int? _spom_tid;
        private int? _spl_tid;
        private int? _stw_tid;
        /// <summary>
        /// 
        /// </summary>
        public string SPL_JobCode
        {
            set { _spl_jobcode = value; }
            get { return _spl_jobcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPL_Name
        {
            set { _spl_name = value; }
            get { return _spl_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPL_Port
        {
            set { _spl_port = value; }
            get { return _spl_port; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPOM_JobCode
        {
            set { _spom_jobcode = value; }
            get { return _spom_jobcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPOM_SMCode
        {
            set { _spom_smcode = value; }
            get { return _spom_smcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPOM_SMSpec
        {
            set { _spom_smspec = value; }
            get { return _spom_smspec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPOM_CustomerCode
        {
            set { _spom_customercode = value; }
            get { return _spom_customercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SGX_Tid
        {
            set { _sgx_tid = value; }
            get { return _sgx_tid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SGX_JobCode
        {
            set { _sgx_jobcode = value; }
            get { return _sgx_jobcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SGX_Name
        {
            set { _sgx_name = value; }
            get { return _sgx_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SGX_IsBind
        {
            set { _sgx_isbind = value; }
            get { return _sgx_isbind; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STW_CardIP
        {
            set { _stw_cardip = value; }
            get { return _stw_cardip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPL_IPAddress
        {
            set { _spl_ipaddress = value; }
            get { return _spl_ipaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SPOM_Tid
        {
            set { _spom_tid = value; }
            get { return _spom_tid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SPL_Tid
        {
            set { _spl_tid = value; }
            get { return _spl_tid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? STW_Tid
        {
            set { _stw_tid = value; }
            get { return _stw_tid; }
        }
        #endregion Model
    }
}
