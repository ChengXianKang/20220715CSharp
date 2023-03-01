using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Model
{
    /// <summary>
    /// 工序异常提报明细表
    /// </summary>
    [Serializable]
    public partial class TPL_ProcessFail_Sub
    {
        public TPL_ProcessFail_Sub()
        { }
        #region Model
        private int _tfs_tid;
        private int? _tfs_tfmtid;
        private int? _tfs_bnctid;
        private string _tfs_bncname;
        private string _tfs_type;
        private int? _tfs_addpid;
        private DateTime? _tfs_adddate;
        /// <summary>
        /// 主键
        /// </summary>
        public int TFS_Tid
        {
            set { _tfs_tid = value; }
            get { return _tfs_tid; }
        }
        /// <summary>
        /// 提报主键
        /// </summary>
        public int? TFS_TFMTid
        {
            set { _tfs_tfmtid = value; }
            get { return _tfs_tfmtid; }
        }
        /// <summary>
        /// 不良项主键
        /// </summary>
        public int? TFS_BNCTid
        {
            set { _tfs_bnctid = value; }
            get { return _tfs_bnctid; }
        }
        /// <summary>
        /// 不良项名称
        /// </summary>
        public string TFS_BNCName
        {
            set { _tfs_bncname = value; }
            get { return _tfs_bncname; }
        }
        /// <summary>
        /// 异常来源 （表示该异常是产线提报时判定的还是提报后复判判定的（01 表示提报判定 02 表示复判判定  03表示提报与复判都判定的，01多 说明产线误判率比较高     02多说明产线判定率不较低    03多 说明产线提报准确率高）
        /// </summary>
        public string TFS_Type
        {
            set { _tfs_type = value; }
            get { return _tfs_type; }
        }
        /// <summary>
        /// 提报人员主键
        /// </summary>
        public int? TFS_AddPid
        {
            set { _tfs_addpid = value; }
            get { return _tfs_addpid; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? TFS_AddDate
        {
            set { _tfs_adddate = value; }
            get { return _tfs_adddate; }
        }
        #endregion Model

    }
}