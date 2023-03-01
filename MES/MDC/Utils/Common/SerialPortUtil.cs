using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Diagnostics;

namespace Utils
{
    /// <summary>
    /// 串口通讯类
    /// </summary>
    public class SerialPortUtil
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern uint GetTickCount();

        /// <summary>  
        /// 完整协议的记录处理事件  
        /// </summary>  
        public event DataReceivedEventHandler DataReceived;
        public event DataWritedEventHandler DataWrited;
        public event SerialErrorReceivedEventHandler Error;

        #region 变量属性
        private bool _receiveeventenabled = true;   // 接收事件是否启用，默认true
        private byte[] _endbytes = new byte[] { 0x45, 0x4e, 0x44 };   // 结束符byte数组
        private uint _readtimeout = 20;  // 读取超时时间
        private uint _writetimeout = 5000; // 写入超时时间

        private string _portName = "COM1";//串口号，默认COM1  
        private SerialPortBaudRates _baudRate = SerialPortBaudRates.BaudRate_9600;//波特率  
        private Parity _parity = Parity.None;//校验位  
        private StopBits _stopBits = StopBits.One;//停止位  
        private SerialPortDatabits _dataBits = SerialPortDatabits.EightBits;//数据位  

        private bool _dtrEnable = true;
        private bool _rtsEnable = false;

        private SerialPort comPort = new SerialPort();

        /// <summary>  
        /// 接收事件是否有效 true表示有效  
        /// </summary>  
        public bool ReceiveEventEnabled
        {
            get { return _receiveeventenabled; }
            set { _receiveeventenabled = value; }
        }

        /// <summary>  
        /// 结束符byte数组  
        /// </summary>  
        public byte[] EndBytes
        {
            get { return _endbytes; }
            set { _endbytes = value; }
        }

        /// <summary>
        /// 结束符字符串
        /// </summary>
        public string EndString
        {
            get
            {
                return System.Text.Encoding.ASCII.GetString(_endbytes);
            }
            set
            {
                _endbytes = System.Text.Encoding.ASCII.GetBytes(value);
            }
        }
        /// <summary>  
        /// 串口号  
        /// </summary>  
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        /// <summary>  
        /// 波特率  
        /// </summary>  
        public SerialPortBaudRates BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        /// <summary>  
        /// 奇偶校验位  
        /// </summary>  
        public Parity Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        /// <summary>  
        /// 数据位  
        /// </summary>  
        public SerialPortDatabits DataBits
        {
            get { return _dataBits; }
            set { _dataBits = value; }
        }

        /// <summary>  
        /// 停止位  
        /// </summary>  
        public StopBits StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        #region 读取超出时间
        /// <summary>
        /// 设置读取最大时间
        /// </summary>
        /// <returns></returns>
        public uint ReadTimeout
        {
            get { return _readtimeout; }
            set { _readtimeout = value; }
        }
        #endregion

        #region 写入超出时间
        /// <summary>
        /// 设置写入最大时间
        /// </summary>
        /// <returns></returns>
        public uint WriteTimeout
        {
            get { return _writetimeout; }
            set { _writetimeout = value; }
        }
        #endregion

        /// <summary>  
        /// 是否启用数据终端就绪（DTR）信号  
        /// </summary>  
        public bool DtrEnable
        {
            get { return _dtrEnable; }
            set { _dtrEnable = value; }
        }
        /// <summary>
        /// 是否启用请求发送（RTS）信号
        /// </summary>
        public bool RtsEnable
        {
            get { return _rtsEnable; }
            set { _rtsEnable = value; }
        }


        #endregion

        #region 构造函数

