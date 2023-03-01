<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo05.aspx.cs" Inherits="Demo05" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        邮箱：<asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="邮箱格式不正确" ForeColor="Red"
            ControlToValidate="txtMail" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" >
        </asp:RegularExpressionValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" />
    </p>
    </form>
</body>
</html>
