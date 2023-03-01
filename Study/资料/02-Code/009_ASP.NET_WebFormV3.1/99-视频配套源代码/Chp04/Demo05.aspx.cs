using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo05 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        MyUserDAL dal = new MyUserDAL();
        MyUserEntity entity = dal.Login(this.txtAccount.Text, this.txtPwd.Text);
        if (entity != null) //登录成功
        {
            //使用Session存储用户编号
            //Session["UserId"] = entity.UserId;

            //直接将实体对象进行存储到Session
            Session["MyUser"] = entity;
            Session.Timeout = 30; //Session过期时间设置为30分钟

            Response.Redirect("Demo05_01.aspx");
        }
        else //登录失败
        {
            this.lblErrInfo.Text = "用户名或密码错误!";
            return;
        }
    }
}