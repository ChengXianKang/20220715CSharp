<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemList.aspx.cs" Inherits="MemList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="500" border="1">
            <caption><h3>使用Response.write打印数据</h3></caption>
            <tr>
                <th>用户编号</th>
                <th>用户名</th>
                <th>密码</th>
            </tr>
            <%
                MyUserDAL dal = new MyUserDAL();
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1 = dal.Select();
                foreach (System.Data.DataRow item in dt1.Rows)
                {
                    Response.Write("<tr>");
                        Response.Write("<td>");
                        Response.Write(item["UserId"].ToString());
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(item["UserAccount"].ToString());
                        Response.Write("</td>");
                        Response.Write("<td>");
                        Response.Write(item["UserPwd"].ToString());
                        Response.Write("</td>");
                    Response.Write("</tr>");
                }
            %>
        </table>
        <br /><br />
        <table width="500" border="1">
            <caption><h3>使用=打印数据</h3></caption>
            <tr>
                <th>用户编号</th>
                <th>用户名</th>
                <th>密码</th>
            </tr>
            <% 
                MyUserDAL dal2 = new MyUserDAL();
                System.Data.DataTable dt2 = new System.Data.DataTable();
                dt2 = dal2.Select();
                foreach (System.Data.DataRow item in dt1.Rows)
                {
            %>
            <tr>
                <td><%=item["UserId"].ToString()%></td>
                <td><%=item["UserAccount"].ToString()%></td>
                <td><%=item["UserPwd"].ToString()%></td>
            </tr>
            <% 
                }
            %>
        </table>

        <br /><br />
        <table width="500" border="1">
            <caption><h3>使用List集合作为数据源显示数据</h3></caption>
            <tr>
                <th>用户编号</th>
                <th>用户名</th>
                <th>密码</th>
            </tr>
            <% 
                MyUserDAL dal3 = new MyUserDAL();
                List<MyUserEntity> list = dal3.List();
                foreach (MyUserEntity item in list)
                {
            %>
            <tr>
                <td><%=item.UserId.ToString() %></td>
                <td><%=item.UserAccount %></td>
                <td><%=item.UserPwd %></td>
            </tr>
            <% 
                }
            %>
        </table>
    </div>
    </form>
</body>
</html>
