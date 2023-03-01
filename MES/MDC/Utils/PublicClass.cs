using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utils
{
    /// <summary>
    /// 公共函数
    /// </summary>
    public class PublicSub
    {
        /// <summary>
        /// 延时函数
        /// </summary>
        /// <param name="milliSecond">毫秒</param>
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)//毫秒
            {
                Application.DoEvents();//可执行某无聊的操作
            }
        }

        /// <summary>
        /// 计算字符串中子字符串出现的次数
        /// </summary>
        /// <param name="Str">原字符串</param>
        /// <param name="SubStr">子字符串</param>
        /// <returns>出现的次数</returns>
        public static int SubStringCount(string Str, string SubStr)
        {
            if (Str.Contains(SubStr))
            {
                string strReplaced = Str.Replace(SubStr, "");
                return (Str.Length - strReplaced.Length);
            }
            return 0;
        }

        /// <summary>
        /// 判断字符串中是否连续出现指定次数的子字符串
        /// </summary>
        /// <param name="Str">原字符串</param>
        /// <param name="SubStr">子字符串</param>
        /// <param name="ContinueCount">连续出现的次数</param>
        /// <returns>Boolean</returns>
        public static bool ContinueContains(string Str, string SubStr, int ContinueCount)
        {
            if (ContinueCount <= 0)
            {
                if (Str.Contains(SubStr))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            string s = "";
            for (int i = 1; i < ContinueCount; i++)
            {
                s += SubStr;
            }
            if (Str.Contains(s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string StringToUnicode(string s)
        {
            char[] charBuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charBuffers.Length; i++)
            {
                buffer = Encoding.Unicode.GetBytes(charBuffers[i].ToString());
                sb.Append(string.Format("{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 提示结果对象
    /// </summary>
    public class NoteResult
    {
        /// <summary>
        /// 提示信息状态
        /// </summary>
        public NoteState State { get; set; }
        /// <summary>
        /// 提示信息时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NoteResult()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="state">检测状态</param>
        /// <param name="code">编码</param>
        /// <param name="message">提示信息</param>
        public NoteResult(NoteState state)
        {
            State = state;
            Time = DateTime.Now;
            Code = "";
            Message = "";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="state">检测状态</param>
        /// <param name="code">编码</param>
        /// <param name="message">提示信息</param>
        public NoteResult(NoteState state, string code, string message)
        {
            State = state;
            Time = DateTime.Now;
            Code = code;
            Message = message;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="state">检测状态</param>
        /// <param name="time">时间</param>
        /// <param name="code">编码</param>
        /// <param name="message">提示信息</param>
        public NoteResult(NoteState state, DateTime time, string code, string message)
        {
            State = state;
            Time = time;
            Code = code;
            Message = message;
        }
    }

    /// <summary>
    /// 提示信息状态
    /// </summary>
    public enum NoteState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 正常
        /// </summary>
        OK = 2,
        /// <summary>
        /// 异常
        /// </summary>
        NG = 3,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 4
    }

    /// <summary>
    /// 登录状态
    /// </summary>
    public enum LoginState
    {
        服务器异常 = 0,
        账号不存在 = 1,
        密码错误 = 2,
        登录成功 = 3,
        账号无权限 = 4
    }

    /// <summary>
    /// 工序站点类别
    /// </summary>
    public enum SiteType
    {
        关键扫描点 = 0,
        过站扫描点 = 1,
        过站补扫点 = 2
    }

    /// <summary>
    /// 工序扫码类型
    /// </summary>
    public enum ScanType
    {
        /// <summary>
        /// LCD码
        /// </summary>
        LCD = 1,
        /// <summary>
        /// FPC码
        /// </summary>
        FPC = 2,
        /// <summary>
        /// TP码
        /// </summary>
        TP = 3,
        /// <summary>
        /// 背光码
        /// </summary>
        BL = 4,
        /// <summary>
        /// 成品码
        /// </summary>
        QR = 5,
        /// <summary>
        /// LCD码+FPC码
        /// </summary>
        LCD_and_FPC = 6,
        /// <summary>
        /// FPC码+TP码
        /// </summary>
        FPC_and_TP = 7,
        /// <summary>
        /// FPC码+背光码
        /// </summary>
        FPC_and_BL = 8,
        /// <summary>
        /// 背光码+成品码
        /// </summary>
        BL_and_QR = 9,
        /// <summary>
        /// FPC码+成品码
        /// </summary>
        FPC_and_QR = 10,
        /// <summary>
        /// LCD码或FPC码
        /// </summary>
        LCD_or_FPC = 11
    }

    /// <summary>
    /// 不良复判结果
    /// </summary>
    public enum ExceptionState
    {
        良品 = 0,

        待复判 = 1,

        复判重工 = 2,

        复判报废 = 3,

        复判良品 = 4,

        重工良品 = 5,

        重工报废 = 6
    }
    ///// <summary>
    ///// Cell车间玻璃发料状态
    ///// </summary>
    //public enum DeliveryState
    //{
    //    /// <summary>
    //    /// 还未发料
    //    /// </summary>
    //    未发料 = 0,
    //    /// <summary>
    //    /// 已发料，还未收料和投料
    //    /// </summary>
    //    已发料 = 1,
    //    /// <summary>
    //    /// 已确认收料，还未投料
    //    /// </summary>
    //    已收料 = 2,
    //    /// <summary>
    //    /// 已经投料到机台
    //    /// </summary>
    //    已投料 = 3
    //}

}
