<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PictureTextClass1_Modify.aspx.cs" Inherits="WEB.GL.PictureText.PictureTextClass1_Modify" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改套餐模板</title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/common.js?v=1"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="clearfix ed-title">
                <h3>修改基础栏目
                </h3>
                <div class="close">
                    <a onclick="CloseWindow(true)">
                        <img src="/Images/close.gif" />
                    </a>
                </div>
            </div>

            <div class="clearfix">
                <asp:Label CssClass="green" ID="ltMsg" Visible="false" runat="server">保存成功</asp:Label>
            </div>
            <table class="ed-table">
                <tr>
                    <td class="alignright2">栏目分组：</td>
                    <td>
                        <select runat="server" id="ddlBackSectionsSet"></select>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="red"
                            ControlToValidate="ddlBackSectionsSet" Display="Dynamic"
                            ErrorMessage="栏目分组不能为空" SetFocusOnError="True">栏目分组不能为空</asp:RequiredFieldValidator>

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
                        <span class="red">*唯一值</span>
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
