<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo02.aspx.cs" Inherits="Demo02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        账号：<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="账号不能为空"
          ForeColor="Red"   ControlToValidate="txtAcc" Display="Static">
        </asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" />
    </p>
    </form>
</body>
</html>
