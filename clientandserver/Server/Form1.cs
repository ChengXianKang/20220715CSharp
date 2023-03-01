using System;
using System.Collections.Generic;
using System.ComponentModel;    
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个负责监听的Socket对象
                Socket SocketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//ip地址.版本号（ip4）,套接字类型TCP为流式（Stream）,支持协议,后面两参数相对应
                                                                                                                 //创建IP地址和端口号对象
                IPAddress ip = IPAddress.Any;
                IPEndPoint Point = new IPEndPoint(ip, Convert.ToInt32(textPort.Text));
                //让负责监听的Socket绑定Ip地址跟端口号
                SocketWatch.Bind(Point);

                ShowMsg("监听成功");
                //设置监听队列 在某一时间点内能连入服务段的最大数量
                SocketWatch.Listen(10);

                //创建新线程用于通信连接

                Thread th1 = new Thread(Listen);
                th1.IsBackground = true;
                th1.Start(SocketWatch);


                ////负责监听的Socket 来接收客户端的连接
                ////Socket socketSend = SocketWatch.Accept();
                ////ShowMsg(socketSend.RemoteEndPoint.ToString() + "连接成功");
                //byte[] buffer = new byte[1024*1024*5];
                //int r = socketSend.Receive(buffer);
                //string str = Encoding.UTF8.GetString(buffer,0,r);
                //ShowMsg(socketSend.RemoteEndPoint + ":" + str);
            }
            catch
            {
                //ShowMsg("监听成功");
                MessageBox.Show("请输入ip地址和端口号");
            }
        }
        void ShowMsg(string str)
        {
            textLog.AppendText(str + "\r\n");
            //AppendText 追加文本
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textLog.Clear();
        }
        private void Listen(object o)
        {
            Socket socketwatch = o as Socket;
            while (true)
            {
                Socket socketSend = socketwatch.Accept();
                ShowMsg(socketSend.RemoteEndPoint.ToString() + "连接成功");
                Thread th2 = new Thread(rec);
                th2.IsBackground = true;
                th2.Start(socketSend);
                //while (true)
                //{
                //    byte[] buffer = new byte[1024 * 1024 * 5];
                //    //服务端实际接收的有效字节
                //    int r = socketSend.Receive(buffer);
                //    //转换为字符串
                //    string str = Encoding.UTF8.GetString(buffer, 0, r);
                //    //将内容写入文件


                //    ShowMsg(socketSend.RemoteEndPoint + ":" + str);
                //}
            }


        }
        void rec(object o)
        {
            Socket socketSend  = o as Socket;
            while (true)
            {
                byte[] buffer = new byte[1024 * 1024 * 5];
                //服务端实际接收的有效字节
                int r = socketSend.Receive(buffer);
                if(r == 0)
                {
                    break;
                }
                //转换为字符串
                string str = Encoding.UTF8.GetString(buffer, 0, r);
                //将内容写入文件
                 writeData(str, @"E:\Demos\winForm小程序\text.txt");
                ShowMsg(socketSend.RemoteEndPoint + ":" + str);

            }

        }
        public bool writeData(dynamic data, string path)
        {
            //判断是否已经有了这个文件
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                if (!System.IO.File.Exists(path))
                {
                    //没有则创建这个文件
                    fs = new FileStream(path, FileMode.Create, FileAccess.Write);//创建
                    sw = new StreamWriter(fs);
                    sw.WriteLine(data + DateTime.Now.ToLocalTime().ToString());//开始写入值
                    sw.Close();
                    fs.Close();
                }
                else
                {
                    fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                    sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.Begin);
                    sw.WriteLine(data + DateTime.Now.ToLocalTime().ToString());//开始写入值
                    sw.Close();
                    fs.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                sw.Close();
                fs.Close();
                return false;
            }
        }

        private void textServer_TextChanged(object sender, EventArgs e)
        {

        }

        ////path操作文件路径
        //string txte = @"E:\Demos\1\text.txt";
        //Console.WriteLine(Path.GetFileName(txte));
        //    Console.WriteLine(Path.GetExtension(txte));
        //    Console.WriteLine(Path.GetFileNameWithoutExtension(txte));
        //    Console.WriteLine(Path.GetDirectoryName(txte));
        //    //file 操作文件
        //    File.Create(@"E:\Demos\1\text1.txt");
        //    Console.WriteLine("创建成功");
        //    File.Delete(@"E:\Demos\1\text1.txt");
        //    File.ReadAllLines(Path.GetDirectoryName(txte));
        //    File.ReadAllText(Path.GetDirectoryName(txte));//只可以读取文本文件
        //    File.ReadAllBytes(txte);//读字节
        //    Console.ReadKey();

        //    //相对路径：encoding.default
        //    //file只能读小文件
        //    //大文件需要文件流
        //    //StreamReader.Equals;
        //    FileStream fs = new FileStream(@"E:\Demos\1\text1.txt", FileMode.OpenOrCreate, FileAccess.Read);
        //byte[] buffer = new byte[1024 * 1024 * 5];
        //int r = fs.Read(buffer, 0, buffer.Length);
        //string s = Encoding.UTF8.GetString(buffer, 0, r);//将字节数组中每一个元素按照指定的编码格式解码成字符串、
        //fs.Close();//关闭流
        //    fs.Dispose();//释放流所占用的资源
        //    Console.WriteLine(s);
        //    Console.ReadKey();
        //    using (FileStream fswrite = new FileStream(@"E:\Demos\1\textxxx.txt", FileMode.OpenOrCreate, FileAccess.Write))
        //    {
        //        string words = "劝人入赌，如同杀人父母";
        //byte[] bytes = Encoding.UTF8.GetBytes(words);
        //fswrite.Write(bytes, 0, bytes.Length);
        //        Console.WriteLine("ok");
        //        Console.ReadKey();


        //private void ListenConnection()
        //{
        //    Socket ConnectionSocket = null;
        //    while (true)
        //    {
        //        try
        //        {
        //            ConnectionSocket = ServerSocket.Accept();
        //        }
        //        catch (Exception ex)
        //        {
        //            richTextBox2.Text += "监听套接字异常" + ex.Message;
        //            //Console.WriteLine("监听套接字异常{0}", ex.Message);
        //            break;
        //        }
        //        //获取客户端端口号和IP
        //        IPAddress ClientIP = (ConnectionSocket.RemoteEndPoint as IPEndPoint).Address;
        //        int ClientPort = (ConnectionSocket.RemoteEndPoint as IPEndPoint).Port;
        //        string SendMessage = "本地IP:" + ClientIP +
        //            ",本地端口:" + ClientPort.ToString();
        //        ConnectionSocket.Send(Encoding.UTF8.GetBytes(SendMessage));
        //        string remotePoint = ConnectionSocket.RemoteEndPoint.ToString();
        //        //richTextBox2.Text += "成功与客户端" + remotePoint + "建立连接\r\n";
        //        //richTextBox3.Text += DateTime.Now + ":" + remotePoint + "\r\n";
        //        ClientInformation.Add(remotePoint, ConnectionSocket);
        //        Thread thread = new Thread(ReceiveMessage);
        //        thread.IsBackground = true;
        //        thread.Start(ConnectionSocket);
        //    }
        //}
        //private void ReceiveMessage(Object SocketClient)
        //{
        //    Socket ReceiveSocket = (Socket)SocketClient;
        //    while (true)
        //    {
        //        byte[] result = new byte[1024 * 1024];
        //        try
        //        {
        //            IPAddress ClientIP = (ReceiveSocket.RemoteEndPoint as IPEndPoint).Address;
        //            int ClientPort = (ReceiveSocket.RemoteEndPoint as IPEndPoint).Port;
        //            int ReceiveLength = ReceiveSocket.Receive(result);
        //            string str = ReceiveSocket.RemoteEndPoint.ToString();
        //            string ReceiveMessage = Encoding.UTF8.GetString(result, 0, ReceiveLength);
        //            richTextBox1.Text += "接收客户端:" + ReceiveSocket.RemoteEndPoint.ToString() +
        //            "时间：" + DateTime.Now.ToString() + "\r\n" + "消息：" + ReceiveMessage + "\r\n";
        //            if (ClientInformation.Count == 1) continue;//只有一个客户端
        //            List<string> test = new List<string>(ClientInformation.Keys);
        //            for (int i = 0; i < ClientInformation.Count; i++)
        //            {
        //                Socket socket = ClientInformation[test[i]];
        //                string s = ReceiveSocket.RemoteEndPoint.ToString();
        //                if (test[i] != s)
        //                {
        //                    richTextBox1.Text += DateTime.Now + "\r\n" + "客户端" + str + "向客户端" + test[i] + "发送消息：" + ReceiveMessage;
        //                    string ReceiveMessage1 = DateTime.Now + "\r\n" + "客户端" + str + "向您发送消息：" + ReceiveMessage;
        //                    socket.Send(Encoding.UTF8.GetBytes(ReceiveMessage1));
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            richTextBox2.Text += "监听出现异常！\r\n";
        //            richTextBox2.Text += "客户端" + ReceiveSocket.RemoteEndPoint + "已经连接中断" + "\r\n" +
        //            ex.Message + "\r\n" + ex.StackTrace + "\r\n";
        //            string s = ReceiveSocket.RemoteEndPoint.ToString();
        //            ClientInformation.Remove(s);
        //            ReceiveSocket.Shutdown(SocketShutdown.Both);
        //            ReceiveSocket.Close();
        //            break;
        //        }
        //    }
        //}




    }
}