        /// <summary>  
        /// 参数构造函数（使用枚举参数构造）  
        /// </summary>  
        /// <param name="baud">波特率</param>  
        /// <param name="par">奇偶校验位</param>  
        /// <param name="sBits">停止位</param>  
        /// <param name="dBits">数据位</param>  
        /// <param name="name">串口号</param>  
        public SerialPortUtil(string name, SerialPortBaudRates baud, Parity par, SerialPortDatabits dBits, StopBits sBits)
        {
            try
            {
                _portName = name;
                _baudRate = baud;
                _parity = par;
                _dataBits = dBits;
                _stopBits = sBits;

                comPort.ReceivedBytesThreshold = 1;
                comPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
                comPort.ErrorReceived += new SerialErrorReceivedEventHandler(ComPort_ErrorReceived);

                //comPort.ReceivedBytesThreshold = 1024;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 参数构造函数（使用字符串参数构造）  
        /// </summary>  
        /// <param name="baud">波特率</param>  
        /// <param name="par">奇偶校验位</param>  
        /// <param name="sBits">停止位</param>  
        /// <param name="dBits">数据位</param>  
        /// <param name="name">串口号</param>  
        public SerialPortUtil(string name, string baud, string par, string dBits, string sBits)
        {
            try
            {
                _portName = name;
                _baudRate = (SerialPortBaudRates)Enum.Parse(typeof(SerialPortBaudRates), baud);
                _parity = (Parity)Enum.Parse(typeof(Parity), par);
                _dataBits = (SerialPortDatabits)Enum.Parse(typeof(SerialPortDatabits), dBits);
                _stopBits = (StopBits)Enum.Parse(typeof(StopBits), sBits);

                comPort.ReceivedBytesThreshold = 1;
                comPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
                comPort.ErrorReceived += new SerialErrorReceivedEventHandler(ComPort_ErrorReceived);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 构造函数（仅指定串口名，其他默认9600，8-n-1
        /// </summary>
        public SerialPortUtil(string name)
        {
            try
            {
                _portName = name;
                _baudRate = SerialPortBaudRates.BaudRate_9600;
                _parity = Parity.None;
                _dataBits = SerialPortDatabits.EightBits;
                _stopBits = StopBits.One;

                comPort.ReceivedBytesThreshold = 1;
                comPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
                comPort.ErrorReceived += new SerialErrorReceivedEventHandler(ComPort_ErrorReceived);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 默认构造函数  
        /// </summary>  
        public SerialPortUtil()
        {
            try
            {
                _portName = "COM1";
                _baudRate = SerialPortBaudRates.BaudRate_9600;
                _parity = Parity.None;
                _dataBits = SerialPortDatabits.EightBits;
                _stopBits = StopBits.One;

                comPort.ReceivedBytesThreshold = 1;

                comPort.DataReceived += new SerialDataReceivedEventHandler(ComPort_DataReceived);
                comPort.ErrorReceived += new SerialErrorReceivedEventHandler(ComPort_ErrorReceived);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        #endregion

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        public void Dispose()
        {
            comPort.Dispose();
        }

        /// <summary>  
        /// 端口是否已经打开  
        /// </summary>  
        public bool IsOpen
        {
            get
            {
                return comPort.IsOpen;
            }
        }

        /// <summary>  
        /// 打开端口  
        /// </summary>  
        /// <returns></returns>  
        public void OpenPort()
        {
            try
            {
                if (comPort.IsOpen) comPort.Close();

                comPort.PortName = _portName;
                comPort.BaudRate = (int)_baudRate;
                comPort.Parity = _parity;
                comPort.DataBits = (int)_dataBits;
                comPort.StopBits = _stopBits;

                comPort.DtrEnable = _dtrEnable;
                comPort.RtsEnable = _rtsEnable;

                comPort.Open();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                throw ex;
            }
        }

        /// <summary>  
        /// 关闭端口  
        /// </summary>  
        public void ClosePort()
        {
            try
            {
                if (comPort.IsOpen) comPort.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 丢弃来自串行驱动程序的接收和发送缓冲区的数据  
        /// </summary>  
        public void DiscardBuffer()
        {
            if (comPort != null && comPort.IsOpen)
            {
                comPort.DiscardInBuffer();
                comPort.DiscardOutBuffer();
            }
        }

        ///// <summary>  
        ///// 数据接收处理  
        ///// </summary>  
        //private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    Debug.Print("结束符：" + EndString);
        //    //禁止接收事件时直接退出  
        //    if (!ReceiveEventEnabled) return;

        //    try
        //    {
        //        #region 根据结束字节来判断是否全部获取完成
        //        // 数据暂存List
        //        List<byte> _byteData = new List<byte>();
        //        bool found = false;//是否检测到结束符号  
        //        bool istimeout = false;  // 读取超时
        //        uint start = GetTickCount();  // 记录开始时间
        //        while (!found && !istimeout)
        //        {
        //            if (comPort.BytesToRead > 0)
        //            {
        //                byte[] readbyte = new byte[1];
        //                int count = comPort.Read(readbyte, 0, 1);
        //                // 将数据添加至暂存List
        //                _byteData.Add(readbyte[0]);


        //                //byte[] readBuffer = new byte[comPort.BytesToRead];
        //                //int count = comPort.Read(readBuffer, 0, readBuffer.Length);
        //                //for (int i = 0; i < count; i++)
        //                //{
        //                //    // 将数据逐项添加至暂存List
        //                //    _byteData.Add(readBuffer[i]);
        //                //}//for (int i = 0; i < count; i++)
        //                // 重新记录开始时间
        //                start = GetTickCount();

        //                // 识别到结束符数组的最后一个字节
        //                if (_byteData[_byteData.Count - 1] == _endbytes[_endbytes.Length - 1] && _byteData.Count >= _endbytes.Length)
        //                {
        //                    found = true;
        //                    int j = 2;
        //                    // 判断数据末几位是否与结束符数组完全匹配
        //                    while (j <= _endbytes.Length)
        //                    {
        //                        if (_byteData[_byteData.Count - j] != _endbytes[_endbytes.Length - j])
        //                        {
        //                            found = false;
        //                            break;
        //                        }
        //                        j++;
        //                    }//while (j <= _endbytes.Length)
        //                }
        //            }//if(comPort.BytesToRead > 0)
        //            else
        //            {
        //                if (GetTickCount() - start > _readtimeout)
        //                {
        //                    istimeout = true;
        //                }
        //            }//if(comPort.BytesToRead > 0)
        //        }//while (!found && !istimeout)

        //        #endregion

        //        // 数组转换
        //        byte[] bytesreceived = _byteData.ToArray();

        //        //触发整条记录的处理  
        //        if (DataReceived != null)
        //        {
        //            DataReceived(new DataReceivedEventArgs(bytesreceived));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex.Message, ex);
        //    }
        //}

        /// <summary>  
        /// 数据接收处理  
        /// </summary>  
        private void ComPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //禁止接收事件时直接退出  
            if (!ReceiveEventEnabled) return;
            
            try
            {
                #region 根据两条数据的间隔时间是否超时，判断是否全部获取完成
                // 数据暂存List
                List<byte> _byteData = new List<byte>();
                bool istimeout = false;  // 读取超时
                uint start = GetTickCount();  // 记录开始时间
                while (!istimeout)
                {
                    if (comPort.BytesToRead > 0)
                    {
                        byte[] readBuffer = new byte[comPort.BytesToRead];
                        int count = comPort.Read(readBuffer, 0, readBuffer.Length);
                        for (int i = 0; i < count; i++)
                        {
                            // 将数据逐项添加至暂存List
                            _byteData.Add(readBuffer[i]);
                        }//for (int i = 0; i < count; i++)
                        // 重新记录开始时间
                        start = GetTickCount();
                    }//if(comPort.BytesToRead > 0)
                    else
                    {
                        if (GetTickCount() - start > _readtimeout)
                        {
                            istimeout = true;
                        }
                        Application.DoEvents();
                    }//if(comPort.BytesToRead > 0)
                }//while (!found && !istimeout)

                #endregion

                // 数组转换
                byte[] bytesreceived = _byteData.ToArray();

                SerialPort com = sender as SerialPort;
                string name = com.PortName;

                //触发整条记录的处理  
                if (DataReceived != null)
                {
                    DataReceived.Invoke(new DataReceivedEventArgs(name, bytesreceived));
                }
                //DataReceived?.Invoke(new DataReceivedEventArgs(bytesreceived));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 错误处理函数  
        /// </summary>  
        void ComPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (Error != null)
            {
                Error.Invoke(sender, e);
            }
        }

        #region 数据写入操作

        /// <summary>  
        /// 写入数据  
        /// </summary>  
        /// <param name="msg"></param>  
        public void WriteData(string msg)
        {
            try
            {
                if (!(comPort.IsOpen)) comPort.Open();

                comPort.Write(msg);

                // 打印调试信息
                Debug.Print("\r\n发送:" + msg);

                //触发数据写入事件
                if (DataWrited != null)
                {
                    DataWrited.Invoke(new DataWritedEventArgs(msg));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 写入数据  
        /// </summary>  
        /// <param name="msg">写入端口的字节数组</param>  
        public void WriteData(byte[] msg)
        {
            try
            {
                if (!(comPort.IsOpen)) comPort.Open();

                comPort.Write(msg, 0, msg.Length);

                // 打印调试信息
                Debug.Print("\r\n发送:" + Encoding.ASCII.GetString(msg));

                //触发数据写入事件
                if (DataWrited != null)
                {
                    DataWrited.Invoke(new DataWritedEventArgs(msg));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 写入数据  
        /// </summary>  
        /// <param name="msg">包含要写入端口的字节数组</param>  
        /// <param name="offset">参数从0字节开始的字节偏移量</param>  
        /// <param name="count">要写入的字节数</param>  
        public void WriteData(byte[] msg, int offset, int count)
        {
            try
            {
                if (!(comPort.IsOpen)) comPort.Open();

                comPort.Write(msg, offset, count);

                //触发数据写入事件
                if (DataWrited != null)
                {
                    DataWrited.Invoke(new DataWritedEventArgs(msg));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 发送串口命令  
        /// </summary>  
        /// <param name="SendData">发送数据</param>  
        /// <param name="ReceiveData">接收数据</param>  
        /// <param name="Overtime">通讯超时时间（ms）</param>  
        /// <returns></returns>  
        public int SendCommand(byte[] SendData, ref byte[] ReceiveData, int Overtime)
        {
            try
            {
                if (!(comPort.IsOpen)) comPort.Open();

                ReceiveEventEnabled = false;        //关闭接收事件  
                comPort.DiscardInBuffer();      //清空接收缓冲区                   
                comPort.Write(SendData, 0, SendData.Length);

                // 打印调试信息
                Debug.Print("\r\n发送:" + Encoding.ASCII.GetString(SendData));

                ////触发数据写入事件
                //if (DataWrited != null)
                //{
                //    DataWrited(new DataWritedEventArgs(SendData));
                //}

                int num = 0, ret = 0;
                while (num++ < Overtime)
                {
                    if (comPort.BytesToRead >= ReceiveData.Length) break;
                    System.Threading.Thread.Sleep(1);
                }

                if (comPort.BytesToRead >= ReceiveData.Length)
                {
                    ret = comPort.Read(ReceiveData, 0, ReceiveData.Length);

                    //触发数据接收事件  
                    if (DataWrited != null)
                    {
                        DataWrited.Invoke(new DataWritedEventArgs(ReceiveData));
                    }
                }
                else
                {
                    ret = comPort.Read(ReceiveData, 0, comPort.BytesToRead);
                }

                // 打印调试信息
                Debug.Print("\r\n接收:" + Encoding.Default.GetString(ReceiveData));

                ReceiveEventEnabled = true;       //打开事件  
                return ret;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                return -1;
            }
        }

        #endregion

        #region 常用的列表数据获取和绑定操作

        /// <summary>  
        /// 封装获取串口号列表  
        /// </summary>  
        /// <returns></returns>  
        public static string[] GetPortNames()
        {
            try
            {
                return SerialPort.GetPortNames();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
                return new string[0];
            }
        }

        /// <summary>  
        /// 设置串口号  
        /// </summary>  
        /// <param name="obj"></param>  
        public static void SetPortNameValues(ComboBox obj)
        {
            try
            {
                obj.Items.Clear();
                foreach (string str in SerialPort.GetPortNames())
                {
                    obj.Items.Add(str);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 设置波特率  
        /// </summary>  
        public static void SetBauRateValues(ComboBox obj)
        {
            try
            {
                obj.Items.Clear();
                foreach (SerialPortBaudRates rate in Enum.GetValues(typeof(SerialPortBaudRates)))
                {
                    obj.Items.Add(((int)rate).ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 设置数据位  
        /// </summary>  
        public static void SetDataBitsValues(ComboBox obj)
        {
            try
            {
                obj.Items.Clear();
                foreach (SerialPortDatabits databit in Enum.GetValues(typeof(SerialPortDatabits)))
                {
                    obj.Items.Add(((int)databit).ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 设置校验位列表  
        /// </summary>  
        public static void SetParityValues(ComboBox obj)
        {
            try
            {
                obj.Items.Clear();
                foreach (string str in Enum.GetNames(typeof(Parity)))
                {
                    obj.Items.Add(str);
                }
                //foreach (Parity party in Enum.GetValues(typeof(Parity)))  
                //{  
                //    obj.Items.Add(((int)party).ToString());  
                //}  
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        /// <summary>  
        /// 设置停止位  
        /// </summary>  
        public static void SetStopBitValues(ComboBox obj)
        {
            try
            {
                obj.Items.Clear();
                foreach (string str in Enum.GetNames(typeof(StopBits)))
                {
                    obj.Items.Add(str);
                }
                //foreach (StopBits stopbit in Enum.GetValues(typeof(StopBits)))  
                //{  
                //    obj.Items.Add(((int)stopbit).ToString());  
                //}     
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message, ex);
            }
        }

        #endregion

        #region 格式转换
        /// <summary>  
        /// 转换十六进制字符串到字节数组  
        /// </summary>  
        /// <param name="msg">待转换字符串</param>  
        /// <returns>字节数组</returns>  
        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");//移除空格  

            //create a byte array the length of the  
            //divided by 2 (Hex is 2 characters in length)  
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                //convert each set of 2 characters to a byte and add to the array  
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }

            return comBuffer;
        }

        /// <summary>  
        /// 转换字节数组到十六进制字符串  
        /// </summary>  
        /// <param name="comByte">待转换字节数组</param>  
        /// <returns>十六进制字符串</returns>  
        public static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 2);
            foreach (byte data in comByte)
            {
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
                //builder.Append(Convert.ToString(data, 16).PadLeft(2, '0'));
            }

            return builder.ToString().ToUpper();
        }
        #endregion

        /// <summary>  
        /// 检查端口名称是否存在  
        /// </summary>  
        /// <param name="port_name"></param>  
        /// <returns></returns>  
        public static bool Exists(string port_name)
        {
            foreach (string port in SerialPort.GetPortNames()) if (port == port_name) return true;
            return false;
        }

        /// <summary>  
        /// 格式化端口相关属性  
        /// </summary>  
        /// <param name="port"></param>  
        /// <returns></returns>  
        public static string Format(SerialPort port)
        {
            return String.Format("{0} ({1},{2},{3},{4},{5})",
                port.PortName, port.BaudRate, port.DataBits, port.StopBits, port.Parity, port.Handshake);
        }
    }
}
