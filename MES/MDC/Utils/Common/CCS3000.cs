using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Utils.Common
{
    public class CCS3000
    {
        public const int RESULT_ACK = 0;     //通信成功
        public const int RESULT_NAK = -1;    //通信错误
        public const int RESULT_EMPTY = -2;  //内容为空
        public const int RESULT_ERROR = -3;// 通信异常

        /// <summary>
        /// 通用设置指令，功能：设置一些常规的命令
        /// </summary>
        /// <param name="cmd">设置的指令</param>
        /// <param name="sp">通讯的串口</param>
        /// <returns>设置的结果</returns>
        public static int SetCmd(string cmd, SerialPort sp)
        {
            try
            {
                if (sp != null && !sp.IsOpen) sp.Open();
                if (sp.BytesToRead > 0) sp.ReadExisting();
                sp.Write(Convert.ToChar(2) + cmd + Convert.ToChar(3));
                sp.ReadTimeout = 1500;
                Utils.PublicSub.Delay(100);
                byte[] ack = new byte[1];
                int tmp = sp.Read(ack, 0, ack.Length);

                //if (tmp > 0 && ack[0] == 0x06)
                //{
                //    return RESULT_ACK;
                //}
                //else
                //{
                //    LogHelper.Info(string.Format("发送数据到喷码机失败（返回{0}）：{1}", tmp, cmd));
                //    return RESULT_NAK;
                //}
                return RESULT_ACK;
            }
            catch (Exception exp)
            {
                LogHelper.Error(string.Format("发送数据到喷码机失败：{0}", cmd), exp);
                return RESULT_ERROR;
            }
        }

        public static bool RecvPrintedEndSignal(SerialPort port)
        {
            try
            {
                if (port.BytesToRead > 0)
                {
                    int b = port.ReadByte();
                    if (b == 0x02)
                    {
                        int c = port.ReadByte();
                        int d = port.ReadByte();
                        if (c == 0x31 && d == 0x03)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception exp)
            {
                LogHelper.Error("接收喷码完成信号失败", exp);
                return false;
            }
        }

        public static void ClosePort(ref SerialPort sp)
        {
            if (sp != null && sp.IsOpen)
            {
                sp.Close();
                sp.Dispose();
            }
        }
    }
}
