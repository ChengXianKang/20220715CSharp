using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //创建与客户端连接的socket
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //设置要连接的服务器ip地址
            IPAddress ip = IPAddress.Parse("192.168.124.137");
            //设置端口
            IPEndPoint ep = new IPEndPoint(ip, 8887);   
            string str = "LINE009,出现异常";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);

            socket.Connect(ep);
            
                socket.Send(buffer);
            


        }
    }
}
