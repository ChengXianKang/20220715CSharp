using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.lblValidate.Text = GetRandomValidate(5);
    }

    private string GetRandomValidate(int len)
    {
        string result = "";
        string strList = "abcdefghijkmnprstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ0123456789";
        Random rnd = new Random();
        for (int i = 1; i <= len; i++)
        {
            result += strList[rnd.Next(0,strList.Length)];
        }
        return result;
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        //判断验证码
        if (this.txtValidate.Text.ToLower().Equals(this.lblValidate.Text.ToLower()) == false)
        {
            this.lblErrInfo.Text = "验证码错误!";
            this.lblValidate.Text = GetRandomValidate(5);
            return;
        }
        //判断用户名和密码正确性
        MyUserDAL dal = new MyUserDAL();
        MyUserEntity entity = dal.Login(this.txtAccount.Text, this.txtPwd.Text);
        if (entity == null)
        {
            this.lblErrInfo.Text = "用户名或密码错误!";
            this.lblValidate.Text = GetRandomValidate(5);
            return;
        }
        Session["MyUser"] = entity;
        Response.Redirect("Main.aspx");
    }
}