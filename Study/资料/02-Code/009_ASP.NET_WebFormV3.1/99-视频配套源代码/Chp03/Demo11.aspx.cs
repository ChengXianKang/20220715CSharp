using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindTree();
    }

    private void BindTree()
    {
        GoodTypeDAL dal = new GoodTypeDAL();
        List<GoodTypeEntity> listOne = dal.List().Where(p => p.ParentId == 0).ToList();
        foreach (var itemOne in listOne)
        {
            TreeNode nodeOne = new TreeNode(itemOne.TypeName, itemOne.TypeId.ToString());
            List<GoodTypeEntity> listTwo = dal.List().Where(p => p.ParentId == itemOne.TypeId).ToList();
            foreach (var itemTwo in listTwo)
            {
                TreeNode nodeTwo = new TreeNode(itemTwo.TypeName, itemTwo.TypeId.ToString());
                nodeOne.ChildNodes.Add(nodeTwo);
            }
            this.TreeView1.Nodes.Add(nodeOne);
        }
    }
}