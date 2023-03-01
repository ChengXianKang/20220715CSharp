<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo02_01.aspx.cs" Inherits="Demo02_01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        <asp:Label ID="lblName" runat="server" Text="Label" Font-Bold="True" Font-Size="Larger"></asp:Label>
    </p>
    <p>
        <asp:Image ID="Image1" runat="server" />
    </p>
    <h2>技能介绍</h2>
    <p>
        <asp:Label ID="lblSkill" runat="server" Text="Label"></asp:Label>
    </p>
    <h2>出装介绍</h2>
    <p>
        <asp:Label ID="lblEquipment" runat="server" Text="Label"></asp:Label>
    </p>
    </form>
</body>
</html>
