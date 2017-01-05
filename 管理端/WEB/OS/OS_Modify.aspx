<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OS_Modify.aspx.cs" Inherits="WEB.OS.OS_Modify" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改</title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
   <%-- <script src="/js/layer/layer.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="clearfix ed-title">
                <h3>修改
                </h3>
                <div class="close">
                    <a onclick="CloseWindow()">
                        <img src="/Images/close.gif" />
                    </a>
                </div>
            </div>
            <table class="ed-table">
                <tr>
                    <td class="alignright">客服类型：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlOSEnum"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="alignright">名称：</td>
                    <td>
                        <input type="text" runat="server" id="txtTitle" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtTitle" Display="Dynamic"
                            ErrorMessage="名称不能为空" SetFocusOnError="True">名称不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright">值：</td>
                    <td>
                        <input type="text" runat="server" id="txtValueNum" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red"
                            ControlToValidate="txtValueNum" Display="Dynamic"
                            ErrorMessage="值不能为空" SetFocusOnError="True">值不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright">排序：</td>
                    <td>
                        <input type="text" runat="server" id="txtOrderNum" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="red"
                            ControlToValidate="txtOrderNum" Display="Dynamic"
                            ErrorMessage="排序不能为空" SetFocusOnError="True">排序不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright">备注：</td>
                    <td>
                        <textarea runat="server" id="txtRemark"></textarea>
                    </td>
                </tr>
            </table>
            <div class="boxline"></div>
            <div class="btnbox2">
                <asp:Button Text="确定保存" runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                <a class="btn btn-warning" onclick="CloseWindow()">关闭</a>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
            </div>
        </div>
    </form>

</body>
</html>
