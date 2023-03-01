<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo04.aspx.cs" Inherits="Demo04" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        div, ul, li { margin:0px; padding:0px; list-style-type:none;}
        #imgContainer ul { clear:both;}
        #imgContainer li { margin:10px; float:left; text-align:center;}
    </style>
    <script src="js/jquery-3.5.1.js"></script>
    <script>
        function showInfo(info)
        {
            $("#lblInfo",window.parent.document).html(info);
        }
        function showImg(fileName)
        {
            var $li = $("<li><img height='150' src='upload/" + fileName + "' /><br /><a href='javascript:void(0);'>删除</a></li>");
            $("#imgUL", window.parent.document).append($li);
            var $str = $("#lblInfo", window.parent.document).html();
            if ($str != "")
                $str += ",";
            $str += fileName;
            $("#lblInfo", window.parent.document).html($str);
            $("#hdImgFileName", window.parent.document).val($str);
        }
        $(function () {
            $("#btUpload").click(function () {
                $("#form1").attr("target", "frameFile");
            })
            $("#btOk").click(function () {
                $("#form1").attr("target", "_self");
            })
            $("#imgUL").on("click", " li a", function () {
                var imgSrc = $(this).parent("li").children("img").attr("src");
                var arr = imgSrc.split("/");
                imgSrc = arr[arr.length - 1];
                var $str = $("#lblInfo", window.parent.document).html();
                $str = $str.replace(imgSrc + ",", "");
                $str = $str.replace("," + imgSrc, "");
                $("#lblInfo", window.parent.document).html($str);
                $("#hdImgFileName", window.parent.document).val($str);
                //删除li节点
                $(this).parent("li").remove();
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <iframe id="frameFile" name="frameFile" style="display:none;"></iframe>
        <input type="hidden" id="hdImgFileName" name="hdImgFileName" />
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
        <div id="imgContainer" style="clear:both;">
            <ul id="imgUL"></ul>
        </div>
        <p style="clear:both;">
            <asp:Button ID="btOk" runat="server" Text="注 册" OnClick="btOk_Click" />
        </p>
    </form>
</body>
</html>
