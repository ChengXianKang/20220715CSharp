using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    /// <summary>
    /// 数据接收事件类
    /// </summary>
    public class DataReceivedEventArgs : EventArgs
    {
        //public string AsciiStringReceived;
        public string HexStringReceived;
        public byte[] BytesReceived;
        public string PortName;
        public DataReceivedEventArgs(byte[] m_BytesReceived)
        {
            this.BytesReceived = m_BytesReceived;
            ////ASCII字符转换  
            ////AsciiStringReceived = System.Text.Encoding.ASCII.GetString(m_BytesReceived);
            //AsciiStringReceived = System.Text.Encoding.Default.GetString(m_BytesReceived);
            // 需要转换成16进制字符串
            HexStringReceived = SerialPortUtil.ByteToHex(m_BytesReceived);
        }

        public DataReceivedEventArgs(string m_portName, byte[] m_BytesReceived)
        {
            this.BytesReceived = m_BytesReceived;
            ////ASCII字符转换  
            ////AsciiStringReceived = System.Text.Encoding.ASCII.GetString(m_BytesReceived);
            //AsciiStringReceived = System.Text.Encoding.Default.GetString(m_BytesReceived);
            // 需要转换成16进制字符串
            this.HexStringReceived = SerialPortUtil.ByteToHex(m_BytesReceived);

            this.PortName = m_portName;
        }
    }
    /// <summary>
    /// 引发数据接收事件
    /// </summary>
    /// <param name="e"></param>
    public delegate void DataReceivedEventHandler(DataReceivedEventArgs e);


    /// <summary>
    /// 数据写入事件类
    /// </summary>
    public class DataWritedEventArgs : EventArgs
    {
        public string StringWrited;
        public byte[] BytesWrited;
        public DataWritedEventArgs(byte[] m_BytesWrited)
        {
            this.BytesWrited = m_BytesWrited;
            this.StringWrited = SerialPortUtil.ByteToHex(m_BytesWrited);
        }

        public DataWritedEventArgs(string m_StringWrited)
        {
            this.StringWrited = m_StringWrited;
            this.BytesWrited = SerialPortUtil.HexToByte(m_StringWrited);
        }
    }
    /// <summary>
    /// 引发数据写入事件
    /// </summary>
    /// <param name="e"></param>
    public delegate void DataWritedEventHandler(DataWritedEventArgs e);


    /// <summary>  
    /// 串口数据位列表（5,6,7,8）  
    /// </summary>  
    public enum SerialPortDatabits : int
    {
        FiveBits = 5,
        SixBits = 6,
        SeventBits = 7,
        EightBits = 8
    }

    /// <summary>  
    /// 串口波特率列表。  
    /// 75,110,150,300,600,1200,2400,4800,9600,14400,19200,28800,38400,56000,57600,  
    /// 115200,128000,230400,256000  
    /// </summary>  
    public enum SerialPortBaudRates : int
    {
        BaudRate_75 = 75,
        BaudRate_110 = 110,
        BaudRate_150 = 150,
        BaudRate_300 = 300,
        BaudRate_600 = 600,
        BaudRate_1200 = 1200,
        BaudRate_2400 = 2400,
        BaudRate_4800 = 4800,
        BaudRate_9600 = 9600,
        BaudRate_14400 = 14400,
        BaudRate_19200 = 19200,
        BaudRate_28800 = 28800,
        BaudRate_38400 = 38400,
        BaudRate_56000 = 56000,
        BaudRate_57600 = 57600,
        BaudRate_115200 = 115200,
        BaudRate_128000 = 128000,
        BaudRate_230400 = 230400,
        BaudRate_256000 = 256000
    }
}
