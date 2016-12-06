<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAccount_Modify.aspx.cs" Inherits="WEB.Admin.User.AdminAccount_Modify" %>

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

            <table class="ed-table">
                <tr>
                    <td class="alignright2">密码：</td>
                    <td>
                        <input type="text" runat="server" id="txtPassword" />
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtPassword" Display="Dynamic"
                            ErrorMessage="密码不能为空" SetFocusOnError="True">密码不能为空</asp:RequiredFieldValidator>
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
            <div class="clearfix green">
                <asp:Literal ID="ltMsg" Visible="false" runat="server">保存成功</asp:Literal>
            </div>
        </div>
    </form>
</body>
</html>
