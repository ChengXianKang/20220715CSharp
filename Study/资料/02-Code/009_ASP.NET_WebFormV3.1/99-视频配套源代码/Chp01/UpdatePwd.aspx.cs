using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否登录
        if (Session["MyUser"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        //判断是否登录
        if (Session["MyUser"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }
        int UserId = (Session["MyUser"] as MyUserEntity).UserId;
        MyUserDAL dal = new MyUserDAL();
        MyUserEntity entity = dal.Detail(UserId);
        if (entity.UserPwd.Equals(this.txtPwdOld.Text) == false)
        {
            this.lblInfo.Text = "原始密码输入错误!";
            return;
        }
        if (this.txtUserPwd.Text.Equals(this.txtUserPwdConfirm.Text) == false)
        {
            this.lblInfo.Text = "密码和密码确认不一致!";
            return;
        }
        entity.UserPwd = this.txtUserPwd.Text;
        dal.Update(entity);
        ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>alert('密码修改成功!');window.location.href='Login.aspx';</script>");
    }
}