using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utils.Common
{
    public partial class Pallet : UserControl
    {
        private bool _isshow;
        private string _caption;
        private string _materialname;
        private int _number;
        private PalletState _state;
        private string _floor;

        public Pallet()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN 
                return parms;
            }
        }
        /// <summary>
        /// 货位是否启用
        /// </summary>
        public bool IsShow
        {
            get
            {
                return _isshow;
            }
            set
            {
                _isshow = value;
                this.Visible = _isshow;
            }
        }

        /// <summary>
        /// 托盘标题
        /// </summary>
        public string Caption
        {
            get 
            { 
                return _caption; 
            }
            set 
            { 
                _caption = value;
                lblCaption.Text = value;
            }
        }

        /// <summary>
        /// 物料名
        /// </summary>
        public string MaterialName
        {
            get 
            {
                return _materialname; 
            }
            set 
            {
                _materialname = value;
                if (_number != 0)
                {
                    lblCaption.Text = string.Format("{0}({1})", value, _number);
                }
                else
                {
                    lblCaption.Text = _materialname;
                }
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number
        {
            get 
            {
                return _number; 
            }
            set 
            {
                _number = value;
                if (_number != 0)
                {
                    lblCaption.Text = string.Format("{0}({1})", _materialname, value);
                }
                else
                {
                    lblCaption.Text = _materialname;
                }
            }
        }

        /// <summary>
        /// 托盘状态
        /// </summary>
        public PalletState State
        {
            get 
            { 
                return _state; 
            }
            set
            {
                _state = value;
                switch (_state)
                {
                    case PalletState.Idle:
                        this.BackColor = Color.White;
                        lblCaption.Text = "空闲";
                        //lblState.ImageKey = "Idle";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Idle.png");
                        break;
                    case PalletState.Using:
                        this.BackColor = Color.LightGreen;
                        //lblState.ImageKey = "Using";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Using.png");
                        break;
                    case PalletState.Lock:
                        //this.BackColor = Color.LightSkyBlue;
                        this.BackColor = Color.LightCyan;
                        //lblState.ImageKey = "Lock";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Lock.png");
                        break;
                    case PalletState.Disabled:
                        this.BackColor = Color.DarkGray;
                        lblCaption.Text = "禁用";
                        //lblState.ImageKey = "Disabled";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Disabled.png");
                        break;

                    case PalletState.LongTimeStore:
                        this.BackColor = Color.Yellow;
                        //lblState.ImageKey = "Warning";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Warning.png");
                        break;
                    case PalletState.TooLongTimeStore:
                        this.BackColor = Color.Orange;
                        //lblState.ImageKey = "Error";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Error.png");
                        break;
                    case PalletState.NearExpired:
                        this.BackColor = Color.LightPink;
                        //lblState.ImageKey = "Error";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Error.png");
                        break;
                    case PalletState.Expired:
                        this.BackColor = Color.Red;
                        //lblState.ImageKey = "Error";
                        //lblState.Image = Image.FromFile(Application.StartupPath + "\\Pic\\Error.png");
                        break;
                    default:
                        this.BackColor = Color.White;
                        lblCaption.Text = "";
                        lblState.Image = null;
                        break;
                }

            }
        }
        /// <summary>
        /// 仓库名
        /// </summary>
        public string House { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 库位编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 排
        /// </summary>
        public string Row { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// 层
        /// </summary>
        public string Floor 
        { 
            get
            {
                return _floor;    
            }
            set
            {
                _floor = value;
                lblState.Text = _floor;
            }
        }

        protected override void CreateHandle()

        {

            if (!IsHandleCreated)

            {

                try

                {

                    base.CreateHandle();

                } 

                catch {}

                finally

                {

                    if (!IsHandleCreated)

                    {

                        base.RecreateHandle();

                    }

                }

            }            

        }  
    }

    /// <summary>
    /// 托盘状态
    /// </summary>
    public enum PalletState
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 空闲
        /// </summary>
        Idle = 1,
        /// <summary>
        /// 使用中
        /// </summary>
        Using = 2,
        /// <summary>
        /// 锁定
        /// </summary>
        Lock = 3,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled = 4,
        /// <summary>
        /// 超期存放>30天
        /// </summary>
        LongTimeStore = 5,
        /// <summary>
        /// 超期存放>60天
        /// </summary>
        TooLongTimeStore = 6,
        /// <summary>
        /// 临近过期（<=30天）
        /// </summary>
        NearExpired = 7,
        /// <summary>
        /// 已过期
        /// </summary>
        Expired = 8,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 5,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 6
        
    }
}
