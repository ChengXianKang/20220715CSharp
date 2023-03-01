<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo03.aspx.cs" Inherits="Demo03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	<table width="1000" align="center">
		<caption>用户注册</caption>
		<tr>
			<td width="300" align="right" height="30">用户名:</td>
			<td width="700"><asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">密码:</td>
			<td width="700"><asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">密码确认:</td>
			<td width="700"><asp:TextBox ID="txtPwdOk" runat="server" TextMode="Password"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">邮箱:</td>
			<td width="700"><asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">联系电话:</td>
			<td width="700"><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
			</td>
		</tr>			
		<tr>
			<td width="300" align="right" height="30">性别:</td>
			<td width="700" align="left">
                <input type="radio" name="sex" value="男" checked="checked" />男
                <input type="radio" name="sex" value="女" />女
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">专业:</td>
			<td width="700">
				<asp:DropDownList ID="ddlProfessional" runat="server">
                    <asp:ListItem>请选择</asp:ListItem>
                    <asp:ListItem>软件开发</asp:ListItem>
                    <asp:ListItem>电子商务</asp:ListItem>
                    <asp:ListItem>国际贸易</asp:ListItem>
                    <asp:ListItem>工商管理</asp:ListItem>
                    <asp:ListItem>高级护理</asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>			
		<tr>
			<td width="300" align="right" height="30">爱好:</td>
			<td width="700" align="left">
                <input type="checkbox" name="hobby" value="抽烟" />抽烟
                <input type="checkbox" name="hobby" value="喝酒" />喝酒
                <input type="checkbox" name="hobby" value="嚼槟榔" />嚼槟榔
                <input type="checkbox" name="hobby" value="打游戏" />打游戏
                <input type="checkbox" name="hobby" value="烫头发" />烫头发
                <input type="checkbox" name="hobby" value="足球" />足球
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">头像:</td>
			<td width="700" align="left">
                <asp:FileUpload ID="filePhoto" runat="server" />
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">自我介绍:</td>
			<td width="700">
				<asp:TextBox ID="txtSelft" runat="server" Height="153px" TextMode="MultiLine" Width="427px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td width="300" align="right" height="30">&nbsp;</td>
			<td width="700">
				<asp:Button ID="btReg" runat="server" Text="注册" OnClick="btReg_Click"  />
&nbsp;<input type="reset" value="取消" />
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
