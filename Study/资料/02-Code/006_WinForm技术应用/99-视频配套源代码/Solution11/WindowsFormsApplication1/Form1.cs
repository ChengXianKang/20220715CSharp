﻿using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void BindData()
        {
            DBHelper.PrepareSql("exec procSelectMember");
            this.dataGridView1.DataSource = DBHelper.ExecQuery();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }
    }
}
