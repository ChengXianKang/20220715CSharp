using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    /// <summary>
    /// 华为发送数据的处理结果
    /// </summary>
    public class HuaweiPushResult
    {
        /// <summary>
        /// 0 处理成功 1 发送数据时出错 2 归档已发送数据出错
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 结果消息，如果出错则包含错误信息
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// 成功发送的条数
        /// </summary>
        public int SuccessedCount { get; set; }
        /// <summary>
        /// 总共进行了发送的次数
        /// </summary>
        public int SentTimes { get; set; }
    }
}
