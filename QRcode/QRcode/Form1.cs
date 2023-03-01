using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using System.IO;
namespace QRcode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreatQr_Click(object sender, EventArgs e)
        {
            try
            {
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                // 生成二维码内容模式分为三种，数字,数字字母，字节，这个基本上都设置成Byte，支持汉字
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                // 设置二维码的大小，默认4，在尺寸小的情况下，版本参数过高则设备难以识别二维码
                qrCodeEncoder.QRCodeScale = 7;
                // 设置二维码的版本，默认7 该值影响二维码最高数据容量 7大致对应40个汉字长度，内容超出择需提升该数值
                qrCodeEncoder.QRCodeVersion = 0;
                // 设置错误校验级别，默认中等，二维码被遮挡住一部分实际上也是能扫出内容的，这个效验级别的意思就是
                // 当遮挡部分最大占整体多少时仍然可以被扫出来，M大概在20%左右，H为30%，级别越高相应的数据容量会缩小
                // 那些中间带图标的二维码，其实就是简单粗暴的用LOGO遮挡住了中间部分
                //qrCodeEncoder.QRCodeErrorCorrect = (QRCodeEncoder.ERROR_CORRECTION)Enum.Parse(typeof(QRCodeEncoder.ERROR_CORRECTION), comboBox2.Text);
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                Image image = qrCodeEncoder.Encode(textBox1.Text, Encoding.UTF8);
                string filename = textBox1.Text + ".jpg";
                DateTime dt = DateTime.Now;
                string sPath = $@"E:\QRcode\{dt.Year},{dt.Month},{dt.Day}";
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);//创建路径
                }
                //string filepath = @"E:/QRcode/";
                using (FileStream fs = new FileStream(sPath + "\\" + filename, FileMode.OpenOrCreate, FileAccess.Write))
                {

                    image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                };

                pictureBox1.Image = image;
            }
            catch
            {
                MessageBox.Show("输入有误");
            }
           
        }
    }
}
