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
        public bool IsMouseClick { get; set; } //解决窗体加载时候自动调用listview的AfterSelect事件
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 方案一：使用TreeNode对象添加节点
            ////创建根节点
            //TreeNode root = new TreeNode("武汉********学院",0,0);
            //root.Name = "s1";
            //this.treeView1.Nodes.Add(root); //将根节点附加到控件上
            ////创建专业节点
            //TreeNode tnPro1 = new TreeNode("计算机技术", 0, 0);
            //tnPro1.Name = "p1";
            //TreeNode tnPro2 = new TreeNode("电子商务", 0, 0);
            //tnPro2.Name = "p2";
            ////将专业节点添加到根节点下面作为子节点
            //root.Nodes.Add(tnPro1);
            //root.Nodes.Add(tnPro2);
            ////创建课程节点
            //TreeNode tnCourse1 = new TreeNode("C#程序开发",1,1);
            //tnCourse1.Name = "c1";
            //TreeNode tnCourse2 = new TreeNode("SQL SERVER数据库管理", 1, 1);
            //tnCourse2.Name = "c2";
            //TreeNode tnCourse3 = new TreeNode("搜索引擎优化", 1, 1);
            //tnCourse3.Name = "c3";
            //TreeNode tnCourse4 = new TreeNode("网络营销推广", 1, 1);
            //tnCourse4.Name = "c4";
            ////将课程节点添加到专业节点下面作为子节点
            //tnPro1.Nodes.Add(tnCourse1);
            //tnPro1.Nodes.Add(tnCourse2);
            //tnPro2.Nodes.Add(tnCourse3);
            //tnPro2.Nodes.Add(tnCourse4);
            ////展开所有节点
            //this.treeView1.ExpandAll();

            //删除节点(使用对象删除)
            //this.treeView1.Nodes.Remove(tnCourse3);
            //使用下标删除
            //this.treeView1.Nodes[0].Nodes[1].Nodes.RemoveAt(0);
            //通过键来删除
            //this.treeView1.Nodes["s1"].Nodes["p2"].Nodes.RemoveByKey("c3");
            #endregion

            #region 方案二：使用键值对的方式直接添加节点
            this.treeView1.Nodes.Add("001", "武汉******学院",0,0);
            this.treeView1.Nodes["001"].Nodes.Add("001001", "计算机技术",0,0);
            this.treeView1.Nodes["001"].Nodes.Add("001002", "电子商务", 0, 0);
            this.treeView1.Nodes["001"].Nodes["001001"].Nodes.Add("001001001", "C#程序设计",1,1);
            this.treeView1.Nodes["001"].Nodes["001001"].Nodes.Add("001001002", "SQL SERVER程序设计", 1, 1);
            this.treeView1.Nodes["001"].Nodes["001002"].Nodes.Add("001002001", "搜索引擎优化", 1, 1);
            this.treeView1.Nodes["001"].Nodes["001002"].Nodes.Add("001002002", "网络营销推广", 1, 2);
            this.treeView1.ExpandAll();

            //通过键来删除
            //this.treeView1.Nodes["001"].Nodes["001002"].Nodes.RemoveByKey("001002001");
            #endregion

            this.IsMouseClick = false;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.IsMouseClick == false)
                return;
            TreeNode node = this.treeView1.SelectedNode;
            MessageBox.Show(node.Name + "," + node.Text);
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            this.IsMouseClick = true;
        }
    }
}
