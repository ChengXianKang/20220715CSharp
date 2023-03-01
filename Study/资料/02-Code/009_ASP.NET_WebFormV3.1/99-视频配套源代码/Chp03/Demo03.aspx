<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo03.aspx.cs" Inherits="Demo03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #Image1 { display:none;}
    </style>
    <script src="js/jquery-3.5.1.js"></script>
    <script>
        function showInfo(info)
        {
            $("#lblInfo",window.parent.document).html(info);
        }
        function showImg(fileName)
        {
            $("#Image1", window.parent.document).attr("src", "upload/" + fileName);
            $("#Image1", window.parent.document).css("display", "block");
            $("#hdImgFileName", window.parent.document).val(fileName);
            $("#lblInfo", window.parent.document).html(fileName);
        }
        $(function () {
            $("#btUpload").click(function () {
                $("#form1").attr("target", "frameFile");
            })
            $("#btOk").click(function () {
                $("#form1").attr("target", "_self");
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <iframe id="frameFile" name="frameFile" style="display:none;"></iframe>
        <h2>注册</h2>
        <p>
            账号：<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
        </p>
        <p>
            密码：<asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            &nbsp;
            <asp:Button ID="btUpload" runat="server" Text="上传文件" OnClick="btUpload_Click" />
        </p>
        <p>
            <asp:Label ID="lblInfo" runat="server" Text="" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:Image ID="Image1" runat="server"  Width="200" Height="200" />
            <input type="hidden" id="hdImgFileName" name="hdImgFileName" />
        </p>
        <p>
            <asp:Button ID="btOk" runat="server" Text="注 册" OnClick="btOk_Click" />
        </p>
    </form>
</body>
</html>
