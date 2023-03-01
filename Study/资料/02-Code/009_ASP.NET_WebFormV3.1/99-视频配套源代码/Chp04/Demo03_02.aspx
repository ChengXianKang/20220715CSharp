<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo03_02.aspx.cs" Inherits="Demo03_02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <h2>上传文件-通过Request对象获取文件进行上传</h2>
    <div>
        <%--<asp:FileUpload ID="myFile" runat="server" />--%>
        <input type="file" name="myFile" />
        <asp:Button ID="btUpload" runat="server" Text="上传" OnClick="btUpload_Click"  /><br /><br />
        <asp:Label ID="lblInfo" runat="server" ForeColor="#CC0000"></asp:Label>
        <br /><br />
        <asp:Image ID="myImg" runat="server" Width="300" Height="300" Visible="false" />
    </div>
    </form>
</body>
</html>
