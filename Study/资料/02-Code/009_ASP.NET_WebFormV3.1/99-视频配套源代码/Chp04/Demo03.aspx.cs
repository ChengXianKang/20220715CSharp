using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btReg_Click(object sender, EventArgs e)
    {
        string str = "您提交的数据如下：\\n";
        str += "用户名:" + Request.Form["txtAccount"] + "\\n";
        str += "密码:" + Request.Form["txtPwd"] + "\\n";
        str += "密码确认:" + Request.Form["txtPwdOk"] + "\\n";
        str += "邮箱:" + Request.Form["txtMail"] + "\\n";
        str += "联系电话:" + Request.Form["txtPhone"] + "\\n";
        str += "性别:" + Request.Form["sex"] + "\\n";
        str += "专业:" + Request.Form["ddlProfessional"] + "\\n";
        str += "爱好:" + Request.Form["hobby"] + "\\n";
        if (Request.Files.Count > 0)
        {
            str += "头像:" + Request.Files[0].FileName + "\\n";
        }
        str += "自我介绍:" + Request.Form["txtSelft"] + "\\n";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('"+str+"');</script>");
    }
}