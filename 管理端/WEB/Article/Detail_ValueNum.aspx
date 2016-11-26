<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail_ValueNum.aspx.cs" Inherits="WEB.Article.Detail_ValueNum" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线留言详细信息</title>
    <link href="../css/edit.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/layer/layer.js"></script>
    <script src="../js/common.js?v=1"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="clearfix ed-title">
                <h3>详细信息
                </h3>
                <div class="close">
                    <a onclick="CloseWindow(true)">
                        <img src="../Images/close.gif" />
                    </a>
                </div>
            </div>
            <table class="ed-table">
                <tr>
                    <td class="alignright">标题：</td>
                    <td>
                        <%=articleEntity.Title%>
                    </td>
                </tr>
                <tr>
                    <td class="alignright">值：</td>
                    <td><%=articleEntity.ValueNum%></td>
                </tr>
                <tr>
                    <td class="alignright">排序：</td>
                    <td><%=articleEntity.OrderNum%></td>
                </tr>
                <tr>
                    <td class="alignright">描述：</td>
                    <td><%=articleEntity.Description%></td>
                </tr>
                <tr>
                    <td class="alignright">简介：</td>
                    <td><%=articleEntity.Summay%></td>
                </tr>
                <tr>
                    <td class="alignright">标题图片：</td>
                    <td><%=articleEntity.TitlePictures%></td>
                </tr>
                <tr>
                    <td class="alignright">内容：</td>
                    <td><%=articleEntity.TxtContent    %></td>
                </tr>
                <tr>
                    <td class="alignright">到期时间：</td>
                    <td><%=articleEntity.MaturityDate%></td>
                </tr>
                <tr>
                    <td class="alignright">内容：</td>
                    <td><%=articleEntity.TxtContent  %></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
