using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListViewItem item1 = new ListViewItem("云中君");
            item1.ImageIndex = 2;
            item1.SubItems.Add("中等");
            item1.SubItems.Add("超强");
            item1.SubItems.Add("中等");
            item1.SubItems.Add("难");
            this.listView1.Items.Add(item1);

            ListViewItem item2 = new ListViewItem("猪八戒");
            item2.Name = "zbj";
            item2.ImageIndex = 4;
            item2.SubItems.Add("超强");
            item2.SubItems.Add("中等");
            item2.SubItems.Add("中等");
            item2.SubItems.Add("中等");
            this.listView1.Items.Add(item2);

            //this.listView1.View = View.LargeIcon;

            //直接使用ListViewItem对象名删除
            //this.listView1.Items.Remove(item2);
            //通过索引小标进行删除
            //this.listView1.Items.RemoveAt(1);
            //通过键来删除，键就是ListViewItem的name
            //this.listView1.Items.RemoveByKey("zbj");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            if (item == null)
                return;
            string str = "";
            str += "名字:" + item.Text + "\n";
            str += "生存能力:" + item.SubItems[1].Text + "\n";
            str += "攻击伤害:" + item.SubItems[2].Text + "\n";
            str += "技能效果:" + item.SubItems[3].Text + "\n";
            str += "上手难度:" + item.SubItems[4].Text + "\n";
            MessageBox.Show(str);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("您没有选中项目!");
                return;
            }
            ListViewItem item = this.listView1.SelectedItems[0];
            this.listView1.Items.Remove(item);
        }

        private void 大图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.LargeIcon;
        }

        private void 小图标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.SmallIcon;
        }

        private void 详细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Details;
        }

        private void 列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.List;
        }

        private void 平铺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView1.View = View.Tile;
        }
    }
}
