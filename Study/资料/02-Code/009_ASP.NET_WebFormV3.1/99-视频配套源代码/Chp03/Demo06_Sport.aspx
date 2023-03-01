<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo06_Sport.aspx.cs" Inherits="Demo06_Sport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>我是体育频道</h1>
        <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
        <p>
            <a href="Demo06_Basketball.aspx">篮球</a> | 
            <a href="Demo06_Football.aspx">足球</a>
        </p>
    </div>
    </form>
</body>
</html>
