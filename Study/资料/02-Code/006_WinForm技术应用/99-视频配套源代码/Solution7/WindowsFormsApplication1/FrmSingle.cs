using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class FrmSingle : Form
    {
        private static FrmSingle frm;  //单例
        public static FrmSingle CreateForm()
        {
            if (frm == null || frm.IsDisposed == true)
            {
                frm = new FrmSingle();
            }
            return frm;
        }
        public FrmSingle()
        {
            InitializeComponent();
        }

        private void FrmSingle_Load(object sender, EventArgs e)
        {

        }
    }
}
