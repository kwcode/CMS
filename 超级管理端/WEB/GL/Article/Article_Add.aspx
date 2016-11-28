<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Article_Add.aspx.cs" Inherits="WEB.GL.Article.Article_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增</title>
    <link href="/css/edit.css" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/jquery-layer.js"></script>
    <script src="/js/jquery-uploadpop.js"></script>
    <script src="/js/common.js?v=1"></script>

    <script src="/ueditor1_4_3/ueditor.config.js"></script>
    <script src="/ueditor1_4_3/ueditor.all.js"></script>

    <!-- 实例化编辑器 -->
    <script type="text/javascript">
        $(function () {
            var _layer, isclose = true;
            if (layer.index == 1) {
                isclose = false;
            }
            else {
                _layer = $.tc.showWaiting("请稍等...", 0, 6);
            }
            var htmlContent = document.getElementById("<%=this.hide_Content.ClientID%>").value;
            //正确的初始化方式  阻止复制的div标签自动转换为p标签
            var ue = UE.getEditor('editor', { allowDivTransToP: false });
            //正确的初始化方式
            ue.ready(function () {
                //this是当前创建的编辑器实例
                this.setContent(htmlContent);
                if (isclose) {
                    layer.close(_layer);
                }
            })

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
                <h3>新增
                </h3>
                <div class="close">
                    <a onclick="CloseWindow()">
                        <img src="/Images/close.gif" />
                    </a>
                </div>
            </div>
           <%-- <div class="clearfix">
                <asp:Label CssClass="green" ID="ltMsg" Visible="false" runat="server">保存成功</asp:Label>
            </div>--%>

            <table class="ed-table">
                <tr>
                    <td class="alignright">分类：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlArticleClass1"></asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="red"
                            ControlToValidate="ddlArticleClass1" Display="Dynamic"
                            ErrorMessage="分类不能为空" SetFocusOnError="True">分类不能为空</asp:RequiredFieldValidator>
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
                    <td class="alignright">图片：</td>
                    <td>
                        <img id="img" style="width: 100px; height: 100px;" onclick="OpenUploadFile()" />
                        <asp:HiddenField runat="server" ID="hide_ImgPath" />
                    </td>
                </tr>

                <tr>
                    <td class="alignright">简介：</td>
                    <td>
                        <textarea runat="server" id="txtSummay"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="alignright">描述：</td>
                    <td>
                        <textarea runat="server" id="txtDescription"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="alignright">内容：</td>
                    <td>
                        <!-- 加载编辑器的容器 -->
                        <script id="editor" style="width: 800px; height: 450px; margin: 2px;" name="content" type="text/plain">
                        </script>
                        <!--隐藏控件为编辑器赋值用-->
                        <asp:HiddenField ID="hide_Content" Value="" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td class="btnbox">
                        <asp:Button Text="确定保存" runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <a class="btn btn-warning" onclick="CloseWindow()">关闭</a>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

</body>
</html>

