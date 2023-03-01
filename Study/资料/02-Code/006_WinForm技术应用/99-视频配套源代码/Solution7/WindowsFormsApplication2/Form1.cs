using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bt1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] arr = new string[5];
                arr[5] = "Jack";
            }
            catch (Exception ex)  //发生异常进入到catch
            {
                MessageBox.Show("程序出现异常:" + ex.Message);
            }
            finally //不管是否发生异常都会进入finally,也可以没有finally
            {
                //
            }

        }

        private void bt2_Click(object sender, EventArgs e)
        {
            //通过注释的方式分别产生三种异常，查看会进入哪一个catch处理
            try
            {
                //模拟下标越界异常
                string[] arr = new string[5];
                arr[5] = "Jack";
                //模拟数据库连接异常
                string connStr = "server=.;database=DBTEST;uid=sa;pwd=123456";
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                //模拟其他异常
                int a = 10;
                int b = 0;
                int temp = a / b;
            }
            catch (IndexOutOfRangeException ex) //下标越界异常
            {
                string strErr = "程序出现异常:" + ex.Message + "\n";
                strErr += "请联系项目经理:13558785456";
                MessageBox.Show(strErr);
            }
            catch (System.Data.SqlClient.SqlException ex)  //sql操作异常
            {
                string strErr = "数据库连接异常:" + ex.Message + "\n";
                strErr += "请联系数据库管理员:13558785456";
                MessageBox.Show(strErr);
            }
            catch (Exception ex) //此处为Exception基类，上面所有异常类型都无法匹配的时候会进入到这里处理
            {
                MessageBox.Show("程序出现异常:" + ex.Message);
            }
            finally //不管是否发生异常都会进入finally,也可以没有finally
            {
                //
            }
        }
    }
}
