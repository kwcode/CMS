<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaterMarkEditPopup.aspx.cs" Inherits="WEB.WebImgUpload.WaterMarkEditPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/uploadpop.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/jquery.colorpicker.js"></script>
    <script>
        $(function () {
            $("#imgColor").colorpicker({
                fillcolor: true,
                event: 'mouseover',
                target: $(".spColor")
            });
        })
    </script>
    <style>
        #rbType tr { float: left; }
        .m-table { line-height: normal; border-spacing: 0; background-color: #F6EFE7; border-collapse: collapse; }
        .m-table .m-tr .m-td { border: 1px solid #ddd; line-height: 24px; padding-left: 3px; }
        .m-table th { font-weight: bold; background: #f7e2ca; color: #0014ff; }
        .m-table tbody m-tr:nth-child(2n) { background: #fafafa; }
        .m-table tbody m-tr:hover { background: #f0f0f0; }
        .alignright { width: 80px; font-weight: bold; text-align: right; }

        .n-table { line-height: normal; border-spacing: 0; background-color: #FFF; border-collapse: collapse; }
        .n-table .n-tr .n-td { border: 1px solid #ddd; line-height: 24px; padding-left: 3px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div  >
            <div>
                <table class="m-table">
                    <%--<tr class="m-tr">
                        <td class="alignright m-td">水印类型：</td>
                        <td class="m-td">
                            <asp:RadioButtonList ID="rbType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbType_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True" Text="文字"></asp:ListItem>
                                <asp:ListItem Value="1" Text="图片"></asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>--%>
                    <tr class="m-tr">
                        <td class="alignright m-td"></td>
                        <td class="m-td">
                            <table class="n-table" style="padding: 5px;" runat="server" id="txtBox">
                                <tr class="n-tr">
                                    <td class="n-td">水印文字</td>
                                    <td class="n-td">
                                        <input type="text" id="txtWmkText" runat="server" value="测试水印" /></td>
                                </tr>

                                <tr class="n-tr">
                                    <td class="n-td">字体</td>
                                    <td class="n-td">
                                        <asp:DropDownList runat="server" ID="ddlFamilyName">
                                            <asp:ListItem Value="arial" Text="arial"></asp:ListItem>
                                            <asp:ListItem Value="SimSun" Text="宋体"></asp:ListItem>
                                            <asp:ListItem Value="SimHei" Text="黑体"></asp:ListItem>
                                            <asp:ListItem Value="STCaiyun" Text="华文彩云"></asp:ListItem>
                                            <asp:ListItem Value="STKaiti" Text="华文楷体"></asp:ListItem>
                                            <asp:ListItem Value="细圆" Text="细圆"></asp:ListItem>
                                            <asp:ListItem Value="Helvetica" Text="Helvetica"></asp:ListItem>
                                            <asp:ListItem Value="sans-serif" Text="sans-serif"></asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr class="n-tr">
                                    <td class="n-td">文字样式</td>
                                    <td class="n-td">
                                        <asp:DropDownList runat="server" ID="ddlFontStyle">
                                            <asp:ListItem Value="0" Text="正常"></asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True" Text="加粗"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="斜体"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="底线"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="删除"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="n-tr">
                                    <td class="n-td">文字大小</td>
                                    <td class="n-td">
                                        <input type="text" id="txtSize" runat="server" value="40" /></td>
                                </tr>
                                <tr class="n-tr">
                                    <td class="n-td">文字颜色</td>
                                    <td class="n-td">
                                        <input class="spColor" style="width: 70px; float: left;" type="hidden" id="txtColor" runat="server" value="#FF0000" />
                                        <span id="DisColor" class="spColor" style="padding: 11px; margin-left: 10px; float: left; background: #FF0000;"></span>
                                        <img src="/images/colorpicker.png" id="imgColor" style="cursor: pointer; margin-left: 10px; height: 24px; width: 24px; float: left;" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="m-tr" runat="server" id="WatermarkSizePercentBox">
                        <td class="alignright m-td">水印大小：
                        </td>
                        <td class="m-td">
                            <asp:DropDownList runat="server" ID="ddlWatermarkSizePercent">
                                <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                <asp:ListItem Value="25" Selected="True" Text="25%"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35%"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr class="m-tr">
                        <td class="alignright m-td">水印位置：
                        </td>
                        <td class="m-td">
                            <asp:DropDownList ID="ddlWmkPosition" runat="server">
                                <asp:ListItem Value="0" Text="左上角"></asp:ListItem>
                                <asp:ListItem Value="1" Text="正上方"></asp:ListItem>
                                <asp:ListItem Value="2" Text="右上角"></asp:ListItem>
                                <asp:ListItem Value="3" Text="左中处"></asp:ListItem>
                                <asp:ListItem Value="4" Text="正中处"></asp:ListItem>
                                <asp:ListItem Value="5" Text="右中处"></asp:ListItem>
                                <asp:ListItem Value="6" Text="左下角"></asp:ListItem>
                                <asp:ListItem Value="7" Text="正下方"></asp:ListItem>
                                <asp:ListItem Value="8" Selected="True" Text="右下角"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr class="m-tr">
                        <td class="alignright m-td">不透明度：
                        </td>
                        <td class="m-td">
                            <asp:DropDownList runat="server" ID="ddlAlpha">
                                <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30%"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40%"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                <asp:ListItem Value="60" Text="60%"></asp:ListItem>
                                <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                <asp:ListItem Value="80" Selected="True" Text="80%"></asp:ListItem>
                                <asp:ListItem Value="90" Text="90%"></asp:ListItem>
                                <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr class="m-tr">
                        <td class="alignright m-td">效果预览：
                        </td>
                        <td class="m-td">
                            <asp:Button ID="btnPreview" Text="重新生成" runat="server" OnClick="btnPreview_Click" /></td>
                    </tr>

                    <tr class="m-tr">
                        <td></td>
                        <td class="m-td">
                            <img id="img" style="width: 200px; height: 200px; border: 1px solid #8c8c8c;"
                                runat="server" />
                        </td>
                    </tr>
                    <tr class="m-tr">
                        <td></td>
                        <td class="m-td">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" />
                        </td>
                    </tr>
                </table>
                <div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
