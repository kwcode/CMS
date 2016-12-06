<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Domain_Modify.aspx.cs" Inherits="WEB.Admin.Domain.Domain_Modify" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增域名</title>
    <link href="/css/edit.css?v=2" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/common.js?v=1"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="ed-table">
                <tr>
                    <td class="alignright2">关联UserID：</td>
                    <td>
                        <asp:Label CssClass="green" runat="server" ID="lbUserID"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">域名：</td>
                    <td>
                        <input type="text" class="input150" runat="server" id="txtDomainName" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtDomainName" Display="Dynamic"
                            ErrorMessage="域名不能为空" SetFocusOnError="True">域名不能为空</asp:RequiredFieldValidator>
                        <span>*如www.szyjkj.com</span>
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">到期时间：</td>
                    <td>
                        <input type="text" runat="server" id="txtEndDate" class="input150" />
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
