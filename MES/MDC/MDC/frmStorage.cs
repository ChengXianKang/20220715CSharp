using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Utils.Common;

namespace MDC
{
    public partial class frmStorage : Form
    {
        /// <summary>
        /// SQL Server数据库操作类
        /// </summary>
        private static YJ.Data.SqlServerProvider conn = new YJ.Data.SqlServerProvider();
        /// <summary>
        /// 库区货架总排数
        /// </summary>
        private int rowCount;
        /// <summary>
        /// 仓库货位表
        /// </summary>
        private DataTable dtPallet;
        /// <summary>
        /// 已解锁货位表
        /// </summary>
        private DataTable dtUnlock;
        /// <summary>
        /// 超期存放货位表
        /// </summary>
        private DataTable dtLongTimeStore;
        /// <summary>
        /// 长时间超期存放货位表
        /// </summary>
        private DataTable dtTooLongTimeStore;
        /// <summary>
        /// 临近过期货位表
        /// </summary>
        private DataTable dtNearExpired;
        /// <summary>
        /// 已过期货位表
        /// </summary>
        private DataTable dtExpired;
        /// <summary>
        /// 货位分类数量表
        /// </summary>
        private DataTable dtNum;

        ///// <summary>
        ///// 货架控件表
        ///// </summary>
        //private Dictionary<string, Shelf> dicShelf;

        //private List<string> lstRow;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }

        public frmStorage()
        {
            InitializeComponent();
        }

        private void frmStorage_Load(object sender, EventArgs e)
        {
            flpStorage.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
.SetValue(flpStorage, true, null);
            //// 默认小库区
            //this.cmbHouseArea.SelectedIndex = 0;

            // 默认大库区 每页12排
            this.cmbUnit.SelectedIndex = 11;

            // 获取指定库区数据
            GetData("1", 1, 12);
            lblTime.Text = string.Format("更新时间：{0}", DateTime.Now.ToString("HH:mm:ss"));
            // 分页
            cmbPage.Items.Clear();
            int p = (int)Math.Ceiling((double)this.rowCount / 12);
            for (int i = 1; i <= p; i++)
            {
                cmbPage.Items.Add(i);
            }
            // 默认第一页
            cmbPage.SelectedIndex = 0;

            // 显示首页
            ShowData();

            // 启动定时器自动刷新数据
            tmrRefresh.Start();
        }

        private void frmStorage_Shown(object sender, EventArgs e)
        {

        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (cmbPage.SelectedIndex == cmbPage.Items.Count - 1)
            {
                cmbPage.SelectedIndex = 0;
            }
            else
            {
                cmbPage.SelectedIndex += 1;
            }

            string area = rdoArea1.Checked ? "1" : "0";
            // 获取指定库区数据
            GetData(area, cmbPage.SelectedIndex + 1, cmbUnit.SelectedIndex + 1);

            flpStorage.Controls.Clear();

            ShowData();
        }

        ///// <summary>
        ///// 切换显示库区
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cmbHouseArea_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (cmbHouseArea.SelectedIndex == -1) return;

        //    if (cmbHouseArea.SelectedIndex == 0)
        //    {
        //        cmbUnit.SelectedIndex = 3;
        //    }
        //    else
        //    {
        //        cmbUnit.SelectedIndex = 11;
        //    }

        //    // 获取指定库区数据
        //    GetData(cmbHouseArea.SelectedIndex.ToString(), 1, cmbUnit.SelectedIndex + 1);

        //    // 分页
        //    cmbPage.Items.Clear();
        //    int p = (int)Math.Ceiling((double)this.rowCount / (cmbUnit.SelectedIndex + 1));
        //    for (int i = 1; i <= p; i++)
        //    {
        //        cmbPage.Items.Add(i);
        //    }
        //    // 默认第一页
        //    cmbPage.SelectedIndex = 0;
        //    flpStorage.Controls.Clear();
        //    // 显示首页
        //    ShowData();
        //}
        /// <summary>
        /// 切换显示库区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoArea1_CheckedChanged(object sender, EventArgs e)
        {
            // 选择大库区
            if (rdoArea1.Checked)
            {
                cmbUnit.SelectedIndex = 11;
                // 获取大库区数据
                GetData("1", 1, cmbUnit.SelectedIndex + 1);

            }
            else// 选择小库区
            {
                cmbUnit.SelectedIndex = 3;
                // 获取小库区数据
                GetData("0", 1, cmbUnit.SelectedIndex + 1);
            }

            // 分页
            cmbPage.Items.Clear();
            int p = (int)Math.Ceiling((double)this.rowCount / (cmbUnit.SelectedIndex + 1));
            for (int i = 1; i <= p; i++)
            {
                cmbPage.Items.Add(i);
            }
            // 默认第一页
            cmbPage.SelectedIndex = 0;
            flpStorage.Controls.Clear();
            // 显示首页
            ShowData();
        }
        /// <summary>
        /// 换页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // 获取指定库区数据
            string area = rdoArea1.Checked ? "1" : "0";
            // 获取指定库区数据
            GetData(area, cmbPage.SelectedIndex + 1, cmbUnit.SelectedIndex + 1);
            flpStorage.Controls.Clear();
            ShowData();
        }

