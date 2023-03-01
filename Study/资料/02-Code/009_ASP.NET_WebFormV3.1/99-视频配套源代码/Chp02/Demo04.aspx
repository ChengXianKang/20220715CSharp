<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo04.aspx.cs" Inherits="Demo04" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        请输入成绩：<asp:TextBox ID="txtScore" runat="server"></asp:TextBox>
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
            ErrorMessage="分数必须在0-100之间" ForeColor="Red"
            ControlToValidate="txtScore" Type="Double" 
            MinimumValue="0" MaximumValue="100" >
        </asp:RangeValidator>
    </p>
    <p>
        <asp:Button ID="btOk" runat="server" Text="提  交" />
    </p>
    </form>
</body>
</html>
