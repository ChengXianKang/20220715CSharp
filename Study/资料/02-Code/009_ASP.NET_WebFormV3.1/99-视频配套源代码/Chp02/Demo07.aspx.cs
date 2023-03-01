using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo07 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CustomValidator1_Validate(object sender,  ServerValidateEventArgs e)
    {
        string str = this.txtAcc.Text;
        //假设liubei,guanyu,zhangfei是已经注册的用户
        if (str.Equals("liubei") || str.Equals("guanyu") || str.Equals("zhangfei"))
            e.IsValid = false;
        else
            e.IsValid = true;
    }

    protected void btOk_Click(object sender, EventArgs e)
    {
        if(Page.IsValid == true)
        {
            Response.Write("数据提交成功!");
        } 
             
    }
}