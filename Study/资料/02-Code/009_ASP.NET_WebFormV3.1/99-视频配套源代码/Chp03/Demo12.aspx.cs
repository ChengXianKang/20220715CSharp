using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo12 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindTree();
    }
    GoodTypeDAL dal = new GoodTypeDAL();
    private void BindTree()
    {
        
        List<GoodTypeEntity> list = dal.List().Where(p => p.ParentId == 0).ToList();
        List<GoodTypeEntity> listAll = dal.List().ToList();
        foreach (var item in list)
        {
            TreeNode node = new TreeNode(item.TypeName, item.TypeId.ToString());
            //调用函数，实现自动绑定下面所有级别的数据（递归函数）：参数:当前节点，所有集合数据
            BindSonNodes(node, listAll);
            this.TreeView1.Nodes.Add(node);
        }
    }

    private void BindSonNodes(TreeNode parentNode, List<GoodTypeEntity> listAll)
    {
        List<GoodTypeEntity> list = dal.List().Where(p => p.ParentId == int.Parse(parentNode.Value)).ToList();
        foreach (var item in list)
        {
            TreeNode node = new TreeNode(item.TypeName, item.TypeId.ToString());
            BindSonNodes(node, listAll);
            parentNode.ChildNodes.Add(node);
        }
    }

}