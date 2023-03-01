using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Utils.HBaseClass;
using Utils;
using Utils.Model;

namespace MDC
{
    public partial class frmHBase : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 玻璃码列表
        /// </summary>
        List<string> lstGlassCode;
        /// <summary>
        /// 后台处理自动删除
        /// </summary>
        BackgroundWorker bgw = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        public frmHBase()
        {
            InitializeComponent();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
        }



        /// <summary>
        /// 获取玻璃码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetGlass_Click(object sender, EventArgs e)
        {
//            string sql = string.Format(@"SELECT IBS_GlassCode,IBS_productDate INTO #TEMP FROM TPL_PackingInnerBox_Sub_201904 WHERE IBS_productDate < '{0}' ORDER BY IBS_productDate ASC;
//                                       SELECT TOP 2296467 * INTO #TEMP2 FROM #TEMP ORDER BY IBS_productDate DESC;
//                                       SELECT IBS_GlassCode FROM #TEMP2 ORDER BY IBS_productDate ASC;", "2019-04-01");
//            string sql = string.Format(@"SELECT IBS_Tid,IBS_GlassCode,IBS_productDate INTO #TEMP FROM TPL_PackingInnerBox_Sub_201904 WHERE IBS_productDate < '{0}' ORDER BY IBS_productDate ASC;
//                                            DELETE FROM #TEMP WHERE IBS_Tid IN (SELECT TOP 1800000 IBS_Tid FROM #TEMP);
//                                            SELECT IBS_GlassCode FROM #TEMP ORDER BY IBS_productDate ASC;", "2019-04-01");
            string sql = string.Format(@"SELECT IBS_GlassCode FROM TPL_PackingInnerBox_Sub_201904 WHERE IBS_productDate < '{0}' ORDER BY IBS_productDate ASC", "2019-04-01");
            DataTable dtGlass = conn.ExecuteDataTable(sql, "Base");
            if (dtGlass == null || dtGlass.Rows.Count == 0)
            {
                MessageBox.Show(this, "未查询到玻璃码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.lstGlassCode = dtGlass.AsEnumerable().Select(d => d.Field<string>("IBS_GlassCode")).ToList();
            //string[] arrGlass = dtGlass.AsEnumerable().Select(d => d.Field<string>("IBS_GlassCode")).ToArray();

            this.lstGlass.Items.Clear();
            if (this.lstGlassCode.Count >= 100)
            {
                for (int i = 0; i < 100; i++)
                {
                    this.lstGlass.Items.Add(this.lstGlassCode[i]);
                }
            }
            else
            {
                for (int i = 0; i < this.lstGlassCode.Count; i++)
                {
                    this.lstGlass.Items.Add(this.lstGlassCode[i]);
                }
            }
            if (this.lstGlass.Items.Count > 0)
            {
                this.lstGlass.SelectedIndex = 0;
                string code = lstGlass.SelectedItem.ToString();
                GetData(code);
            }
        }
        /// <summary>
        /// 选中玻璃码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstGlass_Click(object sender, EventArgs e)
        {
            if (lstGlass.SelectedIndex < 0) return;
            string code = lstGlass.SelectedItem.ToString();
            GetData(code);
        }
        /// <summary>
        /// 获取玻璃信息
        /// </summary>
        /// <param name="glassCode"></param>
        private void GetData(string glassCode)
        {
            List<string> lst1 = GetData01Key(glassCode);
            this.lstData01Key.Items.Clear();
            this.lstData01Key.Items.AddRange(lst1.ToArray());
            if (this.lstData01Key.Items.Count > 0)
            {
                this.lstData01Key.SelectedIndex = 0;
                txtData01Value.Text = GetDataValue(this.lstData01Key.Items[0].ToString(), "data_01");
            }
            else
            {
                txtData01Value.Text = "";
            }

            List<string> lst2 = GetData02Key(glassCode);
            this.lstData02Key.Items.Clear();
            this.lstData02Key.Items.AddRange(lst2.ToArray());
            if (this.lstData02Key.Items.Count > 0)
            {
                this.lstData02Key.SelectedIndex = 0;
                txtData02Value.Text = GetDataValue(this.lstData02Key.Items[0].ToString(), "data_02");
            }
            else
            {
                txtData02Value.Text = "";
            }

            this.lstData06Key.Items.Clear();
            string data06value = GetDataValue(glassCode, "data_06");
            if (string.IsNullOrEmpty(data06value))
            {
                txtData06Value.Text = "";
            }
            else
            {
                this.lstData06Key.Items.Add(glassCode);
                this.lstData06Key.SelectedIndex = 0;
                txtData06Value.Text = data06value;
            }
        }

        private List<string> GetData01Key(string glassCode)
        {
            List<string> lst = new List<string>();
            dynamic objLCD = GetHBaseDataClass.Instance.GetData01(glassCode);
            if (objLCD == null) return lst;
            string fpc = objLCD.fpcCode;
            string tp = objLCD.tpCode;
            string bl = objLCD.backCode;
            string qr = objLCD.intactCode;
            lst.Add(glassCode);
            if (!string.IsNullOrEmpty(fpc))
            {
                lst.Add(fpc);
            }
            if (!string.IsNullOrEmpty(tp))
            {
                lst.Add(tp);
            }
            if (!string.IsNullOrEmpty(bl))
            {
                lst.Add(bl);
            }
            if (!string.IsNullOrEmpty(qr))
            {
                lst.Add(qr);
            }
            return lst;
        }

        private List<string> GetData02Key(string glassCode)
        {
            List<string> keys = new List<string>();
            keys = GetHBaseDataClass.Instance.GetRowKeys(glassCode, "data_02");
            return keys;
        }

        private string GetDataValue(string key, string table)
        {
            dynamic jobject = GetHBaseDataClass.Instance.GetDataValueByKey(key, table);
            if (jobject != null)
            {
                return jobject.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="key"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private bool DeleteRow(string key, string table)
        {
            if (!key.Contains("$T2020") && !key.Contains("$T201904") && !key.Contains("$T201905") && !key.Contains("$T201906") && !key.Contains("$T201907") && !key.Contains("$T201908") && !key.Contains("$T201909") && !key.Contains("$T201910") && !key.Contains("$T201911") && !key.Contains("$T201912"))
            {
                return GetHBaseDataClass.Instance.DeleteRowKey(key, table);
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 选中data_01的Rowkey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstData01Key_Click(object sender, EventArgs e)
        {
            if (lstData01Key.SelectedIndex < 0) return;
            string key = lstData01Key.SelectedItem.ToString();
            txtData01Value.Text = GetDataValue(key, "data_01");
        }
        /// <summary>
        /// 选中data_02的Rowkey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstData02Key_Click(object sender, EventArgs e)
        {
            if (lstData02Key.SelectedIndex < 0) return;
            string key = lstData02Key.SelectedItem.ToString();
            txtData02Value.Text = GetDataValue(key, "data_02");
        }
        /// <summary>
        /// 删除data_01
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel01_Click(object sender, EventArgs e)
        {
            if (lstData01Key.SelectedIndex < 0) return;
            string key = lstData01Key.SelectedItem.ToString();
            if (DeleteRow(key, "data_01"))
            {
                MessageBox.Show("删除成功");
                txtData01Value.Text = "";
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }
        /// <summary>
        /// 删除data_02
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel02_Click(object sender, EventArgs e)
        {
            if (lstData02Key.SelectedIndex < 0) return;
            string key = lstData02Key.SelectedItem.ToString();
            if (DeleteRow(key, "data_02"))
            {
                MessageBox.Show("删除成功");
                List<string> lst2 = GetData02Key(lstGlass.SelectedItem.ToString());
                this.lstData02Key.Items.Clear();
                this.lstData02Key.Items.AddRange(lst2.ToArray());
                if (this.lstData02Key.Items.Count > 0)
                {
                    this.lstData02Key.SelectedIndex = 0;
                    txtData02Value.Text = GetDataValue(this.lstData02Key.Items[0].ToString(), "data_02");
                }
                else
                {
                    txtData02Value.Text = "";
                }
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }
        /// <summary>
        /// 删除data_06
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel06_Click(object sender, EventArgs e)
        {
            if (lstData06Key.SelectedIndex < 0) return;
            string key = lstData06Key.SelectedItem.ToString();
            if (DeleteRow(key, "data_06"))
            {
                MessageBox.Show("删除成功");
                txtData06Value.Text = "";
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }
        /// <summary>
        /// 自动删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoDel_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = this.lstGlassCode.Count;
            progressBar1.Value = 0;
            lblPgs.Text = string.Format("{0}/{1}", progressBar1.Value, this.lstGlassCode.Count);

            bgw.RunWorkerAsync(this.lstGlassCode);
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bgw.IsBusy)
            {
                bgw.CancelAsync();
            }
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null) return;
            // 完整玻璃列表
            List<string> lstLCD = e.Argument as List<string>;
            // 进度
            int n = 0;
            while (lstLCD.Count > 0)
            {
                //判断是否取消操作  
                if (bgw.CancellationPending)
                {
                    e.Cancel = true; //这里才真正取消  
                    return;
                }

                n++;
                string glassCode = lstLCD[0];
                // 选择第一个玻璃码
                bgw.ReportProgress(n, new object[]{ "SelectGlass" });

                //if (glassCode.Length < 9) break;
                //if (glassCode == "P") break;
                if (glassCode.Length < 9)
                {
                    // 删除玻璃码
                    LogHelper.Note(glassCode + ": 删除成功");
                    lstLCD.RemoveAt(0);
                    bgw.ReportProgress(n, new object[] { "DeleteGlass" });

                    if (lstLCD.Count >= 100)
                    {
                        // 补充玻璃码
                        bgw.ReportProgress(n, new object[] { "AddGlass", lstLCD[99] });
                    }
                    continue;
                }

                // 获取data_01的rowkey列表
                List<string> lst1 = GetData01Key(glassCode);
                // 显示Data01Key
                bgw.ReportProgress(n, new object[] { "ShowData01Key", lst1 });
                if (lst1.Count > 0)
                {
                    //string value1 = GetDataValue(lst1[0], "data_01");
                    //// 显示Data01Value
                    //bgw.ReportProgress(n, new object[] { "ShowData01Value", value1 });
                }
                else
                {
                    //// 显示Data01Value
                    //bgw.ReportProgress(n, new object[] { "ShowData01Value", "" });
                }

                // 获取data_02的rowkey列表
                List<string> lst2 = GetData02Key(glassCode);
                // 显示Data02Key
                bgw.ReportProgress(n, new object[] { "ShowData02Key", lst2 });
                if (lst2.Count > 0)
                {
                    //string value2 = GetDataValue(lst2[0], "data_02");
                    //// 显示Data02Value
                    //bgw.ReportProgress(n, new object[] { "ShowData02Value", value2 });
                }
                else
                {
                    //// 显示Data02Value
                    //bgw.ReportProgress(n, new object[] { "ShowData02Value", "" });
                }

                string data06value = GetDataValue(glassCode, "data_06");
                if (string.IsNullOrEmpty(data06value))
                {
                    // 显示Data06Key
                    bgw.ReportProgress(n, new object[] { "ShowData06Key", "" });
                    // 显示Data06Value
                    bgw.ReportProgress(n, new object[] { "ShowData06Value", "" });
                }
                else
                {
                    // 显示Data06Key
                    bgw.ReportProgress(n, new object[] { "ShowData06Key", "" });
                    // 显示Data06Value
                    bgw.ReportProgress(n, new object[] { "ShowData06Value", data06value });
                }

                //删除data_06
                if (!string.IsNullOrEmpty(data06value))
                {
                    if (DeleteRow(glassCode, "data_06"))
                    {
                        LogHelper.Note(glassCode + ": 删除data_06成功");
                    }
                    else
                    {
                        LogHelper.Note(glassCode + ": 删除data_06失败");
                    }
                    // 删除Data06
                    bgw.ReportProgress(n, new object[] { "DeleteData06" });
                }

                //循环删除data_02
                while (lst2.Count > 0)
                {
                    string data_02key = lst2[0];
                    if (DeleteRow(data_02key, "data_02"))
                    {
                        LogHelper.Note(glassCode + ": 删除data_02成功,Rowkey = " + data_02key);
                        // 删除data_02Key
                        bgw.ReportProgress(n, new object[] { "DeleteData02" });
                        lst2.RemoveAt(0);
                        if (lst2.Count > 0)
                        {
                            //string value2 = GetDataValue(lst2[0], "data_02");
                            //// 显示Data02Value
                            //bgw.ReportProgress(n, new object[] { "ShowData02Value", value2 });
                        }
                        else
                        {
                            //// 显示Data02Value
                            //bgw.ReportProgress(n, new object[] { "ShowData02Value", "" });
                        }

                    }
                    else
                    {
                        LogHelper.Note(glassCode + ": 删除data_02失败,Rowkey = " + data_02key);
                    }
                }//while (lst2.Count > 0)

                //循环删除data_01
                while (lst1.Count > 0)
                {
                    string data_01key = lst1[0];
                    if (DeleteRow(data_01key, "data_01"))
                    {
                        LogHelper.Note(glassCode + ": 删除data_01成功,Rowkey = " + data_01key);
                        // 删除data_01Key
                        bgw.ReportProgress(n, new object[] { "DeleteData01" });
                        lst1.RemoveAt(0);
                        if (lst1.Count > 0)
                        {
                            //string value1 = GetDataValue(lst1[0], "data_01");
                            //// 显示Data01Value
                            //bgw.ReportProgress(n, new object[] { "ShowData01Value", value1 });
                        }
                        else
                        {
                            //// 显示Data01Value
                            //bgw.ReportProgress(n, new object[] { "ShowData01Value", "" });
                        }

                    }
                    else
                    {
                        LogHelper.Note(glassCode + ": 删除data_01失败,Rowkey = " + data_01key);
                    }
                }//while (lst1.Count > 0)

                // 删除玻璃码
                LogHelper.Note(glassCode + ": 删除成功");
                lstLCD.RemoveAt(0);
                bgw.ReportProgress(n, new object[] { "DeleteGlass" });

                if (lstLCD.Count >= 100)
                {
                    // 补充玻璃码
                    bgw.ReportProgress(n, new object[] { "AddGlass", lstLCD[99] });
                }


            }//while (lst.Count > 0)
        }

        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lblPgs.Text = string.Format("{0} / {1}", e.ProgressPercentage, progressBar1.Maximum);
            if (e.UserState == null) return;
            object[] obj = e.UserState as object[];
            string type = obj[0].ToString();
            switch (type)
            {
                case "SelectGlass"://选中玻璃码
                    if (this.lstGlass.Items.Count > 0)
                    {
                        this.lstGlass.SelectedIndex = 0;
                    }
                    break;

                case "ShowData01Key":
                    List<string> list1 = obj[1] as List<string>;
                    this.lstData01Key.Items.Clear();
                    if (list1 != null && list1.Count > 0)
                    {
                        this.lstData01Key.Items.AddRange(list1.ToArray());
                        if (this.lstData01Key.Items.Count > 0)
                        {
                            this.lstData01Key.SelectedIndex = 0;
                        }
                    }
                    break;

                case "ShowData01Value":
                    string value1 = obj[1].ToString();
                    txtData01Value.Text = value1;
                    break;

                case "ShowData02Key":
                    List<string> list2 = obj[1] as List<string>;
                    this.lstData02Key.Items.Clear();
                    if (list2 != null && list2.Count > 0)
                    {
                        this.lstData02Key.Items.AddRange(list2.ToArray());
                        if (this.lstData02Key.Items.Count > 0)
                        {
                            this.lstData02Key.SelectedIndex = 0;
                        }
                    }
                    break;

                case "ShowData02Value":
                    string value2 = obj[1].ToString();
                    txtData02Value.Text = value2;
                    break;

                case "ShowData06Key":
                    string key6 = obj[1].ToString();
                    this.lstData06Key.Items.Clear();
                    if (!string.IsNullOrEmpty(key6))
                    {
                        this.lstData06Key.Items.Add(key6);
                        if (this.lstData06Key.Items.Count > 0)
                        {
                            this.lstData06Key.SelectedIndex = 0;
                        }
                    }
                    break;

                case "ShowData06Value":
                    string value6 = obj[1].ToString();
                    txtData06Value.Text = value6;
                    break;

                case "DeleteData06":
                    this.lstData06Key.Items.Clear();
                    txtData06Value.Text = "";
                    break;

                case "DeleteData02":
                    if (this.lstData02Key.Items.Count > 0)
                    {
                        this.lstData02Key.Items.RemoveAt(0);
                    }
                    if (this.lstData02Key.Items.Count > 0)
                    {
                        this.lstData02Key.SelectedIndex = 0;
                    }
                    break;

                case "DeleteData01":
                    if (this.lstData01Key.Items.Count > 0)
                    {
                        this.lstData01Key.Items.RemoveAt(0);
                    }
                    if (this.lstData01Key.Items.Count > 0)
                    {
                        this.lstData01Key.SelectedIndex = 0;
                    }
                    break;

                case "DeleteGlass":
                    if (this.lstGlass.Items.Count > 0)
                    {
                        this.lstGlass.Items.RemoveAt(0);
                    }
                    if (this.lstGlass.Items.Count > 0)
                    {
                        this.lstGlass.SelectedIndex = 0;
                    }
                    break;

                case "AddGlass":
                    string code = obj[1].ToString();
                    this.lstGlass.Items.Add(code);
                    break;

            }
        }


        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("操作已取消");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("处理异常：" + e.Error.ToString());
            }
            else
            {
                MessageBox.Show("操作完成"); ;      // 从 DoWork   
            }  
        }

        private void btnData02Clear_Click(object sender, EventArgs e)
        {

        }

        ///// <summary>
        ///// rowKey左前缀查询
        ///// </summary>
        ///// <param name="_rowKey">rowKey前缀</param>
        //public List<TRowResult> GetRowsWithPrefix(string _rowKey, string table)
        //{
        //    List<TRowResult> reslut = null;
        //    int n = 0;
        //    while (reslut == null)
        //    {
        //        try
        //        {
        //            if (!GetDataFromHBase.Instance.transport.IsOpen)
        //            {
        //                GetDataFromHBase.Instance.OpenConnect();
        //            }
        //            n++;
        //            if (n <= 3)
        //            {
        //                List<byte[]> columns = new List<byte[]>(0);
        //                columns.Add(Encoding.UTF8.GetBytes("logValue"));
        //                int ScannerID = GetDataFromHBase.Instance.client.scannerOpenWithPrefix(Encoding.UTF8.GetBytes(table), Encoding.UTF8.GetBytes(_rowKey), columns, null);
        //                reslut = GetDataFromHBase.Instance.client.scannerGetList(ScannerID, 50000);
        //            }
        //        }
        //        catch (Exception exp)
        //        {
        //            LogHelper.Error("rowKey左前缀查询失败：" + exp.Message, exp);
        //            GetDataFromHBase.Instance.OpenConnect();
        //        }
        //    }
        //    return reslut;
        //}
    }
}
