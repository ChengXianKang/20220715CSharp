using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Utils
{
    /// <summary>
    /// KEYENCE扫码枪
    /// </summary>
    public class ReaderAccessor
    {
        #region 私有成员
        private Socket _LocationClientSocket;           //本地侦听服务  
        private string _IpAddress;                      //扫码枪IP地址  
        private int _Port = 9004;                       //扫码枪端口  
        private int _RecvMax = 1024;                    //接收缓冲区大小   
        private Thread ClientThread;                    //客户端侦听线程
        private bool IsStop = false;
        private ManualResetEvent connectDone = new ManualResetEvent(false);
        //private static ManualResetEvent sendDone = new ManualResetEvent(false);
        //private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        //// The response from the remote device.
        //private string response = string.Empty;
        #endregion 私有成员

        #region 事件
        /// <summary>
        /// 客户端已连接事件
        /// TCPEventArgs = new TCPEventArgs()
        /// </summary>
        public event EventHandler<TCPEventArgs> ReaderConnected;

        /// <summary>
        /// 客户端断开事件
        /// TCPEventArgs = new TCPEventArgs()
        /// </summary>
        public event EventHandler<TCPEventArgs> ReaderClosed;

        /// <summary>
        /// 接收到服务端数据事件
        /// TCPEventArgs = new TCPEventArgs(byte[] Data, int Len)
        /// </summary>
        public event EventHandler<TCPEventArgs> DataArrived;

        /// <summary>
        /// 异常事件
        /// TCPEventArgs = new TCPEventArgs(Exception ex)
        /// </summary>
        public event EventHandler<TCPEventArgs> Exception;

        /// <summary>
        /// 套接字异常事件
        /// TCPEventArgs = new TCPEventArgs(SocketException sex)
        /// </summary>
        public event EventHandler<TCPEventArgs> SocketException;

        #endregion

        #region 属性
        /// <summary>
        /// 是否已经连接
        /// </summary>
        public bool Connected
        {
            get
            {
                return _LocationClientSocket.Connected;
            }
        }

        /// <summary>
        /// 扫码枪IP
        /// </summary>
        public string IpAddress
        {
            get
            {
                return _IpAddress;
            }
            set
            {
                _IpAddress = value;
            }
        }

        /// <summary>
        /// 扫码枪端口
        /// </summary>
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }
        #endregion 属性

        #region 构造函数
        /// <summary>
        /// 实例化ReaderAccessor
        /// </summary>
        public ReaderAccessor()
        {

        }

        /// <summary>
        /// 实例化ReaderAccessor
        /// </summary>
        /// <param name="RemoteIP">需要连接服务的IP地址</param>
        public ReaderAccessor(string IpAddr)
        {
            _IpAddress = IpAddr;
        }
        /// <summary>
        /// 实例化ReaderAccessor
        /// </summary>
        /// <param name="IpAddr">需要连接服务的IP地址</param>
        /// <param name="Port">扫码枪端口</param>
        public ReaderAccessor(string IpAddr, int Port)
        {
            _IpAddress = IpAddr;
            _Port = Port;
        }
        #endregion 构造函数

        #region 方法

        /// <summary>
        /// 异步连接扫码枪
        /// </summary>
        public bool Connect()
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(_IpAddress), _Port);
                //建立客户端套接字  
                _LocationClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //Connect to the remote endpoint.
                _LocationClientSocket.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _LocationClientSocket);
                //Wait for connect.
                //connectDone.WaitOne();
                connectDone.WaitOne(1000, true);

                if (_LocationClientSocket.Connected)
                {
                    _LocationClientSocket.SendBufferSize = _RecvMax;
                    _LocationClientSocket.ReceiveBufferSize = _RecvMax;
                    //IPEndPoint clientInfoT;
                    //clientInfoT = (IPEndPoint)_LocationClientSocket.LocalEndPoint;

                    IsStop = false;
                    if (ReaderConnected != null)
                    {
                        ReaderConnected(this, new TCPEventArgs());
                    }
                    //autoEvent.Set();
                    Thread ClientThread = new Thread(new ThreadStart(ClientListen));
                    ClientThread.IsBackground = true;
                    ClientThread.Name = "客户端侦听线程，循环接收数据";
                    ClientThread.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SocketException sex)
            {
                if (SocketException != null)
                {
                    SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
                }
                return false;
            }
            catch (Exception ex)
            {
                if (Exception != null)
                {
                    Exception(this, new TCPEventArgs(ex));
                }
                return false;
                //throw;
            }

        }

        /// <summary>
        /// 异步连接回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                //Signal that the connection has been made.
                //connectDone.Set();
                //Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;
                //Complete the connection.
                client.EndConnect(ar);
                //Signal that the connection has been made.
                connectDone.Set();
            }
            catch (SocketException sex)
            {
                connectDone.Set();
                if (SocketException != null)
                {
                    SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
                }
            }
            catch (Exception ex)
            {
                connectDone.Set();
                if (Exception != null)
                {
                    Exception(this, new TCPEventArgs(ex));
                }
                //throw;
            }
        }

        /// <summary>
        /// 客户端侦听程序
        /// </summary>
        private void ClientListen()
        {
            byte[] tmpByt = new byte[512];
            byte[] recData;
            int R;

            while (!IsStop)
            {
                try
                {
                    if (_LocationClientSocket.Poll(50, SelectMode.SelectWrite))
                    {
                        R = _LocationClientSocket.Receive(tmpByt);
                        if (R > 0)
                        {
                            recData = new byte[R];
                            Array.Copy(tmpByt, recData, R);
                            if (DataArrived != null)
                            {
                                DataArrived(this, new TCPEventArgs(recData));
                            }
                        }
                        else
                        {
                            if (_LocationClientSocket != null)
                            {
                                _LocationClientSocket.Close();
                                _LocationClientSocket = null;
                                IsStop = true;
                                if (ReaderClosed != null)
                                {
                                    ReaderClosed(this, new TCPEventArgs());
                                }
                            }
                        }
                    }
                }
                catch (SocketException sex)
                {
                    if (sex.ErrorCode == 10054)
                    {
                        if (_LocationClientSocket != null)
                        {
                            _LocationClientSocket.Close();
                            _LocationClientSocket = null;
                            IsStop = true;
                            if (ReaderClosed != null)
                            {
                                ReaderClosed(this, new TCPEventArgs());
                            }
                        }
                    }
                    else
                    {
                        if (SocketException != null)
                        {
                            SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (Exception != null)
                    {
                        Exception(this, new TCPEventArgs(ex));
                    }
                }
            }
        }

        /// <summary>
        /// 关闭扫码枪连接
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (_LocationClientSocket == null)
                {
                    return;
                }
                IsStop = true;
                if (ClientThread != null)
                {
                    Thread.Sleep(5);
                    ClientThread.Abort();
                }
                _LocationClientSocket.Close();
                _LocationClientSocket = null;
                ClientThread = null;
                if (ReaderClosed != null)
                {
                    ReaderClosed(this, new TCPEventArgs());
                }
            }
            catch (SocketException sex)
            {
                if (SocketException != null)
                {
                    SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
                }
            }
            catch (Exception ex)
            {
                if (Exception != null)
                {
                    Exception(this, new TCPEventArgs(ex));
                }
            }
        }

        /// <summary>
        /// 执行指令
        /// </summary>
        /// <param name="command">指令字符串</param>
        /// <returns></returns>
        public bool ExecCommand(string command)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(command + "\r");
                _LocationClientSocket.Send(data);
                return true;
            }
            catch (SocketException sex)
            {
                if (SocketException != null)
                {
                    SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
                }
                return false;
            }
            catch (Exception ex)
            {
                if (Exception != null)
                {
                    Exception(this, new TCPEventArgs(ex));
                }
                return false;
            }
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="command">指令字符串</param>
        /// <returns></returns>
        public bool SendCommand(string command)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(command);
                _LocationClientSocket.Send(data);
                return true;
            }
            catch (SocketException sex)
            {
                if (SocketException != null)
                {
                    SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
                }
                return false;
            }
            catch (Exception ex)
            {
                if (Exception != null)
                {
                    Exception(this, new TCPEventArgs(ex));
                }
                return false;
            }
        }
        ///// <summary>
        ///// 发送消息给服务端
        ///// </summary>
        ///// <param name="Data">发送的数据，二进制数组</param>
        ///// <returns></returns>
        //public bool SendData(byte[] Data)
        //{
        //    try
        //    {
        //        _LocationClientSocket.Send(Data);
        //        return true;
        //    }
        //    catch (SocketException sex)
        //    {
        //        SocketException(this, new TCPEventArgs(sex)); //触发套接字异常事件
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception(this, new TCPEventArgs(ex));
        //        return false;
        //    }
        //}
        #endregion 方法
    }

    /// <summary>
    ///  用于保存TCP通讯事件信息的类
    /// </summary>
    public class TCPEventArgs : EventArgs
    {
        //私有成员
        private string ip;
        private string port;
        private byte[] databytes;
        private string asciistring;
        private string hexstring;
        private int len;
        private Exception ex;
        private SocketException sex;

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        /// <summary>
        /// 端口号
        /// </summary>
        public string Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Len
        {
            get
            {
                return len;
            }

            set
            {
                len = value;
            }
        }
        /// <summary>
        /// 数据(二进制数组)
        /// </summary>
        public byte[] DataBytes
        {
            get
            {
                return databytes;
            }

            set
            {
                databytes = value;
            }
        }
        /// <summary>
        /// 数据(ASCII字符串)
        /// </summary>
        public string AsciiString
        {
            get
            {
                return asciistring;
            }

            set
            {
                asciistring = value;
            }
        }
        /// <summary>
        /// 数据(十六进制字符串)
        /// </summary>
        public string HexString
        {
            get
            {
                return hexstring;
            }

            set
            {
                hexstring = value;
            }
        }
        /// <summary>
        /// 程序异常
        /// </summary>
        public Exception Exception
        {
            get
            {
                return ex;
            }

            set
            {
                ex = value;
            }
        }

        /// <summary>
        /// 套接字异常
        /// </summary>
        public SocketException SocketException
        {
            get
            {
                return sex;
            }

            set
            {
                sex = value;
            }
        }

        #region 构造函数
        /// <summary>
        /// 实例化TCPEventArgs
        /// </summary>
        public TCPEventArgs()
        {
        }
        /// <summary>
        /// 实例化TCPEventArgs
        /// </summary>
        /// <param name="Exception">程序异常</param>
        public TCPEventArgs(Exception Exception)
        {
            ex = Exception;
        }
        /// <summary>
        /// 实例化TCPEventArgs
        /// </summary>
        /// <param name="SocketException">套接字异常</param>
        public TCPEventArgs(SocketException SocketException)
        {
            sex = SocketException;
        }
        /// <summary>
        /// 实例化TCPEventArgs
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <param name="Port">端口号</param>
        public TCPEventArgs(string IP, string Port)
        {
            ip = IP;
            port = Port;
        }

        /// <summary>
        /// 实例化TCPEventArgs
        /// </summary>
        /// <param name="DataBytes">接收的二进制数组数据</param>
        public TCPEventArgs(byte[] DataBytes)
        {
            databytes = DataBytes;
            len = DataBytes.Length;
            asciistring = Encoding.ASCII.GetString(DataBytes);
            hexstring = byteToHexStr(DataBytes);
        }

        /// <summary>
        /// 实例化TCPEventArgs
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <param name="Port">端口号</param>
        /// <param name="DataBytes">接收的二进制数组数据</param>
        /// <param name="AsciiString">接收的ASCII字符串数据</param>

        public TCPEventArgs(string IP, string Port, byte[] DataBytes)
        {
            ip = IP;
            port = Port;
            databytes = DataBytes;
            len = DataBytes.Length;
            asciistring = Encoding.ASCII.GetString(DataBytes);
            hexstring = byteToHexStr(DataBytes);
        }
        #endregion

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
    }//public class TCPEventArgs : EventArgs
}
