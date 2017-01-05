<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seo_Set.aspx.cs" Inherits="WEB.SEO.Seo_Set" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/datagrid.css?v=1" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/jquery-page.js"></script>
    <script src="/js/common.js"></script>
    <script src="/js/tc-list.js"></script>
    <link href="../css/edit.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <div class="g-div-e">  
             <div class="clearfix ed-title">
                <h3>首页Seo优化
                </h3> 
            </div>
            <div>
                <table class="ed-table">
                    <tr>
                        <td class="alignright">标题：</td>
                        <td>
                            <input type="text" runat="server" style="width: 500px" id="txtTitle" />
                        </td>
                    </tr>
                    <tr>
                        <td class="alignright">网站关键字：</td>
                        <td>
                            <textarea runat="server" id="txtKeyWords"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="alignright">网站描述：
                        </td>
                        <td>
                            <textarea runat="server" id="txtDescription"></textarea>

                        </td>
                    </tr>

                </table>
                <div class="btnbox2">
                    <asp:Button Text="确定保存" runat="server" ID="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
