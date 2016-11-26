<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Product_Add.aspx.cs" Inherits="WEB.Product.Product_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增产品</title>
    <link href="../css/edit.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/layer/layer.js"></script>
    <script src="../js/jquery-layer.js"></script>
    <script src="../js/jquery-uploadpop.js"></script>
    <script src="../js/common.js?v=1"></script>

    <script src="../ueditor1_4_3/ueditor.config.js"></script>
    <script src="../ueditor1_4_3/ueditor.all.js"></script>

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
                        <img  src="../Images/close.gif" />
                    </a>
                </div>
            </div>
            <div class="clearfix">
                <asp:Label CssClass="green" ID="ltMsg" Visible="false" runat="server">保存成功</asp:Label>
            </div>

            <table class="ed-table">
                <tr>
                    <td class="alignright">分类：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlProductClass1" AutoPostBack="true" OnSelectedIndexChanged="ddlProductClass1_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlProductClass2" AutoPostBack="true" OnSelectedIndexChanged="ddlProductClass2_SelectedIndexChanged"></asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlProductClass3"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="alignright">产品状态：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlProductStatus">
                            <asp:ListItem Value="0">请选择</asp:ListItem>
                            <asp:ListItem Value="8">推荐到首页</asp:ListItem>
                        </asp:DropDownList>
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
                    <td class="alignright">图片：</td>
                    <td>
                        <img id="img"    style="width: 100px; height: 100px;" onclick="OpenUploadFile()" />
                        <asp:HiddenField runat="server" ID="hide_ImgPath"   />
                    </td>
                </tr>
                <tr>
                    <td class="alignright">产品数量：
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtProductNum" style="width: 100px;" />
                        单位：
                         <input type="text" runat="server" id="txtUnits" style="width: 100px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red"
                            ControlToValidate="txtUnits" Display="Dynamic"
                            ErrorMessage="单位不能为空" SetFocusOnError="True">单位不能为空</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="red"
                            ControlToValidate="txtProductNum" Display="Dynamic"
                            ErrorMessage="产品数量不能为空" SetFocusOnError="True">产品数量不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright">市场价：
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtMarketPrice" style="width: 100px;" />
                        会员价：
                         <input type="text" runat="server" id="txtMemberPrice" style="width: 100px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="red"
                            ControlToValidate="txtUnits" Display="Dynamic"
                            ErrorMessage="单位不能为空" SetFocusOnError="True">单位不能为空</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="red"
                            ControlToValidate="txtProductNum" Display="Dynamic"
                            ErrorMessage="产品数量不能为空" SetFocusOnError="True">产品数量不能为空</asp:RequiredFieldValidator>

                    </td>
                </tr>
                <tr>
                    <td class="alignright">简介：</td>
                    <td>
                        <textarea runat="server" id="txtSummay"></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="alignright">Seo标题：</td>
                    <td>
                        <input type="text" runat="server" id="txtSeoTitle" />
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
