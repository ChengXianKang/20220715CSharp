<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo01.aspx.cs" Inherits="Demo01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            真实姓名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </p>
        <p>
            身份证号:<asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btOk" runat="server" Text="确定" OnClick="btOk_Click" />
            输入信息，在下方进行显示
        </p>
        <p>
            <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
