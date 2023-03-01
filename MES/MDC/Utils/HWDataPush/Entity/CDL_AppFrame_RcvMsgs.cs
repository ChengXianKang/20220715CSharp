using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// 非工序链机台数据
    /// </summary>
    [Serializable]
    public partial class CDL_AppFrame_RcvMsgs
    {
        public CDL_AppFrame_RcvMsgs()
        { }

        private DateTime _eventTime;
        private string _msgData;
        //private string _lineCode;
        //private string _deviceIP;
        //private string _lcdCode;
        //private string _state;
        //private string _testResult;
        //private DateTime _testTime;

        /// <summary>
        /// 数据接收时间
        /// </summary>
        public DateTime EventTime
        {
            get
            {
                return _eventTime;
            }
            set
            {
                _eventTime = value;
            }
        }

        /// <summary>
        /// 数据内容
        /// </summary>
        public string MsgData
        {
            get
            {
                return _msgData;
            }
            set
            {
                _msgData = value;

                ////{JT;FJ;5101;172.16.7.19;#;#;P630A1AJ31PS023JB1;1;NG_7;20190507201313; ;#;#;#;#}
                //string[] para = _msgData.Split(';');
                //_lineCode = para[2];
                //_deviceIP = para[3];
                //_lcdCode = para[6];
                //_state = para[7];
                //_testResult = para[8];
                //_testTime = Convert.ToDateTime(para[9]);
            }
        }

        ///// <summary>
        ///// 产线编码
        ///// </summary>
        //public string LineCode
        //{
        //    get
        //    {
        //        return _lineCode;
        //    }
        //    set
        //    {
        //        _lineCode = value;
        //    }
        //}

        ///// <summary>
        ///// 设备IP地址
        ///// </summary>
        //public string DeviceIP
        //{
        //    get
        //    {
        //        return _deviceIP;
        //    }
        //    set
        //    {
        //        _deviceIP = value;
        //    }
        //}
        ///// <summary>
        ///// LCD编码
        ///// </summary>
        //public string LCDCode
        //{
        //    get
        //    {
        //        return _lcdCode;
        //    }
        //    set
        //    {
        //        _lcdCode = value;
        //    }
        //}
        ///// <summary>
        ///// 测试状态
        ///// </summary>
        //public string State
        //{
        //    get
        //    {
        //        return _state;
        //    }
        //    set
        //    {
        //        _state = value;
        //    }
        //}
        ///// <summary>
        ///// 测试值
        ///// </summary>
        //public string TestResult
        //{
        //    get
        //    {
        //        return _testResult;
        //    }
        //    set
        //    {
        //        _testResult = value;
        //    }
        //}
        ///// <summary>
        ///// 测试时间
        ///// </summary>
        //public DateTime TestTime
        //{
        //    get
        //    {
        //        return _testTime;
        //    }
        //    set
        //    {
        //        _testTime = value;
        //    }
        //}
    }
}
