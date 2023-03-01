<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo06_Play.aspx.cs" Inherits="Demo06_Play" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>我是娱乐频道</h1>
        <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
        <p>
            <a href="Demo06_Movie.aspx">电影</a> | 
            <a href="Demo06_Music.aspx">音乐</a>
        </p>
    </div>
    </form>
</body>
</html>
