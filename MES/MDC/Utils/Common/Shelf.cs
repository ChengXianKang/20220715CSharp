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
    public partial class Shelf : UserControl
    {
        private string _caption;
        private string _shelfname;
        private string _row;
        private string _column;
        public Dictionary<string, Pallet> Pallets;

        public Shelf()
        {
            InitializeComponent();
            Pallets = new Dictionary<string, Pallet>()
            {
                {"001", Pallet1},
                {"002", Pallet2},
                {"003", Pallet3},
                {"004", Pallet4}
            };
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
        /// 货架标题
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
                lblCaption.Text = _caption;
            }
        }

        /// <summary>
        /// 货架名称
        /// </summary>
        public string ShelfName
        {
            get
            {
                return _shelfname;
            }
            set
            {
                _shelfname = value;
                
            }
        }

        /// <summary>
        /// 排
        /// </summary>
        public string Row 
        { 
            get
            {
                return _row;
            }
            set
            {
                _row = value;
                if (string.IsNullOrWhiteSpace(_row) && string.IsNullOrWhiteSpace(_column))
                {
                    _shelfname = string.Format("{0}排 {1}列", _row, _column);
                    lblCaption.Text = _shelfname; 
                }
            }
        }

        /// <summary>
        /// 列
        /// </summary>
        public string Column 
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
                if (string.IsNullOrWhiteSpace(_row) && string.IsNullOrWhiteSpace(_column))
                {
                    _shelfname = string.Format("{0}排 {1}列", _row, _column);
                    lblCaption.Text = _shelfname;
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
        ///// <summary>
        ///// 库位编码
        ///// </summary>
        //public string Code { get; set; }

        ///// <summary>
        ///// 层数
        ///// </summary>
        //public int FloorCount { get; set; }

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
}
