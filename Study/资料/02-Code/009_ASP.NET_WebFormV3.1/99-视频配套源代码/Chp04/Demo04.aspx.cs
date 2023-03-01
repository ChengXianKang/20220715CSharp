using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo04 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<h1>ASP.NET 你好!</h1>");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Write("<script>alert('ASP.NET 你好!');</script>");
    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Demo03.aspx");
    }


    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Write("<h1>ASP.NET 你好!</h1>");
        Response.End();
        Response.Write("<h1>ASP.NET 你好!</h1>");
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.WriteFile("readme.txt");
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        int cnt = 1;
        while (true)
        {
            Response.Write("ASP.NET 你好!");
            cnt++;
            if (cnt == 1000)
            {
                Response.Flush(); //将内容强制输出到浏览器
                Response.Clear(); //清除缓冲区内容
                cnt = 1;
            }
        }

    }

}