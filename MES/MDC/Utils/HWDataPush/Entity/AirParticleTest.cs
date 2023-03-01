using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// AirParticleTest:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class AirParticleTest
    {
        public AirParticleTest()
        { }
        #region Model
        private string _id;
        private string _eventdate;
        private string _eventshift;
        private string _eventuser;
        private string _linenum;
        private string _stationname;
        private int _particleone;
        private int _particletwo;
        private DateTime? _eventtime;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EventDate
        {
            set { _eventdate = value; }
            get { return _eventdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EventShift
        {
            set { _eventshift = value; }
            get { return _eventshift; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EventUser
        {
            set { _eventuser = value; }
            get { return _eventuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LineNum
        {
            set { _linenum = value; }
            get { return _linenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StationName
        {
            set { _stationname = value; }
            get { return _stationname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParticleOne
        {
            set { _particleone = value; }
            get { return _particleone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParticleTwo
        {
            set { _particletwo = value; }
            get { return _particletwo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EventTime
        {
            set { _eventtime = value; }
            get { return _eventtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
