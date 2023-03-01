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

    protected void btDown_Click(object sender, EventArgs e)
    {
        //if(如果没有登录)
        //    退出；

        Response.Clear(); //清空删除缓存中的所有的输出
        Response.Buffer = true; //使用页面缓存
        Response.ContentType = "application/unknow";
        Response.AddHeader("content-disposition", "attchment;filename=upload.zip");
        //string filename = Server.MapPath("upload.zip");
        string filename = "E:\\upload.zip";
        Response.TransmitFile(filename);
        Response.End();
    }
}