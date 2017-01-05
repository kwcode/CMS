<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar_List.aspx.cs" Inherits="WEB.Nav.NavigationBar_List" %>

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
</head>
<body>
    <div class="g-div-e">
        <form id="form1" runat="server">
            <asp:GridView ID="gv_List" DataKeyNames="ID" CssClass="m-table" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <%#PageSize*(PageIndex-1)+Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="栏目">
                        <ItemStyle HorizontalAlign="Center" Width="80" />
                        <ItemTemplate>
                            <input type="hidden" class="hide_id" value='<%#Eval("ID")%>' />
                            <asp:Literal ID="Title" runat="server" Text='<%#Eval("Title")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="URL">
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <ItemTemplate>
                            <asp:Literal ID="Url" runat="server" Text='<%#Eval("Url")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SEO标题">
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <ItemTemplate>
                            <span title="<%#Eval("SEOTitle")%>"><%#UICommon.Util.SubString(Eval("SEOTitle"),25)%></span> 
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SEO关键字">
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <ItemTemplate>
                            <span title="<%#Eval("SEOKeyWords")%>"><%#UICommon.Util.SubString(Eval("SEOKeyWords"),25)%></span>  
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SEO描述">
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <ItemTemplate>
                            <span title="<%#Eval("SEODescription")%>"><%#UICommon.Util.SubString(Eval("SEODescription"),25)%></span> 　
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="描述">
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <ItemTemplate>
                            <asp:Literal ID="Description" runat="server" Text='<%#Eval("Description")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="排序">
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <asp:Literal ID="OrderNum" runat="server" Text='<%#Eval("OrderNum")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间">
                        <ItemStyle HorizontalAlign="Center" Width="120" />
                        <ItemTemplate>
                            <asp:Literal ID="CreateTS" runat="server" Text='<%#Eval("CreateTS","{0:yyyy-MM-dd HH:mm}")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <ItemTemplate>
                            <a class="td-btn" href="NavigationBar_Modify.aspx?id=<%#Eval("ID")%>" target="_blank">
                                <span class="td-btn-icon  icon-edit"></span>
                                <span class="td-btn-text">修改</span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
        <!--分页控件-->
        <div class="page" data-page="<%=PageIndex%>" data-total="<%=TotalCount%>" data-size="<%=PageSize%>" style="float: left"></div>
    </div>

</body>
</html>

