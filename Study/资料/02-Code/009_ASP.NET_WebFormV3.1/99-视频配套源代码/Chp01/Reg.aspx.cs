using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
            BindQuestion(); 
    }

    private void BindQuestion()
    {
        MyQuestionDAL dal = new MyQuestionDAL();
        this.ddlQuestion.DataSource = dal.List();
        this.ddlQuestion.DataValueField = "QuestionTitle";
        this.ddlQuestion.DataTextField = "QuestionTitle";
        this.ddlQuestion.DataBind();
        this.ddlQuestion.Items.Insert(0, new ListItem("--请选择--", ""));
    }

    protected void btReg_Click(object sender, EventArgs e)
    {
        MyUserDAL dal = new MyUserDAL();
        if (dal.IsAccCanUse(this.txtUserAccount.Text) == false) //用户名不可用，占用
        {
            this.lblInfo.Text = "对不起，用户名被占用!";
            return;
        }
        MyUserEntity entity = new MyUserEntity();
        entity.UserAccount = this.txtUserAccount.Text;
        entity.UserPwd = this.txtUserPwd.Text;
        entity.Question = this.txtQuestion.Text;
        entity.Answer = this.txtAnswer.Text;
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
        if (this.fileUserPhoto.HasFile)  //如果用户选择了文件
        {
            string fileName = this.fileUserPhoto.FileName;   //获取文件名
            string[] arrFileName = fileName.Split('.');  //将文件名使用 点切割成数组
            string fileFix = arrFileName[arrFileName.Length - 1];  //获取文件后缀名
            string serverFileName = Guid.NewGuid().ToString() + "." + fileFix;  //生成唯一的文件名
            string serverFullName = Server.MapPath("uploadimg") + "\\" + serverFileName;  //生成完成的路径+文件名
            this.fileUserPhoto.SaveAs(serverFullName);
            entity.UserPhoto = serverFileName;
        }
        dal.Add(entity);
        ClientScript.RegisterStartupScript(this.GetType(), "js", "<script>alert('注册成功!');window.location.href='Login.aspx';</script>");
    }

}