        /// <summary>
        /// 切换每页排数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbPage.Items.Clear();
            // 分页
            int p = (int)Math.Ceiling((double)this.rowCount / (cmbUnit.SelectedIndex + 1));
            for (int i = 1; i <= p; i++)
            {
                cmbPage.Items.Add(i);
            }
            // 默认第一页
            cmbPage.SelectedIndex = 0;
            // 获取指定库区数据
            string area = rdoArea1.Checked ? "1" : "0";
            // 获取指定库区数据
            GetData(area, cmbPage.SelectedIndex + 1, cmbUnit.SelectedIndex + 1);
            flpStorage.Controls.Clear();
            ShowData();
        }

        private void bgwShow_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgwShow_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bgwShow_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        /// <summary>
        /// 从数据库获取数据
        /// </summary>
        /// <param name="area">库区（0：小库区，1：大库区）</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页排数</param>
        private void GetData(string area, int pageIndex, int pageSize)
        {
            string sql = string.Format("EXEC Store_Place_TuoStock_Pro '{0}',{1},{2}", area, pageIndex, pageSize);
            DataSet ds = conn.ExecuteDataSet(sql, "Base");

            lblTime.Text = string.Format("更新时间：{0}", DateTime.Now.ToString("HH:mm:ss"));

            //货架总排数
            this.rowCount = YJ.Common.Utils.StrToInt(ds.Tables[0].Rows[0][0], 0);
            // 仓库货位表
            this.dtPallet = ds.Tables[1];
            // 已解锁货位表
            this.dtUnlock = ds.Tables[2];
            // 超期存放货位表
            this.dtLongTimeStore = ds.Tables[3];
            // 长时间超期存放货位表
            this.dtTooLongTimeStore = ds.Tables[4];
            // 临近过期货位表
            this.dtNearExpired = ds.Tables[5];
            // 已过期货位表
            this.dtExpired = ds.Tables[6];
            // 货位分类数量表
            this.dtNum = ds.Tables[7];
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        private void ShowData()
        {
            try
            {
                this.lblIdle.Text = string.Format("空闲|{0}", this.dtNum.Rows[0]["IdleNum"].ToString());
                this.lblUnlock.Text = string.Format("解锁|{0}", this.dtNum.Rows[0]["UnlockNum"].ToString());
                this.lblLock.Text = string.Format("锁定|{0}", this.dtNum.Rows[0]["LockNum"].ToString());
                this.lblDel.Text = string.Format("禁用|{0}", this.dtNum.Rows[0]["DelNum"].ToString());
                this.lbl30Day.Text = string.Format("超30天|{0}", this.dtNum.Rows[0]["Store30DayNum"].ToString());
                this.lbl60Day.Text = string.Format("超60天|{0}", this.dtNum.Rows[0]["Store60DayNum"].ToString());
                this.lblNearExpired.Text = string.Format("15天过期|{0}", this.dtNum.Rows[0]["NearExpiredNum"].ToString());
                this.lblExpired.Text = string.Format("已过期|{0}", this.dtNum.Rows[0]["ExpiredNum"].ToString());

                //flpStorage.Controls.Clear();
                Dictionary<string, Shelf> dicShelf = new Dictionary<string, Shelf>();
                DataView dv = new DataView(this.dtPallet);
                for (int i = 0; i < dv.Count; i++)
                {
                    DataRowView datarow = dv[i];
                    string area = datarow["Area"].ToString();
                    string row = datarow["RowNum"].ToString();
                    string col = datarow["ColumnNum"].ToString();
                    string floor = datarow["FloorNum"].ToString();
                    string key = string.Format("{0}-{1}", row, col);
                    string location = datarow["SP_Location"].ToString();
                    bool del = datarow["SP_Del"].ToString() == "1" ? true : false;
                    string material = datarow["STSM_MaterialName"] == null ? "" : datarow["STSM_MaterialName"].ToString();
                    string count = datarow["STSM_SecondCount"] == null ? "" : datarow["STSM_SecondCount"].ToString().Replace(".00", "");

                    if (flpStorage.Controls.ContainsKey(key))
                    //if (dicShelf.ContainsKey(key))
                    {
                        Shelf shelf = flpStorage.Controls[key] as Shelf;

                        if (!shelf.Pallets.ContainsKey(floor))
                        {
                            continue;
                        }
                        if (!shelf.Pallets[floor].IsShow)
                        {
                            shelf.Pallets[floor].IsShow = true;
                            shelf.Height += 20;
                        }

                        if (del)
                        {
                            shelf.Pallets[floor].State = PalletState.Disabled;
                        }
                        else if (string.IsNullOrEmpty(material))
                        {
                            shelf.Pallets[floor].State = PalletState.Idle;
                        }
                        else
                        {
                            shelf.Pallets[floor].Caption = string.Format("{0}|{1}", material, count);
                            shelf.Pallets[floor].State = GetState(location);
                        }
                    }
                    else
                    {
                        Shelf shelf = new Shelf()
                        {
                            ShelfName = key,
                            Caption = string.Format("{0}排 {1}列", row, col),
                            Name = key,
                            Row = row,
                            Column = col
                        };
                        if (!shelf.Pallets.ContainsKey(floor))
                        {
                            continue;
                        }
                        if (!shelf.Pallets[floor].IsShow)
                        {
                            shelf.Pallets[floor].IsShow = true;
                            shelf.Height += 20;
                        }

                        if (del)
                        {
                            shelf.Pallets[floor].State = PalletState.Disabled;
                        }
                        else if (string.IsNullOrEmpty(material))
                        {
                            shelf.Pallets[floor].State = PalletState.Idle;
                        }
                        else
                        {
                            shelf.Pallets[floor].Caption = string.Format("{0}|{1}", material, count);
                            shelf.Pallets[floor].State = GetState(location);
                        }
                        dicShelf.Add(key, shelf);
                        flpStorage.Controls.Add(shelf);

                        if (flpStorage.Controls.Count > 1 && (flpStorage.Controls[flpStorage.Controls.Count - 1] as Shelf).Row != (flpStorage.Controls[flpStorage.Controls.Count - 2] as Shelf).Row)
                        {
                            flpStorage.SetFlowBreak(flpStorage.Controls[flpStorage.Controls.Count - 2], true);
                        }

                    }//if (i < flpStorage.Controls.Count)

                }//for (int i = 0; i < dv.Count; i++)
            }
            catch (Exception ex)
            {
                LogHelper.Error("货位加载失败.", ex);
            }

        }

        /// <summary>
        /// 判断货位状态
        /// </summary>
        /// <param name="location">货位编码</param>
        /// <returns></returns>
        private PalletState GetState(string location)
        {
            DataView dv = new DataView(this.dtExpired);
            dv.RowFilter = string.Format("STSM_Place = '{0}'", location);
            if (dv.Count > 0)
            {
                return PalletState.Expired;
            }

            dv = new DataView(this.dtNearExpired);
            dv.RowFilter = string.Format("STSM_Place = '{0}'", location);
            if (dv.Count > 0)
            {
                return PalletState.NearExpired;
            }

            dv = new DataView(this.dtTooLongTimeStore);
            dv.RowFilter = string.Format("STSM_Place = '{0}'", location);
            if (dv.Count > 0)
            {
                return PalletState.TooLongTimeStore;
            }

            dv = new DataView(this.dtLongTimeStore);
            dv.RowFilter = string.Format("STSM_Place = '{0}'", location);
            if (dv.Count > 0)
            {
                return PalletState.LongTimeStore;
            }

            dv = new DataView(this.dtUnlock);
            dv.RowFilter = string.Format("STSM_Place = '{0}'", location);
            if (dv.Count > 0)
            {
                return PalletState.Using;
            }

            return PalletState.Lock;
        }




    }
}
