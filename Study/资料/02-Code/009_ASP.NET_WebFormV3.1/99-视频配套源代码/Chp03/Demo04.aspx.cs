using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo04 : System.Web.UI.Page
{
    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        RAR = 8297,
        exe = 7790,
        xml = 6063,
        html = 6033,
        aspx = 239187,
        cs = 117115,
        js = 119105,
        txt = 210187,
        sql = 255254
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btUpload_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile == false)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>window.parent.showInfo('对不起，您没有选择文件!')</script>");
            return;
        }
        string[] arrFileNmae = this.FileUpload1.FileName.Split('.');
        string fileFix = arrFileNmae[arrFileNmae.Length - 1].ToLower();
        //根据后缀名进行判断文件类型
        if (!fileFix.Equals("jpg") && !fileFix.Equals("jpeg") && !fileFix.Equals("gif") && !fileFix.Equals("png"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>window.parent.showInfo('对不起，文件格式不正确!')</script>");
            return;
        }
        //根据文件内容进行判断文件类型
        FileExtension[] arrFe = { FileExtension.GIF, FileExtension.JPG, FileExtension.PNG };
        if (ISAllowUpload(this.FileUpload1, arrFe) == false)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>window.parent.showInfo('对不起，文件格式不正确!')</script>");
            return;
        }
        string fileName = Guid.NewGuid().ToString() + "." + fileFix;
        string fullName = Server.MapPath("upload") + "/" + fileName;
        this.FileUpload1.SaveAs(fullName);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>window.parent.showImg('"+ fileName + "')</script>");
        //this.Image1.ImageUrl = "upload/" + fileName;
        //this.Image1.Visible = true;
    }

    private bool ISAllowUpload(FileUpload myFile, FileExtension[] fileExt)
    {
        int fileLen = myFile.PostedFile.ContentLength;
        byte[] imgArray = new byte[fileLen];
        myFile.PostedFile.InputStream.Read(imgArray, 0, fileLen);
        string fileValue = imgArray[0].ToString() + imgArray[1].ToString();
        bool result = false; //默认类型是不允许的
        foreach (FileExtension item in fileExt)
        {
            if (int.Parse(fileValue) == (int)item)
            {
                result = true;
                break;
            }
        }
        return result;
    }


    protected void btOk_Click(object sender, EventArgs e)
    {
        string str = "提交数据如下: \\n";
        str += "账号:" + this.txtAcc.Text + "\\n";
        str += "密码:" + this.txtPwd.Text + "\\n";
        str += "图片:" + Request["hdImgFileName"].ToString() + "\\n";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "JS", "<script>window.parent.alert('"+str+"');</script>");
    }
}