using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo03_02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btUpload_Click(object sender, EventArgs e)
    {
        HttpFileCollection files = Request.Files;
        HttpPostedFile postFile = files["myFile"];
        if (postFile == null || postFile.ContentLength == 0)
        {
            this.lblInfo.Text = "您没有选择文件或您上传的文件是空文件";
            return;
        }
        string[] arrFileName = postFile.FileName.Split('.');
        string fileFix = arrFileName[arrFileName.Length - 1].ToLower();
        //通过后缀名判断文件类型
        if (!fileFix.Equals("jpg") && !fileFix.Equals("jpeg") && !fileFix.Equals("png") && !fileFix.Equals("gif"))
        {
            this.lblInfo.Text = "文件格式不正确!";
            return;
        }
        string myFileName = Guid.NewGuid() + "." + fileFix;
        postFile.SaveAs(Server.MapPath("upload") + "/" + myFileName);
        this.myImg.Visible = true;
        this.myImg.ImageUrl = "upload/" + myFileName;    
    }
}