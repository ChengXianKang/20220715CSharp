using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo10 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindTree();
    }

    private void BindTree()
    {
        GoodTypeDAL dal = new GoodTypeDAL();
        List<GoodTypeEntity> list = dal.List().Where(p => p.ParentId == 0).ToList();
        foreach (var item in list)
        {
            TreeNode node = new TreeNode(item.TypeName, item.TypeId.ToString());
            this.TreeView1.Nodes.Add(node);
        }
    }
}