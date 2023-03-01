using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.HWDataPush.Entity
{
    public class PushResult
    {
        /// <summary>
        /// 回传错误具体信息
        /// </summary>
        public string errorMsg { get; set; }
        /// <summary>
        /// 回传成功信息
        /// </summary>
        public string successMsg { get; set; }
        /// <summary>
        /// 回传状态（true表示成功，false表示失败）
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public long date { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public string year { get; set; }
        /// <summary>
        /// showLast1Year
        /// </summary>
        public bool showLast1Year { get; set; }
    }
}
