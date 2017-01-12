<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Support_Modify.aspx.cs" Inherits="WEB.Support.Support_Modify" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=KeyID>0?"修改":"新增" %></title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/common.js?v=2"></script>
    <script> 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="clearfix ed-title">
                <h3><%=KeyID>0?"修改":"新增" %></h3>
                <div class="close">
                    <a onclick="CloseWindow(true)">
                        <img src="/Images/close.gif" />
                    </a>
                </div>
            </div>
            <div class="clearfix">
                <asp:Label CssClass="green" ID="ltMsg" Visible="false" runat="server">修改成功</asp:Label>
            </div>
            <table class="ed-table">
                <tr>
                    <td class="alignright2">标题：</td>
                    <td>
                        <input type="text" runat="server" id="txtTitle" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtTitle" Display="Dynamic"
                            ErrorMessage="标题不能为空" SetFocusOnError="True">标题不能为空</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">关键字：</td>
                    <td>
                        <input type="text" runat="server" id="txtKeysword" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="red"
                            ControlToValidate="txtKeysword" Display="Dynamic"
                            ErrorMessage="关键字不能为空" SetFocusOnError="True">关键字不能为空</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">内容：</td>
                    <td>
                        <input type="text" runat="server" id="txtContent" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="red"
                            ControlToValidate="txtContent" Display="Dynamic"
                            ErrorMessage="内容不能为空" SetFocusOnError="True">内容不能为空</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">浏览次数：</td>
                    <td>
                        <input type="text" runat="server" id="txtLookCount" value="0" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red"
                            ControlToValidate="txtLookCount" Display="Dynamic"
                            ErrorMessage="浏览次数不能为空" SetFocusOnError="True">浏览次数不能为空</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">状态：</td>
                    <td>
                        <select id="selStatus" runat="server">
                            <option value="1">启用</option>
                            <option value="0">未启用</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
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
