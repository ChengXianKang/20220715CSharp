using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Utils;
using Utils.HBaseClass;

namespace MDC
{
    public partial class frmTest : Form
    {
        //Utils.SerialPortUtil com = new Utils.SerialPortUtil("COM5");
        System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();

        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();

        DataTable dtData = null;

        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            //port = new System.IO.Ports.SerialPort("COM5", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            //port.ReceivedBytesThreshold = 1;
            //port.DtrEnable = true;
            //port.RtsEnable = true;
            
            //port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
            //port.Open();
        }

        void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] data = null;
                Utils.PublicSub.Delay(50);
                if (port.BytesToRead > 0)
                {
                    data = new byte[port.BytesToRead];
                    port.Read(data, 0, port.BytesToRead);
                }
                string DataString = (new UTF8Encoding().GetString(data)).Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("|", "/").Replace(";", "；"); ;
                ShowResult(DataString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //void com_DataReceived(Utils.DataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        byte[] data = e.BytesReceived;
        //        string DataString = (new UTF8Encoding().GetString(data)).Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("|", "/").Replace(";", "；"); ;
        //        ShowResult(DataString);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void frmTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //port.Close();
        }

        /// <summary>
        /// 跨线程输出结果的委托
        /// </summary>
        /// <param name="shareModel"></param>
        private delegate void ShowResultCallback(string message);

        /// <summary>
        /// 跨线程输出结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        private void ShowResult(string message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                ShowResultCallback d = new ShowResultCallback(ShowResult);
                this.BeginInvoke(d, new object[] { message });
            }
            else
            {
                //textBox1.AppendText(message);

            }
        }

        #region 根据物料生产日期查询玻璃
        private void button1_Click(object sender, EventArgs e)
        {
            dtData = null;
            string sql = string.Format(@"SELECT [SPOLM_Batch],[SPOLM_SPOMJobCode]
                                                    FROM [Store_ProduceOrder_loadMaterial]
                                                    WHERE [SPOLM_ProductionDate] = '{0}'
                                                    AND [SPOLM_MaterialCode] = '{1}'", dateTimePicker1.Value.ToString("yyyy-MM-dd"), textBox2.Text.Trim());
            DataView dvBatch = conn.ExecuteDataView(sql, "Base");
            progressBar1.Maximum = dvBatch.Count;
            progressBar1.Value = 0;
            foreach (DataRowView row in dvBatch)
            {
                progressBar1.Value += 1;
                string batch = row["SPOLM_Batch"].ToString();
                string order = row["SPOLM_SPOMJobCode"].ToString();
                string material = textBox2.Text.Trim();
                DataTable dtBatchGlass = GetHBaseDataClass.Instance.GetBatchGlass(material, batch, order);
                if(dtBatchGlass != null && dtBatchGlass.Rows.Count > 0)
                {
                    if(dtData == null)
                    {
                        dtData = dtBatchGlass.Clone();
                    }

                    dtData.Merge(dtBatchGlass);
                }
            }

            dataGridView1.DataSource = dtData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportExcel(this.dtData, string.Format("{0}物料批次对应玻璃列表", dateTimePicker1.Value.ToString("yyyy-MM-dd")));
        }

        #endregion 根据物料生产日期查询玻璃


        #region 查询工序过站玻璃信息
        private void button3_Click(object sender, EventArgs e)
        {
            string[] linecode = new string[] { "2101", "2201", "2301", "2401", "2501" };
            string processcode = "008";
            DateTime dtStart = new DateTime(2021, 8, 3, 0, 0, 0);
            DateTime dtStop = new DateTime(2021, 8, 4, 0, 0, 0);

            linecode = txtLineCode.Text.Split(',');
            processcode = txtProcessCode.Text;
            dtStart = dateTimePicker2.Value;
            dtStop = dateTimePicker3.Value;

            this.dtData = null;
            foreach (string line in linecode)
            {
                DataTable dtBatchGlass = GetHBaseDataClass.Instance.GetProcessGlassInfo(line, processcode, dtStart, dtStop);

                if (dtBatchGlass != null && dtBatchGlass.Rows.Count > 0)
                {
                    if (dtData == null)
                    {
                        dtData = dtBatchGlass.Clone();
                    }

                    dtData.Merge(dtBatchGlass);
                }

            }
            dataGridView2.DataSource = dtData;
        }

        #endregion 查询工序过站玻璃信息

        private void button4_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportExcel(this.dtData, string.Format("{0}-{1}工序过站玻璃列表", dateTimePicker2.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd")));
        }
    }
}
