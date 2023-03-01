<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo08.aspx.cs" Inherits="Demo08" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        电子邮箱：<asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="邮箱格式不正确" ForeColor="Red" ControlToValidate="txtMail"
             ValidationExpression="^\w+@\w+\.[a-zA-Z]{2,3}(\.[a-zA-Z]{2,3})?$">
        </asp:RegularExpressionValidator>
    </p>
    <p>
        手机号码：<asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
            ErrorMessage="手机号码格式不正确" ForeColor="Red" ControlToValidate="txtMobile"
             ValidationExpression="^(13|15|18)\d{9}$">
        </asp:RegularExpressionValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" />
<%--        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
             ForeColor="Red" HeaderText="错误信息如下：" ShowSummary="true"
             DisplayMode="BulletList" />--%>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
             ForeColor="Red" HeaderText="错误信息如下：" ShowSummary="false"
             ShowMessageBox="true" DisplayMode="BulletList" />
    </p>
    </form>
</body>
</html>
