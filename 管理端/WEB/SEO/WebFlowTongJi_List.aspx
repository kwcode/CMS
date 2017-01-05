<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFlowTongJi_List.aspx.cs" Inherits="WEB.SEO.WebFlowTongJi_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/base.css" rel="stylesheet" />
    <link href="/css/datagrid.css?v=1" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/jquery-page.js"></script>
    <script src="/js/tc-list.js"></script>
    <script src="/js/common.js"></script>
    <style>
          <%if (IsIpTongJi == 1)
            { 
     %>
        .editbotton { position: absolute; top: 9px; left: 9px; }
        <%  }
            else
            {
                %>
                    .ed-title { background: #308ad2; height: 35px; line-height: 35px; padding-left: 10px; color: #FFF; }
                    .ed-title h3 { float: left; color: #FFF; font-weight: bold;}
                    <%
            } %>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="g-div-e">
            <div class="t-tool">
                <%if (IsIpTongJi != 1)
                  {
                %>
                <div class="clearfix ed-title">
                    <h3>自定义流量统计代码
                    </h3>
                </div>
                <div>
                    <textarea style="width: 500px; height: 150px; margin-bottom: 10px; margin-top: 10px;" runat="server" id="txtTJJsCode"></textarea>
                </div>
                <asp:Button Text="保存自定义流量统计代码" runat="server" ID="btnSaveTJ" CssClass="editbotton btn btn-primary" OnClick="btnSaveTJ_Click" />
                <%
                  } %>
                <asp:Button Text="关闭流量监控" runat="server" ID="btnSave" CssClass="btn btn-warning" OnClick="btnSave_Click" />
                <%if (IsIpTongJi == 1)
                  {
                %>
                <a class="l-btn" target="_blank" href="WebFlow_Charts.aspx">
                    <span class="l-btn-icon  icon-chart"></span>
                    <span class="l-btn-text">查看统计图表</span>
                </a>
                <%
                  } %>
            </div>
            <%if (IsIpTongJi == 1)
              {
            %>
            <asp:GridView ID="gv_List" DataKeyNames="ID" CssClass="m-table" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <%#PageSize*(PageIndex-1)+Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="时间">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <%#Eval("CreateTS","{0:yyyy-MM-dd HH:mm:ss fff}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="访问IP">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <%#Eval("IP")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="来源地址">
                        <ItemStyle HorizontalAlign="Left" Width="200" />
                        <ItemTemplate>
                            <span title="<%#Eval("IPSourceUrl")%>"><%#UICommon.Util.SubString(Eval("IPSourceUrl"),50)%></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="浏览地址">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <a href="<%#Eval("BrowseUrl")%>" target="_blank"><%#Eval("BrowseUrl")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="操作" FooterText="操作">
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <ItemTemplate>
                            <a class="td-btn" href="Product_Modify.aspx?id=<%#Eval("ID")%>" target="_blank">
                                <span class="td-btn-icon  icon-edit"></span>
                                <span class="td-btn-text">修改</span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <!--分页控件-->
            <div class="page" data-page="<%=PageIndex%>" data-total="<%=TotalCount%>" data-size="<%=PageSize%>" style="float: left"></div>
            <%
              } %>
        </div>
    </form>
</body>
</html>
