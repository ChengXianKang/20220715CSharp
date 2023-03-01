<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo05.aspx.cs" Inherits="Demo05" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <h1>文件下载</h1>
        <p>
            1-通过超级链接的方式实现文件下载 
            <a href="upload.zip">文件下载</a>
        </p>
        <p>
            2-通过文件流的方式进行下载
            <asp:Button ID="btDown" runat="server" Text="文件下载" OnClick="btDown_Click" />
        </p>
    </div>
    </form>
</body>
</html>
