<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product_List.aspx.cs" Inherits="WEB.Product.Product_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/datagrid.css?v=1" rel="stylesheet" />
    <script src="/js/jquery-1.8.3.min.js"></script> 
    <script src="/js/layer/layer.js"></script>
    <script src="/js/jquery-page.js"></script> 
    <script src="/js/tc-list.js"></script>
    <script src="/js/common.js"></script>
    <script>
        //$.post(
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="g-div-e">
            <div class="t-tool">
                <a class="l-btn" target="_blank" href="Product_Add.aspx">
                    <span class="l-btn-icon  icon-add"></span>
                    <span class="l-btn-text">新增</span>
                </a>
                <a class="l-btn btn-del" onclick="Del_ChecdList()">
                    <span class="l-btn-icon  icon-remove"></span>
                    <span class="l-btn-text">删除</span>
                </a>
            </div>
            <asp:GridView ID="gv_List" DataKeyNames="ID" CssClass="m-table" runat="server" AutoGenerateColumns="False"
                 Width="100%">
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
                    <asp:TemplateField HeaderText="产品信息" FooterText="产品信息">
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <div class="imgbox">
                                <asp:Image ImageUrl='<%#Eval("TitlePictures")%>' runat="server" CssClass="titlepictures" ID="TitlePictures" />
                            </div>
                            <div class="txtbox">
                                <p><strong><asp:Literal ID="Title" runat="server" Text='<%#Eval("Title")%>'></asp:Literal></strong></p>
                                <p><asp:Literal ID="MaturityDate" runat="server" Text='<%#Eval("MaturityDate","{0:yyyy-MM-dd}")%>'></asp:Literal></p>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="操作" FooterText="操作">
                        <ItemStyle HorizontalAlign="Center" Width="100" />
                        <ItemTemplate>
                            <a class="td-btn" href="Product_Modify.aspx?id=<%#Eval("ID")%>" target="_blank">
                                <span class="td-btn-icon  icon-edit"></span>
                                <span class="td-btn-text">修改</span>
                            </a>
                           <%-- <a class="td-btn" href="Product_Modify.aspx?id=<%#Eval("ID")%>" target="_blank">
                                <span class="td-btn-icon  icon-edit"></span>
                                <span class="td-btn-text">图片管理</span>
                            </a>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <!--分页控件-->
            <div class="page" data-page="<%=PageIndex%>" data-total="<%=TotalCount%>" data-size="<%=PageSize%>" style="float: left"></div>
        </div>
    </form>
</body>
</html>
