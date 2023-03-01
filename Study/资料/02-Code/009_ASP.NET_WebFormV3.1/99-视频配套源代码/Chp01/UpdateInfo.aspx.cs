using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //判断是否登录
        if (Session["MyUser"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!IsPostBack)
            BindDetail();
    }

    private void BindDetail()
    {
        //判断是否登录
        if (Session["MyUser"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }
        //查登录用户的详情
        int UserId = (Session["MyUser"] as MyUserEntity).UserId;
        MyUserDAL dal = new MyUserDAL();

        MyUserEntity entity = dal.Detail(UserId);

        this.lblAcc.Text = entity.UserAccount;
        this.txtUserMail.Text = entity.UserMail;
        this.txtUserPhone.Text = entity.UserPhone;
        if (entity.UserSex.Equals("男"))
            this.rbBoy.Checked = true;
        else
            this.rbGirl.Checked = true;
        this.ddlUserPro.SelectedValue = entity.UserPro;
        foreach (var item in this.pHobby.Controls)
        {
            if (item.GetType() == typeof(CheckBox))
            {
                if (entity.UserHobby.Split(',').Contains((item as CheckBox).Text))
                {
                    (item as CheckBox).Checked = true;
                }
            }
        }
        this.txtUserSelf.Text = entity.UserSelf;
    }


    protected void btReg_Click(object sender, EventArgs e)
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
        entity.UserMail = this.txtUserMail.Text;
        entity.UserPhone = this.txtUserPhone.Text;
        entity.UserSex = this.rbBoy.Checked == true ? "男" : "女";
        entity.UserPro = this.ddlUserPro.SelectedValue;
        string strHobby = "";
        foreach (var item in this.pHobby.Controls)
        {
            if (item.GetType() == typeof(CheckBox))
            {
                if (((CheckBox)item).Checked == true)
                {
                    if (!strHobby.Equals(""))
                        strHobby += ",";
                    strHobby += ((CheckBox)item).Text;
                }
            }
        }
        entity.UserHobby = strHobby;
        entity.UserSelf = this.txtUserSelf.Text;
        dal.Update(entity);
        ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>alert('基本信息修改成功!');window.location.href='UpdateInfo.aspx';</script>");
    }
}