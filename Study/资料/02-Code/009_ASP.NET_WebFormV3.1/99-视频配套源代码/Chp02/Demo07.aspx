<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo07.aspx.cs" Inherits="Demo07" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        账号：<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1" runat="server"
             ErrorMessage="对不起,用户名被占用" ForeColor="Red"
             ControlToValidate="txtAcc"  OnServerValidate="CustomValidator1_Validate" >
        </asp:CustomValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" OnClick="btOk_Click" />
    </p>
    </form>
</body>
</html>
