<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateInfo.aspx.cs" Inherits="UpdateInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改基本信息</title>
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
            <caption>用户基本信息修改</caption>
            <tr>
                <td class="left">用户名：</td>
                <td>
                    <asp:Label ID="lblAcc" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="left">邮箱：</td>
                <td>
                    <asp:TextBox ID="txtUserMail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">联系电话：</td>
                <td>
                    <asp:TextBox ID="txtUserPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left">性别：</td>
                <td>
                    <asp:RadioButton ID="rbBoy" runat="server" GroupName="UserSex" Text="男" />
                    <asp:RadioButton ID="rbGirl" runat="server" GroupName="UserSex" Text="女"/>
                </td>
            </tr>
            <tr>
                <td class="left">专业：</td>
                <td>
                    <asp:DropDownList ID="ddlUserPro" runat="server">
                        <asp:ListItem Text="--请选择--" Value=""></asp:ListItem>
                        <asp:ListItem Text="计算机" Value="计算机"></asp:ListItem>
                        <asp:ListItem Text="国际贸易" Value="国际贸易"></asp:ListItem>
                        <asp:ListItem Text="英语" Value="英语"></asp:ListItem>
                        <asp:ListItem Text="历史" Value="历史"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="left">爱好：</td>
                <td>
                    <asp:Panel ID="pHobby" runat="server">
                        <asp:CheckBox ID="ck1" runat="server" Text="抽烟" />
                        <asp:CheckBox ID="ck2" runat="server" Text="喝酒" />
                        <asp:CheckBox ID="ck3" runat="server" Text="烫头" />
                        <asp:CheckBox ID="ck4" runat="server" Text="游戏" />
                        <asp:CheckBox ID="ck5" runat="server" Text="画画" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="left">自我介绍：</td>
                <td>
                    <asp:TextBox ID="txtUserSelf" runat="server" TextMode="MultiLine" Height="102px" Width="562px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btReg" runat="server" Text="修改基本信息" OnClick="btReg_Click" />
                    &nbsp;
                    <input type="reset" value="取消" />
                    <asp:Label ID="lblInfo" runat="server" ForeColor="Red"></asp:Label>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
