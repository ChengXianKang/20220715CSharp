<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo01.aspx.cs" Inherits="Demo01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>JS验证</title>
    <script>
        function fun()
        {
            var obj = document.getElementById("txtAcc");
            if (obj.value == "")
            {
                alert("账号不能为空！");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        账号：<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提 交" OnClick="btOk_Click" OnClientClick="return fun();"  />
    </p>
    </form>
</body>
</html>
