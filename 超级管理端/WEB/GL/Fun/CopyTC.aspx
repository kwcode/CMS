<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyTC.aspx.cs" Inherits="WEB.GL.Fun.CopyTC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function checkForm() {
            if (!confirm("确定要复制此数据？\n①：复制此数据则把此客户之前的配置信息全部清空。\n②：然后把选择的配置信息复制到当前客户中")) {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 10px; text-align: center;">
            <asp:CheckBox runat="server" ID="cb_BackgroundMenu" Text="复制后台一类和菜单" />
            <asp:CheckBox runat="server" ID="cb_NavigationBar" Text="复制前台栏目" />
            <asp:CheckBox runat="server" ID="cb_Banner" Text="复制百叶窗" />
            <asp:CheckBox runat="server" ID="cb_PictureText" Text="复制插图" />
        </div>
        <div style="padding: 10px; text-align: center;">
            <label>复制</label>
            <asp:TextBox runat="server" ID="txtFromUserID"></asp:TextBox>
            <label>到</label>
            <label><%="["+userInfo.ID+"]"+userInfo.UserName%></label>
            <asp:Button Text="确定复制" OnClientClick="return checkForm()" runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
        <table class="" style="width: 100%; text-align: center; border: 1px solid #c1b7b7; margin: 10px 0px;">
            <asp:Repeater ID="Rep1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Literal Text="<%#Container.ItemIndex+1 %>" runat="server" />、<%#Eval("TxtContent") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </form>
</body>
</html>
