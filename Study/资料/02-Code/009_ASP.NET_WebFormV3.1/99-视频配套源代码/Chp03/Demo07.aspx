<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo07.aspx.cs" Inherits="Demo07" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>TreeView可视化工具</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TreeView ID="TreeView1" runat="server">
            <Nodes>
                <asp:TreeNode Text="后台管理" Value="后台管理">
                    <asp:TreeNode Text="用户管理" Value="用户管理">
                        <asp:TreeNode Text="用户查询" Value="用户查询"></asp:TreeNode>
                        <asp:TreeNode Text="用户添加" Value="用户添加"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="商品管理" Value="商品管理">
                        <asp:TreeNode Text="商品查询" Value="商品查询"></asp:TreeNode>
                        <asp:TreeNode Text="新建节点" Value="商品添加"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="订单管理" Value="订单管理"></asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
    </div>
    </form>
</body>
</html>
