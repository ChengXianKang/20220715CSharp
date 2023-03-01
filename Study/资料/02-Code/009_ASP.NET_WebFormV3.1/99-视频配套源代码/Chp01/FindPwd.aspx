<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindPwd.aspx.cs" Inherits="FindPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>找回密码</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" >
            <asp:View ID="View1" runat="server">
                <h2>找回密码步骤一，输入账号:</h2>
                <p>
                    <asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
                    <asp:Button ID="btNext1" runat="server" Text="下一步" OnClick="btNext1_Click" />
                    <asp:Label ID="lblMsg1" runat="server" Text="" ForeColor="Red"></asp:Label>
                </p>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <h2>找回密码步骤二，回答密保问题：</h2>
                <p>
                    密保问题：<asp:Label ID="lblQuestion" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    密保答案：<asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
                </p>
                <p>
                    <asp:Button ID="btPrev2" runat="server" Text="上一步" OnClick="btPrev2_Click" />
                    <asp:Button ID="btNext2" runat="server" Text="下一步" OnClick="btNext2_Click" />
                    <asp:Label ID="lblMsg2" runat="server" Text="" ForeColor="Red"></asp:Label>
                </p>
            </asp:View>
            <asp:View ID="View3" runat="server">
                <h2>找回密码步骤三，设置新密码：</h2>
                <p>
                    新密码：<asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                </p>
                <p>
                    确认密码：<asp:TextBox ID="txtPwdConfirm" runat="server" TextMode="Password"></asp:TextBox>
                </p>
                <p>
                    <asp:Button ID="btPrev3" runat="server" Text="上一步" OnClick="btPrev3_Click" />
                    <asp:Button ID="btOk" runat="server" Text="完成" OnClick="btOk_Click" />
                    <asp:Label ID="lblMsg3" runat="server" Text="" ForeColor="Red"></asp:Label>
                </p>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
