<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackgroundMenu_Add.aspx.cs" Inherits="WEB.GL.BackgroundMenu.BackgroundMenu_Add" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增</title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/common.js?v=1"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="clearfix ed-title">
                <h3>新增
                </h3>
                <div class="close">
                    <a onclick="CloseWindow(true)">
                        <img src="/Images/close.gif" />
                    </a>
                </div>
            </div>
            <table class="ed-table">
                <tr>
                    <td class="alignright2">一类：</td>
                    <td>
                        <select runat="server" id="ddlBackgroundMenuClass1"></select>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" CssClass="red"
                            ControlToValidate="ddlBackgroundMenuClass1" Display="Dynamic"
                            ErrorMessage="一类不能为空" SetFocusOnError="True">一类不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">名称：</td>
                    <td>
                        <input type="text" runat="server" id="txtTitle" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtTitle" Display="Dynamic"
                            ErrorMessage="名称不能为空" SetFocusOnError="True">名称不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">值：</td>
                    <td>
                        <input type="text" runat="server" id="txtValueNum" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="red"
                            ControlToValidate="txtValueNum" Display="Dynamic"
                            ErrorMessage="值不能为空" SetFocusOnError="True">值不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">排序：</td>
                    <td>
                        <input type="text" runat="server" id="txtOrderNum" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red"
                            ControlToValidate="txtOrderNum" Display="Dynamic"
                            ErrorMessage="排序不能为空" SetFocusOnError="True">排序不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">管理地址：</td>
                    <td>
                        <input type="text" runat="server" id="txtManageUrl" />
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">描述：</td>
                    <td>
                        <input type="text" runat="server" id="txtDescription" />
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
