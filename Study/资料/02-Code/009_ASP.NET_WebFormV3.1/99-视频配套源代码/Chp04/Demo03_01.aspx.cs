using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo03_01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str = "";
            str += "客户端主机所使用的语言:" + Request.UserLanguages[0] + "<br /><br />";
            str += "客户端主机的DNS名称:" + Request.UserHostName + "<br /><br />";
            str += "客户端主机的IP地址:" + Request.UserHostAddress + "<br /><br />";
            str += "客户端浏览器版本:" + Request.UserAgent + "<br /><br />";
            str += "当前要求的URL:" + Request.Url + "<br /><br />";
            str += "客户端网页的传送方式(Get/Post):" + Request.RequestType + "<br /><br />";
            str += "当前页面的URL:" + Request.RawUrl + "<br /><br />";
            str += "当前网页在服务器端的实际路径:" + Request.PhysicalPath + "<br /><br />";
            str += "目前联机的安全性:" + Request.IsSecureConnection + "<br /><br />";
            str += "目前联机是否有效:" + Request.IsAuthenticated + "<br /><br />";
            str += "客户端浏览器的字符设置:" + Request.ContentEncoding + "<br /><br />";
            str += "当前运行程序的服务器端虚拟目录:" + Request.ApplicationPath + "<br /><br />";
            this.lblInfo.Text = str;
        }
    }
}