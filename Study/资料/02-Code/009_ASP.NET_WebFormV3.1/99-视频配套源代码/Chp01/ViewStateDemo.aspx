<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewStateDemo.aspx.cs" Inherits="ViewStateDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            <asp:Button ID="Button1" runat="server" Text="点击我记录点击次数" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" Text="按钮被点击了【0】次"></asp:Label>
        </p>

    </form>
</body>
</html>
