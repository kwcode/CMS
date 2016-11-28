<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackgroundMenuClass1_List.aspx.cs" Inherits="WEB.GL.BackgroundMenu.BackgroundMenuClass1_List" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/datagrid.css?v=1" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.js"></script>
    <script src="/js/layer/layer.js"></script>
    <script src="/js/jquery-page.js"></script>
    <script src="/js/common.js"></script>
    <script src="/js/tc-list.js"></script>
</head>
<body>
    <div class="g-div-e">
        <div class="t-tool">
            <div class="left">
                <a class="l-btn" onclick="PopShow('BackgroundMenuClass1_Add.aspx',{area: ['500px', '350px']})">

                    <span class="l-btn-icon  icon-add"></span>
                    <span class="l-btn-text">新增</span>
                </a>
                <a class="l-btn btn-del" onclick="Del_ChecdList()">
                    <span class="l-btn-icon  icon-remove"></span>
                    <span class="l-btn-text">删除</span>
                </a>
            </div>
            <div class="right" style="margin-right: 20px; padding: 0px 10px;">
                <div class="searchbox clearfix">
                    <input type="text" class="inpt-search" value="<%=KeyWords%>" onkeydown="OnEnter(this)" id="txtSearch" />
                    <a class="btn-search" onclick="Search()">
                        <span class="s-icon"></span>
                        <span class="s-text">搜索</span>
                    </a>
                </div>
            </div>
        </div>
        <form id="form1" runat="server">
            <asp:GridView ID="gv_List" DataKeyNames="ID" CssClass="m-table" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="全选">
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <%#PageSize*(PageIndex-1)+Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input id="chkCheckAll" runat="server" name="controlAll" type="checkbox" class="list_checkall" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <input id="chkSelect" value='<%#Eval("ID")%>' runat="server" name="checkitems" type="checkbox" class="list_checkitems" />
                            <input type="hidden" class="hide_id" value='<%#Eval("ID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="栏目一类">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Literal ID="Title" runat="server" Text='<%#Eval("Title")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="值">
                        <ItemStyle HorizontalAlign="Center" Width="50" />
                        <ItemTemplate>
                            <asp:Literal ID="ValueNum" runat="server" Text='<%#Eval("ValueNum")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="管理地址">
                        <ItemStyle HorizontalAlign="Left" Width="50" />
                        <ItemTemplate>
                            <asp:Literal ID="ManageUrl" runat="server" Text='<%#Eval("ManageUrl")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="描述">
                        <ItemStyle HorizontalAlign="Left" Width="150" />
                        <ItemTemplate>
                            <asp:Literal ID="Description" runat="server" Text='<%#Eval("Description")%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                            <a class="td-btn" onclick="PopShow('BackgroundMenuClass1_Modify.aspx?id=<%#Eval("ID")%>',{area: ['500px', '350px']})">
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

