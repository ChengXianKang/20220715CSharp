using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utils
{
    public partial class dlgImageView : Form
    {
        public dlgImageView(string caption, Image image)
        {
            InitializeComponent();

            this.Text = caption;
            this.BackgroundImage = image;
        }
    }
}
