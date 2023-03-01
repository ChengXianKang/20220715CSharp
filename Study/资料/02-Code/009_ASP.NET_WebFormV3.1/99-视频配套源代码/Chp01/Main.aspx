<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>会员中心的主界面</title>
    <style type="text/css">
        #welcome { font-size:30px; font-weight:bold;}
        #updateLink { margin-left:60px;}
        #content { text-align:center; margin-top:50px; font-size:60px; font-weight:bold;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <span id="welcome">登录成功，欢迎来到*******系统！</span>
            <span id="updateLink">
                <a href="UpdateInfo.aspx" target="_blank">修改基本信息</a> |
                <a href="UpdatePhoto.aspx" target="_blank">修改头像</a> |
                <a href="UpdatePwd.aspx" target="_blank">修改密码</a>
            </span>
        </div>
        <div id="content">
            会员中心主界面
        </div>
    </form>
</body>
</html>
