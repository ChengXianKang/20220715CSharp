<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo04.aspx.cs" Inherits="Demo04" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Response</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="打印文本信息" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="输出JS代码" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="页面重定向" OnClick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" Text="结束输出" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="输出文件内容" OnClick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" Text="大数据的输出" OnClick="Button6_Click" />
    </div>
    </form>
</body>
</html>
