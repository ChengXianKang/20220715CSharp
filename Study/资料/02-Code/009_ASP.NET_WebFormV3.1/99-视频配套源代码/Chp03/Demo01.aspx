<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo01.aspx.cs" Inherits="Demo01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>单文件上传（通过文件后缀判断文件类型保证安全）</h2>
        <p>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            &nbsp;
            <asp:Button ID="btUpload" runat="server" Text="上传文件" OnClick="btUpload_Click" />
        </p>
        <p>
            <asp:Label ID="lblInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:Image ID="Image1" runat="server"  Width="200" Height="200" Visible="false" />
        </p>
    </form>
</body>
</html>
