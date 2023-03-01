<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePhoto.aspx.cs" Inherits="UpdatePhoto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改头像</title>
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
            <caption>修改头像</caption>
            <tr>
                <td class="left">头像：</td>
                <td>
                    <asp:FileUpload ID="fileUserPhoto" runat="server" />
                    <asp:Button ID="btUpdate" runat="server" Text="修改头像" OnClick="btUpdate_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Image ID="Image1" runat="server" Width="100" Height="100" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
