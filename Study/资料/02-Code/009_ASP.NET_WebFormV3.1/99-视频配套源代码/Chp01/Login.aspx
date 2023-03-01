<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登录</title>
    <style type="text/css">
        .myTable { width:800px; margin:20px auto;}
        .myTable tr { height:30px; }
        .myTable tr td:first-child { width:200px; text-align:right;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="myTable">
            <tr>
                <th colspan="2">用户登录</th>
            </tr>
            <tr>
                <td>用户名：</td>
                <td>
                    <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>验证码：</td>
                <td>
                    <asp:TextBox ID="txtValidate" runat="server" Width="59px"></asp:TextBox>
                    <asp:Label ID="lblValidate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btLogin" runat="server" Text="登 录" OnClick="btLogin_Click" />
                    <a href="Reg.aspx">注册</a>
                    <a href="FindPwd.aspx">找回密码</a>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblErrInfo" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
