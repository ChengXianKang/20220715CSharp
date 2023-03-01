<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo03.aspx.cs" Inherits="Demo03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        密码：<asp:TextBox ID="txtPwd1" runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        密码确认：<asp:TextBox ID="txtPwd2" runat="server" TextMode="Password"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ErrorMessage="两次密码输入不一致" ForeColor="Red"
            ControlToValidate="txtPwd2" ControlToCompare="txtPwd1" Type="String"
             Operator="Equal" >
        </asp:CompareValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" />
    </p>
    </form>
</body>
</html>
