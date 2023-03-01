using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public class Client
    {
        public string ipString;   // 服务器端ip
        public int port = 17888;                // 服务器端口
        public Socket socket;
        public Print print;                     // 运行时的信息输出方法
        public bool connected = false;          // 标识当前是否连接到服务器
        public string localIpPort = "";         // 记录本地ip端口信息
        public string localIp = "";             // 记录本地ip
        public Client(Print _print = null, string _ipString = null, int _port = -1)
        {
            this.print = _print;
            if (_ipString != null)
            {
                this.ipString = _ipString;
            }
            if (_port >= 0)
            {
                this.port = _port;
            }
        }
        public Client(Print _print = null, string _ipString = null, string _port = "-1")
        {
            this.print = _print;
            if (_ipString != null)
            {
                this.ipString = _ipString;
            }
            if (Int32.Parse(_port) >= 0)
            {
                this.port = Int32.Parse(_port);
            }
        }

        /// <summary>
        /// Print用于输出Server的输出信息
        /// </summary>
        public delegate void Print(string info);

        /// <summary>
        /// 启动客户端，连接至服务器
        /// </summary>
        public void start()
        {
            //设定服务器IP地址  
            IPAddress ip = IPAddress.Parse(ipString);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(new IPEndPoint(ip, port));   // 连接服务器
                if (print != null)
                {
                    print("连接服务器【" + socket.RemoteEndPoint.ToString() + "】成功"); // 连接成功
                    LogHelper.Info("Client.start().连接服务器【" + socket.RemoteEndPoint.ToString() + "】成功");
                }
                localIpPort = socket.LocalEndPoint.ToString();
                localIp = ((System.Net.IPEndPoint)socket.LocalEndPoint).Address.ToString();
                connected = true;
                Thread thread = new Thread(receiveData);
                thread.Start(socket);      // 在新的线程中接收服务器信息
            }
            catch (Exception ex)
            {
                if (print != null) print("连接服务器失败   " + ex.Message.ToString()); // 连接失败
                LogHelper.Error("连接接收服务器失败", ex);
                connected = false;
            }
        }

        /// <summary>
        /// 结束客户端
        /// </summary>
        public void stop()
        {
            try
            {
                connected = false;
                if (print != null)
                {
                    print("与服务器【" + socket.RemoteEndPoint.ToString() + "】连接已断开."); // 断开连接
                    LogHelper.Info("Client.stop().与服务器【" + socket.RemoteEndPoint.ToString() + "】连接已断开.");
                }
                socket.Shutdown(SocketShutdown.Both);
                Thread.Sleep(200);
                socket.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Error("断开接收服务器连接失败", ex);
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        string BuffInfo = "";

        public void Send(string info)
        {
            try
            {
                if (socket.Poll(1, SelectMode.SelectRead))
                {
                    try
                    {
                        byte[] temp = new byte[1024];
                        int nRead = socket.Receive(temp);
                        if (nRead == 0)
                        {
                            LogHelper.Info("Client.Send(string info);socket.Poll, nRead = 0, 服务器端已断开，【" + socket.RemoteEndPoint.ToString() + "】,系统自动重连");
                            stop();
                            Thread.Sleep(100);
                            start();
                        }
                    }
                    catch(Exception ex)
                    {
                        LogHelper.Info("Client.Send(string info);socket.Poll, catch, 服务器端已断开，【" + socket.RemoteEndPoint.ToString() + "】,系统自动重连");
                        LogHelper.Error("Client.Send(string info);socket.Poll, catch", ex);
                        stop();
                        Thread.Sleep(100); 
                        start();
                    }
                }
                SendData(info);
            }
            catch (Exception ex)
            {
                if (print != null) print("服务器端已断开,系统尝试重新连接服务器");
                LogHelper.Error("Client.Send", ex);
                LogHelper.Info("服务器端已断开，【" + socket.RemoteEndPoint.ToString() + "】,系统自动重连." + ex.Message);
                stop();
                Thread.Sleep(100); 
                start();
                SendData(BuffInfo);
                Thread.Sleep(300);
                SendData(info);
            }
        }

        /// <summary>
        /// 通过socket发送数据data
        /// </summary>
        private void SendData(string data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    if (socket == null || !socket.Connected)
                    {
                        start();
                    }
                    byte[] bytes = strToHexByte(data);
                    socket.Send(bytes);
                    BuffInfo = data;
                }
            }
            catch (Exception ex)
            {
                if (print != null) print("服务器端已断开,系统尝试重新连接服务器");
                LogHelper.Error("Client.Send", ex);
                LogHelper.Info("服务器端已断开，【" + socket.RemoteEndPoint.ToString() + "】,系统自动重连." + ex.Message);
                stop();
                Thread.Sleep(100); 
                start();
                byte[] bytes = strToHexByte(data);
                socket.Send(bytes);
                BuffInfo = data;
            }
        }
        // 字符串转换16进制字节数组
        public byte[] strToHexByte(string hexString)
        {
            try
            {
                hexString = hexString.Replace(" ", "");
                if ((hexString.Length % 2) != 0)
                    hexString += " ";
                byte[] returnBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
                return returnBytes;
            }
            catch
            {
                return null;
            }
        }

        public string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }
        /// <summary>
        /// 接收通过socket发送过来的数据
        /// </summary>
        public void receiveData(object socket)
        {
            Socket ortherSocket = (Socket)socket;
            while (true)
            {
                try
                {
                    String data = Receive(ortherSocket);       // 接收客户端发送的信息
                    if (!data.Equals(""))
                    {
                        if (print != null) print(data);
                        if (data.Equals("[.Shutdown]")) System.Environment.Exit(0);
                    }
                }
                catch (Exception)
                {
                    if (print != null) print("连接已自动断开");
                    //ortherSocket.Shutdown(SocketShutdown.Both);
                    //ortherSocket.Close();
                    connected = false;
                    break;
                }
                if (!connected) break;
                Thread.Sleep(200);      // 延时0.2后处理接收到的信息
            }
        }

        /// <summary>
        /// 从socket接收数据
        /// </summary>
        public string Receive(Socket socket)
        {
            string data = "";
            byte[] bytes = null;
            int len = socket.Available;
            if (len > 0)
            {
                bytes = new byte[len];
                int receiveNumber = socket.Receive(bytes);
                //data = Encoding.UTF8.GetString(bytes, 0, receiveNumber);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.AppendFormat(Convert.ToString(bytes[i], 16));
                }
                data = sb.ToString().ToUpper();
            }
            return data;
        }
    }
}
