<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo06.aspx.cs" Inherits="Demo06" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script>
        function fun(source, arguments)
        {
            if (arguments.Value.length < 5 || arguments.Value.length > 12)
                arguments.IsValid = false;
            else
                arguments.IsValid = true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        账号：<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1" runat="server"
             ErrorMessage="账号长度必须在5-12位之间" ForeColor="Red"
             ControlToValidate="txtAcc" ClientValidationFunction="fun" >
        </asp:CustomValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" />
    </p>
    </form>
</body>
</html>
