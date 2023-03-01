using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btUpload_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile == false)
        {
            this.lblInfo.Text = "对不起，您没有选择文件!";
            return;
        }
        string[] arrFileNmae = this.FileUpload1.FileName.Split('.');
        string fileFix = arrFileNmae[arrFileNmae.Length - 1].ToLower();
        if (!fileFix.Equals("jpg") && !fileFix.Equals("jpeg") && !fileFix.Equals("gif") && !fileFix.Equals("png"))
        {
            this.lblInfo.Text = "对不起，文件格式不正确!";
            return;
        }
        string fileName = Guid.NewGuid().ToString() + "." + fileFix;
        string fullName = Server.MapPath("upload") + "/" + fileName;
        this.FileUpload1.SaveAs(fullName);
        this.Image1.ImageUrl = "upload/" + fileName;
        this.Image1.Visible = true;
    }

}