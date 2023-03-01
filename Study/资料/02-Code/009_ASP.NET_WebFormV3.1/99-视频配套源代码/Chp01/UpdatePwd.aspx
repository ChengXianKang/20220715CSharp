<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePwd.aspx.cs" Inherits="UpdatePwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码</title>
    <style type="text/css">
        .mytable td { padding:6px;
        }
        .left { width:300px; text-align:right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="1000" align="center" class="mytable">
            <caption>密码修改</caption>
            <tr>
                <td class="left">原始密码：</td>
                <td>
                    <asp:TextBox ID="txtPwdOld" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">密码：</td>
                <td>
                    <asp:TextBox ID="txtUserPwd" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">密码确认：</td>
                <td>
                    <asp:TextBox ID="txtUserPwdConfirm" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btUpdate" runat="server" Text="修改密码" OnClick="btUpdate_Click"  />
                    <input type="reset" value="取消" />
                    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>             
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
