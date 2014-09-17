<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PagePermiTallot.aspx.cs"
    Inherits="admin_PM_PagePermiTallot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="g-div-e" style="display: block; position: absolute; left: 600px; top: 0px;
            z-index: 10">
            <table class="m-table">
                <thead>
                    <tr>
                        <th class="thead" colspan="2">
                            未控制权限页面
                        </th>
                    </tr>
                    <tr class="bgtr">
                        <th>
                            URL
                        </th>
                        <th>
                            管理
                        </th>
                    </tr>
                </thead>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left;">
                                <%#Eval("PermitURL")%>
                            </td>
                            <td>
                                <button type="button" class="button gray medium" onclick="ShowPer('<%#Eval("PermitURL") %>');">
                                    添加</button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
