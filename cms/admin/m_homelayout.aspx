<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m_homelayout.aspx.cs" Inherits="admin_m_homelayout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>页面布局</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="t_page" runat="server">
                <ItemTemplate>
                    <div><%=Eval("PageName") %></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
