<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo08.aspx.cs" Inherits="Demo08" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1">
            <DataBindings>
                <asp:TreeNodeBinding DataMember="menu" NavigateUrlField="url"  TextField="title" ValueField="title" />
                <asp:TreeNodeBinding DataMember="menu" NavigateUrlField="url" TextField="title" ValueField="title" />
                <asp:TreeNodeBinding DataMember="menu" NavigateUrlField="url" TextField="title" ValueField="title" />
            </DataBindings>
        </asp:TreeView>
    </div>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Menu.xml"></asp:XmlDataSource>
    </form>
</body>
</html>
