<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo02.aspx.cs" Inherits="Demo02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>游戏英雄列表</h1>
        <div>
            <%
                MyHeroDAL dal = new MyHeroDAL();
                List<MyHeroEntity> list = dal.List();
                foreach (MyHeroEntity item in list)
                {
            %>
                <div style=" width:220px; text-align:center;float:left;">
                    <img src="img/<%=item.HeroPic %>" /> <br />
                    <a href="Demo02_01.aspx?HeroId=<%=item.HeroId %>"><%=item.HeroName %></a>
                </div>
            <%
                }
            %>
        </div>
    </form>
</body>
</html>
