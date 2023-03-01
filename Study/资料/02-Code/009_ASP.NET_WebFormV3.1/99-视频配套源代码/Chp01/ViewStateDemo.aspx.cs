using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewStateDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            ViewState["count"] = 0;
    }

    //int count = 0;
    protected void Button1_Click(object sender, EventArgs e)
    {
        //count++;
        //this.Label1.Text = "按钮被点击了【"+count+"】次";
        ViewState["count"] = int.Parse(ViewState["count"].ToString()) + 1;
        this.Label1.Text = "按钮被点击了【" + ViewState["count"].ToString() + "】次";
    }

}