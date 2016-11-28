<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Banner_Add.aspx.cs" Inherits="WEB.GL.Banner.Banner_Add" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改</title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/common.js?v=2"></script>
    <script src="/js/jquery-uploadpop.js"></script>
    <script type="text/javascript">
        $(function () {
            //给图片赋值
            var imgPath = document.getElementById("<%=this.hide_ImgPath.ClientID%>").value;
            $("#img").attr("src", imgPath);
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="clearfix ed-title">
                <h3>修改
                </h3>
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
                    <td class="alignright2">名称：</td>
                    <td>
                        <input type="text" runat="server" id="txtTitle" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
                            ControlToValidate="txtTitle" Display="Dynamic"
                            ErrorMessage="名称不能为空" SetFocusOnError="True">名称不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright2">图片：</td>
                    <td>
                        <img id="img" runat="server" style="width: 100px; height: 100px;" onclick="OpenUploadFile()" />
                        <asp:HiddenField runat="server" ID="hide_ImgPath" />
                    </td>
                </tr>
                <tr>
                    <td class="alignright2">Url：</td>
                    <td>
                        <input type="text" runat="server" id="txtUrl" />
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
