using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace MDC
{
    public partial class dlgOrder : Form
    {
        /// <summary>
        /// 失败提示框
        /// </summary>
        private FailMessage failMsg;
        /// <summary>
        /// 确认提示框
        /// </summary>
        private WarnMessage warnMsg;

        private string _order;
        private string _nextOrder;
        private int _orderNum;
        private int _feedNum;
        private int _inputNum;
        private string _note;
        private string _orderToDo;

        /// <summary>
        /// 生产中工单
        /// </summary>
        public string Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }
        /// <summary>
        /// 待产工单
        /// </summary>
        public string NextOrder
        {
            get
            {
                return _nextOrder;
            }
            set
            {
                _nextOrder = value;
            }
        }
        /// <summary>
        /// 工单数量
        /// </summary>
        public int OrderNum
        {
            get
            {
                return _orderNum;
            }
            set
            {
                _orderNum = value;
            }
        }
        /// <summary>
        /// 投料数量
        /// </summary>
        public int FeedNum
        {
            get
            {
                return _feedNum;
            }
            set
            {
                _feedNum = value;
            }
        }
        /// <summary>
        /// 清洗投入数量
        /// </summary>
        public int InputNum
        {
            get
            {
                return _inputNum;
            }
            set
            {
                _inputNum = value;
            }
        }

        /// <summary>
        /// 工单完结/挂起说明
        /// </summary>
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
            }
        }

        /// <summary>
        /// 上一工单操作模式（Over：工单完结，Hang：工单挂起）
        /// </summary>
        public string OrderToDo
        {
            get
            {
                return _orderToDo;
            }
            set
            {
                _orderToDo = value;
            }
        }

        //public dlgOrder()
        //{
        //    InitializeComponent();
        //}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Order">在制工单</param>
        /// <param name="NextOrder">待产工单</param>
        /// <param name="OrderNum">工单数量</param>
        /// <param name="FeedNum">投料数量</param>
        /// <param name="InputNum">投入数量</param>
        public dlgOrder(string Order, string NextOrder, int OrderNum, int FeedNum, int InputNum)
        {
            InitializeComponent();
            _order = Order;
            _nextOrder = NextOrder;
            _orderNum = OrderNum;
            _feedNum = FeedNum;
            _inputNum = InputNum;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dlgOrder_Load(object sender, EventArgs e)
        {
            txtProductionOrder.Text = _order;
            txtNextOrder.Text = _nextOrder;
            txtOrderNumber.Text = _orderNum.ToString();
            txtFeedNumber.Text = _feedNum.ToString();
            txtInputNumber.Text = _inputNum.ToString();
            txtNoScan.Text = (_orderNum - _inputNum).ToString();

            txtCount1.Text = (_orderNum - _feedNum).ToString();
            txtSum.Text = txtCount1.Text;
            txtCount2.Text = "0";
            txtCount3.Text = "0";

        }
        /// <summary>
        /// 仅能输入数字
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>

        public bool isNumber(int keyCode)
        {
            // 数字
            if (keyCode >= 48 && keyCode <= 57) return true;
            // 小数字键盘
            if (keyCode >= 96 && keyCode <= 105) return true;
            // Backspace, del, 左右方向键
            if (keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39) return true;
            return false;
        }

        
        /// <summary>
        /// 只能输入数字，Backspace, del, 左右方向键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8 && e.KeyChar != (char)46 && e.KeyChar != (char)37 && e.KeyChar != (char)39 && e.KeyChar != (char)('-') && !(Char.IsNumber(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 重新计算合计数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCount_TextChanged(object sender, EventArgs e)
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int.TryParse(txtCount1.Text, out count1);
            int.TryParse(txtCount2.Text, out count2);
            int.TryParse(txtCount3.Text, out count3);
            int sum = count1 + count2 + count3;

            txtSum.Text = sum.ToString();
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            int count1 = YJ.Common.Utils.StrToInt(txtCount1.Text, 0);
            if (count1 != (_orderNum - _feedNum))
            {
                failMsg = new FailMessage("输入的未投料数与实际不符！", 9999);
                failMsg.ShowDialog(this);
                txtCount1.SelectAll();
                txtCount1.Focus();
                return;
            }
            int sum = YJ.Common.Utils.StrToInt(txtSum.Text, 0);
            if (sum != (_orderNum - _inputNum))
            {
                failMsg = new FailMessage("输入的玻璃差异数量与实际不符！", 9999);
                failMsg.ShowDialog(this);
                txtCount2.SelectAll();
                txtCount2.Focus();
                return;
            }

            _orderToDo = rdoOver.Checked ? "完结" : "挂起";
            string msg = string.Format("即将{0}工单{1},\r\n并开始工单{2}.\r\n是否继续？", _orderToDo, _order, _nextOrder);
            warnMsg = new WarnMessage(msg, MessageBoxButtons.OKCancel, 9999, "Cancel");
            if (warnMsg.ShowDialog(this) == DialogResult.OK)
            {
                _note = string.Format("未投料：{0}({1})|丢失：{2}({3})|来料不良：{4}({5})", txtCount1.Text, txtNote1.Text, txtCount2.Text, txtNote2.Text, txtCount3.Text, txtNote3.Text);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }




        

    }
}
