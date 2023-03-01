using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo05_02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string info = "";
        if (Session["MyUser"] != null) //登录状态
        {
            MyUserEntity entity = (MyUserEntity)Session["MyUser"];
            info += "SessionID:" + Session.SessionID + "<br /><br />";
            info += "用户ID:" + entity.UserId + "<br /><br />";
            info += "账号:" + entity.UserAccount + "<br /><br />";
            info += "密码:" + entity.UserPwd + "<br /><br />";
            info += "邮箱:" + entity.UserMail + "<br /><br />";
            info += "电话:" + entity.UserPhone + "<br /><br />";
            info += "性别:" + entity.UserSex + "<br /><br />";
            this.lblUserInfo.Text = info;
        }
        else
        {
            this.lblUserInfo.Text = "当前您还没有登录!";
        }
    }
}