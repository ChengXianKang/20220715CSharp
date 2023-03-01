using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo09 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindTree();
    }

    private void BindTree()
    {
        //一级节点
        TreeNode root = new TreeNode("后台管理", "后台管理");
        //二级节点
        TreeNode node1 = new TreeNode("用户管理", "用户管理");
        TreeNode node2 = new TreeNode("商品管理", "商品管理");
        //三级节点
        TreeNode node1_1 = new TreeNode("用户查询", "用户查询");
        TreeNode node1_2 = new TreeNode("用户新增", "用户新增");
        TreeNode node2_1 = new TreeNode("商品查询", "商品查询");
        TreeNode node2_2 = new TreeNode("商品新增", "商品新增");
        //将三级节点绑定到二级节点的下层
        node1.ChildNodes.Add(node1_1);
        node1.ChildNodes.Add(node1_2);
        node2.ChildNodes.Add(node2_1);
        node2.ChildNodes.Add(node2_2);
        //将二级放到一级的下面
        root.ChildNodes.Add(node1);
        root.ChildNodes.Add(node2);
        //将TreeNode和TreeView控件进行关联
        this.TreeView1.Nodes.Add(root);

    }
}