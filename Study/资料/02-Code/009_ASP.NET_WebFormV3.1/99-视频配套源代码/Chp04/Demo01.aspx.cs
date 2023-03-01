using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtName.Text = "请输入姓名";
            this.txtCode.Text = "请输入身份证号";
        }
    }

    protected void btOk_Click(object sender, EventArgs e)
    {
        this.lblInfo.Text = "真实姓名:" + this.txtName.Text + "<br />"
            + "身份证号:" + this.txtCode.Text + "<br />";
    }
}