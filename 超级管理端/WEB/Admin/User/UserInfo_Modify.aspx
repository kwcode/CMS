<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo_Modify.aspx.cs" Inherits="WEB.Admin.User.UserInfo_Modify" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改用户</title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/common.js?v=1"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- <div class="clearfix ed-title">
                <h3>新增用户
                </h3>
                <div class="close">
                    <a onclick="CloseWindow(true)">
                        <img src="/Images/close.gif" />
                    </a>
                </div>
            </div>--%>

            <div class="clearfix">
                <asp:Label CssClass="green" ID="ltMsg" Visible="false" runat="server">保存成功</asp:Label>
            </div>
            <table class="ed-table">
                <tr>
                    <td class="alignright2">模板：</td>
                    <td>
                        <%--<input type="text" runat="server" id="txtTC_Name" />--%>
                        <asp:DropDownList runat="server" ID="ddlTemplates"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red"
                            ControlToValidate="ddlTemplates" Display="Dynamic"
                            ErrorMessage="模板不能为空" SetFocusOnError="True">模板不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">用户名：</td>
                    <td>
                        <input type="text" runat="server" id="txtUserName" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtUserName" Display="Dynamic"
                            ErrorMessage="用户名不能为空" SetFocusOnError="True">用户名不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">到期时间：</td>
                    <td>
                        <input type="text" runat="server" id="txtMaturityTime" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="btnbox">
                        <asp:Button Text="确定保存" runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <a class="btn btn-warning" onclick="CloseWindow(true)">关闭</a>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
