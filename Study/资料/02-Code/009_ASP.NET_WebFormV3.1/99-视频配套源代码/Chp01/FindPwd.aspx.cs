using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FindPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btNext1_Click(object sender, EventArgs e)
    {
        MyUserDAL dal = new MyUserDAL();
        MyUserEntity entity = dal.Detail(this.txtAcc.Text);
        if (entity == null)
        {
            this.lblMsg1.Text = "用户名输入错误!";
            return;
        }
        this.lblQuestion.Text = entity.Question;
        this.MultiView1.ActiveViewIndex++;
    }

    protected void btNext2_Click(object sender, EventArgs e)
    {
        MyUserDAL dal = new MyUserDAL();
        MyUserEntity entity = dal.Detail(this.txtAcc.Text);
        if (entity.Answer.Equals(this.txtAnswer.Text) == false)
        {
            this.lblMsg2.Text = "密码答案回答错误!";
            return;
        }
        this.MultiView1.ActiveViewIndex++;
    }

    protected void btPrev2_Click(object sender, EventArgs e)
    {
        this.MultiView1.ActiveViewIndex--;
    }

    protected void btPrev3_Click(object sender, EventArgs e)
    {
        this.MultiView1.ActiveViewIndex--;
    }

    protected void btOk_Click(object sender, EventArgs e)
    {
        if (this.txtPwd.Text.Equals(this.txtPwdConfirm.Text) == false)
        {
            this.lblMsg3.Text = "两次密码输入不一致";
            return;
        }
        MyUserDAL dal = new MyUserDAL();
        MyUserEntity entity = dal.Detail(this.txtAcc.Text);
        entity.UserPwd = this.txtPwd.Text;
        dal.Update(entity);
        ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>alert('密码修改成功!');window.location.href='Login.aspx';</script>");
    }
}