using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePhoto : System.Web.UI.Page
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
        this.Image1.ImageUrl = "uploadimg/" + entity.UserPhoto;
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
        dal.Update(entity);
        ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>alert('头像修改成功!');window.location.href='UpdatePhoto.aspx';</script>");
    }
}