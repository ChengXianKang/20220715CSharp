using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Utils
{
    public partial class ListViewNote : ListView
    {
        public ListViewNote()
        {
            InitializeComponent();

            // 开启双缓冲
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //// Enable the OnNotifyMessage event so we get a chance to filter out 
            //// Windows messages before they get to the form's WndProc
            //this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            UpdateStyles();
        }

        public ListViewNote(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            // 开启双缓冲
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            //// Enable the OnNotifyMessage event so we get a chance to filter out 
            //// Windows messages before they get to the form's WndProc
            //this.SetStyle(ControlStyles.EnableNotifyMessage, true);

            UpdateStyles();
        }

        //protected override void OnNotifyMessage(Message m)
        //{
        //    //Filter out the WM_ERASEBKGND message
        //    if (m.Msg != 0x14)
        //    {
        //        base.OnNotifyMessage(m);
        //    }

        //}

        /// <summary>
        /// Note信息右键菜单-复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopyCode_Click(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count == 0) return;
            string content = "";
            foreach (ListViewItem item in this.SelectedItems)
            {
                content += item.SubItems[3].Text + "\r\n";
            }
            Clipboard.SetDataObject(content);
        }
        /// <summary>
        /// Note信息右键菜单-复制整行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            if (this.SelectedItems.Count == 0) return;
            string content = "";
            foreach (ListViewItem item in this.SelectedItems)
            {
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    content += item.SubItems[i].Text + "    ";
                }
                content += "\r\n";
            }
            Clipboard.SetDataObject(content);
        }
        /// <summary>
        /// Note信息右键菜单-全部清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuClear_Click(object sender, EventArgs e)
        {
            this.Items.Clear();
        }

        /// <summary>
        /// 添加记录信息
        /// </summary>
        /// <param name="state">信息状态</param>
        /// <param name="code">编码</param>
        /// <param name="message">提示信息</param>
        public void AddNote(NoteState state, string code, string message)
        {
            // 添加Note信息
            ListViewItem item = this.Items.Add((this.Items.Count + 1).ToString(), state.ToString());
            item.SubItems.AddRange(new string[]
                {
                    state.ToString(), //状态
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),//时间
                    code, //编码
                    message, //提示信息
                });

            switch (state)
            {
                case NoteState.Success:
                    // 字体设置为绿色
                    item.ForeColor = Color.Green;
                    break;

                case NoteState.Fail:
                    // 字体设置为红色
                    item.ForeColor = Color.Red;
                    break;

                case NoteState.OK:
                    // 字体设置为绿色
                    item.ForeColor = Color.Green;
                    break;

                case NoteState.NG:
                    // 字体设置为红色
                    item.ForeColor = Color.Red;
                    break;

                case NoteState.Error:
                    // 字体设置为橘黄色
                    item.ForeColor = Color.DarkOrange;
                    break;
            }

            this.colIdx.Width = -1;
            this.colState.Width = -1;
            this.colTime.Width = -1;
            this.colMessage.Width = -1;

            //item.Selected = true;
            if (this.SelectedItems.Count == 0)
            {
                item.EnsureVisible();
            }
        }

        /// <summary>
        /// 添加记录信息
        /// </summary>
        /// <param name="state">信息状态</param>
        /// <param name="time">时间</param>
        /// <param name="code">编码</param>
        /// <param name="message">提示信息</param>
        public void AddNote(NoteState state, DateTime time, string code, string message)
        {
            // 添加Note信息
            ListViewItem item = this.Items.Add((this.Items.Count + 1).ToString(), state.ToString());
            item.SubItems.AddRange(new string[]
                {
                    state.ToString(), //状态
                    time.ToString("yyyy-MM-dd HH:mm:ss.fff"),//时间
                    code, //编码
                    message, //提示信息
                });

            switch (state)
            {
                case NoteState.Success:
                    // 字体设置为绿色
                    item.ForeColor = Color.DarkGreen;
                    break;

                case NoteState.Fail:
                    // 字体设置为红色
                    item.ForeColor = Color.Red;
                    break;

                case NoteState.OK:
                    // 字体设置为绿色
                    item.ForeColor = Color.DarkGreen;
                    break;

                case NoteState.NG:
                    // 字体设置为红色
                    item.ForeColor = Color.Red;
                    break;

                case NoteState.Error:
                    // 字体设置为橘黄色
                    item.ForeColor = Color.Orange;
                    break;
            }

            this.colIdx.Width = -1;
            this.colState.Width = -1;
            this.colTime.Width = -1;
            this.colMessage.Width = -1;

            //item.Selected = true;
            if (this.SelectedItems.Count == 0)
            {
                item.EnsureVisible();
            }
        }
    }
}
